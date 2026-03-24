using Unity.VisualScripting;
using UnityEngine;

public class BarrelExplosionDamage : MonoBehaviour
{
    [SerializeField] private int _damage = 20;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("In collider");
        var position = collision.transform.position;
        TryApplyDamage(collision.gameObject);
        SpawnImpact(position);
    }
    private void OnTriggerEnter(Collider other)
    {
        TryApplyDamage(other.gameObject);
        Debug.Log("In collider");
        var position = other.transform.position;
        SpawnImpact(position);
    }


    private void TryApplyDamage(GameObject target)
    {
        var damageReceiver = target.GetComponent<IDamageReceiver>();
        if (damageReceiver != null)
        {
            var info = new DamageInfo
            {
                Amount = _damage,
                Source = gameObject,
                HitPoint = target.transform.position,
                HitNormal = Vector3.up
            };
            damageReceiver.ApplyDamage(info);
            Debug.Log($"[ContactDamage] Damaaged {target.name} for {_damage}");
            Destroy(gameObject);
        }
    }
    #region Particle
    [SerializeField] private GameObject _impactVfxPrefab;
    void SpawnImpact(Vector3 position)
    {
        Instantiate(_impactVfxPrefab, position, Quaternion.identity);
    }
    #endregion
}
