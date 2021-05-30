using System;

namespace chatApp.Mobile
{
    public class Helper
    {
        private static Random random = new Random();
        public static string Alphabet =
"abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/=-(*)&^%$#@!";
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
        //        var tmp = await _productsService.GetById<Model.Products>(item.ProductId);
        //        item.Price = tmp.Price * item.Quantity;
        //    }
        //}

    }
}