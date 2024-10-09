using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootCannonBall : MonoBehaviour
{
    public GameObject cannonBallPrefab;

    public Transform firePoint;

    public float fireRate = 1f;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        GameObject cannonBall = Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation);
        cannonBall.GetComponent<Rigidbody2D>().velocity = transform.right * 10f;
    }

}
