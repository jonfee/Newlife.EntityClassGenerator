using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EntityResolve.ClassFile
{
    /// <summary>
    /// 实体描述解释器
    /// 针对“{Entity_Description}”的解析
    /// </summary>
    public class EntityDescriptionExpression : EntityClassExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"></param>
        public override void Interpret(EntityClassResolveContext context)
        {
            if (context == null || context.TableDesc == null) return;

            Regex regex = new Regex(@"\{Entity_Description\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.OutputEntityClassContent;

            context.OutputEntityClassContent = regex.Replace(text, context.TableDesc.Description);
        }
    }
}
