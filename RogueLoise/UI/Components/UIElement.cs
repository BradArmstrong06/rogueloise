using System;
using System.Collections.Generic;
using System.Linq;

namespace RogueLoise.UI.Components
{
    public class UIElement : DrawableGameObject
    {
        public UIElement(Game game) : base(game)
        {
            SelectIndex = -1;
        }

        public bool HandlingKeys
        {
            get
            {
                return Game.HandlingKeysInUI;
            }
        }

        public UIElement Parent { get; set; }

        public readonly List<UIElement> ChildList = new List<UIElement>();

        public bool Selectable { get; set; }

        public bool IsSelected { get; set; }

        public int SelectIndex { get; set; }

        public int RealSelectIndex { get; set; }

        //public int GlobalSelectIndex
        //{
        //    get
        //    {
        //        return Parent == null ? SelectIndex :  Parent.GlobalSelectIndex == -1 ? -1 : Parent.GlobalSelectIndex + SelectIndex;
        //    }
        //}

        //public int AbsoluteSelectIndex
        //{
        //    get
        //    {
        //        if (Parent == null)
        //            return SelectIndex;

        //        return Parent.AbsoluteSelectIndex + SelectIndex;
        //    }
        //}

        public bool IsAnyChildSelected
        {
            get
            {
                return ChildList.Any(c => c.IsSelected);
            }
        }

        public Vector Size { get; set; }

        public Vector AbsolutePosition
        {
            get { return Parent != null ? Position + Parent.AbsolutePosition : Position; }
        }

        public ConsoleColor BackgroundColor { get; set; }

        public ConsoleColor ForegroundColor { get; set; }

        public void AddChild(UIElement child)
        {
            child.Parent = this;
            if (child.Selectable && child.SelectIndex == -1)
                child.SelectIndex = ChildList.Count;

            ChildList.Add(child);

            Selectable = false;
        }

        public override void Draw(DrawArgs args)
        {
            if(!IsVisible)
                return;

            DoDraw(args);
            foreach (var child in ChildList)
            {
                child.Draw(args);
            }
        }

        protected virtual void DoDraw(DrawArgs args)
        {

        }

        public override void Update(UpdateArgs args)
        {
            foreach (var uiElement in ChildList)
            {
                uiElement.Update(args);
            }
        }
    }
}