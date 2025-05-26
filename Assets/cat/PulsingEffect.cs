using UnityEngine;

public class PulsingEffect : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Vector3 maxScale = new Vector3(1.2f, 1.2f, 1.2f);
    [SerializeField] private Vector3 minScale = new Vector3(0.8f, 0.8f, 0.8f);

    [Header("Pulse Settings")]
    [SerializeField] private float pulseDuration = 1.0f;
    [SerializeField] private bool isPulsing = true;

    private float pulseTimer = 0f;
    private bool scalingUp = true;

    void Start()
    {
        if (targetObject == null)
        {
            targetObject = this.gameObject;
        }
    }

    void Update()
    {
        if (isPulsing)
        {
            PulseObject();
        }
    }

    void PulseObject()
    {
        pulseTimer += Time.deltaTime;
        float lerpFactor = pulseTimer / (pulseDuration / 2);

        if (scalingUp)
        {
            targetObject.transform.localScale = Vector3.Lerp(minScale, maxScale, lerpFactor);
        }
        else
        {
            targetObject.transform.localScale = Vector3.Lerp(maxScale, minScale, lerpFactor);
        }

        if (lerpFactor >= 1.0f)
        {
            scalingUp = !scalingUp;
            pulseTimer = 0f;
        }
    }
}

