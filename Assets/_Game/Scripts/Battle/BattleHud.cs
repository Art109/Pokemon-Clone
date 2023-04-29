using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text txtName;
    [SerializeField] Text txtLevel;
    [SerializeField] HpBar hpBar;

    Pokemon _pokemon;

    public void SetData(Pokemon pokemon)
    {
        _pokemon = pokemon;

        txtName.text = pokemon.Base.Name;
        txtLevel.text = $"Lvl {pokemon.Level}";
        hpBar.setHP((float)pokemon.HP/ pokemon.MaxHP);
    }


    // tentar chamar esse método assim que um pokemon atacar o outro para atualizar a UI
    public IEnumerator UpdateHP(){
        yield return hpBar.SetHPSmooth((float)_pokemon.HP/ _pokemon.MaxHP);

    }

 
}
