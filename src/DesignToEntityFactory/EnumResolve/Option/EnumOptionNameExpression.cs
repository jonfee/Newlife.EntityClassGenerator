using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EnumResolve.Option
{
    /// <summary>
    /// 枚举类成员名称解释器
    /// 针对“{Enum_OptionName}”的解析
    /// </summary>
    public class EnumOptionNameExpression : EnumClassOptionExpression
    {
        public override void Interpret(EnumOptionResolveContext context)
        {
            if (context == null || context.Option == null) return;

            Regex regex = new Regex(@"\{Enum_OptionName\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output; ;

            context.Output = regex.Replace(text, context.Option.Name);
        }
    }
}
