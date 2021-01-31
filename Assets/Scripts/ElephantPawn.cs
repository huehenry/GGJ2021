using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantPawn : Pawn
{
	public CameraShake shaker;

	public void Update()
	{
		if(cameraShake==true)
		{
			shaker.ShakeCamera();
		}
	}


}
