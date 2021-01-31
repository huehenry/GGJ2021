using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsCheckpointOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance!=null) {
            GameManager.instance.lastMouseCheckpoint = GetComponent<MouseHole>();
        }
    }
}
