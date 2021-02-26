using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class SessionCart : Cart
    {
       /* this method is a factory for creating SessionCart objects and providing them with an
        * ISession object so they can store themselves */
        public static Cart GetCart(IServiceProvider service)
        {
            /* we obtain an instance of the IHttpContextAccessor service which provides us 
             * with access to HttpContext object that inturn provide us the ISession because
             ISession is not provided as a regular service*/
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart")
                ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        // this attr prevent a property from serialize or deserialize
        [JsonIgnore]
        public ISession Session { get; set; }

        /* this class method call the base implementations and then store the updated state
        * in the session using the extension methods on the ISession interface */
        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart",this);
        }
        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }

    }
}
