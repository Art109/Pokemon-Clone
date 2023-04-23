using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{

    [Header("ATRIBUTOS PLAYER")]
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;


    [Header("ATRIBUTOS INIMIGO")]
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud enemyHud;


    private void Start()
    {
        SetupBattle();
    }

    public void SetupBattle()
    {
        playerUnit.Setup();
        playerHud.SetData(playerUnit.Pokemon);

        enemyUnit.Setup();
        enemyHud.SetData(enemyUnit.Pokemon);

    }


}
