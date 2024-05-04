using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which represents enemies that fly
/// </summary>
public class FlyingEnemy : Enemy
{
    public enum BehaviorAtStopDistance    { Stop, CircleClockwise, CircleAnticlockwise    }

    [Header("Flying Enemy Settings")]
    public float stopDistance = 5.0f;
    public BehaviorAtStopDistance stopBehavior = BehaviorAtStopDistance.CircleClockwise;

    protected override Vector3 CalculateDesiredMovement()
    {
        if (target != null)
        {
            if ((target.position - transform.position).magnitude > stopDistance)
            {
                return transform.position + (target.position - transform.position).normalized * moveSpeed * Time.deltaTime;
            }
            else
            {
                switch (stopBehavior)
                {
                    case BehaviorAtStopDistance.Stop:
                        break;
                    case BehaviorAtStopDistance.CircleClockwise:
                        return transform.position + Vector3.Cross(target.position - transform.position, transform.up).normalized * moveSpeed * Time.deltaTime;
                    case BehaviorAtStopDistance.CircleAnticlockwise:
                        return transform.position - Vector3.Cross(target.position - transform.position, transform.up).normalized * moveSpeed * Time.deltaTime;
                }
            }
        }
        return base.CalculateDesiredMovement();
    }

    protected override Quaternion CalculateDesiredRotation()
    {
        if (target != null)
        {
            return Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        }
        return base.CalculateDesiredRotation();
    }
}
