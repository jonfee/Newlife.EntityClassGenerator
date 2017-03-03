using DesignToEntityFactory.Models;

namespace DesignToEntityFactory.EnumResolve.Option
{
    /// <summary>
    /// 枚举成员解析处理对象上下文
    /// </summary>
    public class EnumOptionResolveContext
    {
        private EnumOption _option;
        /// <summary>
        /// 枚举成员
        /// </summary>
        public EnumOption Option
        {
            get { return _option; }
            set { this._option = value; }
        }

        private string _output;
        /// <summary>
        /// 输出的对象属性内容
        /// </summary>
        public string Output
        {
            get { return _output; }
            set { this._output = value; }
        }

        /// <summary>
        /// 实例化<see cref="EnumOptionResolveContext"/>实例
        /// </summary>
        /// <param name="option">枚举成员对象</param>
        /// <param name="optionTemplate">枚举成员模板</param>
        public EnumOptionResolveContext(EnumOption option, string optionTemplate)
        {
            _option = option;
            _output = optionTemplate;
        }
    }
}
