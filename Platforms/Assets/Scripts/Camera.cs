using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    public Transform PlayerTransform;
    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = (transform.position - player.transform.position)+new Vector3(0,16,0);        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player.transform.position.y<-2){
        Vector3 newPos = player.transform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        }
    }
}
