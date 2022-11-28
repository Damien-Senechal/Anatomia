using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    public Material mMaterial;

    private void Update()
    {

    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        else
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        else
        {
            transform.position = GetMouseWorldPos() + mOffset;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (this.name == "Femur a placer" && other.name == "Emplacement femur")
        {
            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            other.gameObject.GetComponent<Outline>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
        else if(this.name == "Crane a placer" && other.name == "Emplacement crane")
        {
            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            other.gameObject.GetComponent<Outline>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
        else if (this.name == "Cubitus a placer" && other.name == "Emplacement cubitus")
        {
            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            other.gameObject.GetComponent<Outline>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
        else if (this.name == "Humerus a placer" && other.name == "Emplacement Humerus")
        {
            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            other.gameObject.GetComponent<Outline>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
