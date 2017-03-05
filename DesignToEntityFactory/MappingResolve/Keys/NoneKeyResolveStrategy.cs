using DesignToEntityFactory.Models;
using System.Collections.Generic;

namespace DesignToEntityFactory.MappingResolve.Keys
{
    /// <summary>
    /// 无主键时的解析策略
    /// 返回模板本身
    /// </summary>
    public class NoneKeyResolveStrategy : KeysResolveStrategy
    {
        public override string Execute(string template, List<TableColumn> columns)
        {
            return template;
        }
    }
}
