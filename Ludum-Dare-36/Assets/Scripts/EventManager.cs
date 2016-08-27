using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class EventManager : MonoBehaviour
{
   
     List<UnityEvent> Actions = new List<UnityEvent>();
    void OnEnable()
    {
        for (int i = 0; i < 6; i++)
        {
            UnityEvent Checkpoint = new UnityEvent();
            Actions.Add(Checkpoint);
        }
    }
    /*
        Actions[0] = Checkpoint 1 complete
          Actions[1] = Checkpoint 2 complete
            Actions[2] = Checkpoint 3 complete
              Actions[3] = Checkpoint 4 complete
                Actions[4] = GameWin
                  Actions[5] = GameLose
        */

    void Start()
    {

    }

    public void AddLisener(int i, UnityAction function)
    {
        Actions[i].AddListener(function);
    }

    public void Activate(int index)
    {
        if (index < Actions.Count)
            Actions[index].Invoke();
        Debug.Log("Activate");
        
    }

 
   

    
    
}

