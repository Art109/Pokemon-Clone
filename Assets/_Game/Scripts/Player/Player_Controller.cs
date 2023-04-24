using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed;
    bool isMoving;
    Vector2 input;
    private Vector3 offsetOverlap = new Vector3(0, -0.5f, 0);

    Animator animator;
    [SerializeField] LayerMask solidObjectMask;
    [SerializeField] LayerMask grassLayer;



    void Start()
    {
        animator= GetComponent<Animator>();
    }

   
    void Update()
    {
        // Movimenta��o vai ser de tile em tile 
        if (!isMoving)
        {

            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //Remove o movimento diagonal
            if (input.x != 0)
                input.y = 0;
           
            //N�o estou usando a velocity pra ter o controle exato do movimento
            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                Vector3 position = transform.position;
                position.x += input.x;
                position.y += input.y;

                if (isMovable(position))
                {
                    StartCoroutine(Move(position));
                }
                
            }
        }

        
        
        animator.SetBool("isMoving", isMoving);

        
    }

    IEnumerator Move(Vector3 position)
    {
        isMoving = true;
        while((position - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = position;
        isMoving= false;

        checkForEncounters();
    }

    private  bool isMovable(Vector3 position)
    {
        if (Physics2D.OverlapCircle(position, 0.2f, solidObjectMask))
        {
            return false;
        }
        return true;
    }

    private void checkForEncounters()
    { 
        if(Physics2D.OverlapCircle(transform.position + offsetOverlap, 0.2f, grassLayer) != null)
        {
            if(Random.Range(1,101) <= 10)
            {
                // logica de começar batalha aqui
                Debug.Log("A Wild Pokemon Appered");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position+ offsetOverlap,0.2f);
    }
}
