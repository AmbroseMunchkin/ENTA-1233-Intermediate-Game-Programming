using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _footstepSource1;
    [SerializeField] private AudioSource _footstepSource2;
    [SerializeField] private AudioSource _landingSource;
    [SerializeField] private AudioSource _jumpSource;

    public void PlayFootstep1()
    {
        _footstepSource1?.Play();
    }
    public void PlayFootstep2()
    {
        _footstepSource2?.Play();
    }
    public void PlayLanding()
    {
        _landingSource?.Play();
    }
    public void PlayJump()
    {
        _jumpSource?.Play();
    }
}
