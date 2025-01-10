using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airon : Enemy
{

    public float movementSpeed = 3.0f;
    public float attackCooldown = 0.5f;
    public GameObject enemyObject;
    public Transform projectileSpawn;
    public GameObject projectile;

    public List<AudioSource> attackSfx;

    private bool attacking;

    void Start()
    {
        spawnDelay = 1f;
        StartCoroutine(Spawn());
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (!attacking)
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {
        attacking = true;
        GameObject curProjectile = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
        curProjectile.GetComponent<AironProjectile>().Launch();
        attackSfx[Random.Range(0, attackSfx.Count)].Play();

        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }

    public override IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        enemyObject.SetActive(true);
        enemyAnimator.SetBool("open", true);
    }
}
