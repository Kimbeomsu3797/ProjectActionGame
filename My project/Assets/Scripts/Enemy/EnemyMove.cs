using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //이런걸 게임매니저에 넣어놓고 값가져와서 적으면 최적화가 될까?
        nav = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nav.enabled)
        {
            nav.SetDestination(player.position);
        }
    }
}
