using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<EnemyController>
{
    [SerializeField]
    private float timeBetweenAttacks;

    [SerializeField]
    private float baseAttackDamage;

    private float timer;

    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        timer = timeBetweenAttacks;
        controller.Agent.isStopped = true;
        controller.Agent.stoppingDistance = controller.AttackDistance;
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
                timer += Time.deltaTime;
                if (timer >= timeBetweenAttacks)
                {
                    Debug.Log("Hago Daño!");
                    timer = 0;
                }
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
    public override void OnExitState()
    {
        controller.Agent.isStopped = false;
    }

}
