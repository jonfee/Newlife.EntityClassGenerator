using DesignToEntityFactory.Models;
using DesignToEntityFactory.PropertyResolver;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EntityClassResolve
{
    /// <summary>
    /// 循环处理实体对象属性解释器
    /// 针对“<ForEach_Properties>”的解析
    /// </summary>
    public class ForEachPropertiesExpression : EntityClassExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"></param>
        public override void Interpret(EntityClassResolveContext context)
        {
            if (context == null || context.TableDesc == null) return;

            Regex regex = new Regex(@"<(?<foreach>ForEach_Properties)>(?<template>((?!<separator>).|\n)*)<separator>(?<separator>((?!</separator>).|\n)*)</separator>[^<]*</\k<foreach>>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.OutputEntityClassContent;

            MatchCollection mc = regex.Matches(text);

            //遍历处理匹配结果的解析
            foreach (Match m in mc)
            {
                //获取当前匹配结果中的属性模板
                var propertyTemplate = m.Groups["template"].Value;

                //分隔器
                var separator = m.Groups["separator"].Value;

                //获取当前匹配结果解析后的code内容
                var data = GetTemplateReplaceAfterChars(propertyTemplate, separator, context.TableDesc.Columns);

                text = text.Replace(m.Value, data);
            }

            //实体对象输出内容
            context.OutputEntityClassContent = text;
        }

        /// <summary>
        /// 获取属性模板替换后的结果字符串
        /// </summary>
        /// <param name="propertyTemplate">属性模板</param>
        /// <param name="seprator">分隔器</param>
        /// <param name="columnList">属性集合</param>
        /// <returns></returns>
        private string GetTemplateReplaceAfterChars(string propertyTemplate, string seprator, List<TableColumn> columnList)
        {
            //定义一个存储实体属性的code变量
            StringBuilder sb = new StringBuilder();

            //解析出限制或非限制长度的属性模板
            //1. 限制长度的模板
            var limitTemplate = ResolverPropertyTemplate(propertyTemplate, true);
            //2. 非限制长度的模板
            var unlimitTemplate = ResolverPropertyTemplate(propertyTemplate, false);

            //循环处理每个字段信息
            foreach (var column in columnList)
            {
                //循环项中的模板
                var template = column.LimitLength > 0 ? limitTemplate : unlimitTemplate;

                PropertyResolverContext context = new PropertyResolverContext(column, template);

                List<PropertyExpression> expList = new List<PropertyExpression>();
                expList.Add(new PropertyNameExpression());          //属性名解释器
                expList.Add(new PropertyDataTypeExpression());      //属性数据类型解释器
                expList.Add(new PropertyLimitLengthExpression());   //属性数据长度限制解释器
                expList.Add(new ProperytyDescriptionExpression());  //属性描述解释器

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

        /// <summary>
        /// 根据属性值长度限制解析出新属性模板
        /// </summary>
        /// <param name="inputTemplate">原属性模板</param>
        /// <param name="limitLength">是否限制长度</param>
        /// <returns></returns>
        private string ResolverPropertyTemplate(string inputTemplate, bool limitLength)
        {
            Regex regex = new Regex(@"\<If_Limit_Length\>(?<if>((?!\<Else_Limit_Length\>).|\n)*)\<Else_Limit_Length\>(?<else>((?!\</End_Limit_Length\>).|\n)*)\</End_Limit_Length\>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(inputTemplate);

            string limitTemp = "";

            if (limitLength)
            {
                limitTemp = match.Groups["if"].Value;
            }
            else
            {
                limitTemp = match.Groups["else"].Value;
            }

            return regex.Replace(inputTemplate, limitTemp);
        }
    }
}
