using DesignToEntityFactory.Models;

namespace DesignToEntityFactory.MappingResolve.Columns.UniquePrimary
{
    /// <summary>
    /// 唯一主键检测者
    /// </summary>
    public class UniquePrimaryChecker : UniquePrimaryHandler
    {
        /// <summary>
        /// 解析
        /// 1、如果为唯一主键，则交给继任者处理
        /// 2、非唯一主键，则返回空，表示无需此配置内容
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="column"><see cref="TableColumn"/>对象</param>
        /// <returns></returns>
        public override string Resolve(string template, TableColumn column)
        {
            string result = "";

            if (column.IsUniquePrimary && Successor != null)
            {
                result = Successor.Resolve(template, column);
            }

            return result;
        }
    }
}
