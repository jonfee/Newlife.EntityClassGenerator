using System.Text.RegularExpressions;

namespace DesignToEntityFactory.PropertyResolver
{
    /// <summary>
    /// 属性数据类型解释器
    /// 针对“{Property_DataType}”的解析
    /// </summary>
    public class PropertyDataTypeExpression : PropertyExpression
    {
        public override void Interpret(PropertyResolverContext context)
        {
            if (context == null || context.Column == null) return;

            Regex regex = new Regex(@"\{Property_DataType\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output;

            context.Output = regex.Replace(text, context.Column.DataType);
        }
    }
}
