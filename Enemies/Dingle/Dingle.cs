using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dingle : Enemy
{
    public float movementSpeed = 3.0f;
    public float rotationSpeed = 5.0f;
    public float aggroDistance = 5.0f;
    public float attackCooldown = 0.5f;
    public Rigidbody rb;
    public GameObject enemyObject;
    public Transform projectileSpawn;
    public GameObject projectile;

    private bool attacking;
    public List<AudioSource> attackSfx;

    void Start()
    {
        spawnDelay = 1f;
        StartCoroutine(Spawn());
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();
        if (!(Vector3.Distance(player.transform.position, transform.position) <= aggroDistance))
        {
            rb.velocity = direction * movementSpeed;
        } else if (!attacking)
        {
            StartCoroutine(Attack(direction));
        }
        rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.fixedDeltaTime);
    }

    public IEnumerator Attack(Vector3 projectileDirection)
    {
        attacking = true;
        GameObject curProjectile = Instantiate(projectile, projectileSpawn.position, Quaternion.LookRotation(-projectileDirection));
        curProjectile.GetComponent<DingleProjectile>().Launch(projectileDirection); //sigh

        attackSfx[Random.Range(0, attackSfx.Count)].Play();
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }

    public override IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        enemyObject.SetActive(true);
        enemyAnimator.SetBool("walking", true);
    }
}
