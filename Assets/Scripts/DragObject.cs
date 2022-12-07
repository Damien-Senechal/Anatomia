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
        if(gameManager.getActualLevel() == 2 && tag == "tozoom")
        {
            GameManager.inZoom = true;
            gameManager.DownCam.transform.position = repere.transform.position + new Vector3(0, 0.5f, 0);
            gameManager.sclapel.transform.position = repere.transform.position + new Vector3(0.1f, 0.1f, 0);
            /*if (name == "tete")
            {
                
            }
            else if (name == "biceps droit")
            {
                gameManager.DownCam.transform.position = new Vector3(-1.11099994f, 0.875f + 0.5f, 3.48600006f);
                gameManager.sclapel.transform.position = new Vector3(-1.11099994f + 0.1f, 0.875f + 0.1f, 3.48600006f);
            }
            else if (name == "biceps gauche")
            {
                gameManager.DownCam.transform.position = new Vector3(-0.629000008f, 0.875f + 0.5f, 3.48600006f);
                gameManager.sclapel.transform.position = new Vector3(-0.629000008f + 0.1f, 0.875f + 0.1f, 3.48600006f);
            }
            else if (name == "poignet droite")
            {
                gameManager.DownCam.transform.position = new Vector3(-1.21300006f, 0.875f + 0.5f, 3.6329999f);
                gameManager.sclapel.transform.position = new Vector3(-1.21300006f + 0.1f, 0.875f + 0.1f, 3.6329999f);
            }
            else if (name == "poignet gauche")
            {
                gameManager.DownCam.transform.position = new Vector3(-0.531000018f, 0.875f + 0.5f, 3.6329999f);
                gameManager.sclapel.transform.position = new Vector3(-0.531000018f + 0.1f, 0.875f + 0.1f, 3.6329999f);
            }
            else if (name == "hanche droite")
            {
                gameManager.DownCam.transform.position = new Vector3(-0.967000008f, 0.875f + 0.5f, 4.02799988f);
                gameManager.sclapel.transform.position = new Vector3(-0.967000008f + 0.1f, 0.875f + 0.1f, 4.02799988f);
            }
            else if (name == "hanche gauche")
            {
                gameManager.DownCam.transform.position = new Vector3(-0.764999986f, 0.875f + 0.5f, 4.02799988f);
                gameManager.sclapel.transform.position = new Vector3(-0.764999986f + 0.1f, 0.875f + 0.1f, 4.02799988f);
            }
            else if (name == "mollet droit")
            {
                gameManager.DownCam.transform.position = new Vector3(-1.01999998f, 0.875f + 0.5f, 4.32200003f);
                gameManager.sclapel.transform.position = new Vector3(-1.01999998f + 0.1f, 0.875f + 0.1f, 4.32200003f);
            }
            else if (name == "mollet gauche")
            {
                gameManager.DownCam.transform.position = new Vector3(-0.720000029f, 0.875f + 0.5f, 4.32200003f);
                gameManager.sclapel.transform.position = new Vector3(-0.720000029f + 0.1f, 0.875f + 0.1f, 4.32200003f);
            }*/
            //gameManager.DownCam.transform.position = transform.position + new Vector3(0, .5f, 0);
        }
        else
        {
            if (canGrab)
            {
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
            other.gameObject.GetComponent<DragObject>().canGrab = true;
            other.gameObject.GetComponent<BoxCollider>().enabled = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
