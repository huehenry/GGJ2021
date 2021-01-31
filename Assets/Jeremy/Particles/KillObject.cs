using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObject : MonoBehaviour
{
	float deathTimer = 5;
	float currentTimer= 0;

	public AudioClip[] explodeSounds;

	public void Awake()
	{
		GetComponent<AudioSource> ().PlayOneShot (explodeSounds[Random.Range(0, explodeSounds.Length)]);
	}

    // Update is called once per frame
    void Update()
    {
		currentTimer += Time.deltaTime;
		if (currentTimer >= deathTimer) {
			GameObject.Destroy (this.gameObject);
		}
    }
}
