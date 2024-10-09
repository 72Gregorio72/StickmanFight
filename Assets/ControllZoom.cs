using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ControllZoom : MonoBehaviour
{
    public CinemachineFollowZoom followZoom;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");     
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerMovement>().horizontalMovement != 0 || player.GetComponent<PlayerMovement>().rb.velocity.y != 0) {
            followZoom.m_Width = 100; 
        } else {
            followZoom.m_Width = 0; 
        }
        
    }
}
