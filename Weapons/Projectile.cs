using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;
    public float knockback;
    public float deathTime;

    public abstract void Launch();
    public abstract void SetDirection(Vector3 direction);
}
