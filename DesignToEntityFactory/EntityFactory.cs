using DesignToEntityFactory.Core;
using DesignToEntityFactory.EntityResolver;
using DesignToEntityFactory.Models;
using DesignToEntityFactory.TableResolver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DesignToEntityFactory
{
    /// <summary>
    /// 数据表转实体工厂
    /// </summary>
    public class EntityFactory
    {
        #region 私有成员

        /// <summary>
        /// 源文件地址
        /// </summary>
        private string _sourceFile;

        /// <summary>
        /// 输出目录
        /// </summary>
        private string _output;

        /// <summary>
        /// 数据表队列
        /// </summary>
        private Queue<TableDesc> _tableQueue;

        #endregion

        #region 公共方法

        /// <summary>
        /// 运行
        /// </summary>
        public void Run()
        {
            SettingsHandler handler = new SettingsHandler();
            handler.GetSettings();

            _sourceFile = handler.HtmlFilePath;
            _output = handler.Output;

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

            //实体类模板文件路径
            string templatePath = $"{AppContext.BaseDirectory}{ConfigurationManager.AppSettings["EntityTemplatePath"]}";
            //获取模板内容
            string templateContent = Tools.ReadFileContent(templatePath);

            while (_tableQueue.Count() > 0)
            {
                var table = _tableQueue.Dequeue();

                EntityResolverContext context = new EntityResolverContext(templateContent, table);

                List<EntityExpression> exps = new List<EntityExpression>();
                exps.Add(new ModuleNameExpression());               //模块名称文法解释器
                exps.Add(new EntityNameExpression());               //实体名称文法解释器
                exps.Add(new EntityDescriptionExpression());        //实体类描述文法解释器
                exps.Add(new ForEachPropertiesExpression());        //实体属性循环处理文法解释器
                exps.Add(new ForeachPrimaryKeysExpression());       //实体主键处理方法解释器

                foreach (var exp in exps)
                {
                    exp.Interpret(context);
                }

                string fileName = $"{context.TableDesc.Name}.cs";

                //保存文件
                SaveFile(context.TableDesc.Module, fileName, context.OutputEntityContent);
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="fileName">存储的文件名</param>
        /// <param name="fileContent">文件内容</param>
        private void SaveFile(string moduleName, string fileName, string fileContent)
        {
            //存储目录
            string folder = $"{_output}\\{moduleName}";
            //目录不存在时创建
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }
            Directory.CreateDirectory(folder);

            //存储的最终文件路径
            string filePath = $"{folder}\\{fileName}";

            //写入文件并保存，存在则覆盖，不存在则新建
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.Write(fileContent);
            }
        }

        /// <summary>
        /// 解析数据表描述到队列
        /// </summary>
        /// <returns></returns>
        private void ResolverTableDescToQueue()
        {
            //待处理数据表描述队列
            _tableQueue = new Queue<TableDesc>();

            //获取数据表的匹配集合
            var tableMatchs = GetTableMatchs();

            //循环处理
            // 将每个HTML的内容描述转换为TableDesc对象 
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
        }

        /// <summary>
        /// 获取数据表匹配集合
        /// </summary>
        /// <returns></returns>
        private MatchCollection GetTableMatchs()
        {
            //文件内容
            string content = Tools.ReadFileContent(_sourceFile);

            if (string.IsNullOrWhiteSpace(content)) return null;

            Regex regex = new Regex(@"<(?<HxTag>h\d+)\s+[^>]*id=""表[^>]+>((?<Nested><\k<HxTag>[^>]*>)|</\k<HxTag>>(?<-Nested>)|.*?)*</\k<HxTag>>(?<clumns>((?!</table>).|\n)*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return regex.Matches(content);
        }

        #endregion
    }
}
