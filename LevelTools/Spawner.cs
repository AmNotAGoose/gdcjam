using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Player player;
    public List<Enemy> enemyList;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Enemy enemy in enemyList)
            {
                enemy.gameObject.SetActive(true);
            }
        }
    }
}
