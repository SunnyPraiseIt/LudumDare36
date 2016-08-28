using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;



public class EventManager : MonoBehaviour
{
    public enum EVENTS
    {
      
      Puzzle1Complete,
      Puzzle2Start,
      Puzzle2Complete,
      Puzzle3Start,
      Puzzle3Complete,
      Puzzle4Start,
      Puzzle4Complete,
      WinGame,
      LoseGame
             
    };

    List<UnityEvent> Actions = new List<UnityEvent>();
    void OnEnable()
    {
        for (int i = 0; i < 6; i++)
        {
            UnityEvent Checkpoint = new UnityEvent();
            Actions.Add(Checkpoint);
        }
    }

    void Start()
    {

    }

    public void AddListener(int i, UnityAction function)
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

