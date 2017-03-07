using DesignToEntityFactory.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EnumResolve.Data
{
    /// <summary>
    /// 枚举成员集合解释器
    /// </summary>
    public class EnumOptionsExpression : EnumExpression
    {
        public override void Interpret(EnumDescContext context)
        {
            if (context == null || string.IsNullOrWhiteSpace(context.EnumHtml)) return;

            //枚举成员的匹配正则式
            Regex regex = new Regex(@"<tr>[\r\n\s]*<td>(?<alias>((?!</td>).|\n)*)</td>[\r\n\s]*<td>(?<name>((?!</td>).|\n)*)</td>[\r\n\s]*<td>(?<desc>((?!</td>).|\n)*)</td>[\r\n\s]*</tr>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

            MatchCollection mc = regex.Matches(context.EnumHtml);

            List<EnumOption> optionList = new List<EnumOption>();

            //当前2的次幂数
            int index = 0;

            //遍历枚举成员处理
            foreach (Match m in mc)
            {
                EnumOption option = new EnumOption();

                option.Name = m.Groups["name"].Value.Trim();
                option.Alias = m.Groups["alias"].Value.Trim();
                option.Description = m.Groups["desc"].Value.Trim();
                option.Value = (int)Math.Pow(2, index++);

                optionList.Add(option);
            }

            context.EnumDesc.Options = optionList;
        }
    }
}
