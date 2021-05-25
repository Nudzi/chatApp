using chatApp.WebAPI.Services;
using chatApp.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace chatApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypesController : ControllerBase
    {
        private readonly IUserTypesService _service;

        public UserTypesController(IUserTypesService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<chatModel.UserTypes> Get()
        {
            return _service.Get();
        }
        [HttpGet("{id}")]
        public chatModel.UserTypes GetById(int id)
        {
            return _service.GetById(id);
        }
        [HttpGet]
        [Route("isAdmin/{userTypeId}")]
        public chatModel.UserTypes isAdmin(int userTypeId)
        {
            return _service.isAdmin(userTypeId);
        }

    }
}
