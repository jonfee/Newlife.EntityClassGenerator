namespace DesignToEntityFactory.MappingResolve.Columns.Precision
{
    /// <summary>
    /// 精度替换者
    /// </summary>
    public class PrecisionReplacer : PrecisionHandler
    {
        public override string Resolve(string template, string dbtype, int totalBit, int decimalBit)
        {
            if (totalBit <= 0) totalBit = 16;
            if (decimalBit <= 0) decimalBit = 4;

            //替换模板中的{Total_Bit}占位，总位数
            string result = template.Replace("{Total_Bit}", totalBit.ToString());
            //替换模板中的{Decimal_Bit}占位，小数位数
            result = result.Replace("{Decimal_Bit}", decimalBit.ToString());

            return result;
        }
    }
}
