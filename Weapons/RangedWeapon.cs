using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public override void Attack()
    {
        Transform curProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
        curProjectile.GetComponent<Projectile>().Launch();
    }

    public override void ToggleActive()
    {
        isCurrentWeapon = !isCurrentWeapon;
    }
}
