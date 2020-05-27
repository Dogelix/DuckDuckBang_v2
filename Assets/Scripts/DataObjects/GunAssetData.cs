using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Gun Data")]
public class GunAssetData : ScriptableObject
{
    public GameObject _gun;
    public ParticleSystem _particle;
    public EGunType _gunType;
    public int _damage;
    public int _maxAmmo;
    public int _clipSize;
    public int _pointsCost;
}

public enum EGunType
{
    Hitscan,
    Projectile
}