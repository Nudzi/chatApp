using AutoMapper;
using chatApp.Database;
using chatModel.Requests.Histories;
using System.Collections.Generic;
using System.Linq;

namespace chatApp.WebAPI.Services
{
    public class HistoriesService : BaseCRUDService<chatModel.Histories, HistoriesSearchRequest, HistoriesUpsertRequest, HistoriesUpsertRequest, Histories>
    {
        public HistoriesService(chatContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public override IList<chatModel.Histories> Get(HistoriesSearchRequest request)
        {
            var query = _context.Set<Histories>().AsQueryable();
            if (request?.UserIdPrimary.HasValue == true)
            {
                query = query.Where(x => x.UserIdPrimary == request.UserIdPrimary);
            }
            if (request?.UserIdSecondary.HasValue == true)
            {
                query = query.Where(x => x.UserIdSecondary == request.UserIdSecondary);
            }
            if (!string.IsNullOrWhiteSpace(request?.Message))
            {
                query = query.Where(x => x.Message.StartsWith(request.Message));
            }
            if (request?.Status.HasValue == true)
            {
                query = query.Where(x => x.Status.Equals(request.Status));
            }
            query = query.OrderByDescending(x => x.ModifiedDate);
            var list = query.ToList();
            var sizedList = new List<Histories>();
            if (list.Count() <= request.ListSize || request.ListSize == null)
                sizedList = list;
            else
            {
                for (int i = 0; i < request.ListSize; i++)
                {
                    sizedList.Add(list[i]);
                }
            }
            return _mapper.Map<List<chatModel.Histories>>(sizedList);
        }
    }
}
