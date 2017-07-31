using UnityEngine;
namespace Inventory
{
    public class Item
    {
        private static int lastUsedID = 0;

        protected int itemID;
        protected string itemName;
        protected string itemDescription;
        protected Sprite itemIcon;

        public Item (string name)
        {
            this.itemID = ++lastUsedID;
            this.itemName = name;
        }

        public Sprite Icon {
            get {
                if (itemIcon == null)
                    throw new System.Exception();
                return itemIcon;
            }
        }
    }
}