using System;
using UnityEngine;

[Serializable]
public class Quest 
{
    public string name;
    public int score;
    public string description;

    public int objective;
    public int progress;
}

[Serializable]
public class QuestCollection
{
    public Quest[] quests;
}