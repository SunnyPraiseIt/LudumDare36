using UnityEngine;
using System.Collections;

public class Enabler : MonoBehaviour
{

    [SerializeField]
    GameObject stone = null;

    [SerializeField]
    GameObject[] portal;

    int i = 0;

    void Update()
    {
        if (i >= 10)
        {
            stone.SetActive(true);

            for(int j = 0; j < portal.Length; ++j)
                portal[i].SetActive(false);
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
