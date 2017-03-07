using DesignToEntityFactory.MappingResolve.Columns.MaxLength;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve.Columns
{
    /// <summary>
    /// 字段数据最大长度约束解析器
    /// 针对“<If_HasMaxLength></EndIf>”的解析
    /// </summary>
    public class MaxLengthExpression : ColumnExpression
    {
        public override void Interpret(ColumnResolveContext context)
        {
            Regex regex = new Regex(@"<If_HasMaxLength>(?<template>((?!</EndIf>).|\n)*)</EndIf>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(context.Output);

            string template = match.Groups["template"].Value;

            MaxLengthChecker checker = new MaxLengthChecker();
            MaxLengthReplacer replacer = new MaxLengthReplacer();
            checker.SetSuccessor(replacer);

            string replaceStr = checker.Resolve(template, context.Column.DbType, context.Column.TotalBit);

            context.Output = regex.Replace(context.Output, replaceStr);
        }
    }
}
