using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool gerak = false, wall = false, tabrakan = false, hancur1 =  false, hancur2 = false;
    private Vector3 posisiAwal;
	private Vector3 posisiAkhir;
	public LayerMask wallLayer, enemyLayer;
    public int HP;

    Animator animator;

	void Start(){
        posisiAwal = transform.position;
		posisiAkhir = posisiAwal;
        animator = GetComponent<Animator>();
    }

    void Update(){
        if(gerak){
            if(!wall && !tabrakan){
                transform.position = Vector3.Lerp(transform.position, posisiAkhir, Time.deltaTime * 7f);

                if (transform.position == posisiAkhir)
                {
                    gerak = false;
                    posisiAwal = transform.position;
                }
            }else{
                gerak = false;
            }
        }

        Animation();
    }

    // void WallDetection(Push){
        
    // }

    public void Push(Vector3 direction)
    {
        EnemyDetection(direction);
        HP--;

        if(HP <= 0){
            if(gameObject.tag == "Enemy1"){
                hancur1 = true;
            }
            if(gameObject.tag == "Enemy2"){
                hancur2 = true;  
            }
        }
    }

	void EnemyDetection(Vector3 playerDirection)
	{
		posisiAkhir = transform.position + playerDirection;
        gerak = true;

        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, playerDirection, 1f, wallLayer);

        if (hitWall.collider != null)
		{
			wall = true;
            print("BANGGGGGG");
		}else{
			wall = false;
		}

        //Vector2 rayStart = transform.position + playerDirection * 0.5f;
        RaycastHit2D hitEnemy = Physics2D.Raycast((transform.position + playerDirection * 0.5f), playerDirection, 1f, enemyLayer);

        if (hitEnemy.collider != null)
		{
			tabrakan = true;
            print("KENA");
		}else{
			tabrakan = false;
		}
	}

    void Animation(){
        animator.SetBool("Hancur1", hancur1);
        animator.SetBool("Hancur2", hancur2);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("Hancur_Animation1") && stateInfo.normalizedTime >= 1){
			//hancur1 = false;
            Destroy(gameObject);
		}
        if(stateInfo.IsName("Hancur_Animation2") && stateInfo.normalizedTime >= 1){
			//hancur1 = false;
            Destroy(gameObject);
		}
    }
}
