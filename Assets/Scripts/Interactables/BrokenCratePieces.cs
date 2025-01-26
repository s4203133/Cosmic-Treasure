using UnityEngine;

public class BrokenCratePieces : MonoBehaviour {

    private Transform thisTransform;

    [SerializeField] private AnimationCurve sizeReduction;
    private float timer;
    private float duration;

    void Awake() {
        thisTransform = transform;
        duration = sizeReduction.keys[sizeReduction.keys.Length - 1].time;
    }

    void FixedUpdate() {
        thisTransform.localScale = CalculateSize();
        timer += Time.deltaTime;

        if(timer > duration ) {
            Destroy(thisTransform.parent.gameObject);
        }
    }

    private Vector3 CalculateSize() {
        float size = sizeReduction.Evaluate(timer);
        return new Vector3(size, size, size);
    }
}
