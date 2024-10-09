using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeJump : MonoBehaviour
{
    public Animator anim;

    Sounds sounds;
    // Start is called before the first frame update
    void Start()
    {
        sounds = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Sounds>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().Jump(35f);
            anim.SetTrigger("Jumped");
            sounds.PlayBoing();
        }
    }
}
