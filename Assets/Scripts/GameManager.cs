using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject UpCam;
    public GameObject DownCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Debug.Log("Up");
            if (UpCam.activeInHierarchy == false)
            {
                UpCam.SetActive(true);
            }
            if (DownCam.activeInHierarchy == true)
            {
                DownCam.SetActive(false);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Debug.Log("Down");
            if (UpCam.activeInHierarchy == true)
            {
                UpCam.SetActive(false);
            }
            if (DownCam.activeInHierarchy == false)
            {
                DownCam.SetActive(true);
            }
        }
    }
}
