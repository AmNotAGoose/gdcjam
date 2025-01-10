using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public Player player;
    public List<Enemy> enemyList;
    public GameObject door;
    public GameObject healthBar;
    
    public AudioSource levelMusic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Enemy enemy in enemyList)
            {
                enemy.gameObject.SetActive(true);
                door.SetActive(true);
                healthBar.SetActive(true);
            }
            if (levelMusic != null)
            {
                levelMusic.Stop();
            }
        }
    }
}
