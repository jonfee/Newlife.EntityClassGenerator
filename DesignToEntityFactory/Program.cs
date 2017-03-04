using DesignToEntityFactory.Core;
using DesignToEntityFactory.Factory;
using DesignToEntityFactory.TableResolve;
using System;
using System.Collections.Generic;

namespace DesignToEntityFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            //设置/获取生成器参数
            SettingsHandler handler = new SettingsHandler();
            handler.GetSettings();

            //读取源文件内容
            string sourceHtml = Tools.ReadFileContent(handler.HtmlFilePath);

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

            Console.ReadKey();
        }
    }
}
