
using UnityEngine;
namespace Inventory
{
    public class Weapon : Item
    {
        Sprite texture;
        int baseDmg;

        public Weapon (string name) : base (name) {
            this.texture = Resources.Load<Sprite> ("Weapons/" + name);
            this.itemIcon = Resources.Load<Sprite> (name);
        }

        public Sprite Texture {
            get {
                return texture;
            }
        }
    }

}