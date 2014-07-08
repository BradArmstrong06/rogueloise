using System;

namespace RogueLoise.UI.Components
{
    public class Button : UIElement
    {
        private Action _onPressed;

        private Panel _border;

        public string Text { get; set; }

        public Vector TextPosition { get; set; }

        public Vector AbsoluteTextPosition
        {
            get { return AbsolutePosition + TextPosition; }
        }

        public ConsoleColor DisabledTextColor { get; set; }

        public ConsoleColor TextColor { get; set; }

        public ConsoleColor SelectedTextColor { get; set; }

        public bool DrawBorders
        {
            get
            {
                return _border.IsDrawBorders;
            }
            set
            {
                _border.IsDrawBorders = value;
            }
        }


        public Button(Game game, Vector position, Vector size, Action onPressed) : base(game)
        {
            _onPressed = onPressed;
            Size = size;
            Position = position;
            _border = new Panel(game)
            {
                Position = new Vector(),
                Size = size,
                BorderTiles = "║═╔╗╚╝"
            };
            AddChild(_border);
            TextColor = Color;
            SelectedTextColor = Color;
            DisabledTextColor = ConsoleColor.Gray;
            DrawBorders = true;
            Selectable = true;
        }

        public override void Update(UpdateArgs args)
        {
            if (IsSelected)
            {
                if (args.Key == ConsoleKey.Enter)
                {
                    _onPressed();
                }
            }
        }

        protected override void DoDraw(DrawArgs args)
        {
            if (!IsEnabled)
            {
                args.DrawAtAbsolutePoint(AbsoluteTextPosition, Text, DisabledTextColor);
            }
            else
            {
                if (IsSelected)
                {
                    args.DrawAtAbsolutePoint(AbsoluteTextPosition, Text, SelectedTextColor);
                }
                else
                {
                    args.DrawAtAbsolutePoint(AbsoluteTextPosition, Text, TextColor);
                }
            }
        }
    }
}