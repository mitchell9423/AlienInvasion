using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alien.UI
{
    public class NewPlayerMenu : Menu
    {
        void Awake()
        {
            menuType = MenuController.MenuType.NewPlayerMenu;
        }
    }
}
