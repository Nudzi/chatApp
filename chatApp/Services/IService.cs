using System.Collections.Generic;

namespace chatApp.WebAPI.Services
{
    public interface IService<T, TSearch>
    {
        IList<T> Get(TSearch search = default(TSearch));
        T GetById(int id);
    }
}
