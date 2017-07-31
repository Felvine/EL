using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class Slot
    {
        private GameObject slot;
        private Item item;

        public Slot (GameObject slotPrefab, GameObject parent, int rowNumber, int colNumber, Vector3 offset, Item item)
        {
            this.slot = GameObject.Instantiate (slotPrefab);
            this.slot.GetComponent<Button> ().onClick.AddListener (EquipWeapon);
            this.slot.transform.SetParent (parent.transform);
            this.slot.transform.localPosition = new Vector3 (colNumber * offset.x, rowNumber * offset.y, 0);
            this.slot.name = "Inventory Slot" + rowNumber + "," + colNumber;
            this.item = item; 
        }
        public void ChangeItem (Item item)
        {
            this.item = item;
            Refresh ();
        }

        private void Refresh () {
            if (item != null) {
                Image icon = this.slot.GetComponentInChildren<Image> ();
                icon.enabled = true;
                icon.sprite = item.Icon;
            }
        }

        public void EquipWeapon ()
        {
            if (this.item.GetType () == typeof (Weapon)) {
                Weapon weapon = item as Weapon;
                GameObject sword = GameObject.FindGameObjectWithTag ("Weapon");
                sword.GetComponent<SpriteRenderer> ().sprite = weapon.Texture;
            }
        }
    }
}
