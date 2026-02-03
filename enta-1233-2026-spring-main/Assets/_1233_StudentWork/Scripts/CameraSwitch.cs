using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineCamera cameraToActivate;
    public CinemachineCamera cameraToDeactivate;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraToActivate.Priority = 2;
            cameraToDeactivate.Priority = 1;
        }
    }
    public void OnTriggerExit(Collider other)
    {

        cameraToActivate.Priority = 1;
        cameraToDeactivate.Priority = 2;
    }

}
