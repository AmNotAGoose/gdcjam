using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Krampus : Enemy
{
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public float throwRange = 10f;
    public GameObject presentProjectile;
    public GameObject spawnProjectile;
    public Transform spawnPoint;

    private bool isThrowing = false;
    private bool isSpawning = false;
    private bool spawned = false;

    public List<AudioSource> presentSfx;

    void Update()
    {
        if (!isThrowing && spawned)
        {   
            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction.y = 0;
            direction.Normalize();

            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(direction), 5 * Time.fixedDeltaTime);

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer >= 15 && distanceToPlayer <= 20)
            {
                StartCoroutine(ThrowPresent());
            } else if (distanceToPlayer > 20)
            {
                if (!isSpawning)
                {
                    StartCoroutine(DecideToSpawnStuff());
                }
                rb.velocity = new Vector3(direction.x * moveSpeed * 2, rb.velocity.y, direction.z * moveSpeed * 2);
            } else if (distanceToPlayer < 15)
            {
                if (!isSpawning)
                {
                    StartCoroutine(DecideToSpawnStuff());
                }
                rb.velocity = new Vector3(-direction.x * moveSpeed, rb.velocity.y, -direction.z * moveSpeed);
            }
        }
    }

    public override IEnumerator Spawn()
    {
        bossSpawnSfx.Play();
        yield return new WaitForSeconds(3f);
        bossMusic.Play();
        spawned = true;
    }

    private IEnumerator ThrowPresent()
    {
        isThrowing = true;

        enemyAnimator.SetTrigger("throw");
        presentSfx[Random.Range(0, presentSfx.Count)].Play();

        yield return new WaitForSeconds(0.5f);

        GameObject projectile = Instantiate(presentProjectile, spawnPoint.position, Quaternion.identity);

        projectile.GetComponent<PresentProjectile>().SetDirection(new Vector3(player.transform.position.x, player.transform.position.y - 1.5f, player.transform.position.z) - transform.position);
        projectile.GetComponent<PresentProjectile>().Launch();

        yield return new WaitForSeconds(1.5f);

        isThrowing = false;
    }

    private IEnumerator DecideToSpawnStuff()
    {
        isSpawning = true;
        yield return new WaitForSeconds(0.3f);

        if (Random.Range(0, 10) == 2)
        {
            StartCoroutine(SpawnStuff());
        } else
        {
            isSpawning = false;
        }
    }

    private IEnumerator SpawnStuff()
    {
        isSpawning = true;

        enemyAnimator.SetTrigger("spawnstuff");

        Instantiate(spawnProjectile, new Vector3(spawnPoint.position.x, spawnPoint.position.y - 6.5f, spawnPoint.position.z), Quaternion.identity);

        yield return new WaitForSeconds(1.5f);

        isSpawning = false;
    }
}
