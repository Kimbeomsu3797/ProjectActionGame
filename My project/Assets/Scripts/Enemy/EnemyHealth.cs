using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int stratingHealth = 100;
    public int currentHealth;

    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    public float sinkSpeed = 1f;

    bool isDead;
    bool isSinking;
    bool damaged;
    private void Awake()
    {
        currentHealth = stratingHealth;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = true;
        StartSinking();
    }
    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }

    public IEnumerator StartDamege(int damage, Vector3 playerPosition, float delay, float pushBack)//넉백처리 중요!
    {
        yield return new WaitForSeconds(delay);

        try//이걸 실행해보고 문제가 없다면 실행
        {
            TakeDamage(damage);
            Vector3 diff = playerPosition - transform.position;
            diff = diff / diff.sqrMagnitude;
            GetComponent<Rigidbody>().
            AddForce((transform.position - new Vector3(diff.x, diff.y, 0f)) * 50f * pushBack);

        }
        catch(MissingReferenceException e)// 문제가 있다면 에러메세지 출력
        {
            Debug.Log(e.ToString());
        }
        //예외처리문
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", flashColor);
        }
        else
        {
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor
                ("_OutlineColor", Color.Lerp(transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_OutlineColor"),
                Color.black, flashSpeed * Time.deltaTime));
            damaged = false;

            if (isSinking)
            {
                transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
            }
        }
    }
}
