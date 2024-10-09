using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class followPlayerCam : MonoBehaviour
{
    private Transform player;
    public float searchRadius = 10f; // Radius to search for the player
    public float searchRadiusOffsetX = 0f; // X Offset for the search radius
    public float searchRadiusOffsetY = 0f; // Y Offset for the search radius
    public GameObject camHead;
    public GameObject startLine;
    private LineRenderer lineRenderer;

    public Animator anim;

    private float contactTime = 0f;
    private bool isPlayerInContact = false;
    private bool stopFollowing = false;
    private bool isLineThick = false;

    Sounds sounds;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set up the LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2;

        sounds = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Sounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopFollowing)
            return;

        Vector3 adjustedPosition = new Vector3(transform.position.x + searchRadiusOffsetX, transform.position.y + searchRadiusOffsetY, transform.position.z);

        // Check if player is within the adjusted search radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(adjustedPosition, searchRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Vector3 direction = player.position - camHead.transform.position;
                RaycastHit2D hit = Physics2D.Linecast(startLine.transform.position, player.position);

                if (hit.collider != null && hit.collider.CompareTag("Player") == false)
                {
                    anim.SetBool("PlayerEntered", false);
                    sounds.StopLaserCharging();
                    anim.enabled = true;
                    
                    // If the linecast hits something that is not the player, disable the LineRenderer and stop camera rotation
                    lineRenderer.enabled = false;
                    isPlayerInContact = false;
                    contactTime = 0f;
                    return; // Exit the update function to stop the camera rotation
                }
                else
                {
                    sounds.PlayLaserCharging();
                    anim.SetBool("PlayerEntered", true);
                    anim.enabled = false;
                    // Otherwise, enable and update the LineRenderer positions
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, startLine.transform.position);
                    lineRenderer.SetPosition(1, player.position);

                    //sounds.StopLaserCharging();

                    // Rotate the camera to look at the player
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    camHead.transform.rotation = Quaternion.RotateTowards(camHead.transform.rotation, rotation, 10f);
                    // Track the player's contact time with the line
                    isPlayerInContact = true;
                }
                break; // Exit the loop once player is found
            }
        }

        if (!colliders.Any(c => c.CompareTag("Player")))
        {
            isPlayerInContact = false;
            contactTime = 0f;
            lineRenderer.enabled = false;
            sounds.StopLaserCharging();
        }

        if (isPlayerInContact)
        {
            
            contactTime += Time.deltaTime;
            if (contactTime > 3f)
            {
                // Increase the line width and stop following the player
                lineRenderer.startWidth = 0.1f;
                lineRenderer.endWidth = 0.1f;
                isLineThick = true;
                stopFollowing = true;
                
                player.GetComponent<Death>().Die();
                sounds.StopLaserCharging();
                sounds.PlayLaserShooting();

                StartCoroutine(ResumeFollowingAfterDelay(0.5f));
            }
        }
        else
        {
            contactTime = 0f;
            sounds.StopLaserCharging();
        }
    }

    private IEnumerator ResumeFollowingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        lineRenderer.startWidth = 0.03f; // Reset line width if desired
        lineRenderer.endWidth = 0.03f;
        isLineThick = false;
        stopFollowing = false;
        contactTime = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 adjustedPosition = new Vector3(transform.position.x + searchRadiusOffsetX, transform.position.y + searchRadiusOffsetY, transform.position.z);
        Gizmos.DrawWireSphere(adjustedPosition, searchRadius);
    }
}
