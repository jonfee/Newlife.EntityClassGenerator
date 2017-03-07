namespace DesignToEntityFactory.MappingResolve.Columns
{
    /// <summary>
    /// Mapping类字段代码解释器-抽象类
    /// </summary>
    public abstract class ColumnExpression
    {
        public abstract void Interpret(ColumnResolveContext context);
    }
}
