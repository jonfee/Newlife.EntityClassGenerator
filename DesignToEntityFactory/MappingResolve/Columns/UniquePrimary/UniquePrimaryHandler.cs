using DesignToEntityFactory.Models;

namespace DesignToEntityFactory.MappingResolve.Columns.UniquePrimary
{
    /// <summary>
    /// 唯一主键处理
    /// </summary>
    public abstract class UniquePrimaryHandler
    {
        protected UniquePrimaryHandler Successor;

        public void SetSuccessor(UniquePrimaryHandler handler)
        {
            Successor = handler;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="column"><see cref="TableColumn"/>对象</param>
        /// <returns></returns>
        public abstract string Resolve(string template, TableColumn column);
    }
}
