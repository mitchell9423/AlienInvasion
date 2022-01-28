using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Save_Script : MonoBehaviour
{
    public static Save_Script Instance { get; private set; }
    public static int minScore, score = 0, streak = 1, display = 1;

    public Text highScores;
    public Text playerName;
    public TextMeshProUGUI[] saveSlot;
    public Text playerNanites;
    public GameObject NewPlayerPanel, SavedGames, Waypoint;

    //convert to private
    public string pname;
    public float healthMod;
    public float capMod;
    public float rechargeMod;
    public float fireRateMod;
    public float speedMod;
    public int nanites;


    GameObject player;
    string data;
    Plant[] trees;
    Crate[] crate;
    Weapon_Manager pScript;
    public LoadSceneOnClick loadScene;
    List<HighScoreScript> highScoreList = new List<HighScoreScript>();

    private void Awake()
	{
        if (Instance == null) Instance = this;
	}

    public void Start()
    {
        load_saves();
        readHighScore();
    }

    public void Update()
    {
        if (Player.isDead)
            save();
        playerNanites.text = "Nanite Material: " + nanites.ToString();
    }

    public void upgrade(int choice)
    {
        if (nanites > 1000)
            switch (choice)
            {
                case 1:
                    healthMod += 1.0f;
                    nanites -= 1000;
                    break;
                case 2:
                    capMod += 1.0f;
                    rechargeMod += 1.0f;
                    nanites -= 1000;
                    break;
                case 3:
                    fireRateMod += .1f;
                    nanites -= 1000;
                    break;
                case 4:
                    speedMod += 1.0f;
                    nanites -= 1000;
                    break;
            }
    }

    public void new_game(TextMeshProUGUI _text) => new_game(_text.text);

    public void new_game(string _text)
    {
        //pname = playerName.text;
        pname = _text;
        //healthMod = 1;
        //capMod = 1;
        //powerMod = 1;
        //speedMod = 1;
        if (pname != "Player Name:")
        {
            for (int i = 0; i < 7; i++)
            {
                if (saveSlot[i].text == pname)
                {
                    break;
                }
                else if (string.IsNullOrEmpty(saveSlot[i].text))
                {
                    saveSlot[i].text = pname;
                    break;
                }
            }
        }

        StreamWriter sw = new StreamWriter(Application.dataPath + "/" + "saveFiles" + ".txt");
        foreach (TextMeshProUGUI t in saveSlot)
            sw.WriteLine(t.text);
        sw.Close();
    }

    public void load_saves()
    {
        StreamReader sr = null;
        try
        {
            sr = new StreamReader(Application.dataPath + "/" + "saveFiles" + ".txt");
            foreach (TextMeshProUGUI t in saveSlot)
            {
                t.text = sr.ReadLine();
                if (t.text == "")
                    t.text = "Empty";
            }
            sr.Close();
        }
        catch (FileNotFoundException)
        {
            foreach (TextMeshProUGUI t in saveSlot)
                t.text = "Empty";
        }
        finally
        {
            if (sr != null)
                sr.Close();
        }
    }

    public void save()
    {
        pname = playerName.text;
        StreamWriter sw = new StreamWriter(Application.dataPath + "/" + pname + ".txt");
        sw.WriteLine(pname + "," + healthMod + "," + capMod + "," + rechargeMod + "," + fireRateMod + "," + speedMod + "," + nanites);
        sw.Close();
    }

    public static void score_keeper()
    {
        if (streak > 29)
        {
            display = 4;
            score += 4;
        }
        else if (streak > 19)
        {
            display = 3;
            score += 3;
        }
        else if (streak > 9)
        {
            display = 2;
            score += 2;
        }
        else
        {
            display = 1;
            score++;
        }
        streak++;
    }

    private string[] fragNext(StreamReader sr)
    {
        string[] frag = null;
        string data = sr.ReadLine();
        char[] delim = { ',', '(', ')' };
        if (data != null)
            frag = data.Split(delim);
        return frag;
    }
    
    public void Load_File(int slot)
    {
        string[] frag;
        StreamReader sr = null;
        try
        {
            sr = new StreamReader(Application.dataPath + "/" + saveSlot[slot].text + ".txt");
            frag = fragNext(sr);
            pname = frag[0];
            healthMod = float.Parse(frag[1]);
            capMod = float.Parse(frag[2]);
            rechargeMod = float.Parse(frag[3]);
            fireRateMod = float.Parse(frag[4]);
            speedMod = float.Parse(frag[5]);
            nanites = System.Int32.Parse(frag[6]);
            frag = fragNext(sr);
        }
        catch
        {
            sr = null;
            saveSlot[slot].text = "Empty";
            new_game(saveSlot[slot].text);
            NewPlayerPanel.SetActive(true);
        }
        finally
        {
            if (sr != null)
            {
                playerName.text = saveSlot[slot].text;
                Waypoint.SetActive(true);
                sr.Close();
            }
        }
    }

    public void readHighScore()
    {
        string[] frag;
        highScores.text = "";
        highScoreList.Clear();
        StreamReader sr = null;
        try
        {
            sr = new StreamReader(Application.dataPath + "/highScore.txt");
            string s = sr.ReadLine();

            while (s != null && s != "")
            {
                frag = s.Split(',');
                highScoreList.Add(new HighScoreScript(frag[0], System.Int32.Parse(frag[1])));
                s = sr.ReadLine();
            }
            sr.Close();

            if (highScoreList.Count > 0)
            {
                foreach (HighScoreScript p in highScoreList)
                    highScores.text += p.initials + "   " + p.score + "\n";
                if (highScoreList.Count > 4)
                    minScore = highScoreList[highScoreList.Count - 1].score;
            }
        }
        catch (FileNotFoundException)
        {
        }
        finally
        {
            if (sr != null)
                sr.Close();
        }
    }

    public void saveHighScore(string name)
    {
        readHighScore();

        highScoreList.Add(new HighScoreScript(name, score));

        if (highScoreList.Count > 1)
            highScoreList.Sort();

        while (highScoreList.Count > 5)
        {
            highScoreList.RemoveAt(5);
        }

        highScores.text = "";

        foreach (HighScoreScript p in highScoreList)
            highScores.text += p.initials + "   " + p.score + "\n";

        StreamWriter sw = new StreamWriter(Application.dataPath + "/highScore.txt");
        foreach (HighScoreScript p in highScoreList)
        {
            string print = p.initials + "," + p.score;
            sw.WriteLine(print);
        }
        sw.Close();
    }
}
