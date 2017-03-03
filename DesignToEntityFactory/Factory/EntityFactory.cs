using DesignToEntityFactory.Core;
using DesignToEntityFactory.EntityClassResolve;
using DesignToEntityFactory.Models;
using DesignToEntityFactory.TableResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.Factory
{
    /// <summary>
    /// 实体类文件生成工厂类
    /// </summary>
    public class EntityFactory : FileFactory
    {
        #region 初始化

        /// <summary>
        /// 初始化<see cref="EntityFactory"/>实例
        /// </summary>
        /// <param name="sourceHtml">源文件HTML内容</param>
        /// <param name="output">输出目录</param>
        public EntityFactory(string sourceHtml) : base(sourceHtml, "Models")
        {
            //初始化业务
        }

        #endregion

        #region 实现抽象方法

        /// <summary>
        /// 生成实体类文件
        /// </summary>
        public override void GenerateFiles()
        {
            //获取实体类模板内容
            string templateContent = Tools.ReadFileContent(Configs.EntityTemplatePath);

            //数据表总数
            int tableCount = DataQueue.Count();

            Console.WriteLine($"共有数据表{tableCount}个");

            //处理数据表转换队列
            while (DataQueue.Count() > 0)
            {
                var table = (TableDesc)DataQueue.Dequeue();

                Console.WriteLine($"正在生成{table.Description}({table.Name})……{tableCount - DataQueue.Count()}/{tableCount}");

                //实体类解析对象上下文
                EntityClassResolveContext context = new EntityClassResolveContext(templateContent, table);

                //解析器集合
                List<EntityClassExpression> exps = new List<EntityClassExpression>();
                exps.Add(new ModuleNameExpression());               //模块名称文法解释器
                exps.Add(new EntityNameExpression());               //实体名称文法解释器
                exps.Add(new EntityDescriptionExpression());        //实体类描述文法解释器
                exps.Add(new ForEachPropertiesExpression());        //实体属性循环处理文法解释器
                exps.Add(new ForeachPrimaryKeysExpression());       //实体主键处理方法解释器

                //循环执行解析
                foreach (var exp in exps)
                {
                    exp.Interpret(context);
                }

                //存储的最终文件路径
                string filePath = $@"{OutputDirectory}\{context.TableDesc.Module}\{context.TableDesc.Name}.cs";

                //写入文件并保存
                SaveFile(filePath, context.OutputEntityClassContent);
            }
        }

        /// <summary>
        /// 从源HTML中解析数据到队列
        /// </summary>
        public override void ResolveDataInQueue()
        {
            #region //获取数据表的匹配集合

            if (string.IsNullOrWhiteSpace(SourceHtml)) return;

            Regex regex = new Regex(@"<(?<HxTag>h\d+)\s+[^>]*id=""表[^>]+>((?<Nested><\k<HxTag>[^>]*>)|</\k<HxTag>>(?<-Nested>)|.*?)*</\k<HxTag>>(?<clumns>((?!</table>).|\n)*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            var tableMatchs = regex.Matches(SourceHtml);

            #endregion

            #region //将每个HTML的内容描述转换为TableDesc对象 

            //循环处理
            foreach (Match m in tableMatchs)
            {
                //定义数据表描述对象处理上下文
                TableDescContext context = new TableDescContext(m.Value);

                //将HTMl信息解释为数据表TableDesc的解释器
                List<TableExpression> exps = new List<TableExpression>();
                exps.Add(new TableDescExpression());    //数据表描述信息解释器
                exps.Add(new TableColumnsExpression()); //数据表字段解释器

                //循环执行文法解释器
                foreach (var exp in exps)
                {
                    exp.Interpret(context);
                }

                //将解释出来的TableDesc加入数据队列
                DataQueue.Enqueue(context.Table);
            }

            #endregion
        }

        #endregion
    }
}
