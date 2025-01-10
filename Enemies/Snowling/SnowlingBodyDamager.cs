using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowlingBodyDamager : MonoBehaviour
{
    public int damage; // its all falling apart :(
    public Snowling snowling;
    private bool attacking;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!attacking)
            {
                collision.transform.GetComponent<PlayerStats>().TakeDamage(damage);
                snowling.attackSfx[Random.Range(0, snowling.attackSfx.Count)].Play();
                StartCoroutine(CooldownAttack());
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!attacking)
            {
                collision.transform.GetComponent<PlayerStats>().TakeDamage(damage);
                snowling.attackSfx[Random.Range(0, snowling.attackSfx.Count)].Play();
                StartCoroutine(CooldownAttack());
            }
        }
    }

    IEnumerator CooldownAttack()
    {
        attacking = true;
        yield return new WaitForSeconds(snowling.mainAttackCooldown);
        attacking = false;
    }

    public void TakeDamage(int damage)
    {
        snowling.TakeDamage(damage);
    }
}
