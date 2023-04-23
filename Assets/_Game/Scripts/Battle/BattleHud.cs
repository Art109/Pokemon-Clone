using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text txtName;
    [SerializeField] Text txtLevel;
    [SerializeField] HpBar hpBar;

    
    public void SetData(Pokemon pokemon)
    {
        txtName.text = pokemon.Base.Name;
        txtLevel.text = $"Lvl {pokemon.Level}";
        hpBar.setHP((float)pokemon.HP/ pokemon.MaxHP);
    }


    // tentar chamar esse método assim que um pokemon atacar o outro para atualizar a UI
    public void UpdateStatus(Pokemon pokemon){
        hpBar.setHP((float)pokemon.HP/ pokemon.MaxHP);

    }
}
