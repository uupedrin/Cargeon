using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
	Rigidbody body;
	bool rotating = true;
	bool moving = false;
	bool isAdvancing = false;
	bool willAdvance = false;
	[SerializeField] float rotationSpeed;
	[SerializeField] int odds;
	[SerializeField] float moveForce;
	[SerializeField] float advanceForce;
	[SerializeField] float raycastDistance;
	[SerializeField] float time;
	[SerializeField] GameObject player;

	void Start()
	{
		body = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{   
		if(willAdvance)
		{
			transform.LookAt(player.transform);
		}
		else if(body.velocity.magnitude == 0)
		{
			isAdvancing = false;
			moving = false;
			rotating = true;
		}

		if(!isAdvancing && Brakes())
		{
			moving = false;
			rotating = true;
			body.constraints = RigidbodyConstraints.FreezeAll;
			body.constraints = RigidbodyConstraints.FreezeRotation;
		}
		
		if(rotating)
		{
			transform.Rotate(0, rotationSpeed, 0);
			if(Random.Range(0, odds) == 0)
			{
				rotating = false;
				moving = true;
			}
		}
		else if(moving)
		{
			body.AddForce(transform.forward * moveForce, ForceMode.Acceleration);
		}
	}

	void OnTriggerEnter()
	{
		Invoke("Advance", time);
		willAdvance = true;
		isAdvancing = true;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Bumper") body.AddForce((collision.gameObject.transform.position - transform.position) * -10, ForceMode.Impulse);
		else if(collision.gameObject.tag == "Enemy") Die();
		else if(collision.gameObject.tag == "Player") collision.gameObject.GetComponent<PlayerControls>().ManageHealth();
	}

	public bool Brakes()
	{
		return Physics.Raycast(transform.position, transform.forward, raycastDistance);
	}

	void Advance()
	{
		body.AddForce(transform.forward * moveForce, ForceMode.VelocityChange);
		willAdvance = false;
	}
	
	void Die()
	{
		GameManager.manager.enemyManager.enemiesAlive --;
		Destroy(gameObject);
	}
}
