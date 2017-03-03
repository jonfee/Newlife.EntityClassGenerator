using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EnumResolve.ClassFile
{
    /// <summary>
    /// 枚举类描述解释器
    /// 针对“{Enum_Description}”的解析
    /// </summary>
    public class EnumDescriptionExpression : EnumClassExpression
    {
        public override void Interpret(EnumClassResolveContext context)
        {
            if (context == null || context.EnumDesc == null) return;

            Regex regex = new Regex(@"\{Enum_Description\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.OutputEnumClassContent; ;

            context.OutputEnumClassContent = regex.Replace(text, context.EnumDesc.Description);
        }
    }
}
