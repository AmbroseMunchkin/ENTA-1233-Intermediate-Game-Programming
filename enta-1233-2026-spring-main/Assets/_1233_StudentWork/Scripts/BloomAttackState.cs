using UnityEngine;

public class BloomAttackState : EnemyState
{
    private readonly BloomBrain _brain;

    public BloomAttackState(BloomBrain brain, EnemyStateMachine machine) : base(machine)
    {
        _brain = brain;
    }

    public override void Enter()
    {
        _brain.Mover?.Stop();
        _brain.AnimatorDriver.SetSpeed(0);
    }
    public override void Tick()
    {
        var target = _brain.TargetProvider.GetTarget();
        var targetPos = _brain.TargetProvider.GetTargetPosition();
        if (target == null)
        {
            Debug.Log("target null");
            Machine.ChangeState(new BloomMoveState(_brain, Machine));
            return;
        }

        var distance = Vector3.Distance(_brain.transform.position, target.position);
        var hasLOS = _brain.Detection.HasLineOfSight(target, _brain.TargetProvider.GetTargetOffset());

        if (!hasLOS || distance > _brain.AttackRange)
        {
            Debug.Log($"hasLOS{hasLOS} / distance{distance}");
            Machine.ChangeState(new BloomMoveState(_brain, Machine));
            return;
        }

        _brain.Rotator.FacePosition(targetPos);
        if (_brain.Weapon.CanFire)
        {
            _brain.AnimatorDriver.TriggerAttack();
            _brain.Weapon.Fire(targetPos);
            _brain.Weapon2.Fire(targetPos);
        }

        if (distance < _brain.StopRange - 1f)
        {
            var kiteDir = (_brain.transform.position - target.position).normalized;
            _brain.Mover?.SetDestination(_brain.transform.position + kiteDir * 2f);
        }
    }
}
