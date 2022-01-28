using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alien.UI
{
    public class CreditsMenu : Menu
    {
        void Awake()
        {
            menuType = MenuController.MenuType.CreditsMenu;
        }
    }
}
