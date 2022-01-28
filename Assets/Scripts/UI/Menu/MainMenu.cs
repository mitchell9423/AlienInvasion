using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alien.UI
{
    public class MainMenu : Menu
    {
        void Awake()
		{
            menuType = MenuController.MenuType.MainMenu;
        }
    }
}
