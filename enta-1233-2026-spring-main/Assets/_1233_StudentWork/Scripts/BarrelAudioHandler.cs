using UnityEngine;

public class BarrelAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    public void PlayExplosion()
    {
        _source.Play();
    }
}
