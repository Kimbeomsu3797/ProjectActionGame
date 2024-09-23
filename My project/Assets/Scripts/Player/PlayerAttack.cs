using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int NormalDamage = 10;
    public int SkillDamage = 30;
    public int DashDamage = 30;

    public float attackRange; // 이거 왜필요함? 필요없지않나
    public SkillTarget ST;
    public NormalTarget NT;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NormalAttack()
    {
        List<Collider> target = new List<Collider>(NT.targetList);
        foreach(Collider attackTarget in target)
        {
            EnemyHealth enemyHP = attackTarget.GetComponent<EnemyHealth>();
            if(enemyHP != null)
            {
                StartCoroutine(enemyHP.StartDamege(NormalDamage, transform.position, 0.5f, 0.5f));
            }
            //enemyHP.currentHealth -= NormalDamage;
        }
    }
    public void SkillAttack()
    {
        List<Collider> target = new List<Collider>(ST.targetList);
        foreach (Collider attackTarget in target)
        {
            EnemyHealth enemyHP = attackTarget.GetComponent<EnemyHealth>();
            if (enemyHP != null)
            {
                StartCoroutine(enemyHP.StartDamege(SkillDamage, transform.position, 1f, 2f));
            }
            //enemyHP.currentHealth -= SkillDamage;
        }
    }
    public void DashAttack()
    {
        List<Collider> target = new List<Collider>(ST.targetList);
        foreach (Collider attackTarget in target)
        {
            EnemyHealth enemyHP = attackTarget.GetComponent<EnemyHealth>();
            if (enemyHP != null)
            {
                StartCoroutine(enemyHP.StartDamege(DashDamage, transform.position, 1f, 2f));
            }
            //enemyHP.currentHealth -= DashDamage;
        }
    }
}
