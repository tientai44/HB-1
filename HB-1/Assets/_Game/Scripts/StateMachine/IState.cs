using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    void OnEnter(EnemyController enemy);

    void OnExecute(EnemyController enemy);
    void OnExit(EnemyController enemy);
}
