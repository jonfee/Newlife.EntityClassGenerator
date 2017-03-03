using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EnumResolve.ClassFile
{
    /// <summary>
    /// 枚举类名称解释器
    /// 针对“{Enum_Name}”的解析
    /// </summary>
    public class EnumNameExpression : EnumClassExpression
    {
        public override void Interpret(EnumClassResolveContext context)
        {
            if (context == null || context.EnumDesc == null) return;

            Regex regex = new Regex(@"\{Enum_Name\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.OutputEnumClassContent; ;

            context.OutputEnumClassContent = regex.Replace(text, context.EnumDesc.Name);
        }
    }
}
