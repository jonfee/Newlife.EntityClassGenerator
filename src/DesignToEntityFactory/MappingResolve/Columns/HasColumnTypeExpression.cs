using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve.Columns
{
    /// <summary>
    /// 字段数据类型解析器
    /// 针对“<If_HasColumnType></EndIf>”的解析
    /// </summary>
    public class HasColumnTypeExpression : ColumnExpression
    {
        public override void Interpret(ColumnResolveContext context)
        {
            Regex regex = new Regex(@"<If_HasColumnType>(?<template>((?!</EndIf>).|\n)*)</EndIf>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(context.Output);

            string template = match.Groups["template"].Value;

            string result = template.Replace(@"{Column_DataType}", context.Column.DbType);

            context.Output = regex.Replace(context.Output, result);
        }
    }
}
