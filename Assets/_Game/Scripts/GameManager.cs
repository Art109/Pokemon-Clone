using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState { FreeRoam, Battle}

public class GameManager : MonoBehaviour
{
    

    [SerializeField]private Camera battleCamera;
    [SerializeField]private BattleSystem battleSystem;
    [SerializeField] private Player_Controller playerController;

    GameState state;




    private void OnEnable()
    {
        Player_Controller.OnPokemonFind+= StartBattle;
        BattleSystem.OnBattleEnd += EndBattle;
    }

    private void Update()
    {
        if(state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if(state == GameState.Battle)
        { 
            battleSystem.HandleUpdate();
        }
    }

    void StartBattle(){
        state= GameState.Battle;
        battleSystem.gameObject.SetActive(true);  
        battleSystem.StartBattle();
    }

    void EndBattle(){
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);  
    }


    private void OnDisable()
    {
        Player_Controller.OnPokemonFind-= StartBattle;
        BattleSystem.OnBattleEnd -= EndBattle;

    }

}
