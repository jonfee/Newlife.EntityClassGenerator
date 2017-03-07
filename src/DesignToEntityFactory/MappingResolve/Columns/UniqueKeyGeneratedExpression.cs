using DesignToEntityFactory.MappingResolve.Columns.UniquePrimary;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve.Columns
{
    /// <summary>
    /// 主键字段值生成规则约定解析器
    /// </summary>
    public class UniqueKeyGeneratedExpression : ColumnExpression
    {
        public override void Interpret(ColumnResolveContext context)
        {
            Regex regex = new Regex(@"<If_IsUniquePrimary>(?<template>((?!</EndIf>).|\n)*)</EndIf>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(context.Output);

            string template = match.Groups["template"].Value;

            UniquePrimaryChecker checker = new UniquePrimaryChecker();
            UniquePrimaryReplacer replacer = new UniquePrimaryReplacer();
            checker.SetSuccessor(replacer);

            string replaceStr = checker.Resolve(template, context.Column);

            context.Output = regex.Replace(context.Output, replaceStr);
        }
    }
}
