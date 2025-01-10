using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DingleProjectile : MonoBehaviour
{
    public float speed;
    public int damage;
    public float knockback;
    public float deathTime;
    public GameObject deathEffect;
    private bool launched;
    private Vector3 dir;

    private void Start()
    {
        StartCoroutine(DieTimeout());
    }
    void Update()
    {
        if (launched)
        {
            transform.position += dir * Time.deltaTime * speed;
        }
    }
    public void Launch(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            dir = -gameObject.transform.forward;
        } else
        {
            dir = direction;
        }
        launched = true;
    }

    public IEnumerator DieTimeout()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(this.gameObject);
    }


    public void DieOnHit()
    {
        GameObject particle = Instantiate(deathEffect, transform.position, Quaternion.identity);
        particle.GetComponent<ParticleSystem>().Play();
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (launched && collision.gameObject.CompareTag("Player"))
        {
            print("hit player by dingle");
            collision.transform.GetComponent<PlayerStats>().TakeDamage(damage);
            DieOnHit();
        }
        else
        {
            print(collision.gameObject.name);
            DieOnHit();
        }
    }
}
