namespace DesignToEntityFactory.EntityResolver
{
    /// <summary>
    /// 实体类文件解释器-抽象类
    /// </summary>
    public abstract class EntityExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"></param>
        public abstract void Interpret(EntityResolverContext context);
    }
}
