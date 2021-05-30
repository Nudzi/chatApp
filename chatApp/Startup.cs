using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using chatApp.WebAPI.Services;
using chatApp.WebAPI.Services;
using chatApp.Database;
using Microsoft.AspNetCore.Authentication;
using chatApp.WebAPI.Security;
using Microsoft.OpenApi.Models;
using chatModel.Requests.Friends;
using chatModel.Requests.UserImages;
using chatModel.Requests.Histories;

namespace chatApp.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                        }
                });
            });
            services.AddAuthentication("BasicAuthentication")
               .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IUserTypesService, UserTypesService>();
            //services.AddScoped<IIngredientsIngredientTypesService, IngredientsIngredientTypesService>();
            //services.AddScoped<IWishlistsService, WishlistsService>();
            //services.AddScoped<IReviewsService, ReviewsService>();
            //services.AddScoped<IIngredientsService, IngredientsService>();
            //services.AddScoped<IStoragesService, StoragesService>();

            services.AddScoped<ICRUDService<chatModel.Friends, FriendsSearchRequest, FriendsUpsertRequest, FriendsUpsertRequest>, FriendsService>();
            services.AddScoped<ICRUDService<chatModel.UserImages, UserImagesSearchRequest, UserImagesUpsertRequest, UserImagesUpsertRequest>, UserImagesService>();
            services.AddScoped<ICRUDService<chatModel.Histories, HistoriesSearchRequest, HistoriesUpsertRequest, HistoriesUpsertRequest>, HistoriesService>();
            //services.AddScoped<ICRUDService<chatModel.Orders, OrdersSearchRequest, OrdersUpsertRequest, OrdersUpsertRequest>, OrdersService>();
            //services.AddScoped<ICRUDService<chatModel.InputProducts, InputProductsSearchRequest, InputProductsUpsertRequest, InputProductsUpsertRequest>, InputProductsService>();
            //services.AddScoped<ICRUDService<chatModel.OutputProducts, OutputProductsSearchRequest, OutputProductsUpsertRequest, OutputProductsUpsertRequest>, OutputProductsService>();
            //services.AddScoped<ICRUDService<chatModel.UserAddresses, UserAddressesSearchRequest, UserAddressesUpsertRequest, UserAddressesUpsertRequest>, UserAddressesService>();
            //services.AddScoped<ICRUDService<chatModel.Products, ProductsSearchRequest, ProductsUpsertRequest, ProductsUpsertRequest>, ProductsService>();
            //services.AddScoped<ICRUDService<chatModel.Inputs, InputsSearchRequest, InputsUpsertRequest, InputsUpsertRequest>, InputsService>();
            //services.AddScoped<ICRUDService<chatModel.Outputs, OutputsSearchRequest, OutputsUpsertRequest, OutputsUpsertRequest>, OutputsService>();
            //services.AddScoped<IRecommender, RecommenderService>();
            ////var connection = @"Server=.;Database=natureBeauty;Trusted_Connection=True;ConnectRetryCount=0";
            //var connection = "Data Source=.;Initail Catalog=natureBeauty;Integrated Security=True";
            var connection = Configuration.GetConnectionString("chatApp");
            services.AddDbContext<chatContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();


            app.UseSwagger();
            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");

            });

            //app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
