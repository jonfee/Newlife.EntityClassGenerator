using DesignToEntityFactory.Models;

namespace DesignToEntityFactory.MappingResolve.Columns
{
    /// <summary>
    /// Mapping类字段配置解析上下文
    /// </summary>
    public class ColumnResolveContext
    {
        private string _output;
        /// <summary>
        /// 解析后输出的内容
        /// 将<paramref name="Column"/>对象用模板解析后的内容
        /// </summary>
        public string Output
        {
            get { return _output; }
            set { this._output = value; }
        }

        private TableColumn _column;
        /// <summary>
        /// 数据表字段<see cref="TableColumn"/>对象
        /// </summary>
        public TableColumn Column
        {
            get { return _column; }
            set { this._column = value; }
        }

        /// <summary>
        /// 初始化<see cref="ColumnResolveContext"/>对象
        /// </summary>
        /// <param name="template">字段解析的模板</param>
        /// <param name="column"><see cref="TableColumn"/>对象</param>
        public ColumnResolveContext(string template,TableColumn column)
        {
            this._output = template;
            _column = column;
        }
    }
}
