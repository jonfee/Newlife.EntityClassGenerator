using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EnumResolve.Option
{
    /// <summary>
    /// 枚举类成员常量值解释器
    /// 针对“{Enum_OptionDescription}”的解析
    /// </summary>
    public class EnumOptionDescriptionExpression : EnumClassOptionExpression
    {
        public override void Interpret(EnumOptionResolveContext context)
        {
            if (context == null || context.Option == null) return;

            Regex regex = new Regex(@"\{Enum_OptionDescription\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output; ;

            context.Output = regex.Replace(text, context.Option.Description);
        }
    }
}
