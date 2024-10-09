using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    private Image spriteRenderer;
    public Sprite Gold;
    public Sprite Silver;
    public Sprite Bronze;
    public Sprite Dev;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<Image>();

        if (Mathf.Round(LevelData.finishedTime * 100) / 100 <= Mathf.Round(LevelData.DevTimer * 100) / 100)
        {
            spriteRenderer.sprite = Dev;
        }
        else if (Mathf.Round(LevelData.finishedTime * 100) / 100 <= Mathf.Round(LevelData.goldTime * 100) / 100)
        {
            spriteRenderer.sprite = Gold;
        }
        else if (Mathf.Round(LevelData.finishedTime * 100) / 100 <= Mathf.Round(LevelData.silverTime * 100) / 100)
        {
            spriteRenderer.sprite = Silver;
        }
        else
        {
            spriteRenderer.sprite = Bronze;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
