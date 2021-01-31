using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [Header("Camera")]
    public Camera currentCamera;
    [Header("Particles")]
    public GameObject entryParticle;
    public GameObject exitParticle;
    [Header("Audio")]
    public AudioClip enterHoleSound;
    public AudioClip exitHoleSound;
    public AudioClip moveHoleSound;
    public AudioClip mouseJump;
    public AudioClip mouseFallToDeath;
    public AudioClip trumpetSound;
    [Header("MouseHoleTweaks")]
    public float delayAfterEnter = 1.0f;
    public float delayAfterExit = 1.0f;
    public float delayAfterEnterBeforeMove = 0.5f;
    public float delayMove = 1.0f;
    [Header("DeathAndRespawn")]
    public float respawnTime = 2.5f;
    public MouseHole lastMouseCheckpoint;


    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
