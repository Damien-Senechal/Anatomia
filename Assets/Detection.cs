using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Detection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (this.name == "Detection" || other.name == "Detection")
        {
            other.GetComponent<Outline>().OutlineWidth = 4;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (this.name == "Detection" || other.name == "Detection")
        {
            other.GetComponent<Outline>().OutlineWidth = 0;
        }
    }
}
