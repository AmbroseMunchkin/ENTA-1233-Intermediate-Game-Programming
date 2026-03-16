using UnityEngine;

public class SnakeChaseState : EnemyState
{
    private readonly SnakeBrain _brain;

    public SnakeChaseState(SnakeBrain brain, EnemyStateMachine machine) : base(machine)
    {
        _brain = brain;
    }

    public override void Tick()
    {
        var target = _brain.TargetProvider.GetTarget();

        if (target == null || !_brain.Detection.IsTargetInDetectionRange(target))
        {
            Machine.ChangeState(new SnakeIdleState(_brain, Machine));
            return;
        }

        _brain.Mover?.SetDestination(target.position);
        _brain.AnimatorDriver.SetSpeed(_brain.Mover?.Velocity.magnitude ?? 0f);

        if (_brain.Mover != null)
            _brain.AnimatorDriver.SetSpeed(_brain.Mover.Velocity.magnitude);
        else
            _brain.AnimatorDriver.SetSpeed(0);

        var distance = Vector3.Distance(_brain.transform.position, target.position);
        if (distance <= _brain.AttackRange) Machine.ChangeState(new SnakeAttackState(_brain, Machine));
    }
}
