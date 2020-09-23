using Attributes;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Target : MonoBehaviour
{
    [HideInInspector] public Collider2D Collider;

    [NotNullField] public World World;

    // Start is called before the first frame update
    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
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
        Collider = GetComponent<Collider2D>();
    }
}