using System;
using System.Collections.Generic;
using System.Linq;

namespace RogueLoise.UI.Components
{
    public class UIElement : DrawableGameObject
    {
        public UIElement(Game game) : base(game)
        {
        }

        public UIElement Parent { get; set; }

        public readonly List<UIElement> ChildList = new List<UIElement>();

        public bool IsSelected { get; set; }

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
            ChildList.Add(child);
            child.Parent = this;
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
    }
}