using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (this.tag == "Exit")
        {
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.tag == "Exit")
        {
            var exitRenderer = this.GetComponent<Renderer>();
            exitRenderer.material.SetColor("_Color", Color.black);
        }
    }
}
