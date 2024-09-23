using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public float distanceAway = 7f;
    public float distanceUp = 4f;
    public Transform follow; //�÷��̾� �� ����
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ī�޶��� ��ġ�� distanceUp��ŭ ����, distanceAway��ŭ �տ� ��ġ��Ų��.
        transform.position = follow.position + Vector3.up * distanceUp - Vector3.forward * distanceAway; 
    }
}
