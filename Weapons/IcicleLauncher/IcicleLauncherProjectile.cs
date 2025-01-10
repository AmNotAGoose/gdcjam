using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleLauncherProjectile : Projectile
{
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
    public override void Launch()
    {
        launched = true;
    }

    public override void SetDirection(Vector3 direction) 
    {
        dir = direction;
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
        if (launched && collision.gameObject.CompareTag("Enemy"))
        {
            print("hit enemy");
            if (collision.transform.GetComponent<Enemy>() != null)
            {
                collision.transform.GetComponent<Enemy>().TakeDamage(damage);
            } else if (collision.transform.parent.parent.GetComponent<Enemy>() != null)
            {
                collision.transform.parent.parent.GetComponent<Enemy>().TakeDamage(damage);
            } 
            else
            {
                collision.transform.GetComponent<SnowlingBodyDamager>().TakeDamage(damage);
            }
            DieOnHit();
        }
        else if (!collision.gameObject.CompareTag("Projectile"))
        {
            print(collision.gameObject.name);
            DieOnHit();
        }
    }
}
