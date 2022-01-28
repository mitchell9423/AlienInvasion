using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alien.UI
{
    public class HighScoresMenu : Menu
    {
        void Awake()
        {
            menuType = MenuController.MenuType.HighScoresMenu;
        }
    }
}
