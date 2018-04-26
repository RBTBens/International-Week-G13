using System;
using CoAP.Server.Resources;
using Newtonsoft.Json.Linq;

namespace CoAPServer.DatabaseResources
{
    class TagsResource : Resource
    {
        private Database database;

        public TagsResource(String name, Database database)
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
            database.AddScan(payload);
            exchange.Respond("OK");
        }
    }
}
