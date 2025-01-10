using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public override void Attack()
    {
        fireSfx[Random.Range(0, fireSfx.Count)].Play();
        for (int i = 0; i < 7; i++)
        {
            Vector3 positionOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);

            Transform curProjectile = Instantiate(projectile, projectileSpawn.position + positionOffset, projectileSpawn.rotation * Quaternion.Euler(Random.Range(-10f, 10f), -90, Random.Range(-10f, 10f)));

            curProjectile.GetComponent<Projectile>().SetDirection(curProjectile.transform.right);
            curProjectile.GetComponent<Projectile>().Launch();
        }

        weaponAnimator.SetTrigger("fire");
    }

    public override void ToggleActive()
    {
        isCurrentWeapon = !isCurrentWeapon;
    }
}
