using System;

namespace RogueLoise.UI.Components
{
    public class Button : UIElement
    {
        private Action _onPressed;

        private Panel _border;

        public string Text { get; set; }

        public Vector TextPosition { get; set; }

        public ConsoleColor TextColor { get; set; }

        public Button(Game game, Vector position, Vector size, Action onPressed) : base(game)
        {
            _onPressed = onPressed;
            Size = size;
            Position = position;
            _border = new Panel(game)
            {
                Position = new Vector(),
                Size = size,
            };
            TextColor = Color;
        }

        public override void Update(UpdateArgs args)
        {
            base.Update(args);
        }

        protected override void DoDraw(DrawArgs args)
        {
            args.DrawAtAbsolutePoint(TextPosition, Text, TextColor);
        }
    }
}