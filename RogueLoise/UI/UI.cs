using RogueLoise.UI.Components;

namespace RogueLoise.UI
{
    public class UI
    {
        private readonly Game _game;
        private Vector _gameZoneBegin;
        private Vector _gameZoneEnd;
        private Vector _workZoneEnd;
        private Panel _gameZonePanel;

        public UI(Game game, Settings settings)
        {
            _game = game;
            string tiles = settings.UITiles;


            _workZoneEnd = settings.DrawzoneEnd;
            _gameZoneBegin = settings.UIGamezoneBegin;
            _gameZoneEnd = settings.UIGamezoneEnd;

            _gameZonePanel = new Panel(game){BorderTiles = tiles, Position = _gameZoneBegin - 1, Size = _gameZoneEnd - _gameZoneBegin + 2};
        }


        public void Draw(DrawArgs args)
        {
            _gameZonePanel.Draw(args);

            //todo draw other
        }

        public void Update(UpdateArgs args)
        {
            
        }
    }
}