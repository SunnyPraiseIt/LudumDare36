using UnityEngine;
using System.Collections;

public class PlayerShit : MonoBehaviour
{

    float pickUpDist = 200f;
    [SerializeField]
    GameObject carriedObj = null;

    bool holdingSomething = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        //Actions

        #region Actions
        if ((Input.GetMouseButtonDown(0) && !holdingSomething))
        {
            Debug.Log("PICK UP");
            PickItemUp();
        }
        else if (Input.GetMouseButtonDown(1) && holdingSomething)
        {
            DropItem();
        }
        #endregion


    }

    void PickItemUp()
    {
        RaycastHit hit;
        Ray rey = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        bool why = Physics.Raycast(rey, out hit, 20);
        Debug.DrawLine(transform.position, hit.point, Color.red, 300);
        if (why)
        {
            Debug.Log("RAY HIT");
            PickUP p = hit.collider.GetComponent<PickUP>();
            if (p != null)
            {
                Debug.Log("Pick me the fuck up");
                carriedObj = p.gameObject;
                carriedObj.transform.parent = Camera.main.transform;
                holdingSomething = true;
                p.gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }

    void DropItem()
    {
        carriedObj.gameObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObj = null;
        holdingSomething = false;
    }

}
