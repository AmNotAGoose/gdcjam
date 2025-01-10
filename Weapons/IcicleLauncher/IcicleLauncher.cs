using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleLauncher : Weapon
{
    public override void Attack()
    {
        fireSfx[Random.Range(0, fireSfx.Count)].Play();
        Transform curProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
        curProjectile.GetComponent<Projectile>().SetDirection(transform.right);
        curProjectile.GetComponent<Projectile>().Launch();
        weaponAnimator.SetTrigger("fire");
    }

    public override void ToggleActive()
    {
        isCurrentWeapon = !isCurrentWeapon;
    }
}
