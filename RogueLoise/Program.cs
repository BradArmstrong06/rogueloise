using System;

namespace RogueLoise
{
    internal class Program
    {
        private const string SettingsPath = "\\settings.cfg";

        private static void Main(string[] args)
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