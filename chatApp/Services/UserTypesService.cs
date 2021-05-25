using AutoMapper;
using chatApp.Database;
using System.Collections.Generic;
using System.Linq;

namespace chatApp.WebAPI.Services
{
    public class UserTypesService : IUserTypesService
    {
        private readonly chatContext _context;
        private readonly IMapper _mapper;

        public UserTypesService(chatContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public chatModel.UserTypes GetById(int id)
        {
            var entity = _context.UserTypes.Find(id);

            return _mapper.Map<chatModel.UserTypes>(entity);
        }
        public List<chatModel.UserTypes> Get()
        {
            List<chatModel.UserTypes> result = new List<chatModel.UserTypes>();
            var lista = _context.UserTypes.ToList();

            foreach (var item in lista)
            {
                chatModel.UserTypes userTypes = new chatModel.UserTypes();
                userTypes.Name = item.Name;
                userTypes.Description = item.Description;
                userTypes.Id = item.Id;
                result.Add(userTypes);
            }
            return result;
        }

        public chatModel.UserTypes isAdmin(int userTypesId)
        {
            var lista = _context.UserTypes.ToList();
            chatModel.UserTypes result = new chatModel.UserTypes();

            foreach (var item in lista)
            {
                if (item.Id == userTypesId)
                {
                    if (item.Name.Contains("Admin"))
                    {
                        result.Name = item.Name;
                        result.Description = item.Description;
                        result.Id = item.Id;

                        return result;
                    }

                }
            }
            return null;
        }
    }
}