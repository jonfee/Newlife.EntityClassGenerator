using System;
using System.Configuration;

namespace DesignToEntityFactory.Core
{
    /// <summary>
    /// 模板配置信息
    /// </summary>
    public class Configs
    {
        /// <summary>
        /// 程序集基目录的路径名（已移除尾部的“\”）
        /// </summary>
        public static string BaseDirectory = AppContext.BaseDirectory.TrimEnd('\\');

        /// <summary>
        /// 默认模块名称
        /// </summary>
        public static string DefaultModuleName = ConfigurationManager.AppSettings["DefaultModuleName"];

        /// <summary>
        /// 实体类模板路径
        /// </summary>
        public static string EntityTemplatePath = BaseDirectory + ConfigurationManager.AppSettings["EntityTemplatePath"];

        /// <summary>
        /// 枚举类模板路径
        /// </summary>
        public static string EnumTemplatePath = BaseDirectory + ConfigurationManager.AppSettings["EnumTemplatePath"];

        /// <summary>
        /// Mapping类模板路径
        /// </summary>
        public static string MappingTemplatePath = BaseDirectory + ConfigurationManager.AppSettings["MappingTemplatePath"];
    }
}
