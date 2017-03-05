using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DesignToEntityFactory.MappingResolve
{
    /// <summary>
    /// Mapping类字段解析器
    /// </summary>
    public class MappingColumnsExpression : MappingExpression
    {
        public override void Interpret(MappingResolveContext context)
        {
            if (context == null || context.Table == null) return;

            Regex regex = new Regex(@"<Mapping_Columns>(?<template>((?!</Mapping_Columns>).|\n)*)</Mapping_Columns>", RegexOptions.IgnoreCase | RegexOptions.Compiled);


        }
    }
}
