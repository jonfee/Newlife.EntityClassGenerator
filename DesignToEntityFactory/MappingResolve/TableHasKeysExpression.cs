using DesignToEntityFactory.MappingResolve.Keys;
using System.Linq;
using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve
{
    /// <summary>
    /// 针对数据表主键设置的解释器
    /// </summary>
    public class TableHasKeysExpression : MappingExpression
    {
        public override void Interpret(MappingResolveContext context)
        {
            if (context == null || context.Table == null) return;

            Regex regex = new Regex(@"<If_PrimaryKeys_None>(?<keysnone>((?!<ElseIf_PrimaryKeys_One>).|\n)*)<ElseIf_PrimaryKeys_One>(?<keysone>((?!<ElseIf_PrimaryKeys_More>).|\n)*)<ElseIf_PrimaryKeys_More>(?<keysmore>((?!</EndIf>).|\n)*)</EndIf>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output;

            Match match = regex.Match(text);

            var primaryKeys = context.Table.Columns.Where(p => p.IsPrimaryKey == true).ToList();

            int keysCount = primaryKeys.Count();

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

            context.Output = regex.Replace(text, replaceChars);
        }
    }
}
