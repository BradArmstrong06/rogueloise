using System;

namespace RogueLoise
{
    class Program
    {
        private const string SettingsPath = "\\settings.cfg";

        static void Main(string[] args)
        {
            RegisterEverything();
            new Game();
        }

        private static void RegisterEverything()
        {
            var settingsProvider = new SettingsProvider(Environment.CurrentDirectory + SettingsPath);
            ServiceLocator.AddService<SettingsProvider>(settingsProvider);

            var drawer = new ConsolePrinter();
            ServiceLocator.AddService<IDrawer>(drawer);
        }
    }
}
