using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrampusBody : MonoBehaviour
{
    public Krampus krampus;
    public bool started = false;
    private bool attacking;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3 && !started)
        {
            started = true;
            krampus.enemyAnimator.SetTrigger("spawn");
            StartCoroutine(krampus.Spawn());
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if (!attacking)
            {
                StartCoroutine(CooldownAttack());
                collision.gameObject.GetComponent<PlayerStats>().TakeDamage(5);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!attacking)
            {
                collision.transform.GetComponent<PlayerStats>().TakeDamage(5);
                StartCoroutine(CooldownAttack());
            }
        }
    }

    IEnumerator CooldownAttack()
    {
        attacking = true;
        yield return new WaitForSeconds(0.3f);
        attacking = false;
    }
}
