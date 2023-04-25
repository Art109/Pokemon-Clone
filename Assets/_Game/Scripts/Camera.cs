using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    [Header("Atributos Inspector")]
    [SerializeField]private Transform player;

    //* constantes
    private const float smoothTime = .3f;
    private const int fixedPositionZ = -10;
    private const float speed = 5.0f;
    
    // variaveis da classe
    private Vector2 velocity;

    void FixedUpdate()
    {
        if(!player){
            return;
        }
            Vector3 smooth = Vector2.SmoothDamp(
                transform.position, 
                player.position,
                ref velocity,
                smoothTime,
                speed);
            
           smooth.z = fixedPositionZ;
           transform.position = smooth;
    }
}
