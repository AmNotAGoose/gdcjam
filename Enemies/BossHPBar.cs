using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public Enemy enemy;
    public Slider slider;

    private void Update()
    {
        slider.value = enemy.health;
    }
}
