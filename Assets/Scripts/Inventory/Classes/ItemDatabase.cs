using System.Collections.Generic;

namespace Inventory {
    public class ItemDatabase {
        private List<Item> items = new List<Item>();
        private static ItemDatabase instance;
        private ItemDatabase () { }
        public static ItemDatabase Instance {
            get {
                if (instance == null)
                    instance = new ItemDatabase();
                return instance;
            }
        }

        public static Item GetItem (int id)
        {
            return Instance.items[id];
        }

        public static void Init ()
        {
            Instance.items.Add (new Weapon ("Ancient_greatsword"));
            Instance.items.Add (new Weapon ("Basic_greatsword"));
            Instance.items.Add (new Weapon ("Crystal_greatsword"));
            Instance.items.Add (new Weapon ("Redstone_greatsword"));
        }

        void Start () {
        }
    }
}