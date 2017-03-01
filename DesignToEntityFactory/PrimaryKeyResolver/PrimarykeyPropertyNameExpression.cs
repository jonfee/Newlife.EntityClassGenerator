using System.Text.RegularExpressions;

namespace DesignToEntityFactory.PrimaryKeyResolver
{
    /// <summary>
    /// 主键属性名称解释器
    /// 针对“{Property_Name}”的解析
    /// </summary>
    public class PrimarykeyPropertyNameExpression : PrimaryKeyExpression
    {
        public override void Interpret(PrimarykeyResolverContext context)
        {
            if (context == null || context.Column == null) return;

            Regex regex = new Regex(@"\{Property_Name\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output;

            context.Output = regex.Replace(text, context.Column.Name);
        }
    }
}
