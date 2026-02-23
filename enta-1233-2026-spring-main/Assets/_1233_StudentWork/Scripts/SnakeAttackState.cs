using UnityEngine;

public class SnakeAttackState : EnemyState
{
    private readonly SnakeBrain _brain;
    private float _exitTime;

    public SnakeAttackState(SnakeBrain brain, EnemyStateMachine machine) : base(machine)
    {
        _brain = brain;
    }

    public override void Enter()
    {
        _brain.Mover?.Stop();
        _brain.AnimatorDriver.SetSpeed(0);
        _brain.AnimatorDriver.TriggerAttack();

        _exitTime = Time.time + _brain.AttackCooldown;

        ApplyMeleeDamage();
    }
    public override void Tick()
    {
        var target = _brain.TargetProvider.GetTarget();
        var targetPos = _brain.TargetProvider.GetTargetPosition();
        if (target != null) _brain.Rotator.FacePosition(targetPos);

        if (Time.time >= _exitTime) Machine.ChangeState(new SnakeChaseState(_brain, Machine));
    }
    private void ApplyMeleeDamage()
    {
        var target = _brain.TargetProvider.GetTarget();
        if (target == null) return;

        if (Vector3.Distance(_brain.transform.position, target.position) <= _brain.AttackRange + 0.5f)
        {
            var receiver = target.GetComponent<IDamageReceiver>();
            if (receiver != null)
                receiver.ApplyDamage(new DamageInfo
                {
                    Amount = _brain.AttackDamage,
                    Source = _brain.gameObject,
                    HitPoint = target.position,
                    HitNormal = Vector3.up
                });
        }
    }
}
