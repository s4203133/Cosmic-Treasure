using LMO;
using UnityEngine;

public class SeagullGrappleKill : GrapplePoint
{

    public GameObject Seagull;
    public override void OnReleased() {
        base.OnReleased();
        
        Seagull.SetActive(false);
        //Destroy(Seagull);
         //GameObject.FindObjectOfType<DetectSwingJoints>().allJoints.remove
    }


}
