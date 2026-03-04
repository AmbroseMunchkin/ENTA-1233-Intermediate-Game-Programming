using UnityEngine;

public class BloomMoveState : EnemyState
{
    private readonly BloomBrain _brain;

    public BloomMoveState(BloomBrain brain, EnemyStateMachine machine) : base(machine)
    {
        _brain = brain;
    }

    public override void Tick()
    {
        var target = _brain.TargetProvider.GetTarget();
        if (target == null) return;

        var distance = Vector3.Distance(_brain.transform.position, target.position);
        var hasLOS = _brain.Detection.HasLineOfSight(target, _brain.TargetProvider.GetTargetOffset());

        if (hasLOS && distance <= _brain.AttackRange)
        {
            Machine.ChangeState(new BloomAttackState(_brain, Machine));
            return;
        }

        _brain.Mover?.SetDestination(target.position);

        if (_brain.Mover != null)
            _brain.AnimatorDriver.SetSpeed(_brain.Mover.Velocity.magnitude);
        else
            _brain.AnimatorDriver.SetSpeed(0);
    }
}
