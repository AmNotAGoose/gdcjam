using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeti : Enemy
{
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public float groundPoundRange = 10f;
    public GameObject groundPoundProjectile;
    public Transform spawnPoint;

    public AudioSource groundPoundSfx;

    private bool isGroundPounding = false;
    private bool spawned = false;

    void Update()
    {
        if (!isGroundPounding && spawned)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction.y = 0;
            direction.Normalize();
            rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);

            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(direction), 5 * Time.fixedDeltaTime);

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= groundPoundRange)
            {
                StartCoroutine(GroundPound());
            }
        }
    }

    public override IEnumerator Spawn()
    {
        bossSpawnSfx.Play();
        yield return new WaitForSeconds(3);
        spawned = true;
        bossMusic.Play();
    }

    private IEnumerator GroundPound()
    {
        isGroundPounding = true;

        groundPoundSfx.Play();

        enemyAnimator.SetTrigger("groundPound");

        yield return new WaitForSeconds(1f);

        Instantiate(groundPoundProjectile, spawnPoint.position, Quaternion.identity);

        yield return new WaitForSeconds(3f);

        isGroundPounding = false;
    }
}
