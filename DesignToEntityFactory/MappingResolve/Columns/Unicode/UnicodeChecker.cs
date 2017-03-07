using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve.Columns.Unicode
{
    /// <summary>
    /// 字符检测者
    /// </summary>
    public class UnicodeChecker : UnicodeHandler
    {
        /// <summary>
        /// 解析
        /// 1、为字符类型时，交由继任者处理
        /// 2、非字符类型时，返回空，表示无需此配置内容
        /// </summary>
        /// <param name="template">字符处理配置模板</param>
        /// <param name="dbtype">数据类型</param>
        /// <returns></returns>
        public override string Resolve(string template, string dbtype)
        {
            string result = "";

            bool isChar = IsCharType(dbtype);

            if (isChar && Successor != null)
            {
                result = Successor.Resolve(template, dbtype);
            }

            return result;
        }

        /// <summary>
        /// 检测是否为字符类型
        /// </summary>
        /// <param name="dbType">数据类型</param>
        /// <returns></returns>
        private bool IsCharType(string dbType)
        {
            if (string.IsNullOrWhiteSpace(dbType)) return false;

            Regex regex = new Regex(@"^n?(char|varchar|text)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return regex.IsMatch(dbType);
        }
    }
}
