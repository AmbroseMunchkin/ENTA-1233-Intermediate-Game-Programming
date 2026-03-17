using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineCamera cameraToActivate;
    public CinemachineCamera cameraToDeactivate1;
    public CinemachineCamera cameraToDeactivate2;
    public CinemachineCamera cameraToDeactivate3;
    public CinemachineCamera cameraToDeactivate4;
    public CinemachineCamera cameraToDeactivate5;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraToActivate.Priority = 2;
            cameraToDeactivate1.Priority = 1;
            cameraToDeactivate2.Priority = 1;
            cameraToDeactivate3.Priority = 1;
            cameraToDeactivate4.Priority = 1;
            cameraToDeactivate5.Priority = 1;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraToActivate.Priority = 1;
            cameraToDeactivate1.Priority = 2;
            cameraToDeactivate2.Priority = 2;
            cameraToDeactivate3.Priority = 2;
            cameraToDeactivate4.Priority = 2;
            cameraToDeactivate5.Priority = 2;
        }
    }

}
