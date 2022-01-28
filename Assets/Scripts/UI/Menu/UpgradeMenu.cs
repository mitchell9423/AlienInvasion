using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alien.UI
{
    public class UpgradeMenu : Menu
    {
        void Awake()
        {
            menuType = MenuController.MenuType.UpgradeMenu;
        }
    }
}
