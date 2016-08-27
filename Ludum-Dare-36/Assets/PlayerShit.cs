using UnityEngine;
using System.Collections;

public class PlayerShit : MonoBehaviour
{

    float pickUpDist = 200f;
    [SerializeField]
    Transform carriedObj = null;

    bool holdingSomething = false;
    //int Layer = LayerMask.NameToLayer("PickUp");

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



        //Actions

        #region Actions
        if ((Input.GetMouseButton(0) && !holdingSomething)) //&& Physics.Raycast(transform.position, transform.forward, 25, Layer))
        {
           PickItemUp();
        }
        else if (Input.GetMouseButtonUp(0) && holdingSomething)
        {
            DropItem();
        }
        #endregion


    }

    void PickItemUp()
    {
        RaycastHit hit;
        Ray rey = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(rey,out hit, pickUpDist))
        {
            carriedObj = hit.collider.GetComponent<Collider>().transform;
        }
        
        if (carriedObj != null)
        {
            Destroy(carriedObj.GetComponent<Rigidbody>());
            carriedObj.parent = transform;
            holdingSomething = true;
        }
    }

    void DropItem()
    {
        carriedObj.parent = null;
        carriedObj.gameObject.AddComponent(typeof(Rigidbody));
        carriedObj = null;
    }
}
