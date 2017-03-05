using DesignToEntityFactory.MappingResolve.Keys;
using System.Linq;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve
{
    /// <summary>
    /// 针对数据表主键设置的解释器
    /// </summary>
    public class MappingHasKeysExpression : MappingExpression
    {
        public override void Interpret(MappingResolveContext context)
        {
            if (context == null || context.Table == null) return;

            Regex regex = new Regex(@"<If_PrimaryKeys_None>(?<keysnone>((?!<ElseIf_PrimaryKeys_One>).|\n)*)<ElseIf_PrimaryKeys_One>(?<keysone>((?!<ElseIf_PrimaryKeys_More>).|\n)*)<ElseIf_PrimaryKeys_More>(?<keysmore>((?!</EndIf>).|\n)*)</EndIf>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output;

            Match match = regex.Match(text);

            //主键集合
            var primaryKeys = context.Table.Columns.Where(p => p.IsPrimaryKey == true).ToList();

            //主键数量
            int keysCount = primaryKeys.Count();

            //主键生成的模板，根据主键数量不同，有不同的模板
            string template = "";

            if (keysCount == 0)
            {
                template = match.Groups["keysnone"].Value;
            }
            else if (keysCount == 1)
            {
                template = match.Groups["keysone"].Value;
            }
            else if (keysCount > 1)
            {
                template = match.Groups["keysmore"].Value;
            }

            //定义一个主键解析上下文
            KeysResolveStrategyContext resolveContext = new KeysResolveStrategyContext(keysCount);
            string replaceChars = resolveContext.Execute(template, primaryKeys);

            //输出主键的生成代码字符串内容
            context.Output = regex.Replace(text, replaceChars);
        }
    }
}
