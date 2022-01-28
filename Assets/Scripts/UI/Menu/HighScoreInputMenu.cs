using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alien.UI
{
    public class HighScoreInputMenu : Menu
    {
        void Awake()
        {
            menuType = MenuController.MenuType.HighScoreInputMenu;
        }
    }
}
