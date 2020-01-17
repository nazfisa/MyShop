using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BusketServices
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;
        public const string basketSession = "eCommerceBasket";
        public BusketServices(IRepository<Product> ProductContext,IRepository<Basket> BasketContext)
        {
            this.basketContext = BasketContext;
            this.productContext = productContext;
        }
        private Basket GetBasket(HttpContextBase httpContext, bool creatIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(basketSession);
            Basket basket = new Basket();
            if(cookie !=null)
            {
                string busketId = cookie.Value;
                if (!string.IsNullOrEmpty(busketId))
                {
                    basket = basketContext.Find(busketId);
                }
                else
                {
                    if (creatIfNull)
                    {
                        basket = CreateNewBusket(httpContext);
                    }
                }
            }
            else
            {
                if (creatIfNull)
                {
                    basket = CreateNewBusket(httpContext);
                }
            }
        }
        private Basket CreateNewBusket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();
            HttpCookie cookie = new HttpCookie(basketSession);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);
            return basket;
        }
        public void AddToBusket(HttpContextBase httpContext,string productId)
        {
            Basket basket = GetBasket(httpContext,true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);
            if(item== null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = productId,
                    Quantity = 1
                };

            }
            else
            {
                item.Quantity += 1;
            }
            basketContext.Commit();
        }
        public void REmoveFromBusket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }
            }
}
