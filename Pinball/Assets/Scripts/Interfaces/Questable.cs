using UnityEngine;

public abstract class Questable : MonoBehaviour
{
    protected QuestSystemScript questSystem;

    protected void Start()
    {
        questSystem = GameObject.FindGameObjectWithTag(Constants.QUEST_SYSTEM_TAG).GetComponent<QuestSystemScript>();
    }
    
    protected void OnCollisionEnter(Collision other)
    {
        if(CollisionHelper.DidCollideWithSphere(other.gameObject.tag))
        {
            Progress();
        }
    }

    public void Progress()
    {
        questSystem.Track();
    }
}