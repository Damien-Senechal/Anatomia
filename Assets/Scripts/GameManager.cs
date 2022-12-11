using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject UpCam;
    public GameObject DownCam;
    public GameObject ProfCam;
    public GameObject UI;
    public static bool[] verification = new bool[] {false, false, false, false};
    public bool check = false;
    public GameObject[] levels;
    public GameObject[] canvas;
    public int actualCanvas = 0;
    public int actualLevel = 0;
    public string[] objectifs = { 
        "tete",
        "mollet gauche",
        "mollet droit",
        "poignet gauche",
        "biceps gauche",
        "poignet droite",
        "biceps droit",
        "hanche gauche",
        "hanche droite" 
    };
    public string[] nomOs =
    {
        "crane",
        "cubitus droit",
        "cubitus gauche",
        "humerus droit",
        "humerus gauche",
        "femur droit",
        "femur gauche",
        "tibia droit",
        "tibia gauche",

    };
    public GameObject[] bonesmesh;
    public static string actualObjectif1;
    public static string actualObjectif2;
    public GameObject text1;
    public GameObject text2;
    public static bool inZoom = false;
    public GameObject sclapel;
    private float difference;
    public GameObject menuPause;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < verification.Length; i++)
        {
            verification[i] = false;
        }
        canvas[actualCanvas].SetActive(true);
        levels[actualLevel].SetActive(true);
        //Debug.Log(canvas[actualCanvas]);
        //Debug.Log(text.GetComponent<TextMeshProUGUI>().text);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            difference = currentMagnitude - prevMagnitude;
        }

        /*if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {*/
        if (difference < 0)
        {
            if(inZoom)
            {
                DownCam.transform.position = new Vector3(-0.874000013f, 2.5f, 3.96000004f);
                inZoom = false;
            }
            else
            {
                //Debug.Log("Up");
                if (UpCam.activeInHierarchy == false)
                {
                    UpCam.SetActive(true);
                }
                if (DownCam.activeInHierarchy == true)
                {
                    DownCam.SetActive(false);
                }
            }
        }
        /*else if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {*/
        else if (difference > 0)
        {
            //Debug.Log("Down");
            if (UpCam.activeInHierarchy == true)
            {
                UpCam.SetActive(false);
            }
            if (DownCam.activeInHierarchy == false)
            {
                DownCam.SetActive(true);
            }
        }

        if(actualLevel == 0)
        {
            for (int i = 0; i < verification.Length; i++)
            {
                if (verification[i] == false)
                {
                    check = false;
                    return;
                }
                else
                {
                    check = true;
                }
            }
        }
        else if(actualLevel == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                if (verification[i] == false)
                {
                    check = false;
                    return;
                }
                else
                {
                    check = true;
                }
            }
        }
        

        

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
            UI.SetActive(false);
        }
    }

    public void hideCanvas()
    {
        if (canvas[actualCanvas].activeInHierarchy)
        {
            canvas[actualCanvas].SetActive(false);
            UI.SetActive(true);
            if(actualCanvas == 0)
            {
                DownCam.SetActive(true);
                ProfCam.SetActive(false);
            }
        }
        if(actualCanvas == 2 || actualCanvas == 4)
        {
            ProfCam.SetActive(false);
            DownCam.SetActive(true);
        }
    }

    public void nextCanvas()
    {
        if(canvas[actualCanvas].activeInHierarchy)
        {
            hideCanvas();
        }
        actualCanvas++;
        displayCanvas();
        if(actualCanvas == 2)
        {
            int rand1 = Random.Range(0, objectifs.Length);
            int rand2 = rand1;
            while(rand2 == rand1)
            {
                rand2 = Random.Range(0, objectifs.Length);
            }
            text1.GetComponent<TextMeshProUGUI>().text = "Decouper les membres suivant : \n" + objectifs[rand1] + "\n" + objectifs[rand2];
            actualObjectif1 = objectifs[rand1];
            actualObjectif2 = objectifs[rand2];
        }
        else if(actualCanvas == 4)
        {
            int rand1 = Random.Range(0, nomOs.Length);
            int rand2 = rand1;
            while (rand2 == rand1)
            {
                rand2 = Random.Range(0, nomOs.Length);
            }
            text2.GetComponent<TextMeshProUGUI>().text = "Decouper les membres suivant : \n" + nomOs[rand1] + "\n" + nomOs[rand2];
            actualObjectif1 = nomOs[rand1];
            actualObjectif2 = nomOs[rand2];
        }
    }

    public void nextLevel()
    {
        if (levels[actualLevel].activeInHierarchy)
        {
            levels[actualLevel].SetActive(false);
        }
        actualLevel++;
        levels[actualLevel].SetActive(true);
        for (int i = 0; i < verification.Length; i++)
        {
            verification[i] = false;
        }
        if(actualLevel == 1)
        {
            DownCam.GetComponent<Camera>().orthographicSize = 0.47f;
            DownCam.transform.position = new Vector3(-1.01900005f, 1.86399996f, 3.98099995f);
        }
        

    }

    public int getActualLevel()
    {
        return actualLevel;
    }

    public int getActualCanvas()
    {
        return actualCanvas;
    }

    IEnumerator endOfLevel()
    {
        yield return new WaitForSeconds(2f);
        levels[0].SetActive(false);
        nextCanvas();
        DownCam.SetActive(false);
        ProfCam.SetActive(true);
    }

    public void activeMesh(int x)
    {
        bonesmesh[x].SetActive(true);
    }

    public void endOfTheGame()
    {
        canvas[actualCanvas].SetActive(false);
        canvas[5].SetActive(true);
    }

    public void exitTheGame()
    {
        Application.Quit();
    }

    public void pauseTheGame()
    {
        if(menuPause.activeInHierarchy)
        {
            //displayCanvas();
            menuPause.SetActive(false);
        }
        else
        {
            //hideCanvas();
            menuPause.SetActive(true);
        }
    }
}
