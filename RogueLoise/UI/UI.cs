using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private List<UIElement> _panels = new List<UIElement>();

        private UIElement _selectedElement;

        public bool HandlingKeys { get; set; }

        public UI(Game game, Settings settings)
        {
            _game = game;
            string tiles = settings.UITiles;

            _workZoneEnd = settings.DrawzoneEnd;
            _gameZoneBegin = settings.UIGamezoneBegin;
            _gameZoneEnd = settings.UIGamezoneEnd;

            var gameZonePanel = new Panel(game){BorderTiles = tiles, Position = _gameZoneBegin - 1, Size = _gameZoneEnd - _gameZoneBegin + 2, IsEnabled = false, Selectable = false};
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

            UpdateElementIndexes();
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

        private void UpdateElementIndexes()
        {
            var currentIndex = 0;
            UpdateElementIndexes(_panels, ref currentIndex);
            //UIElement currentElement = null;
            //foreach (var panel in _panels)
            //{
            //    if (panel.SelectIndex < currentSelectIndex)
            //    {
            //        currentSelectIndex = panel.SelectIndex;
            //        currentElement = panel;
            //    }
            //}
            //if (currentElement == null)
            //    return;

            //if (currentElement.Selectable)
            //{
            //    currentElement.RealSelectIndex = currentIndex;
            //    currentIndex++;
            //}

            //for (int i = 0; i < _panels.Count; i++)
            //{

            //}
        }

        private void UpdateElementIndexes(List<UIElement> elements, ref int currentIndex)
        {
            var minSelectIndex = int.MaxValue;
            var lastSelectIndex = -1;
            for (int i = 0; i < elements.Count(); i++)
            {
                UIElement currentElement = null;
                foreach (var element in elements)
                {
                    if (element.SelectIndex != -1 && element.SelectIndex < minSelectIndex && (lastSelectIndex == -1 || element.SelectIndex > lastSelectIndex))
                    {
                        minSelectIndex = element.SelectIndex;
                        currentElement = element;
                    }
                }
                if (currentElement == null)
                    return;

                lastSelectIndex = currentElement.SelectIndex;

                if (currentElement.Selectable)
                {
                    currentElement.RealSelectIndex = currentIndex;
                    currentIndex++;
                }

                UpdateElementIndexes(currentElement.ChildList, ref currentIndex);
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

        private IEnumerable<UIElement> GetChilds(ReadOnlyCollection<UIElement> elements)
        {
            if(elements.Any())
                return new UIElement[0];

            return elements.Select(GetChilds).Aggregate((all, next) => all.Concat(next)).ToArray();
        }
    }
}