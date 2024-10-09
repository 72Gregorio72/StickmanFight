using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBackground : MonoBehaviour
{
    public Material backgroundMaterial;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (backgroundMaterial != null)
        {
            Graphics.Blit(source, destination, backgroundMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
