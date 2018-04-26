using CoAP.Server;
using System;

namespace CoAPServer
{
    class Program
    {
        //public const string CONNECTION_STRING = @"Data Source=LAPTOP-RALPH\WINCC;Initial Catalog=STORE;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public const string CONNECTION_STRING = @"Data Source=mssql8.unoeuro.com;Initial Catalog=tgaarde_dk_db;Persist Security Info=True;User ID=tgaarde_dk;Password=iotWorkshop";

        static void Main(string[] args)
        {
            // Setup the CoapServer object
            CoapServer server = new CoapServer();

            // Create a database handler
            Database database = new Database(CONNECTION_STRING);

            // Add requestable resources
            server.Add(new TestResource("test"));
            server.Add(new DatabaseResources.ProductsResource("products", database));
            server.Add(new DatabaseResources.TagsResource("tags", database));

            try
            {
                server.Start();

                Console.Write("CoAP server [{0}] is listening on", server.Config.Version);

                foreach (var item in server.EndPoints)
                {
                    Console.Write(" ");
                    Console.Write(item.LocalEndPoint);
                }

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();

            DisplayHelp();
            Console.Write("> ");

            // Console input
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                if (line == "exit")
                {
                    break;
                }
                else if (line == "help")
                {
                    DisplayHelp();
                }
                else if (line == "getproducts")
                {
                    database.GetProducts();
                }
                else if (line == "cleartags")
                {
                    database.ClearTags();
                }
                else if (line == "listscans")
                {
                    database.GetLastScans();
                }
                else if (line == "clearscans")
                {
                    database.ClearScans();
                }
                else if (line.Contains(" "))
                {
                    string[] split = line.Split(new char[1] { ' ' }, StringSplitOptions.None);
                    if (split[0] == "addtag")
                    {
                        if (split.Length != 3)
                        {
                            Console.WriteLine("Invalid parameters");
                            continue;
                        }

                        int productId = -1;
                        int.TryParse(split[2], out productId);
                        if (productId == -1)
                        {
                            Console.WriteLine("Invalid product id");
                            continue;
                        }

                        database.AddTag(split[1], productId);
                    }
                    else if (split[0] == "addproduct")
                    {
                        if (split.Length != 3)
                        {
                            Console.WriteLine("Invalid parameters");
                            continue;
                        }

                        double price = -1;
                        double.TryParse(split[2], out price);
                        if (price == -1)
                        {
                            Console.WriteLine("Invalid price");
                            continue;
                        }

                        database.AddProduct(split[1], price);
                    }
                }

                Console.Write("> ");
            }
        }

        static void DisplayHelp()
        {
            Console.WriteLine("CoAP Server Commands:");
            Console.WriteLine("- getproducts");
            Console.WriteLine("- addproduct [name] [price]");
            Console.WriteLine("- addtag [tag_id] [product_id]");
            Console.WriteLine("- cleartags");
            Console.WriteLine("- listscans");
            Console.WriteLine("- clearscans");
            Console.WriteLine("- help");
            Console.WriteLine("- exit");
            Console.WriteLine();
        }
    }
}
