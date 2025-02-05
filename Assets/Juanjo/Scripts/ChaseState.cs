using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State<EnemyController>
{
    [SerializeField]
    private float chaseVelocity;
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);

        controller.Agent.speed = chaseVelocity;
        controller.Agent.stoppingDistance = controller.AttackDistance;
        controller.Agent.acceleration = 1000000f; // Para que no haya aceleración.
        Debug.Log("Entro en el estado de perseguir!");
    }

    public override void OnUpdateState()
    {
        controller.Agent.SetDestination(controller.Target.position);

        // No tengo calculos pendientes y mi distancia hacia el target es menor que la distancia de parada.
        if (!controller.Agent.pathPending && controller.Agent.remainingDistance <= controller.Agent.stoppingDistance)
        {
            controller.ChangeState(controller.AttackState);
        }
    }

    public override void OnExitState()
    {
       
    }


}
