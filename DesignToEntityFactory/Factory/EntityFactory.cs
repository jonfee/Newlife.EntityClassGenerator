using DesignToEntityFactory.Core;
using DesignToEntityFactory.EntityResolve.ClassFile;
using DesignToEntityFactory.Models;
using DesignToEntityFactory.TableResolve;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignToEntityFactory.Factory
{
    /// <summary>
    /// 实体类文件生成工厂类
    /// </summary>
    public class EntityFactory : FileFactory
    {
        /// <summary>
        /// 数据表集合
        /// </summary>
        private List<TableDesc> _tables;

        #region 初始化

        /// <summary>
        /// 初始化<see cref="EntityFactory"/>实例
        /// </summary>
        /// <param name="sourceHtml">源文件HTML内容</param>
        public EntityFactory(string sourceHtml) : this(sourceHtml, null)
        {

        }

        /// <summary>
        /// 初始化<see cref="EntityFactory"/>实例
        /// </summary>
        /// <param name="sourceHtml">源文件HTML内容</param>
        /// <param name="tables">数据表集合</param>
        public EntityFactory(string sourceHtml, List<TableDesc> tables) : base(sourceHtml, "Models")
        {
            _tables = tables;
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

                Console.WriteLine($"正在生成【实体类】{table.Description}({table.Name})……{tableCount - DataQueue.Count()}/{tableCount}");

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
            if (_tables == null)
                _tables = TableHelper.ResolveTables(SourceHtml);

            foreach (var tb in _tables)
            {
                //将解释出来的TableDesc加入数据队列
                DataQueue.Enqueue(tb);
            }
        }

        #endregion
    }
}
