using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace lr1.Classes
{
    class Utilities
    {
        public static void GetStorages(ref List<Storage> storages)
        {
            string currentLine = string.Empty;
            Console.WriteLine("Enter data in a following format \'{Storage name};{Storage address}\'.\nTo stop input an empty line");
            do
            {
                string[] splitted = currentLine.Split(';');
                if (splitted.Length >= 2)
                {
                    storages.Add(new Storage(splitted[0], splitted[1]));
                }
                currentLine = Console.ReadLine();
            } while (currentLine != "");
        }

        public static void GetProducts(ref List<Product> products)
        {
            string currentLine = string.Empty;
            Console.WriteLine("Enter product in a following format \'{Product name};{Product price};{Product count};{date1},{date2}... \'.\nTo stop input an empty line");
            do
            {
                string[] splitted = currentLine.Split(';');
                if (splitted.Length >= 2)
                {
                    products.Add(new Product(splitted[0], double.Parse(splitted[1]),
                        int.Parse(splitted[2]), 
                        Utilities.ParseDates(currentLine.Substring(currentLine.LastIndexOf(';') + 1))));
                }
                currentLine = Console.ReadLine();
            } while (currentLine != "");
        }

        private static List<Date> ParseDates(string datesString)
        {
            List<Date> dates = new List<Date>();
            foreach (var date in datesString.Split(','))
            {
                dates.Add(new Date(date));
            }
            return dates;
        }

        public static void GetManufactures(ref List<Manufacturer> manufacturers)
        {
            string currentLine = string.Empty;
            Console.WriteLine("Enter manufacturer in a following format \'{Manufacturer name};{Manufacturer address}\'.\nTo stop input an empty line");
            do
            {
                string[] splitted = currentLine.Split(';');
                if (splitted.Length >= 2)
                {
                    manufacturers.Add(new Manufacturer(splitted[0], splitted[1]));
                }
                currentLine = Console.ReadLine();
            } while (currentLine != "");
        }

        public static void InsertFromFile(ref List<Storage> storages, 
            ref List<Manufacturer> manufactures, ref List<Product> products)
        {
            using (StreamReader streamReader = new StreamReader("input.txt"))
            {
                int counter = 0, currentIndex = 0;
                string currentLine = string.Empty;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    if (currentLine.Length == 0)
                    {
                        counter++;
                        currentIndex = 0;
                        continue;
                    }
                    if (counter > 4)
                    {
                        break;
                    }
                    switch (counter)
                    {
                        case 0:
                            string[] splittedStorage = currentLine.Split(';');
                            storages.Add(new Storage(splittedStorage[0], splittedStorage[1]));
                            break;
                        case 1:
                            string[] splittedManufacturer = currentLine.Split(';');
                            manufactures.Add(new Manufacturer(splittedManufacturer[0], splittedManufacturer[1]));
                            break;
                        case 2:
                            string[] splittedProduct = currentLine.Split(';');
                            products.Add(new Product(splittedProduct[0], double.Parse(splittedProduct[1]),
                                int.Parse(splittedProduct[2]),
                                Utilities.ParseDates(currentLine.Substring(currentLine.LastIndexOf(';') + 1))));
                            break;
                        case 3:
                            splittedProduct = currentLine.Split(';');
                            for (int i = 0; i < splittedProduct.Length; i++)
                            {
                                manufactures[currentIndex].AddProducts(products[int.Parse(splittedProduct[i])-1]);
                            }
                            currentIndex++;
                            break;
                        case 4:
                            splittedManufacturer = currentLine.Split(';');
                            Random r = new Random();
                            List<Manufacturer> tempManufactures = new List<Manufacturer>();
                            for (int i = 0; i < splittedManufacturer.Length; i++)
                            {
                                tempManufactures.Add(manufactures[int.Parse(splittedManufacturer[i]) - 1]);
                            }
                            for (int i = 0; i < products.Count; i++)
                            {
                                storages[r.Next(storages.Count)].Products.Add(new Tuple<Product, List<Manufacturer>>(
                                    products[i], tempManufactures));
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static void SaveToXML (List<Storage> storages, 
            List<Product> products, List<Manufacturer> manufactures, string fileName)
        {
            Console.WriteLine($"\n***Saving to \"{fileName}\"***\n");
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true
            };
            using (XmlWriter writer = XmlWriter.Create(fileName, settings))
            {
                writer.WriteStartElement("storages");
                foreach (Storage storage in storages)
                {
                    writer.WriteStartElement("storage");
                    writer.WriteAttributeString("name", storage.Name);
                    writer.WriteElementString("location", storage.Location);
                    writer.WriteStartElement("products");
                    foreach (var tuple in storage.Products)
                    {
                        writer.WriteStartElement("product");
                        writer.WriteAttributeString("name", tuple.Item1.Name);
                        writer.WriteElementString("price", tuple.Item1.Price.ToString());
                        writer.WriteElementString("count", tuple.Item1.Count.ToString());
                        writer.WriteStartElement("dates");
                        foreach (var date in tuple.Item1.deliveryDates)
                        {
                            writer.WriteElementString("date", date.ToString());
                        }
                        writer.WriteEndElement();
                        writer.WriteStartElement("manufactures");
                        foreach (var manufacturer in tuple.Item2)
                        {
                            writer.WriteStartElement("manufacturer");
                            writer.WriteAttributeString("name", manufacturer.Name);
                            writer.WriteElementString("address", manufacturer.Address);
                            writer.WriteStartElement("products");
                            foreach (var product in manufacturer.Products)
                            {
                                writer.WriteStartElement("product");
                                writer.WriteAttributeString("name", product.Name);
                                writer.WriteElementString("price", product.Price.ToString());
                                writer.WriteElementString("count", product.Count.ToString());
                                writer.WriteStartElement("dates");
                                foreach (var date in product.deliveryDates)
                                {
                                    writer.WriteElementString("price", date.ToString());
                                }
                                writer.WriteEndElement();
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        public static void PrintXML (string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            int counter = 0;
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {   
                string storageName = node.Attributes["name"].Value;
                string storageLocation = node["location"].InnerText;
                var availableProducts = node["products"].ChildNodes;
                int productsCount = availableProducts.Count;
                Console.WriteLine($"{++counter}. {storageName} " +
                    $"\n- Located in {storageLocation}" +
                    $"\n- There`re {productsCount} products:" +
                    $"{PrintProducts(availableProducts)}" +
                    $"\n");
            }
        }

        private static string PrintManufactures(XmlNodeList manufacturesList)
        {
            string result = string.Empty;
            foreach (XmlNode manufacturer in manufacturesList)
            {
                result += $"\n\t\t{manufacturer.Attributes["name"].Value} " +
                    $"(Located at {manufacturer["address"].InnerText}). Available products:" +
                    $"{PrintProducts(manufacturer["products"].ChildNodes, 3)}";
            }
            return result;
        }

        private static string GetTabCount (int count = 1)
        {
            string result = string.Empty;
            for (int i = 0; i < count; i++)
            {
                result += "\t";
            }
            return result;
        }

        private static string PrintProducts (XmlNodeList availableProducts, int deep = 1)
        {
            string result = string.Empty;
            foreach (XmlNode product in availableProducts)
            {
                if (deep == 1)
                {
                    result += $"\n{GetTabCount(deep)}{product.Attributes["name"].Value} ({product["count"].InnerText}pcs " +
                        $"{product["price"].InnerText}hrn each). " +
                        $"Delivery dates: {FormatDates(product["dates"].ChildNodes)}" +
                        $"{PrintManufactures(product["manufactures"].ChildNodes)}";
                }
                else
                {
                    result += $"\n{GetTabCount(deep)}{product.Attributes["name"].Value} ({product["count"].InnerText}pcs " +
                        $"{product["price"].InnerText}hrn each). " +
                        $"Delivery dates: {FormatDates(product["dates"].ChildNodes)}";
                }

            }
            return result;
        }

        private static string FormatDates(XmlNodeList deliveryDates)
        {
            string result = string.Empty;
            foreach (XmlNode date in deliveryDates)
            {
                result += $"{date.InnerText}, ";
            }
            return result.Substring(0, result.Length - 2);
        }

        public static void PrintData(List<Storage> storages, List<Product> products, List<Manufacturer> manufactures)
        {
            OutText("You`ve entered the following data:");
            Console.WriteLine("The list of storages:");
            for (int i = 0; i < storages.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {storages[i].Name} ({storages[i].Location})");
            }

            Console.WriteLine("\nThe list of manufactures:");
            for (int i = 0; i < manufactures.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manufactures[i].Name} ({manufactures[i].Address})");
            }

            Console.WriteLine("\nThe list of products:");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i]}");
            }

            Console.WriteLine("\nNow you must add products to manufactures and manufactures to storages.");
        }

        public static void ManufacturesToStorage(ref List<Storage> storages, List<Manufacturer> manufactures, List<Product> products)
        {
            string[] splittedData;

            Console.WriteLine("Manufactures available:");
            for (int i = 0; i < manufactures.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {manufactures[i]}");
            }

            Console.WriteLine("Products available:");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i]}");
            }

            for (int i = 0; i < storages.Count; i++)
            {
                Console.WriteLine($"Adding products and manufactures into storage #{i + 1} ({storages[i].Name})");
                Console.WriteLine("Which products and manufactures should be added? (Pick their ids from the list before)");
                Console.WriteLine("Input them in the next format \'{product:manufacture1,manufacture2...};...\'. To stop input an empty line");
                string input = string.Empty;
                do
                {
                    input = Console.ReadLine();
                    splittedData = input.Split(';');
                    foreach (var segment in splittedData)
                    {
                        try
                        {
                            int productId = int.Parse(segment.Split(':')[0]) - 1;
                            var tempManufactures = new List<Manufacturer>();
                            string[] manufacturesId = segment.Substring(segment.IndexOf(':') + 1).Split(',');
                            foreach (var id in manufacturesId)
                            {
                                tempManufactures.Add(manufactures[int.Parse(id) - 1]);
                            }
                            storages[i].Products.Add(new Tuple<Product, List<Manufacturer>>(products[productId], tempManufactures));
                        }
                        catch
                        {
                            continue;
                        }
                    }
                } while (input != "");
            }
        }

        public static void ProductsToManufactures(ref List<Manufacturer> manufactures, List<Product> products)
        {
            string[] splittedData;
            Console.WriteLine("Products available:");
            for(int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i]}");
            }
            Console.WriteLine();
            for (int i = 0; i < manufactures.Count; i++)
            {
                Console.WriteLine($"Adding products to manufacturer #{i + 1} ({manufactures[i].Name}) out of {manufactures.Count}");
                Console.WriteLine("Which products should be added? (Pick their ids from the list before)");
                Console.WriteLine("Input products in the next format \'{1};{2}...\'. To stop input an empty line");
                splittedData = Console.ReadLine().Split(';');
                try
                {
                    for (int j = 0; j < splittedData.Length; j++)
                    {
                        int index = int.Parse(splittedData[i]) - 1;
                        manufactures[i].Products.Add(products[index]);
                    }
                } catch {

                }
            }
        }

        static string OutLine(int count = 30)
        {
            string result = "\n";
            for (int i = 0; i < count; i++)
            {
                result += "=";
            }
            return result + '\n';
        }

        public static void OutText(string text)
        {
            Console.WriteLine($"{OutLine()}{text}{OutLine()}");
        }
    }
}
