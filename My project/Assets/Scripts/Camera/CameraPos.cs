using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public float distanceAway = 7f;
    public float distanceUp = 4f;
    public Transform follow; //플레이어 값 참조
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //카메라의 위치를 distanceUp만큼 위에, distanceAway만큼 앞에 위치시킨다.
        transform.position = follow.position + Vector3.up * distanceUp - Vector3.forward * distanceAway; 
    }
}
