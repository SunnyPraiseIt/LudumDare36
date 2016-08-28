using UnityEngine;
using System.Collections;

public class Enabler : MonoBehaviour
{

    [SerializeField]
    GameObject stone = null;
    GameObject portal = null;

    int i = 0;

    void Update()
    {
        if (i >= 10)
        {
            stone.SetActive(true);
            portal.SetActive(false);
        }
    }
    void OnTriggerEnter()
    {
        i++;
    }

    void OnTriggerExit()
    {
        i--;
    }
}
