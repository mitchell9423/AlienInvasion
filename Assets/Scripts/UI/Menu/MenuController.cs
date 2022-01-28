using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alien.UI
{
    public class MenuController : MonoBehaviour
    {
        public enum MenuType
        {
            MainMenu,
            EscapeMenu,
            NewPlayerMenu,
            HighScoresMenu,
			HighScoreInputMenu,
            InstructionsMenu,
            CreditsMenu,
            SaveMenu,
            WayPointMenu,
            LevelSelectionMenu,
            UpgradeMenu
        }

		public static MenuController Instance { get; private set; }
		[SerializeField] MenuType currentMenu = MenuType.MainMenu;
		public static Stack<MenuType> prevMenu = new Stack<MenuType>();
		[SerializeField] Menu[] menuContainers;

		void Awake()
		{
			if (Instance == null) Instance = this;
			for (int i = 0; i < menuContainers.Length; i++)
			{
				menuContainers[i].menuType = (MenuType)i;
			}
		}

		public void OpenMenu(MenuType _menuType)
		{
			if (_menuType == MenuType.MainMenu)
			{
				prevMenu.Clear();
			}
			else
			{
				PlayAudio();
			}

			ActivateMenu(_menuType);
			prevMenu.Push(currentMenu);
			currentMenu = _menuType;
		}

		private void PlayAudio()
		{
			//throw new NotImplementedException();
		}

		public void Back()
		{
			if (prevMenu.Count <= 0) return;
			ActivateMenu(prevMenu.Peek());
			currentMenu = prevMenu.Pop();
		}

        public void ActivateMenu(MenuType _menuType)
		{
			menuContainers[(int)currentMenu].gameObject.SetActive(false);
			menuContainers[(int)_menuType].gameObject.SetActive(true);
		}
    }
}
