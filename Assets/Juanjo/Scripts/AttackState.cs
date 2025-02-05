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
        Debug.Log("Entramos en ataque! >.<");
        timer = timeBetweenAttacks;

    }
    public override void OnUpdateState()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks)
        {
            Debug.Log("Hago Daño!");
            timer = 0;
        }
    }
    public override void OnExitState()
    {
        
    }

}
