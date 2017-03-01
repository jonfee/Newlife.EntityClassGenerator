using DesignToEntityFactory.Models;

namespace DesignToEntityFactory.PropertyResolver
{
    /// <summary>
    /// 字段属性解释处理上下文
    /// </summary>
    public class PropertyResolverContext
    {
        private TableColumn _column;
        /// <summary>
        /// 表字段属性
        /// </summary>
        public TableColumn Column
        {
            get { return _column; }
            set { this._column = value; }
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
        /// 实例化<see cref="PropertyResolverContext"/>实例
        /// </summary>
        /// <param name="column"></param>
        /// <param name="propertyTemplate"></param>
        public PropertyResolverContext(TableColumn column,string propertyTemplate)
        {
            _column = column;
            _output = propertyTemplate;
        }
    }
}
