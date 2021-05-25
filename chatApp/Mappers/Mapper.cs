using AutoMapper;
using chatApp.Database;
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

            ////ingredients
            //CreateMap<Ingredients, chatModel.Ingredients>();
            //CreateMap<IngredientTypes, chatModel.IngredientTypes>();
            //CreateMap<Ingredients, IngredientsUpsertRequest>().ReverseMap();
            //CreateMap<IngredientsIngredientTypes, chatModel.IngredientsIngredientTypes>().ReverseMap();

            ////products
            //CreateMap<Products, chatModel.Products>();
            //CreateMap<Products, ProductsUpsertRequest>().ReverseMap();
            //CreateMap<ProductTypes, chatModel.ProductTypes>();
            //CreateMap<ProductIngredients, chatModel.ProductsIngredients>();

            ////inputs
            //CreateMap<Inputs, chatModel.Inputs>();
            //CreateMap<Inputs, InputsUpsertRequest>().ReverseMap();
            //CreateMap<InputProducts, chatModel.InputProducts>();
            //CreateMap<InputProducts, InputProductsUpsertRequest>().ReverseMap();
            ////orders
            //CreateMap<Orders, chatModel.Orders>();
            //CreateMap<Orders, OrdersUpsertRequest>().ReverseMap();
            ////outputs
            //CreateMap<Outputs, chatModel.Outputs>();
            //CreateMap<OutputProducts, chatModel.OutputProducts>();
            //CreateMap<Outputs, OutputsUpsertRequest>().ReverseMap();
            //CreateMap<OutputProducts, OutputProductsUpsertRequest>().ReverseMap();

            ////units
            //CreateMap<Units, chatModel.Units>();

            ////productIngredients
            //CreateMap<ProductIngredients, chatModel.ProductsIngredients>();
            //CreateMap<ProductIngredients, ProductsIngredientsUpsertRequest>().ReverseMap();

            ////useraddresses
            //CreateMap<UserAddresses, chatModel.UserAddresses>();
            //CreateMap<UserAddresses, UserAddressesUpsertRequest>().ReverseMap();
            ////storages
            //CreateMap<Storages, StoragesUpsertRequest>().ReverseMap();
            //CreateMap<Storages, chatModel.Storages>();
            ////wishlists
            //CreateMap<Wishlists, chatModel.Wishlists>();
            //CreateMap<Wishlists, WishlistsUpsertRequest>().ReverseMap();
            ////reviews
            //CreateMap<Reviews, Reviews>();
            //CreateMap<Reviews, ReviewsUpsertRequest>().ReverseMap();
            ////storages
            //CreateMap<Storages, Storages>();

        }
    }
}
