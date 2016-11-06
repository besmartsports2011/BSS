using AutoMapper;
using System;

namespace BSS_WebAPI.EntityAutoMapper
{

    //   .ForMember(cv => cv.DateOfBirth, m => m.AddFormatter<DateFormatter>());


    // .ForMember(ev => ev.StartDate, m => m.AddFormatter<DateFormatter>());
    public class DateFormatter : IValueFormatter
    {
        public string FormatValue(ResolutionContext context)
        {
            return ((DateTime)context.SourceValue).ToLongDateString();
        }
    }

}
