using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve.Columns.Unicode
{
    /// <summary>
    /// 字符符号替换者
    /// </summary>
    public class UnicodeReplacer : UnicodeHandler
    {
        public override string Resolve(string template, string dbtype)
        {
            //是否双字符类型
            bool isDoubleChar = CheckDoubleChar(dbtype);

            //替换模板中的{Is_DoubleBytes}占位
            return template.Replace("{Is_DoubleBytes}", isDoubleChar ? "true" : "false");
        }

        /// <summary>
        /// 检测数据类型是否为双字符类型
        /// </summary>
        /// <param name="dbType">数据类型</param>
        /// <returns></returns>
        private bool CheckDoubleChar(string dbType)
        {
            if (string.IsNullOrWhiteSpace(dbType)) return false;

            Regex regex = new Regex(@"^n(char|varchar|text)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return regex.IsMatch(dbType);
        }
    }
}
