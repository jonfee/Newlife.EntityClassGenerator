using DesignToEntityFactory.Core;
using DesignToEntityFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.TableResolve
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

            List<string> keys = new List<string>();

            //遍历字段处理
            foreach (Match m in mc)
            {
                //字段名
                string name = m.Groups["name"].Value.Trim();
                //字段描述
                string desc = ResolverDesc(m.Groups["desc"].Value.Trim());
                //是否可为空
                bool canNullable = string.IsNullOrWhiteSpace(m.Groups["cannullable"].Value);

                //字段枚举类型
                string enumType = ResolverEnumType(desc); //枚举类型

                //数据库数据类型
                string dbType;
                //C#中数据类型
                string csharpType;
                //总限制长度
                int totalBit;
                //小数位限制长度
                int decimalBit;

                //解析数据类型
                ResolverDataType(m.Groups["datatype"].Value, canNullable, out csharpType, out dbType, out totalBit, out decimalBit);
                if (!string.IsNullOrWhiteSpace(enumType)) csharpType = enumType;

                TableColumn column = new TableColumn();

                column.Name = name;
                column.DefaultValue = m.Groups["defaultvalue"].Value;
                column.Description = desc;
                column.CanNullable = canNullable;
                column.DataType = csharpType;
                column.DbType = dbType;
                column.TotalBit = totalBit;
                column.DeicmalBit = decimalBit;
                column.IsPrimaryKey = desc.StartsWith("主键");
                column.IsUniquePrimary = false; //是否为唯一主键，默认false

                columnList.Add(column);

                //加入到主键集合中
                if (column.IsPrimaryKey)
                {
                    keys.Add(name);
                }
            }

            //处理唯一主键
            if (keys.Count == 1)
            {
                var item = columnList.FirstOrDefault(p => p.Name.Equals(keys[0]));
                item.IsUniquePrimary = true;
            }

            context.Table.Columns = columnList;
        }

        /// <summary>
        /// 解析数据类型，同时输出<paramref name="totalBit"/>和<paramref name="decimalBit"/>参数值
        /// </summary>
        /// <param name="datatype">html设计中的数据类型</param>
        /// <param name="canNullable">是否允许为空</param>
        /// <param name="csharpType">out输出CSharp中的数据类型</param>
        /// <param name="dbType">out输出数据库中的数据类型<</param>
        /// <param name="totalBit">out输出总限制长度</param>
        /// <param name="decimalBit">out输出小数位限制长度</param>
        /// <returns></returns>
        private void ResolverDataType(string datatype, bool canNullable, out string csharpType, out string dbType, out int totalBit, out int decimalBit)
        {
            dbType = null;
            totalBit = 0;
            decimalBit = 0;

            Regex regex = new Regex(@"^(?<type>[a-z][^\(（\?]*)([\(（](?<limitLength>[^\)）]+)[\)）])?\??$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(datatype);

            #region //数据类型处理

            string type = match.Groups["type"].Value;
            //数据库数据类型
            dbType = Tools.ConvertToDBType(type);
            //C#中的数据类型
            csharpType = Tools.ConvertToCsharpType(type);
            if (canNullable && csharpType != "string") csharpType = $"{csharpType}?";

            #endregion

            #region //限制长度处理

            string[] limits = (match.Groups["limitLength"].Value ?? string.Empty).Split(new[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);

            if (limits.Length > 0)
            {
                int.TryParse(limits[0], out totalBit);
            }
            if (limits.Length > 1)
            {
                int.TryParse(limits[1], out decimalBit);
            }

            #endregion
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
