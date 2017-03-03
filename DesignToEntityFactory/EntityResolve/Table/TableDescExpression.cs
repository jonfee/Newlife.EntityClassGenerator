using System.Configuration;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EntityResolve.Table
{
    /// <summary>
    /// 数据表描述文法解释器
    /// </summary>
    public class TableDescExpression : TableExpression
    {
        /// <summary>
        /// 执行文法解释
        /// </summary>
        /// <param name="context"><see cref="TableDescContext"/>实例</param>
        public override void Interpret(TableDescContext context)
        {
            if (context == null || string.IsNullOrWhiteSpace(context.TableHtml)) return;

            //数据表描述 的匹配正则式
            Regex regex = new Regex(@"<(?<HxTag>h\d+)\s+[^>]*id=""表[^>]+>\s*\[表\]\s*(?<desc>[^\(（]+)[\(（](?<name>[^\）)]+)[\)）]</\k<HxTag>>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(context.TableHtml);

            string name = match.Groups["name"].Value;
            int splitIndex = name.IndexOf("_");
            string module = splitIndex > 0 ? name.Substring(0, splitIndex) : name;
            if (string.IsNullOrWhiteSpace(name)) name = ConfigurationManager.AppSettings["DefaultModuleName"];

            context.Table.Name = name.Replace("_", "");                     //数据表名
            context.Table.Description = match.Groups["desc"].Value;         //数据表说明
            context.Table.Module = module;                                  //所属模块
        }
    }
}
