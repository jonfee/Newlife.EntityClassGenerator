using DesignToEntityFactory.Core;
using DesignToEntityFactory.EnumResolve.ClassFile;
using DesignToEntityFactory.EnumResolve.Data;
using DesignToEntityFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.Factory
{
    /// <summary>
    /// 枚举类文件生成工厂类
    /// </summary>
    public class EnumFactory : FileFactory
    {
        #region 初始化

        /// <summary>
        /// 初始化<see cref="EnumFactory"/>实例
        /// </summary>
        /// <param name="sourceHtml">源文件HTML内容</param>
        public EnumFactory(string sourceHtml) : base(sourceHtml, "Enums")
        {
        }

        #endregion

        public override void GenerateFiles()
        {
            //获取枚举类模板内容
            string templateContent = Tools.ReadFileContent(Configs.EnumTemplatePath);

            //枚举总数
            int enumCount = DataQueue.Count();

            Console.WriteLine($"共有枚举{enumCount}个");

            //处理数据表转换队列
            while (DataQueue.Count() > 0)
            {
                var enumDesc = (EnumDesc)DataQueue.Dequeue();

                Console.WriteLine($"正在生成【枚举类】{enumDesc.Description}({enumDesc.Name})……{enumCount - DataQueue.Count()}/{enumCount}");

                //枚举类解析对象上下文
                EnumClassResolveContext context = new EnumClassResolveContext(templateContent, enumDesc);

                //解析器集合
                List<EnumClassExpression> exps = new List<EnumClassExpression>();
                exps.Add(new EnumModuleNameExpression());               //模块名称文法解释器
                exps.Add(new EnumNameExpression());                     //实体名称文法解释器
                exps.Add(new EnumDescriptionExpression());              //实体类描述文法解释器
                exps.Add(new ForeachOptionsExpression());               //实体属性循环处理文法解释器

                //循环执行解析
                foreach (var exp in exps)
                {
                    exp.Interpret(context);
                }

                //存储的最终文件路径
                string filePath = $@"{OutputDirectory}\{context.EnumDesc.Module}\{context.EnumDesc.Name}.cs";

                //写入文件并保存
                SaveFile(filePath, context.OutputEnumClassContent);
            }
        }

        public override void ResolveDataInQueue()
        {
            #region //获取枚举的匹配集合

            if (string.IsNullOrWhiteSpace(SourceHtml)) return;

            Regex regex = new Regex(@"<(?<HxTag>h\d+)\s+[^>]*id=""枚举[^>]+>((?<Nested><\k<HxTag>[^>]*>)|</\k<HxTag>>(?<-Nested>)|.*?)*</\k<HxTag>>(?<clumns>((?!</table>).|\n)*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            var enumsMatchs = regex.Matches(SourceHtml);

            #endregion

            #region //将每个HTML的内容描述转换为EnumDesc对象 

            //循环处理
            foreach (Match m in enumsMatchs)
            {
                //定义数据表描述对象处理上下文
                EnumDescContext context = new EnumDescContext(m.Value);

                //将HTMl信息解释为枚举EnumDesc的解释器
                List<EnumExpression> exps = new List<EnumExpression>();
                exps.Add(new EnumDescExpression());         //枚举描述信息解释器
                exps.Add(new EnumOptionsExpression());      //枚举成员解释器

                //循环执行文法解释器
                foreach (var exp in exps)
                {
                    exp.Interpret(context);
                }

                //将解释出来的EnumDesc加入数据队列
                DataQueue.Enqueue(context.EnumDesc);
            }

            #endregion
        }
    }
}
