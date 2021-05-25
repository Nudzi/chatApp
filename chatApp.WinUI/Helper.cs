using System;

namespace chatApp.WinUI
{
    public class Helper
    {
        private static Random random = new Random();
        public static string Alphabet =
"abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/=-(*)&^%$#@!";
        private static int generateSize = 19;
        private static APIService _moviesService = new APIService("movies");
        private static APIService _ordersService = new APIService("orders");
        private static APIService _ordersMoviesService = new APIService("ordersMovies");
        private static APIService _inputsMoviesService = new APIService("inputMovies");
        private static APIService _inputsService = new APIService("inputs");
        private static APIService _moviesTypesService = new APIService("movieTypes");
        public static string GenerateString(int size)
        {
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = Alphabet[random.Next(Alphabet.Length)];
            }
            return new string(chars);
        }
        //public static async Task CalculateInputProductsPrice(List<InputProductsAdd> _productsAdd)
        //{
        //    foreach (var item in _productsAdd)
        //    {
        //        var tmp = await _productsService.GetById<chatModel.Movies>(item.ProductId);
        //        item.Price = tmp.Price * item.Quantity;
        //    }
        //}

    }
}
