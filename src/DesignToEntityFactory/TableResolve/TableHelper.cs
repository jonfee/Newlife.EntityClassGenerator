using DesignToEntityFactory.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.TableResolve
{
    /// <summary>
    /// Table帮助类
    /// </summary>
    public class TableHelper
    {
        /// <summary>
        /// 从Html代码中解析出数据表集合
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static List<TableDesc> ResolveTables(string html)
        {
            if (string.IsNullOrWhiteSpace(html)) return null;

            List<TableDesc> list = new List<TableDesc>();

            #region //获取数据表的匹配集合
            
            Regex regex = new Regex(@"<(?<HxTag>h\d+)\s+[^>]*id=""表[^>]+>((?<Nested><\k<HxTag>[^>]*>)|</\k<HxTag>>(?<-Nested>)|.*?)*</\k<HxTag>>(?<clumns>((?!</table>).|\n)*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            var tableMatchs = regex.Matches(html);

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

                //将解释出来的TableDesc加入到集合中
                list.Add(context.Table);
            }

            #endregion

            return list;
        }
    }
}
