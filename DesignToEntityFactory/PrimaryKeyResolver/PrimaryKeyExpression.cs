namespace DesignToEntityFactory.PrimaryKeyResolver
{
    /// <summary>
    /// 主键信息解释器-抽象类
    /// </summary>
    public abstract class PrimaryKeyExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"><see cref="PrimarykeyResolverContext"/>对象</param>
        public abstract void Interpret(PrimarykeyResolverContext context);
    }
}
