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

        /// <summary>
        /// 转换数据类型为SQL中的数据类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ConvertToDBType(string type)
        {
            string newtype;

            type = type ?? "";

            switch (type.ToLower())
            {
                case "bit":
                case "bool":
                case "boolean":
                    newtype = "bit";
                    break;
                case "time":
                case "timespan":
                    newtype = "time";
                    break;
                case "date":
                    newtype = "date";
                    break;
                case "datetime":
                    newtype = "datetime";
                    break;
                case "bigint":
                case "long":
                case "int64":
                    newtype = "bigint";
                    break;
                case "int":
                case "int32":
                    newtype = "int";
                    break;
                case "smallint":
                case "short":
                case "int16":
                    newtype = "smallint";
                    break;
                case "tinyint":
                case "byte":
                    newtype = "tinyint";
                    break;
                case "guid":
                    newtype = "uniqueidentifier";
                    break;
                default:
                    newtype = type;
                    break;
            }

            return newtype;
        }

        /// <summary>
        /// 转换类型为CSharp程序中的数据类型
        /// </summary>
        /// <param name="type">表示类型的名称</param>
        /// <returns></returns>
        public static string ConvertToCsharpType(string type)
        {
            string newtype;

            type = type ?? "";

            switch (type.ToLower())
            {
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                case "text":
                case "ntext":
                case "string":
                    newtype = "string";
                    break;
                case "bit":
                case "bool":
                case "boolean":
                    newtype = "bool";
                    break;
                case "time":
                case "timespan":
                    newtype = "TimeSpan";
                    break;
                case "date":
                case "datetime":
                    newtype = "DateTime";
                    break;
                case "bigint":
                case "long":
                case "int64":
                    newtype = "long";
                    break;
                case "int":
                case "int32":
                    newtype = "int";
                    break;
                case "short":
                case "smallint":
                case "int16":
                    newtype = "short";
                    break;
                case "byte":
                case "tinyint":
                case "int8":
                    newtype = "byte";
                    break;
                case "uniqueidentifier":
                case "guid":
                    newtype = "Guid";
                    break;
                case "":
                    newtype = "string";
                    break;
                default:
                    newtype = type;
                    break;
            }

            return newtype;
        }
    }
}
