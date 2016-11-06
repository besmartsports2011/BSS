using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BSS_WebAPI.EntityAutoMapper
{
    public class MyUpperUnderscoreNamingConvention : INamingConvention
    {
        private readonly Regex _splittingExpression = new Regex(@"[\p{Lu}0-9]+(?=_?)");

        public Regex SplittingExpression { get { return _splittingExpression; } }

        public string SeparatorCharacter { get { return "_"; } }
    }
}