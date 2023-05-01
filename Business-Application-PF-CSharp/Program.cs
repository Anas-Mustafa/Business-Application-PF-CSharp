using System;
using System.IO;
using System.Collections.Generic;
using Business_Application_PF_CSharp.BL;

namespace Business_Application_PF_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> items = new List<Item>();
            LoadData(items);
            int option;
            bool onMainMenu = true;
            while (onMainMenu)
            {
                Console.Clear();
                option = Menu();
                Console.Clear();

                if (option == 1)
                {
                    Console.WriteLine("DARWAZA.PK > Seller > Add Item\n");
                    Item item = AddItem(items);
                    items.Add(item);
                    Console.Write("\nItem Added Successfully...");
                    WriteData(item);
                }
                else if (option == 2)
                {
                    Console.WriteLine("DARWAZA.PK > Seller > Display Items\n");
                    DisplayItems(items);
                    Console.Write("\n\nPress any key to continue...");
                }
                else if (option == 3)
                {
                    RemoveItem(items);
                    RewriteData(items);
                }
                else if (option == 4)
                {
                    UpdateItem(items);
                    RewriteData(items);
                }
                else if (option == 5)
                {
                    onMainMenu = false;
                }
                Console.ReadKey();
            }
        }

        static int Menu() 
        {
            string option;
            Console.WriteLine("DARWAZA.PK > Seller\n");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Display Items");
            Console.WriteLine("3. Remove Item");
            Console.WriteLine("4. Update Item");
            Console.WriteLine("5. Exit\n");
            do
            {
                Console.Write("Option: ");
                option = Console.ReadLine();
            }
            while (!ValidateOption(option, 5));

            return int.Parse(option);
        }

        static bool ValidateOption(string option, int range)
        {
            bool isValid = true;
            if (option.Length != 1)
            {
                isValid = false;
            } 
            else if (option[0] < 49 || option[0] > 48 + range)
            {
                isValid = false;
            }

            if (!isValid)
            {
                Console.WriteLine("Valid Input is 1-{0}", range);
            }

            return isValid;
        }


        static Item AddItem(List<Item> items)
        {
            Console.Clear();
            Console.Write("Item Name: ");
            string name = Console.ReadLine();
            Console.Write("Item Price: ");
            float price = float.Parse(Console.ReadLine());
            Console.Write("Item Quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            Item item = new Item(name, price, quantity);
            return item;
        }

        static void DisplayItems(List<Item> items)
        {
            Console.Write("No.".PadRight(10));
            Console.Write("Item Name".PadRight(40));
            Console.Write("Item Price".PadRight(20));
            Console.Write("Item Quantity".PadRight(20));
            Console.WriteLine("\n");
            for (int i = 0; i < items.Count; i++)
            {
                Console.Write((i + 1).ToString().PadRight(10));
                Console.Write(items[i].name.PadRight(40));
                Console.Write(items[i].price.ToString().PadRight(20));
                Console.Write(items[i].quantity.ToString().PadRight(20));
                Console.WriteLine();
            }
        }

        static void RemoveItem(List<Item> items)
        {
            string option;
            Console.WriteLine("DARWAZA.PK > Seller > Remove Item\n");
            DisplayItems(items);
            Console.WriteLine("\n");
            do
            {
                Console.Write("Item Number: ");
                option = Console.ReadLine();
            }
            while (!ValidateOption(option, items.Count));

            int index =  int.Parse(option) - 1;
            items.RemoveAt(index);
            Console.Write("\nItem Removed Successfully...");
        }

        static void UpdateItem(List<Item> items)
        {
            string option;
            Console.WriteLine("DARWAZA.PK > Seller > Update Item\n");
            DisplayItems(items);
            Console.WriteLine("\n");
            do
            {
                Console.Write("Item Number: ");
                option = Console.ReadLine();
            }
            while (!ValidateOption(option, items.Count));

            int index = int.Parse(option) - 1;
            Console.WriteLine("\n");
            string itemName;
            int itemQuantity;
            float itemPrice;
            Console.Write("Item Name: ");
            itemName = Console.ReadLine();
            Console.Write("Item Price: ");
            itemPrice = float.Parse(Console.ReadLine());
            Console.Write("Item Quantity: ");
            itemQuantity = int.Parse(Console.ReadLine());

            items[index].name = itemName;
            items[index].price = itemPrice;
            items[index].quantity = itemQuantity;
            Console.Write("\nItem Updated Successfully...");
        }

        static void WriteData(Item item)
        {
            StreamWriter fileVariable;
            string path = "../../data.csv";
            if (File.Exists(path))
            {
                fileVariable = new StreamWriter(path, true);
            } else
            {
                fileVariable = new StreamWriter(path);
            }
            fileVariable.WriteLine("{0},{1},{2}", item.name, item.price, item.quantity);
            fileVariable.Close();
        }

        static void RewriteData(List<Item> items)
        {
            string path = "../../data.csv";
            StreamWriter fileVariable = new StreamWriter(path);
            for (int i = 0; i < items.Count; i++)
            {
                fileVariable.WriteLine("{0},{1},{2}", items[i].name, items[i].price, items[i].quantity);
            }
            fileVariable.Close();
        }

        static void LoadData(List<Item> items)
        {
            string path = "D:\\UET-Tasks\\OOP\\Business-Application-PF-CSharp\\Business-Application-PF-CSharp\\data.csv";
            if (File.Exists(path))
            {
                StreamReader fileVariable = new StreamReader(path);
                string record;
                while ((record = fileVariable.ReadLine()) != null)
                {
                    string[] rawData = record.Split(',');
                    string name = rawData[0];
                    float price = float.Parse(rawData[1]);
                    int quantity = int.Parse(rawData[2]);
                    Item item = new Item(name, price, quantity);
                    items.Add(item);
                }
                fileVariable.Close();
            }
        }
    }
}
