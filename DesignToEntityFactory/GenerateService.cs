using DesignToEntityFactory.Core;
using DesignToEntityFactory.Factory;
using DesignToEntityFactory.TableResolve;
using System.Collections.Generic;

namespace DesignToEntityFactory
{
    /// <summary>
    /// 文件生成服务
    /// </summary>
    public class GenerateService
    {
        /// <summary>
        /// 源文件地址
        /// </summary>
        private string _sourceFile;

        /// <summary>
        /// 实始化<see cref="GenerateService"/>实例
        /// </summary>
        /// <param name="sourceFile">源文件地址</param>
        public GenerateService(string sourceFile)
        {
            _sourceFile = sourceFile;
        }

        public void Running()
        {
            //读取源文件内容
            string sourceHtml = Tools.ReadFileContent(_sourceFile);

            //解析出数据表集合
            var tables = TableHelper.ResolveTables(sourceHtml);

            List<FileFactory> factoryList = new List<FileFactory>();
            factoryList.Add(new EntityFactory(sourceHtml, tables));
            factoryList.Add(new EnumFactory(sourceHtml));
            factoryList.Add(new MappingFactory(sourceHtml, tables));

            //循环执行生成
            foreach (var factory in factoryList)
            {
                factory.Run();
            }
        }
    }
}
