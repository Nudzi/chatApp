using chatModel.Requests.Users;
using System.Collections.Generic;

namespace chatApp.WebAPI.Services
{
    public interface IUsersService
    {
        List<chatModel.Users> Get(UsersSearchRequest request);
        chatModel.Users GetById(int id);
        chatModel.Users Insert(UsersInsertRequest request);
        chatModel.Users Update(int id, UsersInsertRequest request);
        chatModel.Users Authentication(string username, string pass);
    }
}
