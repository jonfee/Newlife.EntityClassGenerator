namespace DesignToEntityFactory.EnumOptionResolve
{
    /// <summary>
    /// 枚举类成员文法解释器-抽象类
    /// </summary>
    public abstract class EnumClassOptionExpression
    {
        public abstract void Interpret(EnumOptionResolveContext context);
    }
}
