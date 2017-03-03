using DesignToEntityFactory.EnumResolve.Option;
using DesignToEntityFactory.Models;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EnumResolve.ClassFile
{
    /// <summary>
    /// 循环处理枚举成员解释器
    /// 针对“{Foreach_Options}”的解析
    /// </summary>
    public class ForeachOptionsExpression : EnumClassExpression
    {
        public override void Interpret(EnumClassResolveContext context)
        {
            if (context == null || context.EnumDesc == null) return;

            Regex regex = new Regex(@"<(?<foreach>ForEach_Options)>(?<template>((?!<separator>).|\n)*)<separator>(?<separator>((?!</separator>).|\n)*)</separator>[^<]*</\k<foreach>>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.OutputEnumClassContent;

            MatchCollection mc = regex.Matches(text);

            //遍历处理匹配结果的解析
            foreach (Match m in mc)
            {
                //获取当前匹配结果中的属性模板
                var propertyTemplate = m.Groups["template"].Value;

                //分隔器
                var separator = m.Groups["separator"].Value;

                //获取当前匹配结果解析后的code内容
                var data = GetTemplateReplaceAfterChars(propertyTemplate, separator, context.EnumDesc.Options);

                text = text.Replace(m.Value, data);
            }

            //枚举对象输出内容
            context.OutputEnumClassContent = text;
        }

        /// <summary>
        /// 获取枚举成员模板替换后的结果字符串
        /// </summary>
        /// <param name="optionTemplate">枚举成员模板</param>
        /// <param name="seprator">分隔器</param>
        /// <param name="optionList">枚举成员集合</param>
        /// <returns></returns>
        private string GetTemplateReplaceAfterChars(string optionTemplate, string seprator, List<EnumOption> optionList)
        {
            //定义一个存储枚举成员code的变量
            StringBuilder sb = new StringBuilder();
            
            //循环处理每个枚举成员信息
            foreach (var option in optionList)
            {
                EnumOptionResolveContext context = new EnumOptionResolveContext(option, optionTemplate);

                List<EnumClassOptionExpression> expList = new List<EnumClassOptionExpression>();
                expList.Add(new EnumOptionNameExpression());            //枚举成员名解释器
                expList.Add(new EnumOptionDescriptionExpression());     //枚举成员描述解释器
                expList.Add(new EnumOptionAliasExpression());           //枚举成员别名解释器
                expList.Add(new EnumOptionValueExpression());           //枚举成员常量值解释器

                foreach (var exp in expList)
                {
                    exp.Interpret(context);
                }

                if (sb.Length > 0)
                {
                    sb.Append(seprator);
                }

                sb.Append(context.Output);
            }

            return sb.ToString();
        }
    }
}
