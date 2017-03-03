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
        /// <param name="context"><see cref="TableDescContext"/>实例</param>
        public override void Interpret(TableDescContext context)
        {
            if (context == null || string.IsNullOrWhiteSpace(context.TableHtml)) return;

            //数据表字段 的匹配正则式
            Regex regex = new Regex(@"<tr>[\r\n\s]*<td>(?<cannullable><strong>)?(?<name>[^<]*)(</strong>)?</td>[\r\n\s]*<td>(?<datatype>[^<]*)</td>[\r\n\s]*<td>(?<defaultvalue>[^<]*)</td>[\r\n\s]*<td>(?<desc>((?!</td>).|\n)*)</td>[\r\n\s]*</tr>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

            MatchCollection mc = regex.Matches(context.TableHtml);

            List<TableColumn> columnList = new List<TableColumn>();

            //遍历字段处理
            foreach (Match m in mc)
            {
                //字段名
                string name = m.Groups["name"].Value.Trim();
                //字段描述
                string desc = ResolverDesc(m.Groups["desc"].Value.Trim());
                //是否可为空
                bool canNullable = string.IsNullOrWhiteSpace(m.Groups["cannullable"].Value);
                //数据类型
                string dataType = ResolverEnumType(desc); //枚举类型
                if (string.IsNullOrWhiteSpace(dataType))
                    dataType = ResolverDataType(m.Groups["datatype"].Value, canNullable);   //非枚举类型

                TableColumn column = new TableColumn();

                column.Name = name;
                column.DefaultValue = m.Groups["defaultvalue"].Value;
                column.Description = desc;
                column.CanNullable = canNullable;
                column.DataType = dataType;
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
            Regex regex = new Regex(@"^(?<type>[a-z][^\(（\?]*)([\(（][^\)）]+[\)）])?\??$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

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
                case "guid":
                    newtype = "Guid";
                    break;
            }

            if (canNullable && newtype != "string") newtype = $"{newtype}?";

            return newtype;
        }

        /// <summary>
        /// 解析出实体属性的描述信息
        /// </summary>
        /// <param name="desc">原字段描述</param>
        /// <returns></returns>
        private string ResolverDesc(string desc)
        {
            if (string.IsNullOrWhiteSpace(desc)) return null;

            Regex regex = new Regex(@"<code>(?<enumtype>((?!</code>).|\n)+)</code>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(desc);

            return regex.Replace(desc, $"“{match.Groups["enumtype"].Value}”");
        }

        /// <summary>
        /// 从字段描述中解析出枚举类型
        /// </summary>
        /// <param name="desc">字段描述</param>
        /// <returns></returns>
        private string ResolverEnumType(string desc)
        {
            if (string.IsNullOrWhiteSpace(desc)) return null;

            string enumType = null;

            //检测是否为枚举类型
            if (desc.IndexOf("枚举") > -1)
            {
                Regex regex = new Regex(@"“(?<enumtype>[^”]+)”", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                Match match = regex.Match(desc);

                enumType = match.Groups["enumtype"].Value;

                enumType = enumType.Replace("_", "");
            }

            return enumType;
        }
    }
}
