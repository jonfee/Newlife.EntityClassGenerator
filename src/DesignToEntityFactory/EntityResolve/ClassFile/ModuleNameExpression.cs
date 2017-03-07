using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EntityResolve.ClassFile
{
    /// <summary>
    /// 模块名称解释器
    /// 针对“{Module_Name}”的解析
    /// </summary>
    public class ModuleNameExpression : EntityClassExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"></param>
        public override void Interpret(EntityClassResolveContext context)
        {
            if (context == null || context.TableDesc == null) return;

            Regex regex = new Regex(@"\{Module_Name\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.OutputEntityClassContent;

            context.OutputEntityClassContent = regex.Replace(text, context.TableDesc.Name);
        }
    }
}
