using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EntityResolve.Property
{
    /// <summary>
    /// 属性数据长度限制解释器
    /// 针对“{Property_Limit_Length}”的解析
    /// </summary>
    public class PropertyLimitLengthExpression : PropertyExpression
    {
        public override void Interpret(PropertyResolverContext context)
        {
            if (context == null || context.Column == null) return;

            Regex regex = new Regex(@"\{Property_Limit_Length\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output;

            context.Output = regex.Replace(text, context.Column.FirstLimitLength.ToString());
        }
    }
}
