using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurprisedState : State<EnemyController>
{
    [SerializeField] private float tiempoSorpresa = 1f;
    [SerializeField] private float fuerzaSalto = 2f;

    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        controller.Agent.isStopped = true;
        Debug.Log("¡Sorpresa! El enemigo ha visto al jugador.");
        controller.Animator.SetTrigger("surprised");

        // Salto de sorpresa
        controller.StartCoroutine(EsperarYSaltar());
    }

    private IEnumerator  EsperarYSaltar()
    {
        Vector3 posicionInicial = controller.transform.position;
        float alturaMaxima = posicionInicial.y + fuerzaSalto;
        float tiempoSubida = 0.2f;
        float tiempoBajada = 0.2f;

        controller.Exclamacion.SetActive(true);
        //subir
        float t = 0;
        while (t < tiempoSubida)
        {
            controller.transform.position = Vector3.Lerp(posicionInicial, new Vector3(posicionInicial.x, alturaMaxima, posicionInicial.z), t / tiempoSubida);
            t += Time.deltaTime;
            yield return null;
        }

        // Bajar
        t = 0;
        while (t < tiempoBajada)
        {
            controller.transform.position = Vector3.Lerp(new Vector3(posicionInicial.x, alturaMaxima, posicionInicial.z), posicionInicial, t / tiempoBajada);
            t += Time.deltaTime;
            yield return null;
        }

        // Esperar un tiempo antes de cambiar a estado de persecución.
        yield return new WaitForSeconds(tiempoSorpresa);
        controller.ChangeState(controller.ChaseState);
    }

    public override void OnUpdateState()
    {

    }

    public override void OnExitState()
    {
        controller.Agent.isStopped = false;
        controller.Exclamacion.SetActive(false);
        controller.Animator.ResetTrigger("surprised"); // Reseteamos el trigger
        controller.Animator.SetFloat("velocity", 1f); // Aseguro que la animación de movimiento se actualice.
        Debug.Log("Saliendo del estado de sorpresa.");
    }
}
