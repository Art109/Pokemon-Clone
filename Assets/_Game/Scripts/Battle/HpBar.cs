using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] GameObject health;
    // Start is called before the first frame update
 

    public void setHP(float currentHP)
    {
        health.transform.localScale= new Vector3(currentHP, 1f);
    }
 
}
