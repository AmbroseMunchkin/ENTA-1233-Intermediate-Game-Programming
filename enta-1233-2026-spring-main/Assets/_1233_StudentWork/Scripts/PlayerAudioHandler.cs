using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _footstepSource1;
    [SerializeField] private AudioSource _footstepSource2;
    [SerializeField] private AudioSource _landingSource;
    [SerializeField] private AudioSource _jumpSource;
    [SerializeField] private AudioSource _hurtSource;

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
    public void PlayHurt()
    {
        _hurtSource?.Play();
    }
}
