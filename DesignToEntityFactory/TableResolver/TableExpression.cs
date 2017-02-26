namespace DesignToEntityFactory.TableResolver
{
    /// <summary>
    /// 数据表解释器文法-抽象类
    /// </summary>
    public abstract class TableExpression
    {
        /// <summary>
        /// 文法解释
        /// </summary>
        /// <param name="context"><seealso cref="DataTableContext"/>对象实例</param>
        public abstract void Interpret(DataTableContext context);
    }
}
