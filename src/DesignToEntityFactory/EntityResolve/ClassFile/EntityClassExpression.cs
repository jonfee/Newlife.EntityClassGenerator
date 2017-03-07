namespace DesignToEntityFactory.EntityResolve.ClassFile
{
    /// <summary>
    /// 实体类文件解释器-抽象类
    /// </summary>
    public abstract class EntityClassExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"></param>
        public abstract void Interpret(EntityClassResolveContext context);
    }
}
