using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class AlertState : State<EnemyController>
{
    [SerializeField] private float tiempoBusqueda = 3f;
    [SerializeField] private float radioBusqueda = 5f;
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        Debug.Log("¡El enemigo ha perdido al jugador! Moviéndose a la última posición conocida...");

        // Asegurar que el NavMeshAgent sigue funcionando
        controller.Agent.isStopped = false;
        controller.Agent.updatePosition = true;
        controller.Agent.updateRotation = true;
        controller.Agent.velocity = Vector3.zero;

        Debug.Log("Yendo a la última posición conocida: " + controller.UltimaPosicionConocida);
        controller.Agent.SetDestination(controller.UltimaPosicionConocida);

        controller.StartCoroutine(EsperarYBuscar());
    }


    private IEnumerator EsperarYBuscar()
    {
        Debug.Log("Iniciando búsqueda en la última posición conocida...");
        controller.Interrogacion.SetActive(true);

        float tiempoRestante = tiempoBusqueda;

        while (tiempoRestante > 0)
        {
            // Si el jugador es detectado, volver a persecución inmediatamente
            if (VerificarJugador())
            {
                Debug.Log("Jugador encontrado durante la búsqueda. Volviendo a persecución.");
                controller.ChangeState(controller.ChaseState);
                yield break;
            }

            // Generar un punto de búsqueda cercano dentro del radio
            Vector3 puntoBusqueda = GenerarPuntoBusqueda(controller.UltimaPosicionConocida);
            Debug.Log("Moviéndome a un punto de búsqueda: " + puntoBusqueda);

            // Verificar que el punto es accesible antes de moverse
            if (NavMesh.SamplePosition(puntoBusqueda, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                Debug.Log("Punto de búsqueda accesible: " + hit.position);
                controller.Agent.SetDestination(hit.position);
            }
            else
            {
                Debug.Log("Punto de búsqueda no accesible, generando otro...");
                continue;
            }

            // Esperar hasta que llegue al punto de búsqueda con un tiempo máximo
            float tiempoEspera = 3f; // Máximo tiempo para llegar
            while (tiempoEspera > 0)
            {
                if (!controller.Agent.pathPending && controller.Agent.remainingDistance <= 0.5f)
                {
                    break; // Salimos del loop si ha llegado
                }
                yield return new WaitForSeconds(0.5f);
                tiempoEspera -= 0.5f;
            }

            Debug.Log("Llegado al punto de búsqueda, esperando...");

            // Pequeña pausa antes de buscar otro punto
            yield return new WaitForSeconds(1f);

            tiempoRestante -= 1f;
        }

        // Si después de buscar no encuentra nada, vuelve a patrullar
        Debug.Log("Jugador no encontrado tras la búsqueda, volviendo a patrullar.");
        controller.ChangeState(controller.PatrolState);
    }


    private Vector3 GenerarPuntoBusqueda(Vector3 ultimaPosicion)
    {
        Vector3 puntoAleatorio;
        int intentos = 5; // Evita loops infinitos

        do
        {
            puntoAleatorio = ultimaPosicion + (Random.insideUnitSphere * Mathf.Clamp(radioBusqueda, 2f, 5f));
            puntoAleatorio.y = controller.transform.position.y;
            intentos--;
        }
        while (!NavMesh.SamplePosition(puntoAleatorio, out _, 2f, NavMesh.AllAreas) && intentos > 0);

        return puntoAleatorio;
    }

    private bool VerificarJugador()
    {
        // Verificar si el jugador está en el rango de visión
        Collider[] collsDetectados = Physics.OverlapSphere(controller.transform.position, controller.RangoVision, controller.QueEsTarget);

        foreach (var col in collsDetectados)
        {
            Vector3 direccionATarget = (col.transform.position - controller.transform.position).normalized;
            float distancia = Vector3.Distance(controller.transform.position, col.transform.position);

            if (Vector3.Angle(controller.transform.forward, direccionATarget) < controller.AnguloVision / 2 &&
                    !Physics.Raycast(controller.transform.position, direccionATarget, distancia, controller.QueEsObstaculo))
            {
                controller.Target = col.transform;
                return true;
            }
        }
        return false;
    }
    public override void OnUpdateState()
    {

    }

    public override void OnExitState()
    {
        controller.Interrogacion.SetActive(false);
    }
}
