using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve.Columns
{
    /// <summary>
    /// 必须字段解析器
    /// 针对“<If_IsRequired></EndIf>”的解析
    /// </summary>
    public class IsRequiredExpression : ColumnExpression
    {
        public override void Interpret(ColumnResolveContext context)
        {
            Regex regex = new Regex(@"<If_IsRequired>(?<result>((?!</EndIf>).|\n)*)</EndIf>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(context.Output);

            string result = match.Groups["result"].Value;

            //非必须字段（可为空），则忽略此属性
            if (context.Column.CanNullable) result = "";

            context.Output = regex.Replace(context.Output, result);
        }
    }
}
