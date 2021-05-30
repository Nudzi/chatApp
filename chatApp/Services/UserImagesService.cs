using AutoMapper;
using chatApp.Database;
using chatApp.WebAPI.Services;
using chatModel.Requests.UserImages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace chatApp.WebAPI.Services
{
    public class UserImagesService : BaseCRUDService<chatModel.UserImages, UserImagesSearchRequest, UserImagesUpsertRequest, UserImagesUpsertRequest, UserImages>
    {
        public UserImagesService(chatContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public override IList<chatModel.UserImages> Get(UserImagesSearchRequest request)
        {
            var query = _context.Set<UserImages>().AsQueryable();
            if (request?.UserId.HasValue == true)
            {
                query = query.Where(x => x.UserId == request.UserId);
            }
            var list = query.ToList();

            return _mapper.Map<List<chatModel.UserImages>>(list);
        }
    }
}
