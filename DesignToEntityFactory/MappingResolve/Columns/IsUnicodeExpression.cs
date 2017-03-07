using DesignToEntityFactory.MappingResolve.Columns.Unicode;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve.Columns
{
    /// <summary>
    /// 是否双字符数据类型解析器
    /// 针对“<If_IsUnicode></EndIf>”的解析
    /// </summary>
    public class IsUnicodeExpression : ColumnExpression
    {
        public override void Interpret(ColumnResolveContext context)
        {
            Regex regex = new Regex(@"<If_IsUnicode>(?<template>((?!</EndIf>).|\n)*)</EndIf>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(context.Output);

            string template = match.Groups["template"].Value;

            UnicodeChecker checker = new UnicodeChecker();
            UnicodeReplacer replacer = new UnicodeReplacer();
            checker.SetSuccessor(replacer);

            string replaceStr = checker.Resolve(template, context.Column.DbType);

            context.Output = regex.Replace(context.Output, replaceStr);
        }
    }
}
