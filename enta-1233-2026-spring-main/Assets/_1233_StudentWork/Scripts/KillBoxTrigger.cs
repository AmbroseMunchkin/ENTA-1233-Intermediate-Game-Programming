using UnityEngine;

public class KillBoxTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameMgr.Instance.GameOver();
        }
    }
}
