using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    public string name;
    public Sprite image;
    public float damage;
    public float knockback;
    public float fireRate;
    public int bullets;
}
