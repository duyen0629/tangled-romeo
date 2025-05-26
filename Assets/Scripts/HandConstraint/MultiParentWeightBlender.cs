using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MultiParentWeightLooper : MonoBehaviour
{
    public MultiParentConstraint multiParentConstraint;

    [Tooltip("Duration of one full blend (in seconds)")]
    public float blendDuration = 2f;

    private float timer = 0f;
    private bool isBlending = true;
    private bool forward = true; // true = Laundry → Basket, false = Basket → Laundry

    void Start()
    {
        SetSourceWeight(0, 1f); // Laundry
        SetSourceWeight(1, 0f); // Basket
    }

    void Update()
    {
        if (!isBlending) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / blendDuration);

        float weightA = forward ? 1f - t : t;
        float weightB = forward ? t : 1f - t;

        SetSourceWeight(0, weightA); // Laundry
        SetSourceWeight(1, weightB); // Basket

        if (t >= 1f)
        {
            // Reset and reverse direction
            timer = 0f;
            forward = !forward;
        }
    }

    private void SetSourceWeight(int index, float weight)
    {
        var sources = multiParentConstraint.data.sourceObjects;
        sources.SetWeight(index, weight);
        multiParentConstraint.data.sourceObjects = sources;
    }
}
