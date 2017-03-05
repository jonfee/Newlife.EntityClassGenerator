using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve
{
    /// <summary>
    /// 模块名称解释器
    /// 针对“{Module_Name}”的解析
    /// </summary>
    public class ModuleNameExpression : MappingExpression
    {
        public override void Interpret(MappingResolveContext context)
        {
            if (context == null || context.Table == null) return;

            Regex regex = new Regex(@"\{Module_Name\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output;

            context.Output = regex.Replace(text, context.Table.Module);
        }
    }
}
