using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokemonParty : MonoBehaviour
{
    [SerializeField] List<Pokemon> pokemons;


    public List<Pokemon> Pokemons { 
        get { return pokemons; } 
    }

    private void Start()
    {
        foreach (var pokemon in pokemons)
        {
            pokemon.Init();
        }
    }

    public Pokemon GetHealthyPokemon()
    {
        //Funcao where vem do System.linq
        //Where procura na lista o pokemon com a condição definida e o firstOrDefault procura o primeiro que satifaz essa condição
        return pokemons.Where(x => x.HP > 0).FirstOrDefault();
    }
}
