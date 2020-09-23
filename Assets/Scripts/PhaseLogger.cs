using UnityEngine;

public class PhaseLogger : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Update");
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }
}