using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreScript : IComparable<HighScoreScript>
{
    public string initials;
    public int score;

    public HighScoreScript(string newInitials, int newScore)
    {
        initials = newInitials;
        score = newScore;
    }

    public int CompareTo(HighScoreScript other)
    {
        int diff = other.score - score;

        if (diff != 0)
            return diff;

        diff = initials.CompareTo(other.initials);

        return diff;
    }

    public override string ToString()
    {
        return initials + "," + score;
    }
}
