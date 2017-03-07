namespace DesignToEntityFactory.MappingResolve.Columns.MaxLength
{
    /// <summary>
    /// 最大限制长度处理者-抽象类
    /// </summary>
    public abstract class MaxLengthHandlder
    {
        protected MaxLengthHandlder Successor;

        public void SetSuccessor(MaxLengthHandlder handler)
        {
            Successor = handler;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="template">模板</param>
        /// <param name="dbtype">数据类型</param>
        /// <param name="maxlength">限制最大长度</param>
        /// <returns></returns>
        public abstract string Resolve(string template, string dbtype, int maxlength);
    }
}
