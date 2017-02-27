using DesignToEntityFactory.Models;
using DesignToEntityFactory.TableResolver;
using System;
using System.Collections.Generic;
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

            //数据表描述对象集合
            List<TableDesc> tables = GetTableDesc();

            //导出为实体类文件，如:Area.cs
            var aa = tables;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取数据表描述对象集合
        /// </summary>
        /// <returns></returns>
        private List<TableDesc> GetTableDesc()
        {
            //获取数据表的匹配集合
            var tableMatchs = GetTableMatchs();

            //数据表描述对象集合
            List<TableDesc> tables = new List<TableDesc>();
            //循环处理
            // 将每个HTML的内容描述转换为TableDesc对象 
            foreach (Match m in tableMatchs)
            {
                //定义数据表描述对象处理上下文
                DataTableContext context = new DataTableContext(m.Value);

                //将HTMl信息解释为数据表TableDesc的解释器
                List<TableExpression> exps = new List<TableExpression>();
                exps.Add(new TableDescExpression());
                exps.Add(new TableColumnsExpression());

                //循环执行文法解释器
                foreach (var exp in exps)
                {
                    exp.Interpret(context);
                }

                //将解释出来的TableDesc加入结果
                tables.Add(context.Table);
            }

            return tables;
        }

        /// <summary>
        /// 获取数据表匹配集合
        /// </summary>
        /// <returns></returns>
        private MatchCollection GetTableMatchs()
        {
            //文件内容
            string content = ReadFileContent();

            if (string.IsNullOrWhiteSpace(content)) return null;

            Regex regex = new Regex(@"<(?<HxTag>h\d+)\s+[^>]*id=""表[^>]+>((?<Nested><\k<HxTag>[^>]*>)|</\k<HxTag>>(?<-Nested>)|.*?)*</\k<HxTag>>(?<clumns>((?!</table>).|\n)*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return regex.Matches(content);
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <returns></returns>
        private string ReadFileContent()
        {
            string content = "";

            if (File.Exists(_sourceFile))
            {
                using (StreamReader sr = new StreamReader(_sourceFile))
                {
                    content = sr.ReadToEnd();
                }
            }

            return content;
        }

        #endregion
    }
}
