using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidBox : MonoBehaviour
{
    public Player player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.Die();
        }
    }
}
