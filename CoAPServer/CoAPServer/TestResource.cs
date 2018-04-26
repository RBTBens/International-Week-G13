using CoAP.Server.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoAPServer
{
    class TestResource : Resource
    {
        public TestResource(String name)
            : base(name)
        {
            Attributes.Title = "Gets a test string";
            Attributes.AddResourceType("TestDisplay");
        }

        protected override void DoGet(CoapExchange exchange)
        {
            exchange.Respond("Hello World!");
        }

        protected override void DoPost(CoapExchange exchange)
        {
            string payload = exchange.Request.PayloadString;
            Console.WriteLine("Post payload was: " + payload);
            exchange.Respond("Received!");

            //String payload = exchange.Request.PayloadString;
            //if (payload == null)
            //    payload = String.Empty;
            //String[] parts = payload.Split('\\');
            //String[] path = parts[0].Split('/');
            //IResource resource = Create(new LinkedList<String>(path));

            //Response response = new Response(StatusCode.Created);
            //response.LocationPath = resource.Uri;
            //exchange.Respond(response);
        }
    }
}
