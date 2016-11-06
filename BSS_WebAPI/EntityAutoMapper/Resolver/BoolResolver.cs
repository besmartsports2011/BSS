using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSS_WebAPI.EntityAutoMapper
{

    // .ForMember(cv => cv.VIP, m => m.ResolveUsing<BoolResolver>().FromMember(x => x.Active))

    public class BoolYNResolver : ValueResolver<bool, string>
    {
        protected override string ResolveCore(bool source)
        {
            return source ? "Y" : "N";
        }
    }

    // .ForMember(x => x.IsYesNoFixedQuestions, opt => opt.ResolveUsing<BoolYesNoResolver>().FromMember(e => e.IsFixedQuestions));   
    public class BoolYesNoResolver : ValueResolver<bool, string>
    {
        protected override string ResolveCore(bool source)
        {
            return source ? "Yes" : "No";
        }
    }

    //  .ForMember(x => x.IsYesNoFixedQuestions, opt => opt.ResolveUsing<BoolYesNoResolver1>());
    //public class BoolYesNoResolver1 : ValueResolver<Exam, string>
    //{
    //    protected override string ResolveCore(Exam source)
    //    {
    //        return source.IsFixedQuestions ? "Yes" : "No";
    //    }
    //}

    public class BoolMaleFemaleResolver : ValueResolver<bool, string>
    {
        protected override string ResolveCore(bool source)
        {
            return source ? "Male" : "Female";
        }
    }


}
