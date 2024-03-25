using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	[SerializeField] EnemyMoving[] enemies;
	[SerializeField] GameObject floor;
	public int enemiesAlive;
	void Start()
	{
		GameManager.manager.enemyManager = this;
		enemiesAlive = enemies.Length;
	}
	
	void Update()
	{
		if(enemiesAlive <= 0)
		{
			floor.GetComponent<Collider>().isTrigger = true;
			floor.tag = "Finish";
		}
	}
}
