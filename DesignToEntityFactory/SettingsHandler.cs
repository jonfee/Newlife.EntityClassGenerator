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

        /// <summary>
        /// 获取相关参数配置
        /// </summary>
        public void GetSettings()
        {
            SetSoure();
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
    }
}
