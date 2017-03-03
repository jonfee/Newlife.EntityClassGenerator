using DesignToEntityFactory.Core;
using DesignToEntityFactory.EntityResolver;
using DesignToEntityFactory.Models;
using DesignToEntityFactory.TableResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.Factory
{
    /// <summary>
    /// 数据表转实体工厂
    /// </summary>
    public class EntityFactory : FileFactory
    {
        #region 私有成员

        /// <summary>
        /// 数据表队列
        /// </summary>
        private Queue<TableDesc> _tableQueue;

        #endregion

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

        #region 公共方法

        /// <summary>
        /// 运行
        /// </summary>
        public override void Run()
        {
            //解析数据表描述到队列
            ResolverTableDescToQueue();

            //生成实体类文件，从队列中生成
            GenerateFiles();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 从队列中生成实体类文件
        /// </summary>
        private void GenerateFiles()
        {
            //获取实体类模板内容
            string templateContent = Tools.ReadFileContent(Configs.EntityTemplatePath);

            //数据表总数
            int tableCount = _tableQueue.Count();

            Console.WriteLine($"共有数据表{tableCount}个");

            //处理数据表转换队列
            while (_tableQueue.Count() > 0)
            {
                var table = _tableQueue.Dequeue();

                Console.WriteLine($"正在生成{table.Description}({table.Name})……{tableCount - _tableQueue.Count()}/{tableCount}");

                //实体类解析对象上下文
                EntityResolverContext context = new EntityResolverContext(templateContent, table);

                //解析器集合
                List<EntityExpression> exps = new List<EntityExpression>();
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
                
                //保存文件
                SaveFile(context.TableDesc.Module, context.TableDesc.Name, context.OutputEntityContent);
            }
        }

        /// <summary>
        /// 解析数据表描述到队列
        /// </summary>
        /// <returns></returns>
        private void ResolverTableDescToQueue()
        {
            #region //获取数据表的匹配集合

            if (string.IsNullOrWhiteSpace(SourceHtml)) return;

            Regex regex = new Regex(@"<(?<HxTag>h\d+)\s+[^>]*id=""表[^>]+>((?<Nested><\k<HxTag>[^>]*>)|</\k<HxTag>>(?<-Nested>)|.*?)*</\k<HxTag>>(?<clumns>((?!</table>).|\n)*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            var tableMatchs= regex.Matches(SourceHtml);

            #endregion

            //待处理数据表描述队列
            _tableQueue = new Queue<TableDesc>();

            #region //循环处理，将每个HTML的内容描述转换为TableDesc对象 
            foreach (Match m in tableMatchs)
            {
                //定义数据表描述对象处理上下文
                DataTableContext context = new DataTableContext(m.Value);

                //将HTMl信息解释为数据表TableDesc的解释器
                List<TableExpression> exps = new List<TableExpression>();
                exps.Add(new TableDescExpression());    //数据表描述信息解释器
                exps.Add(new TableColumnsExpression()); //数据表字段解释器

                //循环执行文法解释器
                foreach (var exp in exps)
                {
                    exp.Interpret(context);
                }

                //将解释出来的TableDesc加入结果
                _tableQueue.Enqueue(context.Table);
            }
            #endregion
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="tableName">数据表名</param>
        /// <param name="fileContent">文件内容</param>
        private void SaveFile(string moduleName, string tableName, string fileContent)
        {
            //存储的最终文件路径
            string filePath = $@"{OutputDirectory}\{moduleName}\{tableName}.cs";

            //写入文件并保存
            SaveFile(filePath, fileContent);
        }

        #endregion
    }
}
