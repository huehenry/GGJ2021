using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudioHandler : MonoBehaviour
{

	public AudioSource[] sfxAudioSources;
	public AudioSource music;
	public AudioSource ambience;
	public AudioSource mouseWalkLoop;
	public AudioClip lostSong;
	public AudioClip foundSong;
	public AudioClip[] mouseChatter;
	public AudioClip[] elephantChatter;

	public bool mouseTalking;
	public bool elephantTalking;
	public float mouseWalking;
	public float talkRateMouse = 0.2f;
	public float talkRateElephant = 0.275f;

	private int currentOneShot;
	private float timerElephant;
	private float timerMouse;


    // Update is called once per frame
    void Update()
    {
		if (mouseTalking == true) {
			timerMouse += Time.deltaTime;
			if (timerMouse >= talkRateMouse) {
				timerMouse = 0;
				sfxAudioSources [currentOneShot].PlayOneShot (mouseChatter [Random.Range (0, mouseChatter.Length)]);
				currentOneShot += 1;
				if (currentOneShot >= sfxAudioSources.Length) {
					currentOneShot = 0;
				}
			}
		}
		if (elephantTalking == true) {
			timerElephant += Time.deltaTime;
			if (timerElephant >= talkRateElephant) {
				timerElephant = 0;
				sfxAudioSources [currentOneShot].PlayOneShot (elephantChatter [Random.Range (0, mouseChatter.Length)]);
				currentOneShot += 1;
				if (currentOneShot >= sfxAudioSources.Length) {
					currentOneShot = 0;
				}
			}
		}
		if (Mathf.Abs(mouseWalking) > 0 && mouseWalkLoop.isPlaying == false) {
			//This was an attempt to use speed sas volume but it doesn't seem to update past the first initial press of the joystick
			mouseWalkLoop.volume = Mathf.Abs(mouseWalking*8);
			mouseWalkLoop.Play ();
		} else if (Mathf.Abs(mouseWalking)<=0) {
			mouseWalkLoop.Pause ();
			mouseWalkLoop.volume = 0;
		}
    }

	public void PlayMusic(bool mouse)
	{
		if (mouse == true) {
			music.clip = lostSong;
			music.Play ();
			ambience.Play ();
		} else {
			music.clip = foundSong;
			music.Play ();
		}
	}
}
