using System;
using CoAP.Server.Resources;
using Newtonsoft.Json.Linq;

namespace CoAPServer.DatabaseResources
{
    class ProductsResource : Resource
    {
        private Database database;

        public ProductsResource(String name, Database database)
            : base(name)
        {
            this.database = database;
        }

        protected override void DoGet(CoapExchange exchange)
        {
            database.GetProducts();
            exchange.Respond("Printed!");
        }

        protected override void DoPost(CoapExchange exchange)
        {
            string payload = exchange.Request.PayloadString;
            dynamic product = JObject.Parse(payload);
            string name = product.name;
            double price = product.price;

            database.AddProduct(name, price);

            exchange.Respond("Added!");
        }
    }
}
