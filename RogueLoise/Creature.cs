using System.Collections.Generic;

namespace RogueLoise
{
    public class Creature : DrawableGameObject
    {
        public Map Map { get; set; }

        public int MaxHp { get; set; }

        public int Hp { get; set; }

        public IList<GameObject> Inventory { get; set; }

        public IList<CreatureAttribute> Attributes { get; set; }

        public IList<AttributeModificator> Modificators { get; set; }

        public override void Update(UpdateArgs args)
        {
            base.Update(args);

            UpdateModificators();
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
                Map.MoveObject(this, newLocation);
            }
        }

        private bool CanMove(Vector newLocation)
        {
            return true;//todo
        }

        private Vector GetNewPosition(Direction direction)
        {
            var dir = (int) direction;
            var newX = dir > 0 && dir < 4
                ? X - 1
                : dir > 4 && dir < 7 ? X + 1 : X;
            var newY = (dir >= 0 && dir < 2) || dir == 7
                ? Y + 1
                : dir > 2 && dir < 6 ? Y - 1 : Y;
            return new Vector(newX, newY);
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