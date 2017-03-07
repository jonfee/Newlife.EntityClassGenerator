namespace DesignToEntityFactory.MappingResolve
{
    /// <summary>
    /// Maping类解释器-抽象类 
    /// </summary>
    public abstract class MappingExpression
    {
        /// <summary>
        /// 执行方法解释
        /// </summary>
        /// <param name="context"><see cref="MappingResolveContext"/>实例</param>
        public abstract void Interpret(MappingResolveContext context);
    }
}
