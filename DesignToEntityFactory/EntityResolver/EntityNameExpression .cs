using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EntityResolver
{
    /// <summary>
    /// 实体类名称解释器
    /// 针对“{Entity_Name}”的解析
    /// </summary>
    public class EntityNameExpression : EntityExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"></param>
        public override void Interpret(EntityResolverContext context)
        {
            if (context == null || context.TableDesc == null) return;

            Regex regex = new Regex(@"\{Entity_Name\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.OutputEntityContent;

            context.OutputEntityContent = regex.Replace(text, context.TableDesc.Name);
        }
    }
}
