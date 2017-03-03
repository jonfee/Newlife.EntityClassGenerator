using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EntityResolve.Property
{
    /// <summary>
    /// 属性说明解释器
    /// 针对“{Property_Description}”的解析
    /// </summary>
    public class ProperytyDescriptionExpression : PropertyExpression
    {
        public override void Interpret(PropertyResolverContext context)
        {
            if (context == null || context.Column == null) return;

            Regex regex = new Regex(@"\{Property_Description\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output;

            context.Output = regex.Replace(text, context.Column.Description);
        }
    }
}
