using System.IO;

namespace DesignToEntityFactory.Core
{
    public static class Tools
    {
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string ReadFileContent(string filePath)
        {
            string content = "";

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    content = sr.ReadToEnd();
                }
            }

            return content;
        }
    }
}
