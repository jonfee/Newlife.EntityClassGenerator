namespace DesignToEntityFactory.MappingResolve.Columns.Precision
{
    /// <summary>
    /// 针对Decimal类型的处理-抽象类
    /// </summary>
    public abstract class PrecisionHandler
    {
        protected PrecisionHandler Successor;

        public void SetSuccessor(PrecisionHandler handler)
        {
            Successor = handler;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="template">字符符号配置模板</param>
        /// <param name="dbtype">数据类型</param>
        /// <param name="totalBit">总位数</param>
        /// <param name="decimalBit">小数位数</param>
        /// <returns></returns>
        public abstract string Resolve(string template, string dbtype, int totalBit, int decimalBit);
    }
}
