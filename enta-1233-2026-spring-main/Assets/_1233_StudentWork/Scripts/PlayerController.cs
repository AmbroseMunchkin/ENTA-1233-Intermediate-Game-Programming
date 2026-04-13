using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    [SerializeField] private int _damage = 20;
    [SerializeField] private float jumpPower;
    private int _numberOfJumps;
    [SerializeField] private int maxNumberOfJumps = 2;

    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    [SerializeField] private float speed;

    private float _gravity = -9.81f;
    [SerializeField] private float gravityMutiplier = 3.0f;
    private float _velocity;

    [SerializeField] private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int StartJump = Animator.StringToHash("StartJump");

    [SerializeField] private PlayerAudioHandler _audioHandler;

    [SerializeField] private Health _health;

    [SerializeField] private GameObject _explosiveBarrel;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        if (_health == null) _health = GetComponent<Health>();
    }
    private void OnEnable()
    {
        if (_health != null)
        {
            _health.OnDamaged += HandleDamaged;
            _health.OnDied += HandleDied;
        }
    }
    private void OnDisable()
    {
        if (_health != null)
        {
            _health.OnDamaged -= HandleDamaged;
            _health.OnDied -= HandleDied;
        }
    }
    public void Update()
    {
        //if (GameMgr.Instance.IsGameRunning == false) return;

        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
        AnimationParameters();
    }
    private void ApplyGravity()
    {
        if (IsGrounded() && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMutiplier * Time.deltaTime;
        }

        _direction.y = _velocity;
    }
    private void ApplyRotation()
    {
        if (_input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }
    private void ApplyMovement()
    {
        _characterController.Move(_direction * speed * Time.deltaTime);
    }
    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!IsGrounded() && _numberOfJumps >= maxNumberOfJumps) return;
        if (_numberOfJumps == 0) StartCoroutine(WaitForLanding());

        _audioHandler.PlayJump();
        _numberOfJumps++;
        _velocity += jumpPower;
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        Debug.Log("Attack!");

        SpawnBarrel();
        //_animator?.SetTrigger("Attack");
    }
    private void SpawnBarrel()
    {
        Instantiate(_explosiveBarrel, transform.position, transform.rotation);
    }

    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);

        _audioHandler.PlayLanding();
        _numberOfJumps = 0;
    }

    private bool IsGrounded() => _characterController.isGrounded;

    private void AnimationParameters()
    {
        _animator.SetFloat(Speed, _input.sqrMagnitude);
        _animator.SetBool(Grounded, _characterController.isGrounded);
        _animator.SetBool(StartJump, !_characterController.isGrounded);
    }
    private void HandleDamaged(DamageInfo info)
    {
        Debug.Log($"[Player] Hit by " + $"{info.Source?.name ?? "Unknown"} " + $"for {info.Amount} damage. " + $"HP: {_health.CurrentHealth}/{_health.MaxHealth}");
        _animator?.SetTrigger("Hit");
    }
    private void HandleDied()
    {
        Debug.Log("[Player] Died!");
        _animator?.SetTrigger("Die");
        _characterController = null;
        _animator = null;
        enabled = false;

        StartCoroutine(GameOverTransition());
    }
    private IEnumerator GameOverTransition()
    {
        yield return new WaitForSeconds(2);
        GameMgr.Instance.GameOver();
    }
    private void OnTriggerEnter(Collider other)
    {
        //TryApplyDamage(other.gameObject);
    }
    private void TryApplyDamage(GameObject target)
    {
        var damageReceiver = target.GetComponent<IDamageReceiver>();
        if (damageReceiver != null)
        {
            var info = new DamageInfo
            {
                Amount = _damage,
                Source = gameObject,
                HitPoint = target.transform.position,
                HitNormal = Vector3.up
            };
            damageReceiver.ApplyDamage(info);
            Debug.Log($"[ContactDamage] Damaaged {target.name} for {_damage}");
        }
    }
}