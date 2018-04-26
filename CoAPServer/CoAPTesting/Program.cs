using CoAP;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoAPTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCall();
        }

        static void TestCall()
        {
            // Create a new client
            var client = new CoapClient();

            // Set the Uri to visit
            client.Uri = new Uri("coap://192.168.0.101:5683/test");

            // Get JSON object from example product
            var response = client.Get();

            // Get the response
            Console.WriteLine("Response from GET: {0}", response.PayloadString);
            Console.ReadLine();
        }

        static void ProductCall()
        {
            // Create a new client
            var client = new CoapClient();

            // Set the Uri to visit
            client.Uri = new Uri("coap://127.0.0.1:5683/products");

            // Build the data
            MemoryStream stream = new MemoryStream();

            // Example product
            dynamic product = new JObject();
            product.name = "Example Product";
            product.price = 120.35;

            // Get JSON object from example product
            var response = client.Post(product.ToString(), MediaType.ApplicationJson);
            Console.WriteLine("Wrote product: {0}", product.ToString());

            // Get the response
            Console.WriteLine("Response from GET: {0}", response.PayloadString);
            Console.ReadLine();
        }
    }
}
