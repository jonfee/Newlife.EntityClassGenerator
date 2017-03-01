using DesignToEntityFactory.Models;
using DesignToEntityFactory.PrimaryKeyResolver;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EntityResolver
{
    /// <summary>
    /// 循环处理实体对象的主键解释器
    /// 针对“<ForEach_PrimaryKeys>”的解析
    /// </summary>
    public class ForeachPrimaryKeysExpression : EntityExpression
    {
        public override void Interpret(EntityResolverContext context)
        {
            if (context == null || context.TableDesc == null) return;

            Regex regex = new Regex(@"\<(?<foreach>ForEach_PrimaryKeys)\>(?<template>((?!\<separator\>).|\n)*)\<separator\>(?<separator>((?!\</separator\>).|\n)*)\</separator\>\</\k<foreach>\>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.OutputEntityContent;

            MatchCollection mc = regex.Matches(text);

            //主键集合
            var primaryKeyslist = context.TableDesc.Columns.Where(p => p.IsPrimaryKey == true).ToList();

            foreach (Match m in mc)
            {
                //当前匹配结果中的主键模板
                string template = m.Groups["template"].Value;

                //分隔器
                string separator = m.Groups["separator"].Value;

                //获取当前匹配结果解析后的code内容
                var data = GetTemplateReplaceAfterChars(template,separator, primaryKeyslist);

                text = text.Replace(m.Value, data);
            }

            //实体对象输出内容
            context.OutputEntityContent = text;
        }

        /// <summary>
        /// 获取主键模板替换后的结果字符串
        /// </summary>
        /// <param name="template">主键处理模板</param>
        /// <param name="seprator">分隔器</param>
        /// <param name="primaryKeyList">主键属性集合</param>
        /// <returns></returns>
        private string GetTemplateReplaceAfterChars(string template,string seprator, List<TableColumn> primaryKeyList)
        {
            //定义一个存储实体属性的code变量
            StringBuilder sb = new StringBuilder();

            //循环处理每个字段信息
            foreach (var column in primaryKeyList)
            {
                PrimarykeyResolverContext context = new PrimarykeyResolverContext(column, template);

                List<PrimaryKeyExpression> expList = new List<PrimaryKeyExpression>();
                expList.Add(new PrimarykeyPropertyNameExpression());          //属性名解释器

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
