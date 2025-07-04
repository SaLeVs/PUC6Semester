using UnityEngine;

public abstract class BaseWeapons : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO weaponData;

    public abstract void Shoot();
    public abstract void Reload();
    public abstract void Launch();

}
