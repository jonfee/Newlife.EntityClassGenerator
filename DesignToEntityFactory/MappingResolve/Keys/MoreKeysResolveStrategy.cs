using DesignToEntityFactory.Models;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve.Keys
{
    /// <summary>
    /// 多个主键时的解析策略
    ///  针对“Primary_Key_Name”及分隔符的解析
    /// </summary>
    public class MoreKeysResolveStrategy : KeysResolveStrategy
    {
        public override string Execute(string template, List<TableColumn> columns)
        {
            StringBuilder sb = new StringBuilder();

            Regex regex = new Regex(@"<Foreach_PrimaryKeys>(?<item>((?!<separator>).|\n)*)<separator>(?<separator>((?!</separator>).|\n)*)</separator>[^<]*</Foreach_PrimaryKeys>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(template);

            //循环项中的模板
            var itemTemplate = match.Groups["item"].Value;

            //分隔符
            var separator = match.Groups["separator"].Value;

            foreach(var column in columns)
            {
                if (sb.Length > 0) sb.Append(separator);

                var itemStr = itemTemplate.Replace(@"{Primary_Key_Name}", column.Name);

                sb.Append(itemStr);
            }

            return regex.Replace(template, sb.ToString());
        }
    }
}
