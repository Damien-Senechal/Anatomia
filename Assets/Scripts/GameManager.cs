using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject UpCam;
    public GameObject DownCam;
    public GameObject DownCam2;
    public GameObject ProfCam;
    public GameObject cameraBrain;
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
    public GameObject fleche;
    public GameObject text1;
    public GameObject text2;
    public static bool inZoom = false;
    public GameObject sclapel;
    private float difference = 0;
    public GameObject menuPause;
    public GameObject Schema;
    public GameObject Schema2;
    public int Malus = 0;
    public GameObject skeleton;
    public GameObject textObjectif;
    public GameObject textObjectif2;
    public GameObject fondu;

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
        if(Malus/2 > 3)
        {
            exitTheGame();
        }
        if (Input.touchCount == 2)
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
                fonduGo();
                /*if(UpCam.GetComponent<CinemachineVirtualCamera>().Priority == 0)
                {
                    cameraBrain.GetComponent<Camera>().orthographic = false;
                    UpCam.GetComponent<CinemachineVirtualCamera>().Priority = 1;
                }
                if (DownCam.GetComponent<CinemachineVirtualCamera>().Priority == 1)
                {
                    DownCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
                }*/
            }
            difference = 0;
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
            fonduGo();
            /*if (UpCam.GetComponent<CinemachineVirtualCamera>().Priority == 1)
            {
                UpCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
            }
            if (DownCam.GetComponent<CinemachineVirtualCamera>().Priority == 0)
            {
                cameraBrain.GetComponent<Camera>().orthographic = true;
                DownCam.GetComponent<CinemachineVirtualCamera>().Priority = 1;
            }*/
            difference = 0;
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
                fonduGo();

            }
        }
        if(actualCanvas == 2 || actualCanvas == 4)
        {
            ProfCam.SetActive(false);
            DownCam2.SetActive(true);
            fonduGo();
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
            text1.GetComponent<TextMeshProUGUI>().text = "Cut the following members : \n" + objectifs[rand1] + "\n" + objectifs[rand2] +"\n And places them in the box on the left";
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
            text2.GetComponent<TextMeshProUGUI>().text = "Cut the following members : \n" + nomOs[rand1] + "\n" + nomOs[rand2];
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
            textObjectif.GetComponent<TextMeshProUGUI>().text = "Goal : Cut " + actualObjectif1 + " and " + actualObjectif2;
            textObjectif2.GetComponent<TextMeshProUGUI>().text = "Goal : Cut " + actualObjectif1 + " and " + actualObjectif2;
            fleche.SetActive(true);
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
        ProfCam.SetActive(true);
        DownCam.SetActive(false);
        fonduGo();
        Debug.Log("TA MERE LA PLUS GROSSE PUTE DU MONDE CEST BON LA ?");
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

    public void afficherSchema()
    {
        if(actualLevel == 0)
        {
            if(Schema.activeInHierarchy)
            {
                Schema.SetActive(false);
            }
            else
            {
                Schema.SetActive(true);
            }
        }
        else if(actualLevel == 1)
        {
            if (Schema2.activeInHierarchy)
            {
                Schema2.SetActive(false);
            }
            else
            {
                Schema2.SetActive(true);
            }
        }
    }

    public void shake()
    {
        skeleton.GetComponent<Animator>().Play("Shake");
    }
    
    public void ehoh()
    {
        Debug.Log("ehoh");
    }

    public void fonduGo()
    {
        StartCoroutine("Fondu");
    }
        
    IEnumerator Fondu()
    {
        fondu.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        fondu.SetActive(false);
    }
}
