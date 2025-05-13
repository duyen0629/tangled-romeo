using UnityEngine;
using System.Collections;

public class AddRiggingBodyAfterDelay : MonoBehaviour
{
    // private Transform mainCameraTransform;
    public float delaySecond;
    public float drag;

    void Start()
    {
        // mainCameraTransform = Camera.main.transform;
        StartCoroutine(AnimateObject());
    }

    IEnumerator AnimateObject()
    {
        yield return new WaitForSeconds(delaySecond);

        // Move the object out from under the basket
        transform.SetParent(null);

        // Add Rigidbody if not already present
        if (!GetComponent<Rigidbody>())
        {
            gameObject.AddComponent<Rigidbody>();
        }
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.linearDamping = drag; // Increase this value to slow down the fall more
    }
}
