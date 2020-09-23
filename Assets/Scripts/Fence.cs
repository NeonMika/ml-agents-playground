using System.Collections;
using System.Collections.Generic;
using Attributes;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Fence : MonoBehaviour
{
    [HideInInspector]
    public BoxCollider2D Collider;

    [NotNullField]
    public World World;
    
    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        transform.localPosition = new Vector3(
            Random.Range(-World.Size.x / 2f  * 0.9f, World.Size.x / 2f  * 0.9f),
            Random.Range(-World.Size.y / 2f  * 0.9f, World.Size.x / 2f  * 0.9f),
            0f);
        Collider = GetComponent<BoxCollider2D>();
    }
}
