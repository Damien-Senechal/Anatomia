using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;
    public GameObject fondu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        StartCoroutine(LaunchGame());
    }

    public void Exit()
    {

    }
    
    IEnumerator LaunchGame()
    {
        camera2.GetComponent<CinemachineVirtualCamera>().Priority = 7;
        fondu.SetActive(true);
        yield return new WaitForSeconds(3);
        
        SceneManager.LoadScene(1);
    }

    IEnumerator ExitGame(int x)
    {
        yield return new WaitForSeconds(x);
        Application.Quit();
    }

    public void lol()
    {
        
    }
}
