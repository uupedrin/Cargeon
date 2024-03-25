using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
	Rigidbody body;
	[SerializeField] float initMoveForce;
	float moveForce;
	[SerializeField] float brakeForce;
	[SerializeField] float maxSpeed;
	[SerializeField] float rotateSpeed;
	[SerializeField] float velocityDamage;
	[SerializeField] float turboForce;
	[SerializeField] float raycastDistance;
	public int health = 3;
	Vector3 respawnLocation;

	void Start()
	{
		body = GetComponent<Rigidbody>();
		moveForce = initMoveForce;
	}

	void FixedUpdate()
	{
		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");
		Debug.Log(GroundedCheck());
		if(GroundedCheck()) moveForce = initMoveForce;
		else moveForce = initMoveForce / 2;
		body.AddForce(v * moveForce * transform.forward);
		transform.Rotate(h * rotateSpeed * transform.up);
		if(Input.GetKey(KeyCode.Space)) body.AddForce(body.velocity * -1 * brakeForce);
		if(body.velocity.magnitude > maxSpeed) body.velocity = Vector3.ClampMagnitude(body.velocity, maxSpeed);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Floor") respawnLocation = collision.transform.position;
		if(collision.gameObject.tag == "Bumper") body.AddForce((collision.gameObject.transform.position - transform.position) * -10, ForceMode.Impulse);
	}

	void OnTriggerEnter(Collider other)
	{
		switch(other.gameObject.tag)
		{
			case "Respawn":
			body.constraints = RigidbodyConstraints.FreezeAll;
			body.constraints = RigidbodyConstraints.FreezeRotation;
			transform.Rotate(-transform.rotation.eulerAngles);
			transform.position = new Vector3(respawnLocation.x, respawnLocation.y + 0.5f, respawnLocation.z);
			GameManager.manager.rPlatforms.RearrangePlatforms();
			break;

			case "Turbo":
			body.constraints = RigidbodyConstraints.FreezeAll;
			body.constraints = RigidbodyConstraints.FreezeRotation;
			body.AddForce(-other.transform.forward * turboForce);
			break;

			case "Finish":
			GameManager.manager.uiManager.SceneChange("Victory");
			GameManager.manager.countingTime = false;
			break;
			
			case "LVL01End":
			GameManager.manager.uiManager.SceneChange("PhaseTwo");
			GameManager.manager.countingTime = false;
			break;
		}
	}

	public bool GroundedCheck()
	{
		return Physics.Raycast(transform.position, -Vector3.up, raycastDistance);
	}
	
	public void ManageHealth(int amount = -1)
	{
		health += amount;
		GameManager.manager.uiManager.UpdatePlayerHealth(health);
		if(health<=0) Die();
	}
	
	private void Die()
	{
		health = 5;
		GameManager.manager.uiManager.SceneChange("Defeat");
	}
}
