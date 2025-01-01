using UnityEngine;

public class WeaponSign : Sign
{
    protected override void Start()
    {
        base.Start();
        Debug.Log("Start Start");
        sign = ObjectPoolManager.Instance.SpawnFromPool(ObjectPoolManager.weaponSign, transform.position, Quaternion.identity);
        
    }
}