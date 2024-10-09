using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public static TilemapManager Instance { get; private set; }

    public Tilemap currentTilemap;
    public TilemapRenderer currentTilemapRenderer;
 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTilemap(Tilemap tilemap, TilemapRenderer tilemapRenderer)
    {
        currentTilemap = tilemap;
        currentTilemapRenderer = tilemapRenderer;
    }
}
