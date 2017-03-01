using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EntityResolver
{
    /// <summary>
    /// 模块名称解释器
    /// 针对“{Module_Name}”的解析
    /// </summary>
    public class ModuleNameExpression : EntityExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"></param>
        public override void Interpret(EntityResolverContext context)
        {
            if (context == null || context.TableDesc == null) return;

            Regex regex = new Regex(@"\{Module_Name\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.OutputEntityContent;

            context.OutputEntityContent = regex.Replace(text, context.TableDesc.Name);
        }
    }
}
