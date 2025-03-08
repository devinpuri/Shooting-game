using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Gun : MonoBehaviour
{
    public int ammo = 10;

    public void Reload()
    {
        Debug.Log("Reloading gun!");
        ammo = 10;
    }

    // Add other gun-related functionality as needed
}
