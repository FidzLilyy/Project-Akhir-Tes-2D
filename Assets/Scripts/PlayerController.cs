using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	bool gerak = false, wall = false, enemyKena = false, kick = false;
	public static bool player;
	private Vector3 posisiAwal;
	private Vector3 posisiAkhir;
	public LayerMask enemyLayer, wallLayer; 
	public string scene;

	Rigidbody2D rb;
	Animator animator;

	[SerializeField]
	Step StepSisa;
	public static int turn;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		posisiAwal = transform.position;
		posisiAkhir = posisiAwal;
		player = true;
	}

	void Update()
	{
		if (!gerak && !Step.death)
		{
			if (Input.GetKeyDown(KeyCode.D) && !kick)
			{
				EnemyDetection(Vector2.right);
				
				if(!wall && !enemyKena)
				{
					posisiAkhir = new Vector3(posisiAwal.x + 1f, transform.position.y, 0f);
					gerak = true;
					StepSisa.StepLeft();
					transform.localScale = new Vector3 (0.7f,transform.localScale.y,transform.localScale.z);
				}
				if(enemyKena){
					kick = true;
					StepSisa.StepLeft();
					transform.localScale = new Vector3 (0.7f,transform.localScale.y,transform.localScale.z);
				}
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				EnemyDetection(Vector2.left);
				
				if(!wall && !enemyKena)
				{
					posisiAkhir = new Vector3(posisiAwal.x + -1f, transform.position.y, 0f);
					gerak = true;
					StepSisa.StepLeft();
					transform.localScale = new Vector3 (-0.7f,transform.localScale.y,transform.localScale.z);
				}
				if(enemyKena){
					kick = true;
					StepSisa.StepLeft();
					transform.localScale = new Vector3 (-0.7f,transform.localScale.y,transform.localScale.z);
				}
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				EnemyDetection(Vector2.up);

				if(!wall && !enemyKena)
				{
					posisiAkhir = new Vector3(transform.position.x, posisiAwal.y + 1f, 0f);
					gerak = true;
					StepSisa.StepLeft();
				}
				if(enemyKena){
					kick = true;
					StepSisa.StepLeft();
				}
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				EnemyDetection(Vector2.down);

				if(!wall && !enemyKena)
				{
					posisiAkhir = new Vector3(transform.position.x, posisiAwal.y + -1f, 0f);
					gerak = true;
					StepSisa.StepLeft();
				}
				if(enemyKena){
					kick = true;
					StepSisa.StepLeft();
				}
			}
		}
		if (gerak)
		{
			print(posisiAkhir);
			transform.position = Vector3.MoveTowards(transform.position, posisiAkhir, 5 * Time.deltaTime);

			if (transform.position == posisiAkhir)
			{
				gerak = false;
				posisiAwal = transform.position;
			}
		}

		Animation();
	}

	void Animation(){
		float moveKanan = Mathf.Abs(transform.position.x - posisiAkhir.x);
		float moveBawah = Mathf.Abs(transform.position.y - posisiAkhir.y);

		if(moveBawah > 0){
			moveKanan = 0;
		}

		animator.SetFloat("Move", moveKanan);
		animator.SetFloat("MoveY", moveBawah);

		animator.SetBool("death", Step.death);
		animator.SetBool("Kick", kick);

		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		if(stateInfo.IsName("death_idle") && stateInfo.normalizedTime >= 1 && !animator.IsInTransition(0)){
			SceneManager.LoadScene(scene);
			Step.death = false;
		}

		if(stateInfo.IsName("kick_idle") && stateInfo.normalizedTime >= 1){
			kick = false;
		}
	}

	void EnemyDetection(Vector2 playerDirection)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection, 1f, enemyLayer);

		if (hit.collider != null)
		{
			hit.collider.GetComponent<Enemy>().Push(playerDirection);
			enemyKena = true;
		}else{
			enemyKena = false;
		}

		RaycastHit2D hitTembok = Physics2D.Raycast(transform.position, playerDirection, 1f, wallLayer);


		if (hitTembok.collider != null)
		{
			wall = true;
		}else{
			wall = false;
		}
	}
}