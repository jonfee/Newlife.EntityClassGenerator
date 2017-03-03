using DesignToEntityFactory.Core;
using DesignToEntityFactory.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.Factory
{
    public abstract class FileFactory
    {
        #region 成员属性

        /// <summary>
        /// 数据表队列
        /// </summary>
        protected Queue<FactoryModel> DataQueue;

        /// <summary>
        /// 源文件HTML
        /// </summary>
        protected readonly string SourceHtml;

        /// <summary>
        /// 文件输出目录
        /// </summary>
        protected readonly string OutputDirectory;

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sourceHtml">源文件HTML内容</param>
        /// <param name="outputFolder">输出的文件夹，如：Models</param>
        public FileFactory(string sourceHtml, string outputFolder)
        {
            DataQueue = new Queue<FactoryModel>();

            //源文件内容
            this.SourceHtml = sourceHtml;

            //生成后的文件输出目录
            string outRoot = $@"{Configs.BaseDirectory}\output";
            if (!string.IsNullOrWhiteSpace(outputFolder))
            {
                this.OutputDirectory = $@"{outRoot}\{outputFolder}";
            }
            else
            {
                this.OutputDirectory = outRoot;
            }

            //清空输出目录
            ClearOutput(outRoot);
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 运行
        /// </summary>
        public void Run()
        {
            //解析源HTML到数据队列
            ResolveDataInQueue();

            //从队列中生成文件
            GenerateFiles();
        }

        #endregion

        #region 抽象方法、虚方法

        /// <summary>
        /// 从源HTML中解析数据到队列
        /// </summary>
        public abstract void ResolveDataInQueue();

        /// <summary>
        /// 生成文件
        /// </summary>
        public abstract void GenerateFiles();

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filePath">存储的文件路径</param>
        /// <param name="fileContent">文件内容</param>
        public virtual void SaveFile(string filePath, string fileContent)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return;

            Regex regex = new Regex(@"^(?<directory>[a-z]:(\\[^\\]+)+)(\\.+\.[a-z0-9]+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(filePath);

            //存储目录
            string folder = match.Groups["directory"].Value;


            //非磁盘根目录，且目录不存在时创建
            if (!string.IsNullOrWhiteSpace(folder) && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            //写入文件并保存，存在则覆盖，不存在则新建
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.Write(fileContent);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 清空输出目录
        /// </summary>
        private void ClearOutput(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory)) return;

            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
        }

        #endregion
    }
}
