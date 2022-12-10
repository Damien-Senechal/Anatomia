using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
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
        StartCoroutine(LaunchGame(1));
    }

    public void Exit()
    {

    }
    
    IEnumerator LaunchGame(int x)
    {
        yield return new WaitForSeconds(x);
        SceneManager.LoadScene(1);
    }

    IEnumerator ExitGame(int x)
    {
        yield return new WaitForSeconds(x);
        Application.Quit();
    }
}
