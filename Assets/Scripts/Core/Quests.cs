using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quests : MonoBehaviour
{
    [SerializeField] UnityEvent completeQuest;
    [SerializeField] bool isQuestComplete = false;
    [SerializeField] int totalStepToComplete;
    [SerializeField] int stepsCompleted;

    private void Update()
    {
        if (isQuestComplete)
        {
            completeQuest.Invoke();
        }
    }

    public void CompleteStep()
    {
        stepsCompleted++;
        if(stepsCompleted >= totalStepToComplete)
        {
            isQuestComplete = true;
        }
    }

}
