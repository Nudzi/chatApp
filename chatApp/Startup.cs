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
using System;
using System.Net.Sockets;

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
            //Connect("192.168.100.96", "/Users/korisnik/Desktop/chair.png");
        }

        static void Connect(String server, String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 2222;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
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
