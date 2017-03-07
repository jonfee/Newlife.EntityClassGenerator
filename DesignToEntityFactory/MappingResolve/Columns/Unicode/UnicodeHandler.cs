namespace DesignToEntityFactory.MappingResolve.Columns.Unicode
{
    /// <summary>
    /// 字符符号处理者
    /// </summary>
    public abstract class UnicodeHandler
    {
        /// <summary>
        /// 继任者
        /// </summary>
        protected UnicodeHandler Successor;

        /// <summary>
        /// 设置继任者
        /// </summary>
        /// <param name="handler"></param>
        public void SetSuccessor(UnicodeHandler handler)
        {
            Successor = handler;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="template">字符符号配置模板</param>
        /// <param name="dbtype">数据类型</param>
        /// <returns></returns>
        public abstract string Resolve(string template, string dbtype);
    }
}
