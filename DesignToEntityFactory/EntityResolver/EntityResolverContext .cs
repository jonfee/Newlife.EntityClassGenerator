using DesignToEntityFactory.Models;

namespace DesignToEntityFactory.EntityResolver
{
    /// <summary>
    /// 数据表描述解析为实体类处理上下文
    /// </summary>
    public class EntityResolverContext
    {
        private TableDesc _tableDesc;

        /// <summary>
        /// 数据表内容描述
        /// </summary>
        public TableDesc TableDesc
        {
            get
            {
                return _tableDesc;
            }
            set
            {
                this._tableDesc = value;
            }
        }

        private string _outputEntityContent;

        /// <summary>
        /// 输出的实体类文件内容
        /// </summary>
        public string OutputEntityContent
        {
            get
            {
                return _outputEntityContent;
            }
            set
            {
                this._outputEntityContent = value;
            }
        }

        /// <summary>
        /// 初始化<see cref="EntityResolverContext"/>实例
        /// </summary>
        /// <param name="entityTemplate">实体类文件模板</param>
        /// <param name="tableDesc">数据表描述对象</param>
        public EntityResolverContext(string entityTemplate,TableDesc tableDesc)
        {
            this._outputEntityContent = entityTemplate;
            this._tableDesc = tableDesc;
        }
    }
}
