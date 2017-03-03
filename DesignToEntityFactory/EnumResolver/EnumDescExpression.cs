using System.Configuration;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EnumResolver
{
    /// <summary>
    /// 枚举描述文法解释器
    /// </summary>
    public class EnumDescExpression : EnumExpression
    {
        public override void Interpret(EnumDescContext context)
        {
            if (context == null || string.IsNullOrWhiteSpace(context.EnumHtml)) return;

            //枚举描述的匹配正则式
            Regex regex = new Regex(@"<(?<HxTag>h\d+)\s+[^>]*id=""枚举[^>]+>\s*\[枚举\]\s*(?<desc>[^\(（]+)[\(（](?<name>[^\）)]+)[\)）]</\k<HxTag>>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match match = regex.Match(context.EnumHtml);

            string name = match.Groups["name"].Value;
            int splitIndex = name.IndexOf("_");
            string module = splitIndex > 0 ? name.Substring(0, splitIndex) : name;
            if (string.IsNullOrWhiteSpace(name)) name = ConfigurationManager.AppSettings["DefaultModuleName"];

            context.EnumDesc.Name = name.Replace("_", "");                     //枚举名称
            context.EnumDesc.Description = match.Groups["desc"].Value;         //枚举说明
            context.EnumDesc.Module = module;                                  //所属模块
        }
    }
}
