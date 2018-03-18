using Znko.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using Znko.AI;
using Znko.Actions;
namespace Znko.Characters
{
    public class Character
    {
        public enum HorizontalDirection { West, East }
        public enum Factions { Player, Enemy, Neutral }
        public enum Races { Humanoid, Giant }

        private CharacterProperties properties;
        private Dictionary<string, ICharacterAction> availableActions = new Dictionary<string, ICharacterAction>();

        private Vector3 direction = Vector3.zero;
        private HorizontalDirection horizontalDir = HorizontalDirection.East;

        private Transform transform;
        private Animation animation;
        private CharacterController controller;

        private Factions faction = Factions.Neutral;
        private Races race = Races.Humanoid;

        private Dictionary<string, Zone> zones = new Dictionary<string, Zone> ();

        private List<ICharacterEvent> events = new List<ICharacterEvent>();


        #region Properties

        public Animation Animation {
            get {
                return animation;
            }
        }


        public Vector3 Direction {
            get {
                return direction;
            }
            set {
                direction = value;
                SetHorizontalDirection();
            }
        }

        public HorizontalDirection GetHorizontalDirection()
        {
            return horizontalDir;
        }

        private void SetHorizontalDirection()
        {
            if (horizontalDir == HorizontalDirection.East && direction.x < 0)
            {
                HorizontalDir = HorizontalDirection.West;
            }
            else if (horizontalDir == HorizontalDirection.West && direction.x > 0)
            {
                HorizontalDir = HorizontalDirection.East;
            }

        }

        public Transform Transform {
            get {
                return transform;
            }
        }

        private HorizontalDirection HorizontalDir {
            set {
                horizontalDir = value;
                foreach (Transform child in transform)
                {
                    if (child.tag == "Character")
                    {
                        switch (horizontalDir)
                        {
                            case HorizontalDirection.East:
                                child.rotation = Quaternion.Euler(45, 0, 0);
                                child.localScale = new Vector3(1, 1, 1);
                                break;
                            case HorizontalDirection.West:
                                child.rotation = Quaternion.Euler(-45, 180, 0);
                                child.localScale = new Vector3(1, 1, -1);
                                break;
                        }
                    }
                }
            }
        }

        public CharacterProperties Properties {
            get {
                return properties;
            }
        }
        public CharacterController Controller {
            get {
                return controller;
            }
        }

        public Factions Faction {
            get {
                return faction;
            }

            set {
                faction = value;
            }
        }

        public Races Race {
            get {
                return race;
            }

            set {
                race = value;
            }
        }

        public List<ICharacterEvent> Events {
            get {
                return events;
            }

            set {
                events = value;
            }
        }

        public Dictionary<string, Zone> Zones {
            get {
                return zones;
            }
        }
        #endregion

        #region Constructors
        public Character(Transform characterTransform)
        {
            this.transform = characterTransform;
            this.animation = characterTransform.GetComponentInChildren<Animation>();
            this.controller = characterTransform.GetComponent<CharacterController>();
            this.properties = new CharacterProperties();
        }


        public Character(Transform characterTransform, Animation animationIn, CharacterController controllerIn)
        {
            this.transform = characterTransform;
            this.properties = new CharacterProperties();
            this.animation = animationIn;
            this.controller = controllerIn;
        }

        #endregion

        public void AddAction(string actionNameIn, ICharacterAction actionIn)
        {
            if (!availableActions.ContainsKey(actionNameIn))
                availableActions.Add(actionNameIn, actionIn);
            else
                throw new Exception("Action has already been registered to character");
        }

        public void AddZones (Zone zone, string zoneName)
        {
            this.zones.Add(zoneName, zone);
        }
        public ICharacterAction GetAction(string actionNameIn)
        {
            if (availableActions.ContainsKey(actionNameIn))
                return availableActions[actionNameIn];
            else
                return null;
        }
        public bool HasAnimation()
        {
            return this.animation != null;
        }
        public CharacterResource GetResource(CharacterResource.Type type)
        {
            return properties.GetResource(type);
        }
        public float GetDistance(Character other)
        {
            return Vector3.Distance(this.transform.position, other.transform.position);
        }
        public Root.Coord GetCoord()
        {
            return this.transform.position;
        }

        public void SetHorizontalDirectionDebug(Character.HorizontalDirection ind)
        {
            this.horizontalDir = ind;
        }
    }
}