using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve.Columns.MaxLength
{
    /// <summary>
    /// 长度限制的类型检测者
    /// </summary>
    public class MaxLengthChecker : MaxLengthHandlder
    {
        /// <summary>
        /// 解析
        /// 1、有长度限制属性的类型时，交给继任者处理
        /// 2、否则，返加空，表示无需此配置内容
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="dbtype">数据类型</param>
        /// <param name="maxlength">限制的长度</param>
        /// <returns></returns>
        public override string Resolve(string template, string dbtype, int maxlength)
        {
            string result = "";

            bool isChar = IsCharType(dbtype);

            if (isChar && Successor != null)
            {
                result = Successor.Resolve(template, dbtype, maxlength);
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
