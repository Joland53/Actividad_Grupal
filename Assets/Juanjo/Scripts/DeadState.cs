using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State<EnemyController>
{
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        Debug.Log("�El enemigo ha muerto!");

        // Desactivar el movimiento del enemigo
        controller.Agent.isStopped = true;

        // Activar la animaci�n de muerte
        controller.Animator.SetTrigger("dead");

        // Sumar puntuaci�n
        controller.ScoreManagerSO.DeadEnemy();

        // Esperar antes de desactivar el enemigo
        controller.StartCoroutine(DesactivarEnemigo(controller, 5f)); // Esperar 3 segundos
    }

    // Corrutina para desactivar el enemigo despu�s de un tiempo
    private IEnumerator DesactivarEnemigo(EnemyController controller, float tiempoEspera)
    {
        yield return new WaitForSeconds(tiempoEspera);
        controller.gameObject.SetActive(false); // Desactiva el enemigo despu�s de la animaci�n
    }

    public override void OnUpdateState()
    {
        //
    }
    public override void OnExitState()
    {
        //
    }



}
