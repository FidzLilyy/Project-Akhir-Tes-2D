using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	bool gerak = false, wall = false, enemyKena = false, kick = false, atas = false, bawah = false, moveKanan = false;
	public static bool transisiScene;
	public static bool player, ibu = false;
	private Vector3 posisiAwal;
	private Vector3 posisiAkhir;
	public LayerMask enemyLayer, wallLayer; 
	public string scene;

	public Pause pause;

	public GameObject transisi;
	Rigidbody2D rb;
	Animator animator;

	[SerializeField]
	Step StepSisa;
	public static int turn;

	// void Awake(){
	// 	DontDestroyOnLoad(transisi);
	// 	transisi.SetActive(true);
	// }
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		posisiAwal = transform.position;
		posisiAkhir = posisiAwal;
		player = true;
		if(pause.level == "Level0Fix"){
			ibu = false;
		}
		transisi.SetActive(true);
	}

	void Update()
	{
		if (!gerak && !Step.death && !player)
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
					//print(posisiAkhir);
					moveKanan = true;
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
					//print(posisiAkhir);
					moveKanan = true;
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
					atas = true;
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
					//print(posisiAkhir);
					bawah = true;
				}
				if(enemyKena){
					kick = true;
					StepSisa.StepLeft();
				}
			}
		}
		if (gerak)
		{
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
		// if(ibu = true)
		// {
		// 	animator.SetBool("Ibu", ibu);
		// }else{
			//float moveKanan = Mathf.Abs(transform.position.x - posisiAkhir.x);
			// float moveBawah = Mathf.Abs(transform.position.y - posisiAkhir.y);

			animator.SetBool("Kanan", moveKanan);
			// animator.SetFloat("MoveY", moveBawah);

			animator.SetBool("death", Step.death);
			animator.SetBool("Kick", kick);
			animator.SetBool("Atas", atas);
			animator.SetBool("Bawah", bawah);

			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			if(stateInfo.IsName("death_idle") && stateInfo.normalizedTime >= 1 && !animator.IsInTransition(0)){
				transisi.SetActive(true);
				transisiScene = true;
				player = true;
				if(!Step.death){
					SceneManager.LoadScene(scene);
					//transisi.SetActive(false);
					transisiScene = false;
					//Step.death = false;
				}
			}

			if((stateInfo.IsName("kick_idle") || stateInfo.IsName("Ibu_Kick")) && stateInfo.normalizedTime >= 1){
				kick = false;
			}

			if((stateInfo.IsName("Move_Atas") || stateInfo.IsName("Ibu_Atas")) && stateInfo.normalizedTime >= 1){
				atas = false;
			}

			if((stateInfo.IsName("Move_Bawah") || stateInfo.IsName("Ibu_Bawah")) && stateInfo.normalizedTime >= 1){
				bawah = false;
			}
			if((stateInfo.IsName("Move_Kanan") || stateInfo.IsName("Ibu_Kanan")) && stateInfo.normalizedTime >= 1){
				moveKanan = false;
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
