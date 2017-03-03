using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EnumResolve.Option
{
    /// <summary>
    /// 枚举类成员常量值解释器
    /// 针对“{Enum_OptionValue}”的解析
    /// </summary>
    public class EnumOptionValueExpression : EnumClassOptionExpression
    {
        public override void Interpret(EnumOptionResolveContext context)
        {
            if (context == null || context.Option == null) return;

            Regex regex = new Regex(@"\{Enum_OptionValue\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output; ;

            context.Output = regex.Replace(text, context.Option.Value.ToString());
        }
    }
}
