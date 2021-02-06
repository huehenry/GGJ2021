using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilypadLerp : MonoBehaviour
{

	public float speed;

	public float xMin;
	public float xMax;

	private float lerpTimer;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
		lerpTimer += speed * Time.deltaTime;
		if (lerpTimer >= 2) {
			lerpTimer -= 2;
		}
		float newX = 0;
		if (lerpTimer >= 1) {
			newX = Mathf.SmoothStep (xMin, xMax, lerpTimer - 1);
		} else {
			newX = Mathf.SmoothStep (xMax, xMin, lerpTimer);
		}
		this.transform.localPosition = new Vector3 (newX, this.transform.localPosition.y, this.transform.localPosition.z);
    }
}
