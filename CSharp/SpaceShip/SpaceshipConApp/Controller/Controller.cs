using System;

namespace SpaceshipConApp
{
    public class SpaceshipController
    {
        private Spaceship? spaceship;
        private Menu? menu;

        public SpaceshipController()
        {
            this.spaceship = new Spaceship("", "", 0, false, "");
            this.menu = new Menu();
        }

        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                menu?.Show();
                int choice = menu?.GetInput() ?? -1;
                switch (choice)
                {
                    case 1:
                        spaceship?.BuildSpaceship();
                        break;
                    case 2:
                        spaceship?.ShowStatus();
                        break;
                    case 3:
                        spaceship?.Refuel();
                        break;
                    case 4:
                        spaceship?.ChangeDestination();
                        break;
                    case 5:
                        spaceship?.BoardPilot();
                        break;
                    case 6:
                        spaceship?.Launch();
                        break;
                    case 7:
                        isRunning = false;
                        break;
                    default:
                        menu.ShowError();
                        break;
                }
            }
        }
    }
}
