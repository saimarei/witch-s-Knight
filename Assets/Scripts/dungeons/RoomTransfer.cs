using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransfer : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector2 playerChange;
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
        if (other.CompareTag("player"))
        {

        }
    }
}
