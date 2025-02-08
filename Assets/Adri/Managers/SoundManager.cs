using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SoundManager", menuName = "Managers/SoundManager")]
public class SoundManager : ScriptableObject
{
    public Action OnShootSound;


    public void WeaponShooted()
    {
        OnShootSound?.Invoke();
    }

}
