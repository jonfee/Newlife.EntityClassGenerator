using System.Text.RegularExpressions;

namespace DesignToEntityFactory.EnumClassResolve
{
    /// <summary>
    /// 枚举类所属模块解释器
    /// 针对“{Module_Name}”的解析
    /// </summary>
    public class EnumModuleNameExpression : EnumClassExpression
    {
        public override void Interpret(EnumClassResolveContext context)
        {
            if (context == null || context.EnumDesc == null) return;

            Regex regex = new Regex(@"\{Module_Name\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.OutputEnumClassContent; ;

            context.OutputEnumClassContent = regex.Replace(text, context.EnumDesc.Module);
        }
        }
}
