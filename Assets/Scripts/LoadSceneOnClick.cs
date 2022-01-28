using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{

    public static GameObject Instance;

    public static bool load = false;
    public static bool newGame;
    public Text playerInitials, currentScore, currentHealth, currentAmmo, currentShells, currentNanite;
    public GameObject escapeMenu, mainMenu, scoreBoard, textbox, levelSelect, savedGames, wayPoint;
    public Save_Script save;
    public Button upgrade;
    Canvas minimap = null;

    public int returnPath;
    Player playerScript;
    bool isdeathscene;
    float percentage;

    private void Start()
    {
        dont_destroy();
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                escape_menu();
        }


        if (GameObject.Find("Canvas"))
            if (minimap == null)
                minimap = GameObject.Find("Canvas").GetComponent<Canvas>();

        addInitials();
        Access_Control();
    }

    public void Access_Control()
    {
        if (wayPoint.activeSelf == true)
        {
            if (save.playerName.text == "Player Name:")
                upgrade.enabled = false;
            else
                upgrade.enabled = true;
        }
    }

    public void overlay()
    {
        currentScore.text = "Score: " + Save_Script.score.ToString() + "    Streak x" + Save_Script.display.ToString();
        currentShells.text = "Warheads: " + Weapon_Three.shells;
        currentNanite.text = "Nanite: " + save.nanites;

        if (playerScript != null)
        {
            percentage = (playerScript.playerHealth / (playerScript.maxHealth * save.healthMod)) * 100.0f;
            if (percentage > 1.0f)
                currentHealth.text = "Reactor: " + Mathf.RoundToInt(percentage).ToString() + "%";
            else if (percentage > 0.0f)
                currentHealth.text = "Reactor: 1%";
            else
                currentHealth.text = "Reactor: 0%";
            
            percentage = (playerScript.cap / (playerScript.maxCap * save.capMod)) * 100.0f;
            if (percentage > 1.0f)
                currentHealth.text += "   Cap: " + Mathf.RoundToInt(percentage).ToString() + "%";
            else if (percentage > 0.0f)
                currentHealth.text += "   Cap: 1%";
            else
                currentHealth.text += "   Cap: 0%";
        }
		else
        {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (!playerScript) Debug.LogError("Cannot Find Player Script");
        }
    }

    public void LoadByIndex(int sceneIndex)
	{
        isdeathscene = false;
        if (sceneIndex == 5)
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.UnloadSceneAsync(sceneIndex);
        }

        SceneManager.LoadScene(sceneIndex);
        pause(1);
    }

    public void loadFile(bool l)
    {
        load = l;
    }

    public void set_return_path(int indexer)
    {
        returnPath = indexer;
    }

    public void back()
    {
        switch(returnPath)
        {
            case 1:
                mainMenu.SetActive(true);
                break;
            case 2:
                escapeMenu.SetActive(true);
                break;
            case 3:
                levelSelect.SetActive(true);
                break;
            case 4:
                savedGames.SetActive(true);
                break;
            case 5:
                wayPoint.SetActive(true);
                break;
        }
    }

    public void escape_menu()
    {
        if (escapeMenu.activeSelf)
        {
            escapeMenu.SetActive(false);
            pause(1);
            minimap.enabled = true;
        }
        else
        {
            minimap.enabled = false;
            pause(0);
            escapeMenu.SetActive(true);
        }
    }

    public void addInitials()
    {
        if (Player.isDead || Controller.isComplete)
        {
            if (Save_Script.score > Save_Script.minScore && !isdeathscene)
            {
                minimap.enabled = false;
                isdeathscene = true;
                textbox.SetActive(true);
            }
            else if (!escapeMenu.activeSelf && !isdeathscene)
            {
                minimap.enabled = false;
                isdeathscene = true;
                returnPath = 5;
                Invoke("back", 7.0f);
            }
        }
    }


    public void revive()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Player.isDead = false;
            isdeathscene = false;
            playerScript.playerHealth = playerScript.maxHealth * save.healthMod;
            Save_Script.score = 0;
            Weapon_Three.shells = 3;
            minimap.enabled = true;
        }
    }

    public void Save_Initials()
    {
        save.saveHighScore(playerInitials.text);
    }

    public void pause(int x)
    {
        Time.timeScale = x;
    }

    public void new_game(bool x)
    {
        newGame = true;
    }

    public void dont_destroy()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this.gameObject;
        }
    }
    
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
