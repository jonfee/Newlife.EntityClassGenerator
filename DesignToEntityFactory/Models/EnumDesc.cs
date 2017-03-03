using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignToEntityFactory.Models
{
    /// <summary>
    /// 枚举信息描述
    /// </summary>
    public class EnumDesc : FactoryModel
    {
        /// <summary>
        /// 枚举名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 枚举说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 所属模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 枚举成员集合
        /// </summary>
        public List<EnumOption> Options { get; set; }

        /// <summary>
        /// 索引器-获取指定名称的字段信息
        /// </summary>
        /// <param name="optionName"></param>
        /// <returns></returns>
        public EnumOption this[string optionName]
        {
            get
            {
                if (Options == null) return null;

                return Options.FirstOrDefault(p => p.Name.Equals(optionName, StringComparison.OrdinalIgnoreCase));
            }
        }

        /// <summary>
        /// 追加枚举成员
        /// 当已存在同名成员名时，则更新成员信息
        /// </summary>
        /// <param name="option"></param>
        public void AppendOption(EnumOption option)
        {
            if (option == null) return;

            if (Options == null) Options = new List<EnumOption>();

            var item = this[option.Name];
            if (item == null)
            {
                Options.Add(option);
            }
            else
            {
                item.Alias = option.Alias;
                item.Value = option.Value;
                item.Description = option.Description;
            }
        }
    }

    /// <summary>
    /// 枚举成员信息
    /// </summary>
    public class EnumOption
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 常量值
        /// </summary>
        public int Value { get; set; }
    }
}
