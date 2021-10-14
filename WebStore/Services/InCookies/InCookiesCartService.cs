using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Services.InCookies
{
    public class InCookiesCartService : ICartService
    {
        private readonly IProductData _productData;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _CartName; //Название cookies, в которой хранится корзина

        private Cart Cart
        {
            get
            {
                //Десериализация из cookies
                var context = _httpContextAccessor.HttpContext;

                //Извлекаем коллекцию cookies
                var cookies = context!.Response.Cookies;

                //Извлекаем нужную нам cookies
                var cart_cookies = context.Request.Cookies[_CartName];
                if (cart_cookies == null)
                {
                    // Создаем новую корзину
                    var cart = new Cart();
                    cookies.Append(_CartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                // По сути переписываем cookies из request в responce
                ReplaceCart(cookies, cart_cookies);
                return JsonConvert.DeserializeObject<Cart>(cart_cookies);
            }
            set
            {
                //Сериализация полученного объекта корзины в cookies
                ReplaceCart(_httpContextAccessor.HttpContext!.Response.Cookies, JsonConvert.SerializeObject(value));
            }
        }

        private void ReplaceCart(IResponseCookies cookies, string cart)
        {
            // Удаляем старую корзину
            cookies.Delete(_CartName);

            //Добавляем новую
            cookies.Append(_CartName, cart);
        }

        public InCookiesCartService(IHttpContextAccessor httpContextAccessor, IProductData productData)
        {
            _productData = productData;
            _httpContextAccessor = httpContextAccessor;

            var user = _httpContextAccessor.HttpContext!.User;
            var userName = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;

            _CartName = $"GB.WebStore.Cart{userName}";
        }

        public void Add(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item == null)
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            else
                item.Quantity++;

            Cart = cart;
        }

        public void Decrement(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item == null) return;

            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity <= 0)
                cart.Items.Remove(item);

            Cart = cart;
        }

        public void Remove(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item == null) return;

            cart.Items.Remove(item);

            Cart = cart;
        }

        public void Clear()
        {
            var cart = Cart;

            cart.Items.Clear();

            Cart = cart;
        }
        public CartViewModel GetViewModel()
        {
            return null;
        }

    }
}
