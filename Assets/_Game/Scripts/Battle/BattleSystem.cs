using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState
{
    Start,
    PlayerAction,
    PlayerMove,
    EnemyMove,
    Busy,
}
public class BattleSystem : MonoBehaviour
{

    [Header("ATRIBUTOS PLAYER")]
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;


    [Header("ATRIBUTOS INIMIGO")]
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud enemyHud;

    [SerializeField] BattleDialogBox dialogBox;

    BattleState state;

    int currentAction;


    private void OnEnable()
    {
        Player_Controller.OnPokemonFind+= StartBattle;  // "StartBattle()" é chamado quando o jogador encontra um pokemon
    }
    private void Start()
    {
        StartCoroutine(SetupBattle());
        
    }


    private void StartBattle(){
        StartCoroutine(SetupBattle());
    }
    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        playerHud.SetData(playerUnit.Pokemon);

        enemyUnit.Setup();
        enemyHud.SetData(enemyUnit.Pokemon);

        yield return dialogBox.TypeDialog($"A wild { enemyUnit.Pokemon.Base.Name} appeared");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    void PlayerAction()
    {

        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);

    }

    private void Update()
    {
        if(state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
    }

    void HandleActionSelection()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentAction < 1)
                ++currentAction;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currentAction > 0)
            {
                --currentAction;
            }
        }
    }

    private void OnDisable()
    {
        Player_Controller.OnPokemonFind-= StartBattle;
    }

}
