using DesignToEntityFactory.Models;

namespace DesignToEntityFactory.EntityResolve.PrimaryKey
{
    /// <summary>
    /// 主键属性解析处理上下文
    /// </summary>
    public class PrimarykeyResolverContext
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
        /// 实例化<see cref="PrimarykeyResolverContext"/>实例
        /// </summary>
        /// <param name="column"></param>
        /// <param name="template"></param>
        public PrimarykeyResolverContext(TableColumn column, string template)
        {
            _column = column;
            _output = template;
        }
    }
}
