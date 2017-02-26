using System;
using System.IO;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory
{
    /// <summary>
    /// 参数配置处理器
    /// </summary>
    public class SettingsHandler
    {
        private string _htmlFilePath;

        /// <summary>
        /// HTML文件路径
        /// </summary>
        public string HtmlFilePath
        {
            get { return _htmlFilePath; }
            set { this._htmlFilePath = value; }
        }

        private string _outNamespace;

        /// <summary>
        /// 导出的实体类文件命名空间
        /// </summary>
        public string OutNamespace
        {
            get { return this._outNamespace; }
            set { this._outNamespace = value; }
        }
        
        /// <summary>
        /// 获取相关参数配置
        /// </summary>
        public void GetSettings()
        {
            SetSoure();

            SetNamespace();
        }

        /// <summary>
        /// 设置文件来源路径
        /// </summary>
        private void SetSoure()
        {
            Console.WriteLine("请输入源文件地址：");

            string path = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(path))
            {
                SetSoure();
            }
            else
            {
                Regex regex = new Regex(@"^[a-z]:\\([^\\]+\\)*(?:(?!html?s?).+).html?s?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                if (regex.IsMatch(path))
                {
                    //文件不存在时处理
                    if (!File.Exists(path))
                    {
                        Console.WriteLine("找不到文件！");
                        SetSoure();
                    }

                    this._htmlFilePath = path;
                }
                else
                {
                    Console.WriteLine("不是有效的html文件地址！");
                    SetSoure();
                }
            }
        }

        /// <summary>
        /// 设置导出实体类的命名空间名称
        /// </summary>
        private void SetNamespace()
        {
            Console.WriteLine("请输入导出实体类的命名空间名称：");

            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                SetNamespace();
            }
            else
            {
                Regex regex = new Regex(@"^[a-z][a-z0-9_\.]*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                if (regex.IsMatch(name))
                {
                    this._outNamespace = name;
                }
                else
                {
                    Console.WriteLine("不是有效的命名空间名称！");
                    SetNamespace();
                }
            }
        }
    }
}
