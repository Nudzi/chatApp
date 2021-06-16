using AutoMapper;
using chatApp.Database;
using chatModel.Requests.Feedbacks;
using chatModel.Requests.Friends;
using chatModel.Requests.Histories;
using chatModel.Requests.UserImages;
using chatModel.Requests.Users;

namespace chatApp.WebAPI.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            //users
            CreateMap<Users, chatModel.Users>();
            CreateMap<Users, UsersInsertRequest>().ReverseMap();

            //userTypes
            CreateMap<UserTypes, chatModel.UserTypes>();
            CreateMap<UserTypes, chatModel.UserTypes>().ReverseMap();

            //friends
            CreateMap<Friends, chatModel.Friends>();
            CreateMap<Friends, FriendsUpsertRequest>().ReverseMap();

            //friends
            CreateMap<UserImages, chatModel.UserImages>();
            CreateMap<UserImages, UserImagesUpsertRequest>().ReverseMap();

            //histories
            CreateMap<Histories, chatModel.Histories>();
            CreateMap<Histories, HistoriesUpsertRequest>().ReverseMap();

            //feedbacks
            CreateMap<Feedbacks, chatModel.Feedbacks>();
            CreateMap<Feedbacks, FeedbacksUpsertRequest>().ReverseMap();
        }
    }
}
