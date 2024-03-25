using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	[SerializeField] Camera playerCamera;
	void Update()
	{
		transform.LookAt(playerCamera.transform);
	} 
}
