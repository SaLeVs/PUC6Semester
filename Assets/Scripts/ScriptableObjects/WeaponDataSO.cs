using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    public string weaponName;
    public Sprite weaponImage;
    public float damage;
    public float fireRate;
}
