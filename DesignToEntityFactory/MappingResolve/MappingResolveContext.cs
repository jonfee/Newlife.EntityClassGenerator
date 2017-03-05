using DesignToEntityFactory.Models;

namespace DesignToEntityFactory.MappingResolve
{
    /// <summary>
    /// Mapping类解析处理上下文
    /// </summary>
    public class MappingResolveContext
    {
        private TableDesc _table;
        /// <summary>
        /// 待解析的<see cref="TableDesc"/>对象
        /// </summary>
        public TableDesc Table
        {
            get { return _table; }
            set { this._table = value; }
        }

        private string _output;
        /// <summary>
        /// 解析后待输出的类文件内容
        /// </summary>
        public string Output
        {
            get { return _output; }
            set { this._output = value; }
        }

        /// <summary>
        /// 实始化<see cref="MappingResolveContext"/>实例
        /// </summary>
        /// <param name="template">Mapping类文件模板</param>
        /// <param name="table">数据表</param>
        public MappingResolveContext(string template,TableDesc table)
        {
            this._output = template;
            this._table = table;
        }
    }
}
