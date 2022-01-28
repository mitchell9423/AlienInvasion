using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alien.UI
{
    public class LevelSelectionMenu : Menu
    {
        void Awake()
        {
            menuType = MenuController.MenuType.LevelSelectionMenu;
        }
    }
}
