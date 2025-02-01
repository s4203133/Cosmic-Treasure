using UnityEngine;

public abstract class GroundPoundComponent {

    public abstract void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded);

    public abstract void Disable();

}


[System.Serializable]
public class GroundPoundBuildUp : GroundPoundComponent {
    [SerializeField] private float buildUpDuration;
    private float buildUpTimer;
    private bool starting;

    PlayerGroundPound groundPound;
    private Rigidbody rigidBody;

    public override void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded) {
        groundPound = targetGroundPound;
        groundPound.OnGroundPoundInitialised += StartGroundPound;
        rigidBody = playerRigidbody;
    }

    public override void Disable() {
        groundPound.OnGroundPoundInitialised -= StartGroundPound;
    }

    public void StartGroundPound() {
        starting = true;
        buildUpTimer = buildUpDuration;
        DisableVelocity();
    }

    private void DisableVelocity() {
        rigidBody.velocity = Vector3.zero;
        rigidBody.useGravity = false;
    }

    public void HandleGroundPoundBuildUp() {
        // While the ground pound is starting, remove all velocity from the player
        if (starting) {
            rigidBody.velocity = Vector3.zero;
            CountdownBuildUp();
        }
    }

    private void CountdownBuildUp() {
        buildUpTimer -= Time.fixedDeltaTime;
        if (buildUpTimer <= 0) {
            EndGroundPoundBuildUp();
        }
    }

    private void EndGroundPoundBuildUp() {
        starting = false;
        rigidBody.useGravity = true;
    }

    public bool IsFinished() {
        return !starting;
    }

    public void ResetTimer() {
        buildUpTimer = buildUpDuration;
    }
}



[System.Serializable]
public class GroundPoundFalling : GroundPoundComponent {
    [SerializeField] private float groundPoundSpeed;

    private Rigidbody rigidBody;

    public override void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded) {
        rigidBody = playerRigidbody;
    }

    public void HandleGroundPoundFalling() {
        // While the ground pound is performing, apply a downwards force to the player, and check if they've reached the ground
        rigidBody.velocity = new Vector3(0, -groundPoundSpeed, 0);
    }

    public override void Disable() {

    }
}



[System.Serializable]
public class GroundPoundLanded : GroundPoundComponent {
    [SerializeField] private float landDuration;
    private float landTimer;

    private Grounded grounded;

    public override void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded) {
        this.grounded = grounded;
    }

    public bool CheckLanded() {
        if (grounded.IsOnGround) {
            landTimer = landDuration;
            return true;
        }
        return false;
    }

    public void HandleLand() {
        landTimer -= Time.fixedDeltaTime;
    }

    public bool FinishedLand() {
        return (landTimer <= 0);
    }

    public override void Disable() {

    }
}



[System.Serializable]
public class GroundPoundColliders : GroundPoundComponent {
    [SerializeField] private Collider fallingCollider;
    [SerializeField] private Collider landCollider;

    private PlayerGroundPound groundPound;

    public override void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded) {
        groundPound = targetGroundPound;
        groundPound.OnGroundPoundStarted += EnableFallingCollider;
        groundPound.OnGroundPoundLanded += EnableLandingCollider;
        groundPound.OnGroundPoundFinished += DisableColliders;
    }

    public override void Disable() {
        groundPound.OnGroundPoundStarted -= EnableFallingCollider;
        groundPound.OnGroundPoundLanded -= EnableLandingCollider;
        groundPound.OnGroundPoundFinished -= DisableColliders;
    }

    public void EnableFallingCollider() {
        fallingCollider.enabled = true;
        landCollider.enabled = false;
    }

    public void EnableLandingCollider() {
        fallingCollider.enabled = false;
        landCollider.enabled = true;
    }

    public void DisableColliders() {
        fallingCollider.enabled = false;
        landCollider.enabled = false;
    }
}



[System.Serializable]
public class DiveTimer : GroundPoundComponent {
    // How long of the action needs to run before allowing the player to dive
    [SerializeField] private float timeUntilDiveCanStart;
    [HideInInspector] public bool canInitiateDive;
    private float allowDiveTimer;

    PlayerGroundPound groundPound;

    public override void Initialise(PlayerGroundPound targetGroundPound, Rigidbody playerRigidbody, Grounded grounded) {
        groundPound = targetGroundPound;
        groundPound.OnGroundPoundInitialised += InitialiseTimer;
        groundPound.OnGroundPoundLanded += DoNotAllowDive;
    }

    public override void Disable() {
        groundPound.OnGroundPoundInitialised -= InitialiseTimer;
        groundPound.OnGroundPoundLanded -= DoNotAllowDive;

    }

    private void InitialiseTimer() {
        allowDiveTimer = timeUntilDiveCanStart;
        canInitiateDive = false;
    }

    public void CountdownTimer() {
        allowDiveTimer -= Time.fixedDeltaTime;
        if (allowDiveTimer <= 0) {
            canInitiateDive = true;
        }
    }

    private void DoNotAllowDive() {
        canInitiateDive = false;
    }
}
