using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create")]
public class PokemonBase : ScriptableObject
{
    [SerializeField] string pName;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;


    [Header("BASE STATUS")]
    [SerializeField] int maxHP;
    [SerializeField] int atk;
    [SerializeField] int def;
    [SerializeField] int spAtk;
    [SerializeField] int spDef;
    [SerializeField] int spd;

    [SerializeField] List<LearnableMove> learnableMoves;

    public string Name
    { get { return pName; } }

    public string Description
    { get { return description; } }
    public Sprite BackSprite
    { get { return backSprite; } }
    public Sprite FrontSprite
    { get { return frontSprite; } }
    public PokemonType Type1
    { get { return type1; } }
    public PokemonType Type2
    { get { return type2; } }

    public int MaxHP
    { get { return maxHP; } }

    public int Atk
    { get { return atk; } }

    public int Def
    { get { return def; } }
    public int SpAtk
    { get { return spAtk; } }
    public int SpDef
    { get { return spDef; } }

    public int Spd
    { get { return spd; } }

    public List<LearnableMove> LearnableMoves
    { get { return learnableMoves; } }


    [System.Serializable]
    public class LearnableMove
    {
        [SerializeField] MoveBase moveBase;
        [SerializeField] int level;

        public MoveBase Base
        {
            get { return moveBase; }
        }

        public int Level
        {
            get { return level; }
        }
    }

    

}
