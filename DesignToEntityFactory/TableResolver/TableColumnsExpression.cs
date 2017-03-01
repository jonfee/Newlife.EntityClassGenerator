using DesignToEntityFactory.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.TableResolver
{
    /// <summary>
    /// 数据表字段文法解释器
    /// </summary>
    public class TableColumnsExpression : TableExpression
    {
        /// <summary>
        /// 执行方法解释
        /// </summary>
        /// <param name="context"><see cref="DataTableContext"/>实例</param>
        public override void Interpret(DataTableContext context)
        {
            /*
              <tr>
                <td><strong>RuleId</strong></td>
                <td>long</td>
                <td>0</td>
                <td>主键，规则ID</td>
               </tr>
             */

            if (context == null || string.IsNullOrWhiteSpace(context.TableHtml)) return;

            //数据表字段 的匹配正则式
            Regex regex = new Regex(@"<tr>[\r\n\s]*<td>(?<cannullable><strong>)?(?<name>[^<]*)(</strong>)?</td>[\r\n\s]*<td>(?<datatype>[^<]*)</td>[\r\n\s]*<td>(?<defaultvalue>[^<]*)</td>[\r\n\s]*<td>(?<desc>[^<]*)</td>[\r\n\s]*</tr>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

            MatchCollection mc = regex.Matches(context.TableHtml);

            List<TableColumn> columnList = new List<TableColumn>();

            //遍历字段处理
            foreach (Match m in mc)
            {
                string name = m.Groups["name"].Value.Trim();
                string desc = m.Groups["desc"].Value.Trim();
                bool canNullable = string.IsNullOrWhiteSpace(m.Groups["cannullable"].Value);

                TableColumn column = new TableColumn();

                column.Name = name;
                column.DefaultValue = m.Groups["defaultvalue"].Value;
                column.Description = desc;
                column.CanNullable = canNullable;
                column.DataType = ResolverDataType(m.Groups["datatype"].Value, canNullable);
                column.IsPrimaryKey = desc.StartsWith("主键");

                columnList.Add(column);
            }

            context.Table.Columns = columnList;
        }

        /// <summary>
        /// 解析数据类型
        /// </summary>
        /// <param name="datatype">html设计中的数据类型</param>
        /// <returns></returns>
        private string ResolverDataType(string datatype, bool canNullable)
        {
            Regex regex = new Regex(@"^(?<type>[a-z][^\(（]*)([\(（][^\)）]+[\)）])?\??$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string newtype = regex.Match(datatype).Groups["type"].Value;

            switch (newtype.ToLower())
            {
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                case "text":
                case "ntext":
                    newtype = "string";
                    break;
                case "bit":
                    newtype = "bool";
                    break;
                case "time":
                    newtype = "TimeSpan";
                    break;
                case "datetime":
                    newtype = "DateTime";
                    break;
                case "bigint":
                    newtype = "long";
                    break;
            }

            if (canNullable && newtype != "string") newtype = $"{newtype}?";

            return newtype;
        }
    }
}
