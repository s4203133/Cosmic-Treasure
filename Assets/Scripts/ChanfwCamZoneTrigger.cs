using LMO;
using UnityEngine;

public class ChanfwCamZoneTrigger : MonoBehaviour
{
    [SerializeField] private CameraSwapper camSwapper;
    public bool activate;

    private void OnTriggerEnter(Collider other) {
        camSwapper.canChange = activate;
    }
}
