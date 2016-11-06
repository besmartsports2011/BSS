using System.Collections.Generic;

namespace BSS_WebAPI
{
    public interface IDataAccessRepository<TEntity, in TPrimaryKey> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        TEntity Get(TPrimaryKey id);
        void Post(TEntity entity);
        void Put(TPrimaryKey id, TEntity entity);
        void Delete(TPrimaryKey id);

        IEnumerable<Y> GetXByFK<X, Y>(string xName, string fkName, string fkValue);
    }
}
