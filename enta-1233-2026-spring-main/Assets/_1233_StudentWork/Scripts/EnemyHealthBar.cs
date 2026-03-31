using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthEnemyFillImage;
    private Health _enemyHealth;
    void Start()
    {
        HandleEnemyAssigned(gameObject);
    }
    private void HandleEnemyAssigned(GameObject enemyObject)
    {
        if (enemyObject == null)
        {
            RefreshHealthBar(null);
            return;
        }
        _enemyHealth = enemyObject.GetComponentInChildren<Health>();
        if (_enemyHealth == null)
        {
            Debug.LogError("GameUI: Enemy object doesnt have a health component");
            return;
        }

        _enemyHealth.OnHealthChanged += RefreshHealthBar;
        RefreshHealthBar(_enemyHealth);
    }
    private void RefreshHealthBar(Health health)
    {
        if (_healthEnemyFillImage == null) return;

        _healthEnemyFillImage.fillAmount = health != null ? health.NormalizedHealth : 0f;
    }
}
