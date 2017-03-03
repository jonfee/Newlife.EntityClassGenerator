using DesignToEntityFactory.Models;

namespace DesignToEntityFactory.TableResolver
{
    /// <summary>
    /// 数据表处理上下文
    /// </summary>
    public class TableDescContext
    {
        private string _tableHtml;
        /// <summary>
        /// 数据表HTMl
        /// </summary>
        public string TableHtml
        {
            get { return _tableHtml; }
            private set { _tableHtml = value; }
        }

        private TableDesc _table;
        /// <summary>
        /// Output-数据表描述
        /// </summary>
        public TableDesc Table
        {
            get { return _table; }
            set { _table = value; }
        }

        /// <summary>
        /// 初始化<see cref="TableDescContext"/>实例
        /// </summary>
        /// <param name="tableHtml">数据表HTML</param>
        public TableDescContext(string tableHtml)
        {
            this._tableHtml = tableHtml;

            //默认初始化，避免为null时操作
            _table = new TableDesc();
        }
    }
}
