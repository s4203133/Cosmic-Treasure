using UnityEngine;

public class SharkDamageArea : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && GameObject.FindAnyObjectByType<SharkEnemy>().canAttack == true) {
            
            
            Debug.Log("do attack animation");
               
            GameObject.FindAnyObjectByType<SharkEnemy>().canAttack = false;
            
        }
    }
    
}
