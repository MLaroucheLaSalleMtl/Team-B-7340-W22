using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class AimController : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera aimCamera;
	[SerializeField] private float normalSens;
	[SerializeField] private float aimSens;
	[SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
	[SerializeField] private Transform debugTransform;
	[SerializeField] private Transform waterProjectile;
	[SerializeField] private Transform spawnProjectilePos;

	private ThirdPersonController thirdPersonController;
	private StarterAssetsInputs starterAssetsInputs;
	private Animator animator;


	private void Awake()
	{
		thirdPersonController = GetComponent<ThirdPersonController>();
		starterAssetsInputs = GetComponent<StarterAssetsInputs>();
		animator = GetComponent<Animator>();
	}
	private void Update()
	{
		Vector3 mouseWorldPosition = Vector3.zero;
		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
		if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
		{
			//debugTransform.position = raycastHit.point;
			mouseWorldPosition = raycastHit.point;
		}
		
		if (starterAssetsInputs.aim)
		{
			aimCamera.gameObject.SetActive(true);
			thirdPersonController.SetSens(aimSens);
			thirdPersonController.SetRotateOnMove(false);
			animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1),1f,Time.deltaTime *10f));

			Vector3 worldAim = mouseWorldPosition;
			worldAim.y = transform.position.y;
			Vector3 aimDir = (worldAim - transform.position).normalized;

			transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20f);

			
			
		}
		else
		{
			aimCamera.gameObject.SetActive(false);
			thirdPersonController.SetSens(normalSens);
			thirdPersonController.SetRotateOnMove(true);
			animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
			starterAssetsInputs.shoot = false;
		}

		if (starterAssetsInputs.shoot && starterAssetsInputs.aim)
		{
			Vector3 aimDir = (mouseWorldPosition - spawnProjectilePos.position).normalized;
			Instantiate(waterProjectile, spawnProjectilePos.position, Quaternion.LookRotation(aimDir, Vector3.up));
			starterAssetsInputs.shoot = false;
		}


	}
}