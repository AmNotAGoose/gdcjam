using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public List<Weapon> weapons;
    private int curSelectedWeapon = 0;
    private bool isReadyToScroll = true;

    void ChangeWeapons(int index)
    {
        weapons[curSelectedWeapon].gameObject.SetActive(false);
        curSelectedWeapon = index;
        weapons[curSelectedWeapon].gameObject.SetActive(true);
        weapons[curSelectedWeapon].equipSfx.Play();
        StartCoroutine(weapons[curSelectedWeapon].Reload());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapons(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapons(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //ChangeWeapons(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //ChangeWeapons(3);
        }

    //    if (isReadyToScroll && Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
     //   {
     //       ChangeWeapons((curSelectedWeapon + 1) % weapons.Count);
    //        CooldownScroll();
     //   }
    //    else if (isReadyToScroll && Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
    //    {
     ///       ChangeWeapons((curSelectedWeapon + 1) % weapons.Count);
     //       CooldownScroll();
     //   }
    }

    IEnumerator CooldownScroll()
    {
        isReadyToScroll = false;
        yield return new WaitForSeconds(0.1f);
        isReadyToScroll = true;
    }
}
