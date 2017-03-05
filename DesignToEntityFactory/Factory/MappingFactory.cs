using DesignToEntityFactory.Core;
using DesignToEntityFactory.MappingResolve;
using DesignToEntityFactory.Models;
using DesignToEntityFactory.TableResolve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignToEntityFactory.Factory
{
    /// <summary>
    /// Mapping类文件生成工厂类
    /// </summary>
    public class MappingFactory : FileFactory
    {
        /// <summary>
        /// 数据表集合
        /// </summary>
        private List<TableDesc> _tables;

        #region 初始化

        /// <summary>
        /// 初始化<see cref="MappingFactory"/>实例
        /// </summary>
        /// <param name="sourceHtml">源文件HTML内容</param>
        public MappingFactory(string sourceHtml) : this(sourceHtml, null)
        {

        }

        /// <summary>
        /// 初始化<see cref="MappingFactory"/>实例
        /// </summary>
        /// <param name="sourceHtml">源文件HTML内容</param>
        /// <param name="tables">数据表集合</param>
        public MappingFactory(string sourceHtml, List<TableDesc> tables) : base(sourceHtml, "Mappings")
        {
            _tables = tables;
        }

        #endregion

        /// <summary>
        /// 生成实体类文件
        /// </summary>
        public override void GenerateFiles()
        {
            //获取Mapping类模板内容
            string templateContent = Tools.ReadFileContent(Configs.MappingTemplatePath);

            //数据表总数
            int tableCount = DataQueue.Count();

            Console.WriteLine($"共有数据表{tableCount}个");

            //处理数据表转换队列
            while (DataQueue.Count() > 0)
            {
                var table = (TableDesc)DataQueue.Dequeue();

                Console.WriteLine($"正在生成【Mapping类】{table.Description}({table.Name})……{tableCount - DataQueue.Count()}/{tableCount}");

                //Mapping类解析对象上下文
                MappingResolveContext context = new MappingResolveContext(templateContent, table);

                //解析器集合
                List<MappingExpression> exps = new List<MappingExpression>();
                exps.Add(new ModuleNameExpression());           //模块名称文法解释器
                exps.Add(new MappingNameExpression());          //实体名称文法解释器
                exps.Add(new TableSchemaExpression());          //实体类描述文法解释器
                exps.Add(new TableHasKeysExpression());         //实体属性循环处理文法解释器

                //循环执行解析
                foreach (var exp in exps)
                {
                    exp.Interpret(context);
                }

                //存储的最终文件路径
                string filePath = $@"{OutputDirectory}\{context.Table.Module}\{context.Table.Name}Mapping.cs";

                //写入文件并保存
                SaveFile(filePath, context.Output);
            }
        }

        /// <summary>
        /// 从源HTML中解析数据到队列
        /// </summary>
        public override void ResolveDataInQueue()
        {
            if (_tables == null)
                _tables = TableHelper.ResolveTables(SourceHtml);

            foreach (var tb in _tables)
            {
                //将解释出来的TableDesc加入数据队列
                DataQueue.Enqueue(tb);
            }
        }
    }
}
