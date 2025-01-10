using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Player player;
    public PlayerStats playerStats;
    public Animator enemyAnimator;
    public float spawnDelay;
    public GameObject deathParticles;
    public int health;
    public bool isBoss = false;

    public AudioSource bossSpawnSfx;
    public AudioSource bossMusic;
    public List<AudioSource> deathSfx;
    public List<AudioSource> hitSfx;

    public abstract IEnumerator Spawn();

    public IEnumerator Die()
    {
        deathSfx[Random.Range(0, deathSfx.Count)].Play();
        playerStats = FindFirstObjectByType(typeof(PlayerStats)).GetComponent<PlayerStats>();
        playerStats.kills += 1;
        deathParticles.SetActive(true);
        deathParticles.transform.SetParent(null);
        deathParticles.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        if (isBoss)
        {
            bossMusic.Stop();
            player.Win();
        }
    }

    public void TakeDamage(int damage)
    {
        hitSfx[Random.Range(0, hitSfx.Count)].Play();
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }
}
