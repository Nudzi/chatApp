using System.Collections.Generic;

namespace chatApp.WebAPI.Services
{
    public interface IUserTypesService
    {
        List<chatModel.UserTypes> Get();
        chatModel.UserTypes GetById(int id);
        chatModel.UserTypes isAdmin(int UlogaId);
    }
}
