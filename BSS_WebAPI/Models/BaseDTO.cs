using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSS_WebAPI.Models
{
    public class BaseDTO
    {
        public IDictionary<string, string> Links { get; } = new Dictionary<string, string>();
    }
}