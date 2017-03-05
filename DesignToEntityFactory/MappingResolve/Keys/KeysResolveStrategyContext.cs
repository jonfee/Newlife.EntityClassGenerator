using DesignToEntityFactory.Models;
using System.Collections.Generic;

namespace DesignToEntityFactory.MappingResolve.Keys
{
    /// <summary>
    /// 主键解析策略上下文
    /// </summary>
    public class KeysResolveStrategyContext
    {
        /// <summary>
        /// 解析策略
        /// </summary>
        KeysResolveStrategy strategy;

        public KeysResolveStrategyContext(int keysCount)
        {
            if (keysCount < 1)
            {
                strategy = new NoneKeyResolveStrategy();
            }
            else if (keysCount == 1)
            {
                strategy = new OneKeyResolveStrategy();
            }else
            {
                strategy = new MoreKeysResolveStrategy();
            }
        }

        /// <summary>
        /// 执行解析策略
        /// 返回解析后的内容字符串
        /// </summary>
        /// <param name="template">解析依赖的原模板</param>
        /// <param name="columns">数据表主键字段<see cref="List{TableColumn}"/>集合</param>
        /// <returns></returns>
        public string Execute(string template,List<TableColumn> colulmns)
        {
            string result = string.Empty;

            if (strategy != null)
            {
                result = strategy.Execute(template, colulmns);
            }

            return result;
        }
    }
}
