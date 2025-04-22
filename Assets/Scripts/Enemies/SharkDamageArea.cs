using UnityEngine;

public class SharkDamageArea : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && GameObject.FindAnyObjectByType<SharkEnemy>().canAttack == true) {
            //PlayerHealth -= DamageAmount;
            //if (PlayerHealth < currentPlayerHealth) {
            //    currentPlayerHealth = PlayerHealth;
            //deal damage to player
            Debug.Log("deal Damage");
               
            GameObject.FindAnyObjectByType<SharkEnemy>().canAttack = false;
            //}
        }
    }
    
}
