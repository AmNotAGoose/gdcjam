using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public Player player;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.Respawn();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
