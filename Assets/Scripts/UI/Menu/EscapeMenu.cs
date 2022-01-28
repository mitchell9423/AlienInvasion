using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alien.UI
{
    public class EscapeMenu : Menu
    {
        void Awake()
        {
            menuType = MenuController.MenuType.EscapeMenu;
        }
    }
}
