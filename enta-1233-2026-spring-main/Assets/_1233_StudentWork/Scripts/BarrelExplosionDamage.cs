using Unity.VisualScripting;
using UnityEngine;

public class BarrelExplosionDamage : MonoBehaviour
{
    [SerializeField] private int _damage = 20;
    [SerializeField] private BarrelAudioHandler _audioHandler;
    [SerializeField] private GameObject _audio;

    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.CompareTag("Player") && other.CompareTag("Bomb")) return;
        if (!other.CompareTag("Player") && !other.CompareTag("Bomb"))
        {
            Debug.Log($"In collider {other.name}");
            var position = other.transform.position;
            TryApplyDamage(other.gameObject);
        }
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
            Debug.Log($"[ContactDamage] Damaged {target.name} for {_damage}");
            var position = target.transform.position;
            SpawnImpact(position);
            _audio.SetActive(true);

            Destroy(gameObject);
        }
    }
    #region Particle
    [SerializeField] private GameObject _impactVfxPrefab;
    void SpawnImpact(Vector3 position)
    {
        _audio.SetActive(true);
        Instantiate(_impactVfxPrefab, position, Quaternion.identity);
        
    }
    #endregion
    
}
