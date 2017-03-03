using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EntityClassResolve
{
    /// <summary>
    /// 实体类名称解释器
    /// 针对“{Entity_Name}”的解析
    /// </summary>
    public class EntityNameExpression : EntityClassExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"></param>
        public override void Interpret(EntityClassResolveContext context)
        {
            if (context == null || context.TableDesc == null) return;

            Regex regex = new Regex(@"\{Entity_Name\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.OutputEntityClassContent;

            context.OutputEntityClassContent = regex.Replace(text, context.TableDesc.Name);
        }
    }
}
