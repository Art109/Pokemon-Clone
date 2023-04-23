using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    

    [SerializeField]private Camera battleCamera;
    [SerializeField]private BattleSystem battleSystem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle(){
        battleCamera.enabled = true;
    }

    public void EndBattle(){
        battleCamera.enabled = false;

    }

}
