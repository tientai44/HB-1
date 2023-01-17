using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IdleState : IState
{
    float randomTime;
    float timer;
    public void OnEnter(EnemyController enemy)
    {
        enemy.StopMoving();
        timer = 0;
        randomTime = Random.Range(2.5f, 4.0f);
    }

    public void OnExecute(EnemyController enemy)
    {
        timer += Time.deltaTime;
        if (timer > randomTime)
        {
            enemy.ChangeState(new PatrolState());
        }
        
    }

    public void OnExit(EnemyController enemy)
    {
        
    }
}
