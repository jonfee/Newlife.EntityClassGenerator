using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve.Columns.Precision
{
    /// <summary>
    /// 精度检测处理者
    /// </summary>
    public class PrecisionChecker : PrecisionHandler
    {
        /// <summary>
        /// 解析
        /// 1、为有精度属性的数据类型时，交由继任者处理
        /// 2、否则，返回空，表示无需此配置内容
        /// </summary>
        /// <param name="template">字符处理配置模板</param>
        /// <param name="dbtype">数据类型</param>
        /// <param name="totalBit">总位数</param>
        /// <param name="decimalBit">小数位数</param>
        /// <returns></returns>
        public override string Resolve(string template, string dbtype, int totalBit, int decimalBit)
        {
            string result = "";

            bool isChar = IsPrecisionType(dbtype);

            if (isChar && Successor != null)
            {
                result = Successor.Resolve(template, dbtype, totalBit, decimalBit);
            }

            return result;
        }

        /// <summary>
        /// 检测是否为有精度属性的数据类型
        /// </summary>
        /// <param name="dbType">数据类型</param>
        /// <returns></returns>
        private bool IsPrecisionType(string dbType)
        {
            if (string.IsNullOrWhiteSpace(dbType)) return false;

            Regex regex = new Regex(@"^decimal|float|double$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return regex.IsMatch(dbType);
        }
    }
}
