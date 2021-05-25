using AutoMapper;
using chatApp.Database;
using System.Collections.Generic;
using System.Linq;

namespace chatApp.WebAPI.Services
{
    public class BaseService<T, TSearch, TDb> : IService<T, TSearch> where TDb:class
    {
        protected chatContext _context;
        protected IMapper _mapper;
        public BaseService(chatContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public virtual IList<T> Get(TSearch request = default)
        {
            var list = _context.Set<TDb>().ToList();
            return _mapper.Map<IList<T>>(list);
        }
        public virtual T GetById(int id)
        {
            var entity = _context.Set<TDb>().Find(id);
            return _mapper.Map<T>(entity);
        }
    }
}
