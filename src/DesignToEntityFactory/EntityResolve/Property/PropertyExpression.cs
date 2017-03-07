namespace DesignToEntityFactory.EntityResolve.Property
{
    /// <summary>
    /// 字段属性解释器-抽象类
    /// </summary>
    public abstract class PropertyExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"></param>
        public abstract void Interpret(PropertyResolverContext context);
    }
}
