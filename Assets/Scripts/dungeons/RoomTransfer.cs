using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransfer : MonoBehaviour
{
    
    public float changeX,changeY;
    public float playerChangeX, playerChangeY;
    private CameraController cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.offset = new Vector3(changeX,changeY,0);
            cam.Roomchange();
            other.transform.position += new Vector3(playerChangeX, playerChangeY, 0);
        }
    }
}
