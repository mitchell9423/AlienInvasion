using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alien.UI
{
    public class InstructionsMenu : Menu
    {
        void Awake()
        {
            menuType = MenuController.MenuType.InstructionsMenu;
        }
    }
}
