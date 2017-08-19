using UnityEngine;
using System.Collections.Generic;
namespace Inventory
{
    public class Inventory : MonoBehaviour
    {

        public int maxrows = 5;
        public int maxcols = 1;
        public GameObject slotPrefab;

        public List<Slot> inventory = new List<Slot> ();

        // Use this for initialization
        void Start ()
        {
            ItemDatabase.Init ();
            for (int j = 0; j < maxcols; j++) {
                for (int i = 0; i < maxrows; i++) {
                    inventory.Add (new Slot (slotPrefab, this.gameObject, i, j, new Vector3 (35, -35, 0), null));
                }
            }
            InsertItemToSlot (ItemDatabase.GetItem (0), 0);
            InsertItemToSlot (ItemDatabase.GetItem (1), 1);
            InsertItemToSlot (ItemDatabase.GetItem (2), 2);
            InsertItemToSlot (ItemDatabase.GetItem (3), 3);

        }

        void InsertItemToSlot (Item item, int n)
        {
            inventory[n].ChangeItem (item);
        }
    }
}