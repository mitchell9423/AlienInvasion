using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alien.UI
{
    public class Menu : MonoBehaviour
    {
        public MenuController.MenuType menuType;

        public void MainMenu() => MenuController.Instance.OpenMenu(MenuController.MenuType.MainMenu);
        public void Restart() => LoadSceneOnClick.Instance.GetComponent<LoadSceneOnClick>().Quit();
        public void NewPlayer() => MenuController.Instance.OpenMenu(MenuController.MenuType.NewPlayerMenu);
        public void NewGame(TMPro.TextMeshProUGUI _text) => Save_Script.Instance.new_game(_text);
        public void Load() => MenuController.Instance.OpenMenu(MenuController.MenuType.SaveMenu);
        public void LoadGame(int slot) => Save_Script.Instance.Load_File(slot);
        public void WayPointMenu() => MenuController.Instance.OpenMenu(MenuController.MenuType.WayPointMenu);
        public void Levels() => MenuController.Instance.OpenMenu(MenuController.MenuType.LevelSelectionMenu);
        public void UpgradeMenu() => MenuController.Instance.OpenMenu(MenuController.MenuType.UpgradeMenu);
        public void HighScore() => MenuController.Instance.OpenMenu(MenuController.MenuType.HighScoresMenu);
        public void Instructions() => MenuController.Instance.OpenMenu(MenuController.MenuType.InstructionsMenu);
        public void Back() => MenuController.Instance.Back();
        public void Quit() => LoadSceneOnClick.Instance.GetComponent<LoadSceneOnClick>().Quit();
    }
}
