using AutoMapper;
using chatApp.Database;
using chatApp.WebAPI.Services;
using chatModel.Requests.Friends;
using System;
using System.Collections.Generic;
using System.Linq;

namespace chatApp.WebAPI.Services
{
    public class FriendsService : BaseCRUDService<chatModel.Friends, FriendsSearchRequest, FriendsUpsertRequest, FriendsUpsertRequest, Friends>
    {
        public FriendsService(chatContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public override IList<chatModel.Friends> Get(FriendsSearchRequest request)
        {
            var query = _context.Set<Friends>().AsQueryable();
            if (request?.UserIdprimary.HasValue == true)
            {
                query = query.Where(x => x.UserIdprimary == request.UserIdprimary);
            }
            if (request?.UserIdsecondary.HasValue == true)
            {
                query = query.Where(x => x.UserIdsecondary == request.UserIdsecondary);
            }
            var list = query.ToList();

            return _mapper.Map<List<chatModel.Friends>>(list);
        }
    }
}
