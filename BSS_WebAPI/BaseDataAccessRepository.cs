using AutoMapper;
using BSS_WebAPI.Models.DTO;
using Microsoft.Practices.Unity;
using BSS_DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace BSS_WebAPI
{
    public class BaseDataAccessRepository 
    {
        [Dependency]
        public BeSmartSportsEntities Context { get; set; }

      
        public IEnumerable<Y> GetXByFK<X, Y>(string xName, string fkName, string fkValue)
        {
            var sql = string.Format(@"Select * From [{0}] WHERE [{1}] = {2}", xName, fkName, fkValue);
 
            var entityList = Context.Database.SqlQuery<X>(sql).ToList();
            var dtoList = entityList.Select(Mapper.Map<X, Y>).ToList();
            return dtoList;
        }
    }
}
