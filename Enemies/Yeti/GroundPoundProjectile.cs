using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoundProjectile : Projectile
{
    public ParticleSystem particle;
    private bool launched;

    public 
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(-90f, 0f, 0f);

        StartCoroutine(DieTimeout());
        Launch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetDirection(Vector3 direction)
    {

    }

    public override void Launch()
    {
        launched = true;
    }

    public IEnumerator DieTimeout()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (launched && other.gameObject.CompareTag("Player"))
        {
            print("hasdi");
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(other.gameObject.GetComponent<Rigidbody>().velocity.x, other.gameObject.GetComponent<Rigidbody>().velocity.y + 40f, other.gameObject.GetComponent<Rigidbody>().velocity.z);
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
