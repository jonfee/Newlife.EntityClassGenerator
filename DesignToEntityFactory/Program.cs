using DesignToEntityFactory.Core;
using DesignToEntityFactory.Factory;
using System;
using System.Collections.Generic;

namespace DesignToEntityFactory
{
    class Program
    {
        /// <summary>
        /// 输出目录
        /// </summary>
        static string _outputDirectory;

        static void Main(string[] args)
        {
            //设置/获取生成器参数
            SettingsHandler handler = new SettingsHandler();
            handler.GetSettings();

            //读取源文件内容
            string sourceHtml = Tools.ReadFileContent(handler.HtmlFilePath);

            List<FileFactory> factoryList = new List<FileFactory>();
            factoryList.Add(new EntityFactory(sourceHtml));
            //factoryList.Add(new EnumFactory(sourceHtml));
            //factoryList.Add(new MappingFactory(sourceHtml));

            //循环执行生成
            foreach (var factory in factoryList)
            {
                factory.Run();
            }

            Console.ReadKey();
        }
    }
}
