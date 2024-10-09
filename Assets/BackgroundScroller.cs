using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    [SerializeField] GameObject player;  // Reference to the player object

    Material myMaterial;
    Vector2 offSet;
    Vector3 lastPlayerPosition;  // Track the player's last position

    void Start()
    {
        // Initialize the player's last position
        lastPlayerPosition = player.transform.position;
        myMaterial = GetComponent<Renderer>().material; // Cache the material reference
    }

    // Update is called once per frame
    void Update()
    {
        ScrollBackground();

        transform.position = player.transform.position;
    }

    void ScrollBackground()
    {
        // Calculate the player's movement since the last frame
        Vector3 playerMovement = player.transform.position - lastPlayerPosition;
        
        // Update the offset based on player movement and scroll speed
        offSet = new Vector2(playerMovement.x, playerMovement.y) * backgroundScrollSpeed;

        // Apply the offset to the material texture
        myMaterial.mainTextureOffset += offSet;

        // Update the last player position to the current position
        lastPlayerPosition = player.transform.position;
    }

}
