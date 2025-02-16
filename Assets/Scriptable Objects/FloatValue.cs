using UnityEngine;

public class FloatValue {

    [SerializeField] private FloatVariable floatVariable;
    [Space(10)]
    [SerializeField] private bool useCustomValue;
    [SerializeField] private float value;

    public void SetValue(float newValue) {
        if (useCustomValue) {
            value = newValue;
        } else {
            floatVariable.value = newValue;
        }
    }

    public float GetValue() {
        if (useCustomValue) {
            return value;
        } else {
            return floatVariable.value;
        }
    }
}
