using DesignToEntityFactory.Models;
using System.Collections.Generic;
using System.Linq;

namespace DesignToEntityFactory.MappingResolve.Keys
{
    /// <summary>
    /// 一个主键时的解析策略
    ///  针对“Primary_Key_Name”的解析
    /// </summary>
    public class OneKeyResolveStrategy : KeysResolveStrategy
    {
        public override string Execute(string template, List<TableColumn> columns)
        {
            string result = "";

            if (columns == null || columns.Count < 1)
            {
                result = template;
            }
            else if (!string.IsNullOrWhiteSpace(template))
            {
                result = template.Replace(@"{Primary_Key_Name}", columns.FirstOrDefault().Name);
            }

            return result;
        }
    }
}
