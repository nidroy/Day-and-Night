using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    private ParallaxLayer[] layers;

    private void Awake()
    {
        foreach (ParallaxLayer layer in layers)
        {
            layer.Initialize();
        }
    }

    private void Update()
    {
        float deltaX = Camera.main.transform.position.x - transform.position.x;
        foreach (ParallaxLayer layer in layers)
        {
            layer.Move(deltaX);
        }
    }
}
