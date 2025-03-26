using System.Linq;
using UnityEngine;

namespace LMO {
    public class ResettableObjectsManager : MonoBehaviour {

        IResettable[] resettables;

        void OnEnable() {
            resettables = FindObjectsOfType<MonoBehaviour>(true).OfType<IResettable>().ToArray();
            for(int i = 0; i < resettables.Length; i++) {
                resettables[i].Enable();
            }
        }

        void OnDisable() {
            for (int i = 0; i < resettables.Length; i++) {
                resettables[i].Disable();
            }
        }
    }
}