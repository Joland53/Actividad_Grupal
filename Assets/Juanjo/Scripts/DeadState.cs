using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State<EnemyController>
{
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        Debug.Log("¡El enemigo ha muerto!");
        controller.ScoreManagerSO.DeadEnemy();
        controller.gameObject.SetActive(false);
        
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
