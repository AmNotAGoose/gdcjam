using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FinalBoss : Enemy
{
    public Rigidbody rb;
    public GameObject redParticles;
    public GameObject blueParticles;
    public GameObject greenParticles;

    public GameObject lastParticle;

    public Projectile groundPoundProjectile;
    public GameObject groundPoundSpawnLocation;

    public GameObject enemySpawnObject;

    public GameObject volley;

    public AudioSource groundPoundSfx;
    public AudioSource spikeArenaSfx;

    public bool spawned;
    public bool attacking;
    public bool actuallyAttacking;

    private void Start()
    {

        StartCoroutine(Spawn());
    }
    void Update()
    {
        if (spawned && !actuallyAttacking)
        {
            rb.AddForce((new Vector3((player.transform.position - transform.position).normalized.x * 25f, 0, (player.transform.position - transform.position).normalized.z * 25f) ) * Time.deltaTime, ForceMode.VelocityChange);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3((player.transform.position - transform.position).normalized.x, 0, (player.transform.position - transform.position).normalized.z)), Time.fixedDeltaTime * 5f);
        }
        if (spawned && !attacking)
        {
            StartCoroutine(DecideWhichAttack());
        }
    }

    public override IEnumerator Spawn()
    {
        enemyAnimator.SetTrigger("spawn");
        bossSpawnSfx.Play();
        yield return new WaitForSeconds(3);
        bossMusic.Play();
        spawned = true;
        transform.position = new Vector3(transform.position.x, 56.1081f, transform.position.z);
        enemyAnimator.enabled = false;
    }

    public IEnumerator DecideWhichAttack()
    {
        attacking = true;
        yield return new WaitForSeconds(0.5f);

        int randomChoice = Random.Range(0, 3);

        if (randomChoice == 0)
        {
            StartCoroutine(RedAttack());
        } else if (randomChoice == 1)
        {
            StartCoroutine(BlueAttack());
        } else if (randomChoice == 2)
        {
            StartCoroutine(GreenAttack());
        }
    }

    public IEnumerator RedAttack()
    {
        if (lastParticle != null)
        {
            lastParticle.SetActive(false);
        }
        redParticles.SetActive(true);

        actuallyAttacking = true;
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.down * 300f, ForceMode.Impulse);

        yield return new WaitForSeconds(1f);
        Instantiate(groundPoundProjectile.gameObject, groundPoundSpawnLocation.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        actuallyAttacking = false;

        Instantiate(enemySpawnObject, new Vector3(transform.position.x, transform.position.y - 3, transform.position.z), Quaternion.identity);

        rb.AddForce(Vector3.up * 30f, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);

        lastParticle = redParticles;
        attacking = false;
    }

    public IEnumerator BlueAttack()
    {
        if (lastParticle != null)
        {
            lastParticle.SetActive(false);
        }
        blueParticles.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            actuallyAttacking = true;
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.down * 300f, ForceMode.Impulse);

            yield return new WaitForSeconds(1f);

            actuallyAttacking = false;
            rb.AddForce(Vector3.up * 30f, ForceMode.Impulse);
            Instantiate(groundPoundProjectile.gameObject, groundPoundSpawnLocation.transform.position, Quaternion.identity);
            groundPoundSfx.Play();

            yield return new WaitForSeconds(1f);
        }
        lastParticle = blueParticles;

        attacking = false;
    }

    public IEnumerator GreenAttack()
    {
        if (lastParticle != null)
        {
            lastParticle.SetActive(false);
        }
        greenParticles.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            GameObject projectile = Instantiate(volley, transform.position, Quaternion.identity);
            projectile.GetComponent<AironProjectile>().Launch();
            spikeArenaSfx.Play();

            yield return new WaitForSeconds(0.5f);
        }

        lastParticle = greenParticles;
        attacking = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") | collision.gameObject.layer == 3) && actuallyAttacking)
        {
            rb.AddForce(Vector3.down * 30f, ForceMode.Impulse);
        }
    }
}
