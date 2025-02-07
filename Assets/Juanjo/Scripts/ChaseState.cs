using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State<EnemyController>
{

    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);

        controller.Agent.isStopped = false;
        controller.Agent.stoppingDistance = controller.AttackDistance;
        controller.Agent.speed = controller.MaximunVelocity;
        
        controller.Animator.ResetTrigger("surprised"); // Reseteamos el trigger
        controller.Animator.SetFloat("velocity", 1f); // Aseguro que la animación de movimiento se actualice.

        Debug.Log("Entro en el estado de perseguir!");
    }

    public override void OnUpdateState()
    {
        float velocidadNormalizada = controller.Agent.velocity.magnitude / controller.MaximunVelocity;
        controller.Animator.SetFloat("velocity", Mathf.Clamp(velocidadNormalizada, 0.1f, 1f)); //Actualizo la animación de movimiento.

        if (controller.Target == null)
        {
            Debug.Log("Jugador perdido, volviendo a patrullar.");
            controller.ChangeState(controller.PatrolState);
            return;
        }

        float distancia = Vector3.Distance(controller.transform.position, controller.Target.position);
        Vector3 direccionATarget = (controller.Target.position - controller.transform.position).normalized;
        bool jugadorEnVision = Vector3.Angle(controller.transform.forward, direccionATarget) <= controller.AnguloVision / 2;
        bool obstaculoEnMedio = Physics.Raycast(controller.transform.position, direccionATarget, distancia, controller.QueEsObstaculo);

        if (jugadorEnVision && !obstaculoEnMedio)
        {
            // ACTUALIZAR LA ÚLTIMA POSICIÓN CONOCIDA
            controller.UltimaPosicionConocida = controller.Target.position;
        }
        else
        {
            Debug.Log("Jugador fuera de visión u obstáculo bloqueando, cambiando a estado de alerta.");
            controller.ChangeState(controller.AlertState);
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
