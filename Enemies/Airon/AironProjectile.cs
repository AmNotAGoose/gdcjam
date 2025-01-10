using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AironProjectile : MonoBehaviour
{
    public List<DingleProjectile> projectiles;

    public void Launch()
    {
        foreach (DingleProjectile projectile in projectiles)
        {
            projectile.Launch(Vector3.zero);
        }
    }
}
