using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PresentProjectile : Projectile
{
    public Vector3 dir;
    public bool started;
    public ParticleSystem death;
    
    void Update()
    {
        if (started)
        {
            transform.position += dir * Time.deltaTime * speed;
        }
    }

    IEnumerator WaitThenLaunch()
    {
        yield return new WaitForSeconds(0.5f);
        started = true;
    }

    public override void Launch()
    {
        StartCoroutine(WaitThenLaunch());
    }

    public override void SetDirection(Vector3 direction)
    {
        dir = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (started && other.gameObject.CompareTag("Player"))
        {
            print("hasdi");
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(other.gameObject.GetComponent<Rigidbody>().velocity.x, other.gameObject.GetComponent<Rigidbody>().velocity.y + 10, other.gameObject.GetComponent<Rigidbody>().velocity.z);
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            death.Play();
        }
    }
}
