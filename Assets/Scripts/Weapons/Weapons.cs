using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO weaponData;

    public abstract void Shoot();

}
