using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallProgress : MonoBehaviour
{
    public float wallSpeed = 0.005f;
    private Transform wallTransform;

    // Start is called before the first frame update

    void Start()
    {
        wallTransform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        wallTransform.position += new Vector3(wallSpeed,0,0);
    }
}
