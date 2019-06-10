using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestSystemScript : MonoBehaviour
{
    
    public Quest currentQuest;
    private bool hasActiveQuest = false;
    public QuestCollection questCollection;

    void Start()
    {
        string path = Path.Combine(Application.dataPath + '/', Constants.HELPER_FOLDER, Constants.QUESTS_FILE);
        
        if(File.Exists(path))
        {
            string questsAsJson = File.ReadAllText(path);

            questCollection = JsonUtility.FromJson<QuestCollection>(questsAsJson);
            Debug.Log($"Quest one: {questCollection.quests[0].name} {questCollection.quests[0].objective}");
        }
        else
        {
            Debug.Log($"Couldn't find quests file. Path = {path}");
        }

        Random.InitState((int)Time.time);
    }

    void Update()
    {
        if(hasActiveQuest)
        {
            if(currentQuest.progress == currentQuest.objective)
            {
                Debug.Log("Finished quest! Whoa!");
                Debug.Log($"Current Progress: {currentQuest.progress}, Objective: {currentQuest.objective}");

                // Give player Score
                currentQuest = null;
                hasActiveQuest = false;
            }
        }
    }

    public void StartQuest()
    {
        if(!hasActiveQuest)
        {
            Debug.Log("Generating new quest!");
            int random = Random.Range(0, 0);

            currentQuest = questCollection.quests[random];
            hasActiveQuest = true;
        }
    }

    public void Track()
    {
        if(hasActiveQuest)
        {
            Debug.Log("Progressed on quest, oh my god!");
            currentQuest.progress++;
        }
    }
}
