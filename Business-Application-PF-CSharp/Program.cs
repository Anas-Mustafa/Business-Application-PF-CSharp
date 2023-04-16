using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                if (option == 1)
                {
                    AddItem(items);
                    WriteData(items);
                }
                else if (option == 2)
                {
                    Console.Clear();
                    Console.WriteLine("DARWAZA.PK > Seller > Display Items\n");
                    DisplayItems(items);
                    Console.Write("\n\nPress any key to continue...");
                    Console.ReadKey();
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


        static void AddItem(List<Item> items)
        {
            Item item = new Item();
            Console.Clear();
            Console.WriteLine("DARWAZA.PK > Seller > Add Item\n");
            Console.Write("Item Name: ");
            item.name = Console.ReadLine();
            Console.Write("Item Price: ");
            item.price = float.Parse(Console.ReadLine());
            Console.Write("Item Quantity: ");
            item.quantity = int.Parse(Console.ReadLine());
            items.Add(item);
            
            Console.Write("\nItem Added Successfully...");
            Console.ReadKey();
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
            Console.Clear();
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
            Console.ReadKey();
        }

        static void UpdateItem(List<Item> items)
        {
            string option;
            Console.Clear();
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
            Console.ReadKey();
        }

        static void WriteData(List<Item> items)
        {
            int itemCount = items.Count;
            StreamWriter fileVariable;
            string path = "D:\\UET-Tasks\\OOP\\Business-Application-PF-CSharp\\Business-Application-PF-CSharp\\data.csv";
            if (File.Exists(path))
            {
                fileVariable = new StreamWriter(path, true);
            } else
            {
                fileVariable = new StreamWriter(path);
            }
            fileVariable.WriteLine("{0},{1},{2}", items[itemCount - 1].name, items[itemCount - 1].price, items[itemCount - 1].quantity);
            fileVariable.Close();
        }

        static void RewriteData(List<Item> items)
        {
            string path = "D:\\UET-Tasks\\OOP\\Business-Application-PF-CSharp\\Business-Application-PF-CSharp\\data.csv";
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
                    Item item = new Item();
                    item.name = rawData[0];
                    item.quantity = int.Parse(rawData[1]);
                    item.price = int.Parse(rawData[2]);
                    items.Add(item);
                }
                fileVariable.Close();
            }
        }
    }
}
