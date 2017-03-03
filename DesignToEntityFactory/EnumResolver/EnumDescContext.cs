using DesignToEntityFactory.Models;

namespace DesignToEntityFactory.EnumResolver
{
    /// <summary>
    /// 枚举解析处理上下文
    /// </summary>
    public class EnumDescContext
    {
        private string _enumHtml;
        /// <summary>
        /// 枚举HTMl
        /// </summary>
        public string EnumHtml
        {
            get { return _enumHtml; }
            private set { _enumHtml = value; }
        }

        private EnumDesc _enumDesc;
        /// <summary>
        /// Output-数据表描述
        /// </summary>
        public EnumDesc EnumDesc
        {
            get { return _enumDesc; }
            set { _enumDesc = value; }
        }

        /// <summary>
        /// 初始化<see cref="EnumDescContext"/>实例
        /// </summary>
        /// <param name="tableHtml">数据表HTML</param>
        public EnumDescContext(string enuMtml)
        {
            this._enumHtml = enuMtml;

            //默认初始化，避免为null时操作
            _enumDesc = new EnumDesc();
        }
    }
}
