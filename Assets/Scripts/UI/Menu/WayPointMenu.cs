using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alien.UI
{
    public class WayPointMenu : Menu
    {
        void Awake()
        {
            menuType = MenuController.MenuType.WayPointMenu;
        }
    }
}
