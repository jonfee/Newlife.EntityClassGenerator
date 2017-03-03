using DesignToEntityFactory.Models;

namespace DesignToEntityFactory.EntityClassResolve
{
    /// <summary>
    /// 数据表描述解析为实体类处理上下文
    /// </summary>
    public class EntityClassResolveContext
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

        private string _outputEntityClassContent;

        /// <summary>
        /// 输出的实体类文件内容
        /// </summary>
        public string OutputEntityClassContent
        {
            get
            {
                return _outputEntityClassContent;
            }
            set
            {
                this._outputEntityClassContent = value;
            }
        }

        /// <summary>
        /// 初始化<see cref="EntityClassResolverContext"/>实例
        /// </summary>
        /// <param name="entityTemplate">实体类文件模板</param>
        /// <param name="tableDesc">数据表描述对象</param>
        public EntityClassResolveContext(string entityTemplate,TableDesc tableDesc)
        {
            this._outputEntityClassContent = entityTemplate;
            this._tableDesc = tableDesc;
        }
    }
}
