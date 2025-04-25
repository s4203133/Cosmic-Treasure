using UnityEngine;
using LMO;

public class ParrotHoverScript : MonoBehaviour
{
    //public GameObject companionAnchor;
    //public float speed = 0.3f; // how fast it'll catch up, 0.3 seconds
    //private Rigidbody rb;

    //////////////////////////////////////////////////

    public GameObject parrot;
    [Header("Scale Settings")]
    private float targetScale;
    private float currentScale;
    [Range(0f, 1f)]
    [SerializeField] private float smoothness;
    [SerializeField] private float parrotSize;

    //////////////////////////////////////////////////

    //Vector3 refVel = Vector3.zero;
    //public float smoothVal = .2f; // Higher = 'Smoother'

    void Start()
    {
        //companionAnchor = GameObject.Find("companionAnchor");
        //rb = GetComponent<Rigidbody>();
        targetScale = 0;
        currentScale = 0;
    }

    private void OnEnable() {
        PlayerHover.OnHoverStarted += ShowParrot;
        PlayerHover.OnHoverContinued += ShowParrot;
        PlayerHover.OnHoverEnded += HideParrot;
    }

    private void OnDisable() {
        PlayerHover.OnHoverStarted -= ShowParrot;
        PlayerHover.OnHoverContinued -= ShowParrot;
        PlayerHover.OnHoverEnded -= HideParrot;
    }

    void FixedUpdate()
    {
                /* float dist = Vector3.SqrMagnitude(transform.position - companionAnchor.transform.position);

                if (dist >= 10.0f)
                {
                    transform.position = Vector3.Lerp(transform.position, companionAnchor.transform.position, smoothVal);
                    //rb.MovePosition(Vector3.SmoothDamp(transform.position, companionAnchor.transform.position, ref refVel, smoothVal));
                }*/
        currentScale = Mathf.Lerp(currentScale, targetScale, smoothness);
        parrot.transform.localScale = Vector3.one * currentScale;
    }

    private void ShowParrot() {
        targetScale = parrotSize;
    }

    private void HideParrot() {
        currentScale = 0f;
        targetScale = 0f;
    }
}