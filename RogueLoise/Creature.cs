using System;
using System.Collections.Generic;
using System.Linq;

namespace RogueLoise
{
    public class Creature : DrawableGameObject
    {
        public Creature(Game game) : base(game)
        {
        }

        public Map Map { get; set; }

        public int MaxHp { get; set; }

        public int Hp { get; set; }

        public bool Playable { get; set; }

        public bool IsPlayer { get; set; }

        public IList<GameObject> Inventory { get; set; }

        public IList<CreatureAttribute> Attributes { get; set; }

        public IList<AttributeModificator> Modificators { get; set; }

        public override void Update(UpdateArgs args)
        {
            if (Updated)
                return;

            UpdateModificators();

            #region Handling keys

            if (Game.HandlingKeysInGame)
            {
                if (IsPlayer)
                {

                    if (args.Key == ConsoleKey.LeftArrow)
                        Move(Direction.Left);

                    if (args.Key == ConsoleKey.RightArrow)
                        Move(Direction.Right);

                    if (args.Key == ConsoleKey.UpArrow)
                        Move(Direction.Up);

                    if (args.Key == ConsoleKey.DownArrow)
                        Move(Direction.Down);
                }
            }

            #endregion


            base.Update(args);
        }

        private void UpdateModificators()
        {
            //todo
        }

        public void Move(Direction direction)
        {
            Vector newLocation = GetNewPosition(direction);

            if (CanMove(newLocation))
            {
                if (Map.MoveObject(this, newLocation))
                {
                    Position = newLocation;
                }
            }
        }

        protected bool CanMove(Vector newLocation)
        {
            return true; //todo
        }

        private Vector GetNewPosition(Direction direction)
        {
            var dir = (int) direction;
            int newX = dir > 0 && dir < 4
                ? X - 1
                : dir > 4 && dir < 7 ? X + 1 : X;
            int newY = (dir >= 0 && dir < 2) || dir == 7
                ? Y + 1
                : dir > 2 && dir < 6 ? Y - 1 : Y;
            return new Vector(newX, newY);
        }

        public override GameObject Clone()
        {
            var clone = new Creature(Game);
            SetClone(clone);
            return clone;
        }

        protected override void SetClone(GameObject obj)
        {
            base.SetClone(obj);
            var creature = (Creature) obj;
            creature.Playable = Playable;
            creature.Hp = Hp;
            creature.MaxHp = MaxHp;
            creature.Map = Map;

            creature.Modificators = Modificators.Select(m => m.Clone()).ToList();
            creature.Attributes = Attributes.Select(a => a.Clone()).ToList();
            creature.Inventory = Inventory.Select(i => i.Clone()).ToList();
        }
    }

    public enum Direction
    {
        Down,       //0
        LeftDown,   //1     3  4  5
        Left,       //2        ^
        LeftUp,     //3     2  |  6
        Up,         //4        H
        RightUp,    //5     1  0  7
        Right,      //6
        RightDown   //7
    }
}