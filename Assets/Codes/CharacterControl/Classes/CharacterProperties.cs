using System.Collections.Generic;
using UnityEngine;
namespace Znko.Characters
{
    public class CharacterProperties
    {
        private bool isAttacking = false;
        private bool isInvulnerable = false;
        private Dictionary<CharacterResource.Type, CharacterResource> resources;
        private Dictionary<CharacterAttribute.Type, CharacterAttribute> attributes;
        private List<CharacterCollider> zColliders;
        //private float walkSpeed;
        //private float runSpeed;
        //private bool invincible = false;

        public CharacterProperties()
        {
            this.resources = new Dictionary<CharacterResource.Type, CharacterResource>();
            this.resources.Add(CharacterResource.Type.Health, new CharacterResource(100, Color.red));

            this.attributes = new Dictionary<CharacterAttribute.Type, CharacterAttribute>();
            this.attributes.Add(CharacterAttribute.Type.Attack, new CharacterAttribute(10));

            this.zColliders = new List<CharacterCollider>();
        }

        internal void SetCollidersToAttack(List<string> attackingTypes, bool v)
        {
            foreach (string at in attackingTypes)
            {
                foreach (CharacterCollider zc in this.zColliders)
                {
                    if (zc.IsType(at))
                        zc.IsAttacking = v;
                }
            }
        }

        public bool IsAttacking {
            get {
                return isAttacking;
            }

            set {
                isAttacking = value;
            }
        }

        public bool IsInvulnerable {
            get {
                return isInvulnerable;
            }

            set {
                isInvulnerable = value;
            }
        }

        public CharacterResource GetResource(CharacterResource.Type type)
        {
            return resources[type];
        }

        public void SetResource(CharacterResource.Type type, CharacterResource resourceIn)
        {
            resources[type] = resourceIn;
        }

        public void AddCollider(CharacterCollider zColliderIn)
        {
            this.zColliders.Add(zColliderIn);
        }
    }
}