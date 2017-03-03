namespace DesignToEntityFactory.EnumResolver
{
    /// <summary>
    /// 枚举解释器文法-抽象类
    /// </summary>
    public abstract class EnumExpression
    {
        /// <summary>
        /// 文法解释
        /// </summary>
        /// <param name="context"><seealso cref="EnumDescContext"/>对象实例</param>
        public abstract void Interpret(EnumDescContext context);
    }
}
