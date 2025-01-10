using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowling : Enemy
{
    public int mainAttackDamage;
    public float mainAttackCooldown;

    public GameObject body;

    public bool isAttacking;
    private bool snapPosition;

    public List<AudioSource> attackSfx;
    public List<AudioSource> bounceSfx;


    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        StartCoroutine(Spawn());
    }
    void Update()
    {
        if (snapPosition)
        {
            transform.position = enemyAnimator.transform.position;
            enemyAnimator.transform.localPosition = Vector3.zero;
            snapPosition = false;
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        enemyAnimator.SetTrigger("attack");

        Vector3 direction = player.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)) * Quaternion.Euler(0, 90, 0);
        bounceSfx[Random.Range(0, bounceSfx.Count)].Play();
        //AnimatorStateInfo stateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);
        //float animationLength = stateInfo.length;

        //print(animationLength);

        yield return new WaitForSeconds(2f);

        snapPosition = true; //oh my days this is so scuffed

        enemyAnimator.SetTrigger("endAttack");

        print("done");

        yield return new WaitForSeconds(mainAttackCooldown);

        print("done2");


        isAttacking = false;

        StartCoroutine(Attack());
    }

    public override IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        body.SetActive(true);
        StartCoroutine(Attack());
    }
}
