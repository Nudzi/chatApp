using chatApp.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatApp.WebAPI.Controllers
{

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    //BaseController beacuse BaseCRUDController has IService from BaseService and it is in BaseCRUDService
    //and BaseCRUDController is from BaseCRUDService
    public class BaseCRUDController<T, TSearch, TInsert, TUpdate> : BaseController<T, TSearch>
    {
        //1. always service
        private readonly ICRUDService<T, TSearch, TInsert, TUpdate> _service;

        //2.constructor, base is from child controller
        public BaseCRUDController(ICRUDService<T, TSearch, TInsert, TUpdate> service) : base(service)
        {
            _service = service;
        }
        [HttpPost]
        public virtual T Insert(TInsert request)
        {
            return _service.Insert(request);
        }
        [HttpPut("{id}")]
        public virtual T Update(int id, [FromBody]TUpdate request)
        {
            return _service.Update(id, request);
        }
    }
}