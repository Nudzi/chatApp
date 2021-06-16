using AutoMapper;
using chatApp.Database;
using chatModel.Requests.Feedbacks;
using System.Collections.Generic;
using System.Linq;

namespace chatApp.WebAPI.Services
{
    public class FeedbacksService : BaseCRUDService<chatModel.Feedbacks, FeedbacksSearchRequest, FeedbacksUpsertRequest, FeedbacksUpsertRequest, Feedbacks>
    {
        public FeedbacksService(chatContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public override IList<chatModel.Feedbacks> Get(FeedbacksSearchRequest request)
        {
            var query = _context.Set<Feedbacks>().AsQueryable();
            if (request?.UserId.HasValue == true)
            {
                query = query.Where(x => x.UserId == request.UserId);
            }
            if (request?.ReportedUserId.HasValue == true)
            {
                query = query.Where(x => x.ReportedUserId == request.ReportedUserId);
            }
            var list = query.ToList();

            return _mapper.Map<List<chatModel.Feedbacks>>(list);
        }
    }
}
