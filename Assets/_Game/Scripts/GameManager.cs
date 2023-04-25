using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    

    [SerializeField]private Camera battleCamera;
    [SerializeField]private BattleSystem battleSystem;


    private void OnEnable()
    {
        Player_Controller.OnPokemonFind+= StartBattle;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartBattle(){
        battleSystem.gameObject.SetActive(true);  
    }

    void EndBattle(){
        battleSystem.gameObject.SetActive(false);  
    }


    private void OnDisable()
    {
        Player_Controller.OnPokemonFind-= StartBattle; 

    }

}
