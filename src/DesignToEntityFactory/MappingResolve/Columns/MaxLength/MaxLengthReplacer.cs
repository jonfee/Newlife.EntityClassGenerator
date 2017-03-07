namespace DesignToEntityFactory.MappingResolve.Columns.MaxLength
{
    /// <summary>
    /// 长度限制约定内容替换者
    /// </summary>
    public class MaxLengthReplacer : MaxLengthHandlder
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="template"></param>
        /// <param name="dbtype"></param>
        /// <param name="maxlength"></param>
        /// <returns></returns>
        public override string Resolve(string template, string dbtype, int maxlength)
        {
            //限制长度 <1 时，替换为空，说明不需要特殊约定
            if (maxlength < 1) return "";

            //替换模板中的{Max_Length}占位，最大长度
            return template.Replace("{Max_Length}", maxlength.ToString());
        }
    }
}
