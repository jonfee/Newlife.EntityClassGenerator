using DesignToEntityFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignToEntityFactory.EnumClassResolve
{
    /// <summary>
    /// 枚举描述解析为枚举类处理上下文
    /// </summary>
    public class EnumClassResolveContext
    {
        private EnumDesc _enumDesc;

        /// <summary>
        /// 枚举内容描述
        /// </summary>
        public EnumDesc EnumDesc
        {
            get
            {
                return _enumDesc;
            }
            set
            {
                this._enumDesc = value;
            }
        }

        private string _outputEnumClassContent;

        /// <summary>
        /// 输出的枚举类文件内容
        /// </summary>
        public string OutputEnumClassContent
        {
            get
            {
                return _outputEnumClassContent;
            }
            set
            {
                this._outputEnumClassContent = value;
            }
        }

        /// <summary>
        /// 初始化<see cref="EnumClassResolveContext"/>实例
        /// </summary>
        /// <param name="enumTemplate">枚举类文件模板</param>
        /// <param name="enumDesc">枚举描述对象</param>
        public EnumClassResolveContext(string enumTemplate, EnumDesc enumDesc)
        {
            this._outputEnumClassContent = enumTemplate;
            this._enumDesc = enumDesc;
        }
    }
}
