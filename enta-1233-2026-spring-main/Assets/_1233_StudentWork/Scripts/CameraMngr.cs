using Unity.Cinemachine;
using UnityEngine;

public class CameraMngr : MonoBehaviour
{
    public CinemachineCamera Camera1;
    public CinemachineCamera Camera2;
    void Start()
    {
        Camera1.Priority = 2;
        Camera2.Priority = 1;
    }

    // Update is called once per frame
    public void SwitchToSecondCamera()
    {
        Camera1.Priority = 1;
        Camera2.Priority = 2;
    }
    public void SwitchToMainCamera()
    {
        Camera1.Priority = 2;
        Camera2.Priority = 1;
    }
}
