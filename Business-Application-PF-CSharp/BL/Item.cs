using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Application_PF_CSharp.BL
{
    class Item
    {
        public Item(string name, float price, int quantity)
        {
            this.name = name;
            this.price = price;
            this.quantity = quantity;
        }

        public string name;
        public float price;
        public int quantity;
    }
}
