using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public GameObject shakeMe;
	public float strength= 0.5f;
	private float timer;
	private Vector3 resetPos;

	void Start()
	{
		resetPos = shakeMe.transform.localPosition;
	}

    // Update is called once per frame
    void Update()
    {

		if (timer > 0) {
			timer -= Time.deltaTime;
			float newX = resetPos.x + Random.Range (-strength, strength);
			float newY = resetPos.y + Random.Range (-strength, strength);
			float newZ = resetPos.z + Random.Range (-strength, strength);
			Vector3 target = new Vector3 (newX, newY, newZ);
			shakeMe.transform.localPosition = Vector3.Lerp (shakeMe.transform.localPosition, target, 0.1f);
		} else {
			timer = 0;
			shakeMe.transform.localPosition = Vector3.Lerp (shakeMe.transform.localPosition, resetPos, 0.1f);
		}
    }

	public void ShakeCamera()
	{
		timer = 0.1f;
	}
}
