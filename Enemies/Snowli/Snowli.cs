using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowli : Enemy
{
    public float movementSpeed = 3.0f;
    public float rotationSpeed = 5.0f;
    public float attackCooldown = 0.5f;
    public int damage = 25;
    public Rigidbody rb;
    public GameObject enemyObject;

    private bool attacking;
    public List<AudioSource> attackSfx;

    void Start()
    {
        enemyAnimator.SetBool("walking", true);
        spawnDelay = 0.8f;
        StartCoroutine(Spawn());
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = -1;
        direction.Normalize();

        rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.fixedDeltaTime);
        //rb.AddForce(direction * movementSpeed, ForceMode.Impulse);
        rb.velocity = direction * movementSpeed;
    }

    public override IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        enemyObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !attacking)
        {
            print("hit player by snowli");
            StartCoroutine(CooldownAttack());
            attackSfx[Random.Range(0, attackSfx.Count)].Play();
            collision.transform.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!attacking)
            {
                print("hit player by snowli");
                collision.transform.GetComponent<PlayerStats>().TakeDamage(damage);
                attackSfx[Random.Range(0, attackSfx.Count)].Play();
                StartCoroutine(CooldownAttack());
            }
        }
    }

    IEnumerator CooldownAttack()
    {
        attacking = true;
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }
}
