using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject UpCam;
    public GameObject DownCam;
    public static bool[] verification = new bool[] {false, false, false, false};
    public bool check = false;
    public GameObject[] levels;
    public GameObject[] canvas;
    public int actualCanvas = 0;
    public int actualLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < verification.Length; i++)
        {
            verification[i] = false;
        }
        Debug.Log(canvas[actualCanvas]);
        canvas[actualCanvas].SetActive(true);
        levels[actualLevel].SetActive(true);
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

        
        for (int i = 0; i < verification.Length; i++)
        {
            if(verification[i] == false)
            {
                check = false;
                return;
            }
            else
            {
                check = true;
            }
        }

        //Debug.Log(check);

        if(check)
        {
            Debug.Log("MES ENORMES BOULES");
            check = false;
            for (int i = 0; i < verification.Length; i++)
            {
                verification[i] = false;
            }
            StartCoroutine("endOfLevel");
        }
    }

    public void displayCanvas()
    {
        if (!canvas[actualCanvas].activeInHierarchy)
        {
            canvas[actualCanvas].SetActive(true);
        }
    }

    public void hideCanvas()
    {
        if (canvas[actualCanvas].activeInHierarchy)
        {
            canvas[actualCanvas].SetActive(false);
        }
    }

    public void nextCanvas()
    {
        if(canvas[actualCanvas].activeInHierarchy)
        {
            canvas[actualCanvas].SetActive(false);
        }
        actualCanvas++;
        canvas[actualCanvas].SetActive(true);
    }

    public void nextLevel()
    {
        if (levels[actualLevel].activeInHierarchy)
        {
            levels[actualLevel].SetActive(false);
        }
        actualLevel++;
        levels[actualLevel].SetActive(true);
    }

    IEnumerator endOfLevel()
    {
        yield return new WaitForSeconds(2f);
        levels[0].SetActive(false);
        nextCanvas();
    }
}
