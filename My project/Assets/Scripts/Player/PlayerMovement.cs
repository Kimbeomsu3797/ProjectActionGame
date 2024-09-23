using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    protected Animator avatar;

    float lastAttackTime, lastSkillTime, lastDashTime;

    public bool attacking = false;

    public bool dashing = false;

    float h, v;
    
    protected PlayerAttack playerAttack;
    // Start is called before the first frame update
    void Start()
    {
        avatar = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
    }
    public void OnStickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }
    // Update is called once per frame
    void Update()
    {
        if (avatar)
        {
            float back = 1f;

            if(v < 0f)
            {
                back = -1f;
            }
        }

        avatar.SetFloat("Speed", (h * h + v * v));

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody)
        {
            Vector3 speed = rigidbody.velocity;
            speed.x = 4 * h;
            speed.z = 4 * v;

            rigidbody.velocity = speed;
            if(h != 0f && v != 0f)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
            }
        }
    }
    public void OnAttackdown()
    {
        attacking = true;
        avatar.SetBool("Combo", true);
        StartCoroutine(StartAttack());
    }
    public void OnAttackUp()
    {
        avatar.SetBool("Combo", false);
        attacking = false;
    }
    IEnumerator StartAttack()
    {
       if(Time.time - lastAttackTime > 1f)
        {
            lastAttackTime = Time.time;

            while (attacking)
            {
                avatar.SetTrigger("AttackStart");
                playerAttack.NormalAttack();
                yield return new WaitForSeconds(1f);
            }
        }
    }
    public void OnSkillDown()
    {
        if(Time.time - lastSkillTime > 1f)
        {
            avatar.SetBool("Skill", true);
            playerAttack.SkillAttack();
            lastSkillTime = Time.time;
        }
    }
    public void OnSkillUp()
    {
        avatar.SetBool("Skill", false);
    }
    public void OnDashDown()
    {
        if(Time.time - lastDashTime > 1f)
        {
            lastDashTime = Time.time;
            dashing = true;
            avatar.SetTrigger("Dash");
            playerAttack.DashAttack();
        }
    }
    public void OnDashUp()
    {
        dashing = false;
    }
}
