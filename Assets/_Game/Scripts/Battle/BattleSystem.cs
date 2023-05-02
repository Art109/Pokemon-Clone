using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public enum BattleState
{
    Start,
    ActionSelection,
    MoveSelection,
    PerformMove,
    Busy,
    BattleOver,
}
public class BattleSystem : MonoBehaviour
{

    [Header("ATRIBUTOS PLAYER")]
    [SerializeField] BattleUnit playerUnit;


    [Header("ATRIBUTOS INIMIGO")]
    [SerializeField] BattleUnit enemyUnit;



    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] PartyScreen partyScreen;

    BattleState state;

    public static event System.Action OnBattleEnd;

    int currentAction;
    int currentMove;

    PokemonParty playerParty;
    Pokemon wildPokemon;


    


    public void StartBattle(PokemonParty playerParty, Pokemon wildPokemon){
        this.playerParty = playerParty;
        this.wildPokemon = wildPokemon;
        StartCoroutine(SetupBattle());
    }
    public IEnumerator SetupBattle()
    {
        playerUnit.Setup(playerParty.GetHealthyPokemon());
        enemyUnit.Setup(wildPokemon);

        partyScreen.Init();

        dialogBox.SetMovesNames(playerUnit.Pokemon.Moves);

        yield return dialogBox.TypeDialog($"A wild { enemyUnit.Pokemon.Base.Name} appeared");
        yield return new WaitForSeconds(1f);

        ActionSelection();
    }


    void BattleOver(bool won){
        state = BattleState.BattleOver;
        //OnBattleEnd(won);
        OnBattleEnd?.Invoke();
    }
    void ActionSelection()
    {

        state = BattleState.ActionSelection;
        dialogBox.SetDialog("Choose an action");
        dialogBox.EnableActionSelector(true);
        

    }

    void OpenPartyScreen()
    {
        
        partyScreen.SetPartyData(playerParty.Pokemons);
        partyScreen.gameObject.SetActive(true);
    }

    void MoveSelection()
    {
        state = BattleState.MoveSelection;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PlayerMove()
    {
        state = BattleState.PerformMove;

        var move = playerUnit.Pokemon.Moves[currentMove];
        yield return RunMove(playerUnit,enemyUnit,move);

        // If the battle stat was not changed by RunMove(), then go to next step
        if (state == BattleState.PerformMove)
        {
            StartCoroutine(EnemyMove());
        }
        
            
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.PerformMove;

        var move = enemyUnit.Pokemon.GetRandomMove();
        yield return RunMove(enemyUnit, playerUnit,move);

        // If the battle stat was not changed by RunMove(), then go to next step
        if (state == BattleState.PerformMove)
        {
            ActionSelection();
        }

    }

    IEnumerator RunMove(BattleUnit sourceUnit, BattleUnit targetUnity, Move move){
        move.PP--;
        yield return dialogBox.TypeDialog($"{sourceUnit.Pokemon.Base.name} used {move.Base.Name}");

        sourceUnit.PlayAttackAnimation();

        yield return new WaitForSeconds(1f);


        targetUnity.PlayHitAnimation();
        bool isFainted = targetUnity.Pokemon.TakeDamage(move, sourceUnit.Pokemon);
        yield return targetUnity.Hud.UpdateHP();

        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{targetUnity.Pokemon.Base.Name} Fainted");
            targetUnity.PlayFaintedAnimation();

            yield return new WaitForSeconds(2f);
            //OnBattleEnd();
            CheckForBattleOver(targetUnity);
        }
    }

    void CheckForBattleOver(BattleUnit faintedUnit){
        if (faintedUnit.IsPlayUnit)
        {
            var nextPokemon = playerParty.GetHealthyPokemon();
            if(nextPokemon != null){
                OpenPartyScreen();
            }
            else{
                //OnBattleEnd(false);
                //OnBattleEnd?.Invoke();
                BattleOver(false);
            }
        }
        else{
            //OnBattleEnd(true);
            //OnBattleEnd?.Invoke();
            BattleOver(true);
        }
    }

    public void HandleUpdate()
    {
        if(state == BattleState.ActionSelection)
        {
            HandleActionSelection();
        }
        else if(state == BattleState.MoveSelection)
        {
            HandleMoveSelection();
        }
    }

    void HandleActionSelection()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            ++currentAction;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            --currentAction;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentAction -= 2;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentAction += 2;
        }

        currentAction = Mathf.Clamp(currentAction, 0, 3);


        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(currentAction == 0)
            {
                //Fight
                MoveSelection();
            }
            if(currentAction == 1) 
            {
                //Run
                OnBattleEnd();
            }
            if (currentAction == 2)
            {
                //Bag
            }

            if (currentAction == 3)
            {
                //Switch Pokemon
                OpenPartyScreen();

            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ++currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            --currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentMove -= 2;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentMove += 2;
        }

        currentMove = Mathf.Clamp(currentMove, 0, playerUnit.Pokemon.Moves.Count - 1);


        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Pokemon.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.X))
        {
            dialogBox.EnableDialogText(true);
            dialogBox.EnableMoveSelector(false);

            ActionSelection();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);

            StartCoroutine(PlayerMove());
        }

    }

    IEnumerator SwitchPokemon(Pokemon newPokemon){
        if (playerUnit.Pokemon.HP > 0)
        {
            yield return dialogBox.TypeDialog($"Come back {playerUnit.Pokemon.Base.name}");
            playerUnit.PlayFaintedAnimation();
            yield return new WaitForSeconds(2f);
        }

        playerUnit.Setup(newPokemon);
        dialogBox.SetMovesNames(newPokemon.Moves);
        yield return dialogBox.TypeDialog($"Go {newPokemon.Base.name}!");

        StartCoroutine(EnemyMove());
    }

    private void OnDisable()
    {
        //Player_Controller.OnPokemonFind-= StartBattle;
    }

}
