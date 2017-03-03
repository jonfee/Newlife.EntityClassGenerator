using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EnumOptionResolve
{
    /// <summary>
    /// 枚举类成员常量值解释器
    /// 针对“{Enum_OptionAlias}”的解析
    /// </summary>
    public class EnumOptionAliasExpression : EnumClassOptionExpression
    {
        public override void Interpret(EnumOptionResolveContext context)
        {
            if (context == null || context.Option == null) return;

            Regex regex = new Regex(@"\{Enum_OptionAlias\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output; ;

            context.Output = regex.Replace(text, context.Option.Alias);
        }
    }
}
