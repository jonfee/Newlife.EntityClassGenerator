using System.Text.RegularExpressions;

namespace DesignToEntityFactory.TableResolver
{
    /// <summary>
    /// 数据表描述文法解释器
    /// </summary>
    public class TableDescExpression : TableExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"><see cref="DataTableContext"/>实例</param>
        public override void Interpret(DataTableContext context)
        {
            if (context == null || string.IsNullOrWhiteSpace(context.TableHtml)) return;

            //数据表描述 的匹配正则式
            Regex regex = new Regex(@"<(?<HxTag>h\d+)\s+[^>]*id=""表[^>]+>\s*\[表\]\s*(?<desc>[^\(]+)\((?<name>[^\)]+)\)</\k<HxTag>>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(context.TableHtml);

            string name = match.Groups["name"].Value;
            
            context.Table.Name = name;                                      //数据表名
            context.Table.Description = match.Groups["desc"].Value;         //数据表说明
            context.Table.Module = name.Substring(0, name.IndexOf("_"));    //所属模块
        }
    }
}
