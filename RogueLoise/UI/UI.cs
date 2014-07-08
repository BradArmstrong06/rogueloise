using System;
using System.Collections.Generic;
using System.Linq;
using RogueLoise.UI.Components;

namespace RogueLoise.UI
{
    public class UI
    {
        private readonly Game _game;
        private Vector _gameZoneBegin;
        private Vector _gameZoneEnd;
        private Vector _workZoneEnd;
        //private Panel _gameZonePanel;

        private List<Panel> _panels = new List<Panel>();

        private UIElement _selectedElement;

        public bool HandlingKeys { get; set; }

        public UI(Game game, Settings settings)
        {
            _game = game;
            string tiles = settings.UITiles;

            _workZoneEnd = settings.DrawzoneEnd;
            _gameZoneBegin = settings.UIGamezoneBegin;
            _gameZoneEnd = settings.UIGamezoneEnd;

            var gameZonePanel = new Panel(game){BorderTiles = tiles, Position = _gameZoneBegin - 1, Size = _gameZoneEnd - _gameZoneBegin + 2, IsEnabled = false};
            _panels.Add(gameZonePanel);
            var otherElementsPanel = new Panel(game)
            {
                BorderTiles = tiles,
                Position = new Vector(_gameZoneEnd.X + 1, 0),
                Size = new Vector(_workZoneEnd.X - _gameZoneEnd.X - 3, _workZoneEnd.Y - 1)
            };
            _panels.Add(otherElementsPanel);
            var button = new Button(game, new Vector(2, 2), new Vector(5, 3), () => { }){TextColor = ConsoleColor.DarkRed, SelectedTextColor = ConsoleColor.Red, Text = "HSHSFDH", TextPosition = new Vector(1,1)};
            otherElementsPanel.AddChild(button);
        }


        public void Draw(DrawArgs args)
        {
            //_gameZonePanel.Draw(args);

            foreach (var panel in _panels)
            {
                panel.Draw(args);
            }
            //todo draw other
        }

        public void Update(UpdateArgs args)
        {
            if (HandlingKeys)
            {
                MoveSelect(args.Key);

            }
        }

        private void SetSelected(UIElement element)
        {

        }

        private void MoveSelect(ConsoleKey key)
        {
            if (key != ConsoleKey.DownArrow && key != ConsoleKey.UpArrow)
                return;

            if (key == ConsoleKey.DownArrow)
            {
                
            }

            if (key == ConsoleKey.UpArrow)
            {
                
            }
        }

        private UIElement GetNextTarget(int i)
        {
            //var allItems = new List<UIElement>();

            //foreach (var panel in _panels)
            //{
            //    allItems.AddRange(GetChilds(panel));
            //}
            //if (!allItems.Any())
            return null;

            //allItems = allItems.OrderBy(item => item.GlobalSelectIndex).ToList();
            //var nextIndex = _selectedElement.GlobalSelectIndex + i;
            //var nextTarget = allItems.FirstOrDefault(item => item.GlobalSelectIndex == nextIndex);
            //if (nextTarget == null)
            //{
            //    return allItems.First();
            //}
        }

        private IEnumerable<UIElement> GetChilds(UIElement element)
        {
            var result = new List<UIElement>();

            foreach (var uiElement in element.ChildList)
            {
                result.AddRange(GetChilds(uiElement));
            }
            return result;
        }
    }
}