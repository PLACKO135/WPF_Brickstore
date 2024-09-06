using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Brickstore
{
    internal class Brkdata
    {
        int ItemID { get; set; }
        string ItemName { get; set; }
        string CategoryName { get; set; }
        string ColorName { get; set; }
        int Qty { get; set; }

        public Brkdata(int itemID, string itemName, string categoryName, string colorName, int qty)
        {
            ItemID = itemID;
            ItemName = itemName;
            CategoryName = categoryName;
            ColorName = colorName;
            Qty = qty;
        }

     
    }
}
