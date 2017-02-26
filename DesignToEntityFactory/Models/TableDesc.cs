using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignToEntityFactory.Models
{
    /// <summary>
    /// 数据表描述
    /// </summary>
    public class TableDesc
    {
        /// <summary>
        /// 数据表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据表说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 数据架构名称
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// 所属模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 表字段集合
        /// </summary>
        public List<TableColumn> Columns { get; set; }

        /// <summary>
        /// 索引器-获取指定名称的字段信息
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public TableColumn this[string columnName]
        {
            get
            {
                if (Columns == null) return null;

                return Columns.FirstOrDefault(p => p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
            }
        }

        /// <summary>
        /// 追加字段
        /// 当已存在同名字段时，则更新字段属性信息
        /// </summary>
        /// <param name="column"></param>
        public void AppendColumn(TableColumn column)
        {
            if (column == null) return;

            if (Columns == null) Columns = new List<TableColumn>();

            var item = this[column.Name];
            if (item == null)
            {
                Columns.Add(column);
            }
            else
            {
                item.CanNullable = column.CanNullable;
                item.DataType = column.DataType;
                item.DefaultValue = column.DefaultValue;
                item.Description = column.Description;
            }
        }
    }

    /// <summary>
    /// 数据表字段
    /// </summary>
    public class TableColumn
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 默认值 
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool CanNullable { get; set; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }
    }
}
