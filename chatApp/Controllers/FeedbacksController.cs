using chatApp.WebAPI.Services;
using chatModel;
using chatModel.Requests.Feedbacks;

namespace chatApp.WebAPI.Controllers
{
    public class FeedbacksController : BaseCRUDController<Feedbacks, FeedbacksSearchRequest, FeedbacksUpsertRequest, FeedbacksUpsertRequest>
    {
        public FeedbacksController(ICRUDService<Feedbacks, FeedbacksSearchRequest, FeedbacksUpsertRequest, FeedbacksUpsertRequest> service) : base(service)
        {

        }
    }
}
