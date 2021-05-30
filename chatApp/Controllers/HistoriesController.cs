using chatApp.WebAPI.Services;
using chatModel;
using chatModel.Requests.Histories;

namespace chatApp.WebAPI.Controllers
{
    public class HistoriesController : BaseCRUDController<Histories, HistoriesSearchRequest, HistoriesUpsertRequest, HistoriesUpsertRequest>
    {
        public HistoriesController(ICRUDService<Histories, HistoriesSearchRequest, HistoriesUpsertRequest, HistoriesUpsertRequest> service) : base(service)
        {

        }
    }
}
