using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve
{
    /// <summary>
    /// Mapping名称解释器
    /// 针对“{Mapping_Name}”的解析
    /// </summary>
    public class MappingNameExpression : MappingExpression
    {
        public override void Interpret(MappingResolveContext context)
        {
            if (context == null || context.Table == null) return;

            Regex regex = new Regex(@"\{Mapping_Name\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output;

            context.Output = regex.Replace(text, context.Table.Name);
        }
    }
}
