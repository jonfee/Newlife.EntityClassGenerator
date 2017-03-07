using DesignToEntityFactory.Models;

namespace DesignToEntityFactory.MappingResolve.Columns.UniquePrimary
{
    /// <summary>
    /// 唯一主键时的处理者
    /// </summary>
    public class UniquePrimaryReplacer : UniquePrimaryHandler
    {
        public override string Resolve(string template, TableColumn column)
        {
            return template;
        }
    }
}
