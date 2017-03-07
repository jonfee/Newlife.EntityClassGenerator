using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve.Columns
{
    /// <summary>
    /// 可选字段解析器
    /// 针对“<If_IsOptional></EndIf>”的解析
    /// </summary>
    public class IsOptionalExpression : ColumnExpression
    {
        public override void Interpret(ColumnResolveContext context)
        {
            Regex regex = new Regex(@"<If_IsOptional>(?<result>((?!</EndIf>).|\n)*)</EndIf>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(context.Output);

            string result = match.Groups["result"].Value;

            //必须字段时（不可为空），忽略此属性
            if (!context.Column.CanNullable) result = "";

            context.Output = regex.Replace(context.Output, result);
        }
    }
}
