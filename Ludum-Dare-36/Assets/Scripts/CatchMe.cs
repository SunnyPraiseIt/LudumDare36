using UnityEngine;
using System.Collections;

public class CatchMe : MonoBehaviour
{
    [SerializeField]
    GameObject finalLocal = null;

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            if (c.GetComponentInChildren<PlayerShit>().isTime)
                c.transform.position = finalLocal.transform.position;
            else
                c.transform.position = c.GetComponentInChildren<PlayerShit>().originPos;
        }
        else if (c.tag == "PickUp")
        {
            c.transform.position = new Vector3(Random.Range(-300, 301),
                150,
                Random.Range(-300, 301));
            c.GetComponent<Rigidbody>().velocity = new Vector3();
        }
    }
}
