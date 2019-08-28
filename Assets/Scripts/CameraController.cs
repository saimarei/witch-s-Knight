using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject roomroot;
    public Vector3 offset = new Vector3(0,0,-10);
    // Start is called before the first frame update
    void Start()
    {
        transform.position = roomroot.transform.position + offset;
        offset.z = 0;
    }

    // Update is called once per frame
    public void Roomchange()
    {
        transform.position += offset;
        
    }
}
