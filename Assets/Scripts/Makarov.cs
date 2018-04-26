using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Valve.VR.InteractionSystem;
using UnityEngine.Events;

public class Makarov : Weapon {

	public int bullets = 6;
	public int maxBullets = 6;

	public float reloadTime = 3f;

	public SoundPlayOneshot fireSound;
	public SoundPlayOneshot reloadSound;

	private bool reloading = false;

	private void HandAttachedUpdate( Hand hand )
	{
		base.HandAttachedUpdate(hand);
		if(hand.controller.GetHairTriggerDown()){
			Debug.Log("shot");
			fireSound.Play();
		}
	}





}
