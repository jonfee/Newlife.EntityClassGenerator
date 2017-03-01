using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EntityResolver
{
    /// <summary>
    /// 实体描述解释器
    /// 针对“{Entity_Description}”的解析
    /// </summary>
    public class EntityDescriptionExpression : EntityExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"></param>
        public override void Interpret(EntityResolverContext context)
        {
            if (context == null || context.TableDesc == null) return;

            Regex regex = new Regex(@"\{Entity_Description\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.OutputEntityContent;

            context.OutputEntityContent = regex.Replace(text, context.TableDesc.Description);
        }
    }
}
