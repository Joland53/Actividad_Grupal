using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<EnemyController>
{

    [SerializeField]
    private float baseAttackDamage;


    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        controller.Agent.isStopped = true;
        controller.Agent.stoppingDistance = controller.AttackDistance;
        controller.Animator.SetBool("isAttacking", true);
        Debug.Log("Entramos en ataque! >.<");

    }
    public override void OnUpdateState()
    {
        if (controller.Target != null)
        {
            float distancia = Vector3.Distance(controller.transform.position, controller.Target.position);

            // El enemigo se mantiene en ataque si el jugador está dentro del 120% del rango de ataque
            if (distancia <= controller.AttackDistance * 1.2f)
            {
                FaceTarget();
            }
            else
            {
                // Cambia a persecución solo si el jugador está claramente lejos del rango de ataque
                Debug.Log("Jugador fuera de rango, volviendo a persecución.");
                controller.ChangeState(controller.ChaseState);
            }
        }
        else
        {
            Debug.Log("No hay objetivo, volviendo a patrullar.");
            controller.ChangeState(controller.PatrolState);
        }
    }

    private void FaceTarget()
    {
        Vector3 directionToTarget = (controller.Target.transform.position - transform.position).normalized;
        directionToTarget.y= 0;
        transform.rotation = Quaternion.LookRotation(directionToTarget);
    }
    public override void OnExitState()
    {
        controller.Agent.isStopped = false;
    }

    // Se ejecuta cuando Se TERMINA la animación de ataque.

    private void OnFinishAttackAnimation()
    {
        //Se nos ha escapado el jugador de nuestro rango de ataque.
        if (Vector3.Distance(controller.transform.position, controller.Target.position) > controller.AttackDistance)
        {
            Debug.Log("Jugador fuera de rango, volviendo a persecución.");
            controller.Animator.SetBool("isAttacking", false);
            controller.ChangeState(controller.ChaseState);
        }
    }
}
