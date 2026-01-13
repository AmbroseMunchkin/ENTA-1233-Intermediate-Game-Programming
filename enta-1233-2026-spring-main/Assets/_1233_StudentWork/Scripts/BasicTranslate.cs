using UnityEngine;
using UnityEngine.UIElements;

public class BasicTranslate : MonoBehaviour
{
    [SerializeField] private Vector3 _speed;
    [SerializeField] private bool _fixedUpdate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_fixedUpdate)
        {
            transform.Translate(_speed * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        if (_fixedUpdate)
        {
            transform.Translate(_speed * Time.fixedDeltaTime);
        }
    }
}
