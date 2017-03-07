using DesignToEntityFactory.Models;
using System.Collections.Generic;

namespace DesignToEntityFactory.MappingResolve.Keys
{
    /// <summary>
    /// 数据表主键解析策略
    /// </summary>
    public abstract class KeysResolveStrategy
    {
        /// <summary>
        /// 执行策略
        /// 返回解析后的内容字符串
        /// </summary>
        /// <param name="template">解析依赖的原模板</param>
        /// <param name="columns">数据表主键字段<see cref="List{TableColumn}"/>集合</param>
        /// <returns></returns>
        public abstract string Execute(string template, List<TableColumn> columns);
    }
}
