using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public Transform layerTransform;
    public float parallaxFactor;

    private Vector3 initialPosition;

    public void Initialize()
    {
        initialPosition = layerTransform.position;
    }

    public void Move(float deltaX)
    {
        Vector3 newPosition = initialPosition + Vector3.left * deltaX * parallaxFactor;
        layerTransform.position = newPosition;
    }
}
