using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve.Columns
{
    /// <summary>
    /// 实体属性名称解析器
    /// 针对“{Property_Name}”的解析
    /// </summary>
    public class MappingPropertyNameExpression : ColumnExpression
    {
        public override void Interpret(ColumnResolveContext context)
        {
            if (context == null || context.Column == null) return;

            Regex regex = new Regex(@"\{Property_Name\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output;

            context.Output = regex.Replace(text, context.Column.Name);
        }
    }
}
