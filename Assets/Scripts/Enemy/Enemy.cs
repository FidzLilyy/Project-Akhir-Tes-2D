using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool gerak = false, wall = false, tabrakan = false;
    private Vector3 posisiAwal;
	private Vector3 posisiAkhir;
	public LayerMask wallLayer, enemyLayer;
    public int HP;

	void Start(){
        posisiAwal = transform.position;
		posisiAkhir = posisiAwal;
    }

    void Update(){


        //if(!gerak){
        //    if(Input.GetKeyDown(KeyCode.D) && KenaKiri.kena)
        //    {
        //        posisiAkhir = new Vector3(posisiAwal.x + 1f, transform.position.y, 0f);
        //        gerak = true;
        //    }else if(Input.GetKeyDown(KeyCode.A) && KenaKanan.kena)
        //    {
        //        posisiAkhir = new Vector3(posisiAwal.x + -1f, transform.position.y, 0f);
        //        gerak = true;
        //    }else if(Input.GetKeyDown(KeyCode.W) && KenaBawah.kena){
        //        posisiAkhir = new Vector3(transform.position.x, posisiAwal.y + 1f, 0f);
        //        gerak = true;
        //    }else if(Input.GetKeyDown(KeyCode.S) && KenaAtas.kena){
        //        posisiAkhir = new Vector3(transform.position.x, posisiAwal.y + -1f, 0f);
        //        gerak = true;
        //    }
        //}

        //if(gerak){
        //        transform.position = Vector3.MoveTowards(transform.position, posisiAkhir, 5 * Time.deltaTime);

        //        if (transform.position == posisiAkhir)
        //        {
        //            gerak = false;
        //            posisiAwal = transform.position;
        //        }
        //}
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
    }

    // void WallDetection(Push){
        
    // }

    public void Push(Vector3 direction)
    {
        EnemyDetection(direction);
        HP--;
        /*if(HP <= 0 && wall || tabrakan){
            Destroy(gameObject);
        }*/

        if(HP <= 0){
            Destroy(gameObject);
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
}
