using System.Text.RegularExpressions;

namespace DesignToEntityFactory.MappingResolve
{
    /// <summary>
    /// Mapping中数据表架构解释器
    /// 针对“{Schema_Name}”的解析
    /// </summary>
    public class TableSchemaExpression : MappingExpression
    {
        public override void Interpret(MappingResolveContext context)
        {
            if (context == null || context.Table == null) return;

            Regex regex = new Regex(@"\{Schema_Name\}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string text = context.Output;

            string schema = context.Table.Schema;
            if (string.IsNullOrWhiteSpace(schema)) schema = "dbo";

            context.Output = regex.Replace(text, schema);
        }
    }
}
