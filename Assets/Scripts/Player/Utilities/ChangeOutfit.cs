using UnityEngine;

public class ChangeOutfit : MonoBehaviour {

    [SerializeField] private Material originalCoatMaterial;
    [SerializeField] private Material originalTShirtaterial;

    [Space(10)]
    [SerializeField] private SkinnedMeshRenderer coat;
    [SerializeField] private SkinnedMeshRenderer tShirt;

    public void ResetMaterial() {
        coat.material = originalCoatMaterial;
        tShirt.material = originalTShirtaterial;
    }

    public void ChangeMaterial(Material mat) {
        coat.material = mat;
        tShirt.material = mat;
    }
}
