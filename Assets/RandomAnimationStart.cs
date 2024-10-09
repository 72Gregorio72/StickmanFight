using UnityEngine;
using System.Collections;

public class RandomAnimationStart : MonoBehaviour
{
    public Animator animator;  // L'animatore collegato a questo oggetto
    public string animationName;  // Il nome dell'animazione da avviare

    void Start()
    {
        // Avvia l'animazione con un ritardo casuale
        animator.Play(animationName, 0, Random.Range(0f, 1f));
    }
}
