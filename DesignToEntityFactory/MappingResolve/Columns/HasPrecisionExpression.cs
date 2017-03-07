using DesignToEntityFactory.MappingResolve.Columns.Precision;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve.Columns
{
    /// <summary>
    /// Decimal类型约束解释器
    /// 针对“<If_HasPrecision></EndIf>”的解析
    /// </summary>
    public class HasPrecisionExpression : ColumnExpression
    {
        public override void Interpret(ColumnResolveContext context)
        {
            Regex regex = new Regex(@"<If_HasPrecision>(?<template>((?!</EndIf>).|\n)*)</EndIf>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(context.Output);

            string template = match.Groups["template"].Value;

            PrecisionChecker checker = new PrecisionChecker();
            PrecisionReplacer replacer = new PrecisionReplacer();
            checker.SetSuccessor(replacer);

            string replaceStr = checker.Resolve(template, context.Column.DbType, context.Column.TotalBit, context.Column.DeicmalBit);

            context.Output = regex.Replace(context.Output, replaceStr);
        }
    }
}
