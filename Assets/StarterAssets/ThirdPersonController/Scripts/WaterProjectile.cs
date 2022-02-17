using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProjectile : MonoBehaviour
{
	//[SerializeField] private Transform vfxHit;
	private Rigidbody waterRB;

	private void Awake()
	{
		waterRB = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		float speed = 30f;
		waterRB.velocity = transform.forward * speed;
	}

	private void OnTriggerEnter(Collider other)
	{
		//if(other.GetComponent<WaterTarget>()!= null)
		//{
		//	Instantiate(vfxHit, transform.position.normalized, Quaternion.identity);
		//}
		//else
		//{
		//	Instantiate(vfxHit, transform.position.normalized, Quaternion.identity);
		//}
		Destroy(gameObject);
	}
}
