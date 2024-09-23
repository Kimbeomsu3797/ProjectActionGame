using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTarget : MonoBehaviour
{
    public List<Collider> targetList;
    private void Awake()
    {
        targetList = new List<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        targetList.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        targetList.Remove(other);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
