using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int kills;
    public int damageTaken;

    public void TakeDamage(int damage)
    {
        health -= damage;
        damageTaken += damage;
        if (health <= 0)
        {
            this.gameObject.GetComponent<Player>().Die();
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
    }
}
