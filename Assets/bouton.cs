using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouton : MonoBehaviour
{
    public GameObject animSprite;
    public Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void suce()
    {
        animSprite.SetActive(true);
    }
}
