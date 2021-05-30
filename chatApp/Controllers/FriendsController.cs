using chatApp.WebAPI.Services;
using chatModel;
using chatModel.Requests.Friends;

namespace chatApp.WebAPI.Controllers
{
    public class FriendsController : BaseCRUDController<Friends, FriendsSearchRequest, FriendsUpsertRequest, FriendsUpsertRequest>
    {
        public FriendsController(ICRUDService<Friends, FriendsSearchRequest, FriendsUpsertRequest, FriendsUpsertRequest> service) : base(service)
        {

        }
    }
}
