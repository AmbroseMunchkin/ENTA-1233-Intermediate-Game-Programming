using Unity.Cinemachine;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private CinemachineBrain _cameraBrain;

    private void OnEnable()
    {
        PlayerMgr.Instance.OnPlayerAssigned += HandleCameraAssigned;
    }
    private void OnDisable()
    {
        PlayerMgr.Instance.OnPlayerAssigned -= HandleCameraAssigned;
    }
    private void HandleCameraAssigned(GameObject playerObject)
    {
        Debug.Log("Camera assigned");
        _mainCamera = Camera.main;
    }
    void LateUpdate()
    {
        if (_mainCamera != null)
        {
            transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
                             _mainCamera.transform.rotation * Vector3.up);
        }
        if (_cameraBrain != null)
        {
            transform.LookAt(transform.position + _cameraBrain.transform.rotation * Vector3.forward,
                             _cameraBrain.transform.rotation * Vector3.up);
        }

    }
}
