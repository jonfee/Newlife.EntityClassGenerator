using DesignToEntityFactory.Models;
using DesignToEntityFactory.TableResolve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignToEntityFactory.Factory
{
    /// <summary>
    /// Mapping类文件生成工厂类
    /// </summary>
    public class MappingFactory : FileFactory
    {
        /// <summary>
        /// 数据表集合
        /// </summary>
        private List<TableDesc> _tables;

        #region 初始化

        /// <summary>
        /// 初始化<see cref="MappingFactory"/>实例
        /// </summary>
        /// <param name="sourceHtml">源文件HTML内容</param>
        public MappingFactory(string sourceHtml) : this(sourceHtml, null)
        {

        }

        /// <summary>
        /// 初始化<see cref="MappingFactory"/>实例
        /// </summary>
        /// <param name="sourceHtml">源文件HTML内容</param>
        /// <param name="tables">数据表集合</param>
        public MappingFactory(string sourceHtml, List<TableDesc> tables) : base(sourceHtml, "Mappings")
        {
            _tables = tables;
        }

        #endregion

        public override void GenerateFiles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 从源HTML中解析数据到队列
        /// </summary>
        public override void ResolveDataInQueue()
        {
            if (_tables == null)
                _tables = TableHelper.ResolveTables(SourceHtml);

            foreach (var tb in _tables)
            {
                //将解释出来的TableDesc加入数据队列
                DataQueue.Enqueue(tb);
            }
        }
    }
}
