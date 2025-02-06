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

        controller.Agent.isStopped = false;
        controller.Agent.speed = chaseVelocity;
        controller.Agent.stoppingDistance = controller.AttackDistance;
        controller.Agent.acceleration = 1000000f; // Para que no haya aceleración.
        Debug.Log("Entro en el estado de perseguir!");
    }

    public override void OnUpdateState()
    {
        if (controller.Target == null)
        {
            Debug.Log("Jugador perdido, volviendo a patrullar.");
            controller.ChangeState(controller.PatrolState);
            return;
        }

        float distancia = Vector3.Distance(controller.transform.position, controller.Target.position);

        // Verificar si el jugador está aún dentro del ángulo de visión y sin obstáculos.
        Vector3 direccionATarget = (controller.Target.position - controller.transform.position).normalized;
        bool jugadoreEnVision = Vector3.Angle(controller.transform.forward, direccionATarget) <= controller.AnguloVision / 2;
        bool obstaculoEnVision = Physics.Raycast(controller.transform.position, direccionATarget, distancia, controller.QueEsObstaculo);

        if (!jugadoreEnVision || obstaculoEnVision)
        {
            Debug.Log("Jugador fuera de visión u obtáculo boloqueando, volviendo a patrullar.");
            controller.ChangeState(controller.PatrolState);
            return;
        }

        // Si el jugador sige en rango y visión, entonces persigue.
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
