namespace DesignToEntityFactory.EnumClassResolve
{
    /// <summary>
    /// 枚举类文法解释器-抽象类
    /// </summary>
    public abstract class EnumClassExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"></param>
        public abstract void Interpret(EnumClassResolveContext context);
    }
}
