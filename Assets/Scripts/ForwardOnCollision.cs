using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardOnCollision : MonoBehaviour
{
    public event Action<Collision> OnCollisionEnterForward;
    public event Action<Collision> OnCollisionExitForward;
    public event Action<Collision> OnCollisionStayForward;
    public event Action<Collider> OnTriggerEnterForward;
    public event Action<Collider> OnTriggerExitForward;
    public event Action<Collider> OnTriggerStayForward;
    public event Action<Collision2D> OnCollisionEnter2DForward;
    public event Action<Collision2D> OnCollisionExit2DForward;
    public event Action<Collision2D> OnCollisionStay2DForward;
    public event Action<Collider2D> OnTriggerEnter2DForward;
    public event Action<Collider2D> OnTriggerExit2DForward;
    public event Action<Collider2D> OnTriggerStay2DForward;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        OnCollisionEnterForward?.Invoke(other);
    }

    private void OnCollisionExit(Collision other)
    {
        OnCollisionExitForward?.Invoke(other);
    }

    private void OnCollisionStay(Collision other)
    {
        OnCollisionStayForward?.Invoke(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnCollisionEnter2DForward?.Invoke(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        OnCollisionExit2DForward?.Invoke(other);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        OnCollisionStay2DForward?.Invoke(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterForward?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitForward?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayForward?.Invoke(other);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter2DForward?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExit2DForward?.Invoke(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerStay2DForward?.Invoke(other);
    }
}