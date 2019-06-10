using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperScript : MonoBehaviour{
    public float initialPosition = 0f;
    public float pressedPosition = 45f;
    public float hitStrenght = 10000f;
    public float flipperDamper = 150f;
    public string inputName;
    
    HingeJoint hinge;

    private QuestSystemScript questSystem;

    // Start is called before the first frame update
    void Start() {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;

        questSystem = GameObject.FindGameObjectWithTag(Constants.QUEST_SYSTEM_TAG).GetComponent<QuestSystemScript>();

        if(questSystem == null)
        {
            Debug.Log("Couldn't Find quest system!");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(CollisionHelper.DidCollideWithSphere(other.gameObject.tag))
        {
            TemporaryGenerateQuest();
        }
    }

    // Update is called once per frame
    void Update() {
        JointSpring spring = new JointSpring();
        spring.spring = hitStrenght;
        spring.damper = flipperDamper;

        if (Input.GetAxis(inputName) == 1) {
            spring.targetPosition = pressedPosition;
        }
        else {
            spring.targetPosition = initialPosition;
        }

        hinge.spring = spring;
        hinge.useLimits = true;
    }

    void TemporaryGenerateQuest()
    {
        questSystem.StartQuest();
    }
}
