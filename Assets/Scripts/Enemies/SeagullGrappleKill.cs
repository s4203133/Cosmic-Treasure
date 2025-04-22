using LMO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SeagullGrappleKill : GrapplePoint
{

    public GameObject Seagull;
    public override void OnReleased() {
        base.OnReleased();

        Seagull.SetActive(false);       
    }
}
