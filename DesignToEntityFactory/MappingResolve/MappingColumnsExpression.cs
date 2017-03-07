using DesignToEntityFactory.MappingResolve.Columns;
using DesignToEntityFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DesignToEntityFactory.MappingResolve
{
    /// <summary>
    /// Mapping类字段解析器
    /// </summary>
    public class MappingColumnsExpression : MappingExpression
    {
        public override void Interpret(MappingResolveContext context)
        {
            if (context == null || context.Table == null) return;

            Regex regex = new Regex(@"<Mapping_Columns>(?<template>((?!<separator>).|\n)*)<separator>(?<separator>((?!</separator>).|\n)*)</separator>[^<]*</Mapping_Columns>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(context.Output);

            //单个字段代码的生成模板
            string columnTemplate = match.Groups["template"].Value;

            //分隔符
            string separator = match.Groups["separator"].Value;

            StringBuilder replaceChars = new StringBuilder();

            foreach (var column in context.Table.Columns)
            {
                string columnCode = ResolvColumn(columnTemplate, column);

                if (replaceChars.Length > 0)
                {
                    replaceChars.Append(separator);
                }

                replaceChars.Append(columnCode);
            }

            //输出替换

            context.Output = regex.Replace(context.Output, replaceChars.ToString());
        }

        /// <summary>
        /// 将<see cref="TableColumn"/>对象用模板解析后的内容
        /// </summary>
        /// <param name="template">Mapping类中的字段生成模板</param>
        /// <param name="column">数据表<see cref="TableColumn"/>字段信息</param>
        /// <returns></returns>
        private string ResolvColumn(string template, TableColumn column)
        {
            string result = string.Empty;

            ColumnResolveContext context = new ColumnResolveContext(template, column);

            List<ColumnExpression> list = new List<ColumnExpression>();
            list.Add(new MappingPropertyNameExpression());
            list.Add(new ColumnNameExpression());
            list.Add(new HasColumnTypeExpression());
            list.Add(new IsRequiredExpression());
            list.Add(new IsOptionalExpression());
            list.Add(new IsUnicodeExpression());
            list.Add(new HasPrecisionExpression());
            list.Add(new MaxLengthExpression());
            list.Add(new UniqueKeyGeneratedExpression());

            foreach(var exp in list)
            {
                exp.Interpret(context);
            }

            return context.Output;
        }
    }
}
