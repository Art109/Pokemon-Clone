using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    private const string sceneGame = "Game";
    [SerializeField]private Button btnPlayGame;
    [SerializeField]private Button btnOptions;
    [SerializeField]private Button btnQuitGame;

    private void OnEnable()
    {
        btnPlayGame.onClick.AddListener(OnButtonPlayGame);
        btnOptions.onClick.AddListener(OnButtonOptions);
        btnQuitGame.onClick.AddListener(OnButtonQuitGame);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnButtonPlayGame(){
        // começar o game
        SceneManager.LoadScene(sceneGame);
    }
    private void OnButtonOptions(){
        // abrir o canvas de opções
    }
    private void OnButtonQuitGame(){
        // sair do jogo
        Application.Quit();
    }

    private void OnDisable()
    {
        btnPlayGame.onClick.RemoveAllListeners();
        btnOptions.onClick.RemoveAllListeners();
        btnQuitGame.onClick.RemoveAllListeners();
    }
}
