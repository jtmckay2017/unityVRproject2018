using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {
	//Is the transform of [CameraRig].
	public Transform cameraRigTransform; 
	// Stores a reference to the teleport reticle prefab.
	public GameObject teleportReticlePrefab;
	// 3
	private GameObject reticle;
	// 4
	private Transform teleportReticleTransform; 
	// 5
	public Transform headTransform; 
	// 6
	public Vector3 teleportReticleOffset; 
	// 7
	public LayerMask teleportMask; 
	// 8
	private bool shouldTeleport;

	private SteamVR_TrackedController trackedObj;

	public GameObject laserPrefab;

	//reference to an instance of the laser
	private GameObject laser;

	private Transform laserTransform;

	private Vector3 hitPoint;

	private SteamVR_Controller.Device Controller
	{
	    get { return SteamVR_Controller.Input((int)trackedObj.controllerIndex); }
	}

	void Awake()
	{
	    trackedObj = GetComponent<SteamVR_TrackedController>();

	}

	void Start()
	{
		laser = Instantiate(laserPrefab);
		laserTransform = laser.transform;
		reticle = Instantiate(teleportReticlePrefab);
		teleportReticleTransform = reticle.transform;
	}

	void Update()
	{
		ControlLaser();
		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
		{
		    Teleport();
		}
	}

	private void ShowLaser(RaycastHit hit)
	{
	    // 1
	    laser.SetActive(true);
	    // 2
	    laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
	    // 3
	    laserTransform.LookAt(hitPoint); 
	    // 4
	    laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
	        hit.distance);
	}

	void ControlLaser()
	{
			// 1
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
		{
		    RaycastHit hit;

		    // 2
			if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
		    {
		        hitPoint = hit.point;
		        ShowLaser(hit);
								// 1
				reticle.SetActive(true);
				// 2
				teleportReticleTransform.position = hitPoint + teleportReticleOffset;
				// 3
				shouldTeleport = true;
		    }
		}
		else // 3
		{
		    laser.SetActive(false);
			reticle.SetActive(false);
		}
	}

	private void Teleport()
	{
	    // 1
	    shouldTeleport = false;
	    // 2
	    reticle.SetActive(false);
	    // 3
	    Vector3 difference = cameraRigTransform.position - headTransform.position;
	    // 4
	    difference.y = 0;
	    // 5
	    cameraRigTransform.position = hitPoint + difference;
	}
}
