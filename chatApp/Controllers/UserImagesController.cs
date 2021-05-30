using chatApp.WebAPI.Services;
using chatModel;
using chatModel.Requests.UserImages;

namespace chatApp.WebAPI.Controllers
{
    public class UserImagesController : BaseCRUDController<UserImages, UserImagesSearchRequest, UserImagesUpsertRequest, UserImagesUpsertRequest>
    {
        public UserImagesController(ICRUDService<UserImages, UserImagesSearchRequest, UserImagesUpsertRequest, UserImagesUpsertRequest> service) : base(service)
        {

        }
    }
}
