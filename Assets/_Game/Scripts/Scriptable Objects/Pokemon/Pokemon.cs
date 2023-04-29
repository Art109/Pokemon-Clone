using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Pokemon
{
    public PokemonBase Base { get; set; }
    public int Level { get; set; }


    public int HP { get; set; }

    public List<Move> Moves { get; set; }

    public Pokemon(PokemonBase @base, int level)
    {
        Base = @base;
        this.Level = level;
        HP = MaxHP;

        Moves= new List<Move>();
        foreach(var move in Base.LearnableMoves)
        {
            if(move.Level <= level)
            {
                Moves.Add(new Move(move.Base));

                if (Moves.Count >= 4)
                    break;
            }
        }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Atk * Level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Def * Level) / 100f) + 5; }
    }
    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpAtk * Level) / 100f) + 5; }
    }
    public int SpDefense
    {
        get { return Mathf.FloorToInt((Base.SpDef * Level) / 100f) + 5; }
    }
    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Spd * Level) / 100f) + 5; }
    }

    public int MaxHP
    {
        get { return Mathf.FloorToInt((Base.MaxHP * Level) / 100f) + 10; }
    }

    public bool TakeDamage(Move move, Pokemon attacker)
    {
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP < 0)
        {
            HP = 0;
            return true;
        }
        return false;
    }

    public Move GetRandomMove() 
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }

}
