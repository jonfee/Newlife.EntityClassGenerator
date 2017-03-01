using System.Text.RegularExpressions;

namespace DesignToEntityFactory.PropertyResolver
{
    /// <summary>
    /// 属性名解释器
    /// 针对“{Property_Name}”的解析
    /// </summary>
    public class PropertyNameExpression : PropertyExpression
    {
        public override void Interpret(PropertyResolverContext context)
        {
            if (context == null || context.Column == null) return;

            Regex regex = new Regex(@"\{Property_Name\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output;

            context.Output = regex.Replace(text, context.Column.Name);
        }
    }
}
