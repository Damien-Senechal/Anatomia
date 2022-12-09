using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    public Material mMaterial;
    public bool canGrab = false;
    private Vector3 initialPosition;
    public GameManager gameManager;
    public GameObject repere;
    public Vector3 scaleToHave;


    private void Start()
    {
        //Debug.Log(mMaterial);
        initialPosition = transform.position;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {

    }

    private void OnMouseDown()
    {
        if(scaleToHave != Vector3.zero)
        {
            transform.localScale = scaleToHave;
            scaleToHave = Vector3.zero;
        }
        if(gameManager.getActualLevel() == 2 && tag == "tozoom")
        {
            GameManager.inZoom = true;
            gameManager.DownCam.transform.position = repere.transform.position + new Vector3(0, 0.5f, 0);
            gameManager.sclapel.transform.position = repere.transform.position + new Vector3(0.1f, 0.1f, 0);
        }
        else
        {
            if (canGrab)
            {
                if(name == "scie")
                {
                    if(transform.localEulerAngles.y != 270)
                    {
                        transform.Rotate(0, 0, 90);
                    }
                    transform.position += new Vector3(0, .5f, 0);
                }
                else if(name == "scalpel")
                {
                    if (transform.localEulerAngles.y != -45)
                    {
                        transform.Rotate(-90, 0, 45);
                    }
                }
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                    mOffset = gameObject.transform.position - GetMouseWorldPos();
                    GetComponent<Rigidbody>().useGravity = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
        
    }

    private void OnMouseUp()
    {
        if (canGrab)
        {
            if(name == "scie")
            {
                transform.position -= new Vector3(0, .5f, 0);
            }
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
            }
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
        if(canGrab)
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "poubelle" && this.name == GameManager.actualObjectif1)
        {
            Debug.Log("ALLO1");
            if (GameManager.verification[0] == false)
            {
                GameManager.verification[0] = true;
            }
        }
        else if(other.name == "poubelle" && this.name == GameManager.actualObjectif2)
        {
            Debug.Log("ALLO2");
            if (GameManager.verification[1] == false)
            {
                GameManager.verification[1] = true;
            }
        }
        if(other.name == "GroundDetection")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = initialPosition+new Vector3(0,.5f,0);

        }
        //Debug.Log(this.name);
        if (this.name == "Femur a placer" && other.name == "Emplacement femur")
        {
            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            other.gameObject.GetComponent<Outline>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            GameManager.verification[0] = true;
        }
        else if(this.name == "Crane a placer" && other.name == "Emplacement crane")
        {
            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            other.gameObject.GetComponent<Outline>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            GameManager.verification[1] = true;
        }
        else if (this.name == "Cubitus a placer" && other.name == "Emplacement cubitus")
        {
            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            other.gameObject.GetComponent<Outline>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            GameManager.verification[2] = true;
        }
        else if (this.name == "Humerus a placer" && other.name == "Emplacement Humerus")
        {
            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            other.gameObject.GetComponent<Outline>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            GameManager.verification[3] = true;
        }
        else if (this.name == "Crane a placer" && other.name == "Emplacement crane2")
        {
            other.gameObject.GetComponent<Outline>().enabled = false;
        }
        else if (this.name == "Lame" && other.name == "biceps gauche")
        {
            if(gameManager.getActualLevel() == 2)
            {

            }
            else if(gameManager.actualLevel == 1)
            {
                other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
                //other.gameObject.GetComponent<Outline>().enabled = false;
                other.gameObject.GetComponent<DragObject>().canGrab = true;
                other.gameObject.GetComponent<BoxCollider>().enabled = true;
                other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        else if (this.name == "Lame" && other.name == "biceps droit")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            other.gameObject.GetComponent<DragObject>().canGrab = true;
            other.gameObject.GetComponent<BoxCollider>().enabled = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (this.name == "Lame" && other.name == "poignet gauche")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            other.gameObject.GetComponent<DragObject>().canGrab = true;
            other.gameObject.GetComponent<BoxCollider>().enabled = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (this.name == "Lame" && other.name == "poignet droite")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            other.gameObject.GetComponent<DragObject>().canGrab = true;
            other.gameObject.GetComponent<BoxCollider>().enabled = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (this.name == "Lame" && other.name == "tete")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            other.gameObject.GetComponent<DragObject>().canGrab = true;
            other.gameObject.GetComponent<BoxCollider>().enabled = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (this.name == "Lame" && other.name == "hanche gauche")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            other.gameObject.GetComponent<DragObject>().canGrab = true;
            other.gameObject.GetComponent<BoxCollider>().enabled = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (this.name == "Lame" && other.name == "hanche droite")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            other.gameObject.GetComponent<DragObject>().canGrab = true;
            other.gameObject.GetComponent<BoxCollider>().enabled = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (this.name == "Lame" && other.name == "mollet gauche")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            other.gameObject.GetComponent<DragObject>().canGrab = true;
            other.gameObject.GetComponent<BoxCollider>().enabled = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (this.name == "Lame" && other.name == "mollet droit")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            other.gameObject.GetComponent<DragObject>().canGrab = true;
            other.gameObject.GetComponent<BoxCollider>().enabled = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (this.name == "scalpel" && other.name == "biceps droit")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            gameManager.activeMesh(2);
            other.tag = "Untagged";
        }
        else if (this.name == "scalpel" && other.name == "biceps gauche")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            gameManager.activeMesh(2);
            other.tag = "Untagged";
        }
        else if (this.name == "scalpel" && other.name == "poignet droit")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            gameManager.activeMesh(2);
            other.tag = "Untagged";
        }
        else if (this.name == "scalpel" && other.name == "poignet gauche")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            gameManager.activeMesh(2);
            other.tag = "Untagged";
        }
        else if (this.name == "scalpel" && other.name == "hanche droite")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            gameManager.activeMesh(2);
            other.tag = "Untagged";
        }
        else if (this.name == "scalpel" && other.name == "hanche gauche")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            gameManager.activeMesh(2);
            other.tag = "Untagged";
        }
        else if (this.name == "scalpel" && other.name == "mollet droit")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            gameManager.activeMesh(2);
            other.tag = "Untagged";
        }
        else if (this.name == "scalpel" && other.name == "mollet gauche")
        {

            other.gameObject.GetComponent<MeshRenderer>().material = mMaterial;
            //other.gameObject.GetComponent<Outline>().enabled = false;
            gameManager.activeMesh(2);
            other.tag = "Untagged";
        }
    }
}
