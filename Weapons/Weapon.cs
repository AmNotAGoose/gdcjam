using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public Transform projectile;
    public Transform projectileSpawn;
    public float reloadTime;
    public bool canFire = true;
    public bool isCurrentWeapon = false;

    public List<AudioSource> fireSfx;
    public AudioSource equipSfx;

    public Animator weaponAnimator;

    private void Start()
    {
        canFire = true;    
    }

    public abstract void Attack();
    public abstract void ToggleActive();

    private void Update()
    {
        if (Input.GetMouseButton(0) && canFire)
        {
            Attack();
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Reload()
    {
        canFire = false;
        yield return new WaitForSeconds(reloadTime);
        canFire = true;
    }
}
