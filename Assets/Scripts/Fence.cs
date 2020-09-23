using Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Fence : MonoBehaviour
{
    [HideInInspector] public BoxCollider2D Collider;

    [NotNullField] public World World;

    void Awake()
    {
        Collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        transform.localPosition = new Vector3(
            Random.Range(-World.Size.x / 2f * 0.9f, World.Size.x / 2f * 0.9f),
            Random.Range(-World.Size.y / 2f * 0.9f, World.Size.x / 2f * 0.9f),
            0f);
        Collider = GetComponent<BoxCollider2D>();
    }
}