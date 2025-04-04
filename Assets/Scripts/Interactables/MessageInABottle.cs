using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace LMO {

    [RequireComponent(typeof(BottleMessage))]
    public class MessageInABottle : MonoBehaviour, IBreakable {

        private BottleMessage message;

        private VisualEffect breakEffect;
        private MeshRenderer meshRenderer;
        private SphereCollider bottleCollider;
        private bool isBroken;

        private MessageInABottleEvents events;
        public static Action OnBroken;
        public static Action<string> OnMessageOpen;

        void Start() {
            message = GetComponent<BottleMessage>();

            breakEffect = GetComponentInChildren<VisualEffect>();
            meshRenderer = GetComponentInChildren<MeshRenderer>();    
            bottleCollider = GetComponent<SphereCollider>();
            isBroken = false;
        }

        void OnEnable() {
            if (events == null) {
                events = new MessageInABottleEvents();
            }
            events.Subscribe();
        }

        void OnDisable() {
            events.Unsubscribe();
        }

        public void Break() {
            if (!isBroken) {
                SmashBottle();
                StartCoroutine(OpenDelay());
                isBroken = true;
            }
        }

        private IEnumerator OpenDelay() {
            OnBroken?.Invoke();
            yield return new WaitForSeconds(0.5f);
            OnMessageOpen?.Invoke(message.Message);
        }

        private void SmashBottle() {
            if (breakEffect != null) {
                breakEffect.Play();
            }
            meshRenderer.enabled = false;
            bottleCollider.enabled = false;
        }
    }
}

namespace LMO {

    public class MessageInABottleEvents {

        private CameraShaker cameraShaker;
        private CinemachineFreeLook camera;

        private PlayerStateMachine playerStateMachine;

        private MessageInABottleUI messageInABottleUI;
        private Animator closeMessageInput;

        private PostProcessingHandler postProcessing;

        private bool init;

        private void Initialise() {
            if (init) {
                return;
            }

            cameraShaker = GameObject.FindObjectOfType<CameraShaker>();
            camera = Camera.main.GetComponent<CinemachineFreeLook>();

            playerStateMachine = GameObject.FindObjectOfType<PlayerStateMachine>();

            messageInABottleUI = GameObject.FindObjectOfType<MessageInABottleUI>();
            closeMessageInput = messageInABottleUI.CloseMessageInput.GetComponent<Animator>();
            closeMessageInput.SetTrigger("Hidden");

            postProcessing = GameObject.FindGameObjectWithTag("PostProcessing").GetComponent<PostProcessingHandler>();

            init = true;
        }

        public void Subscribe() {
            Initialise();
            MessageInABottle.OnBroken += OnBottleBroken;
            MessageInABottle.OnMessageOpen += OnMessageOpen;

            MessageInABottleUI.OnMessageFinishOpened += OnMessageFinishOpened;
            MessageInABottleUI.OnMessageClosed += OnMessageClosed;
        }

        public void Unsubscribe() {
            MessageInABottle.OnBroken -= OnBottleBroken;
            MessageInABottle.OnMessageOpen -= OnMessageOpen;

            MessageInABottleUI.OnMessageFinishOpened -= OnMessageFinishOpened;
            MessageInABottleUI.OnMessageClosed -= OnMessageClosed;
        }

        private void OnBottleBroken() {
            cameraShaker.shakeTypes.medium.Shake();
            playerStateMachine.Idle();
            playerStateMachine.Deactivate();
            //camera.enabled = false;
            camera.m_YAxis.m_MaxSpeed = 0f;
            camera.m_XAxis.m_MaxSpeed = 0f;
        }

        private void OnMessageOpen(string message) {
            postProcessing.BlurScreen();
            messageInABottleUI.DisplayMessage(message);
        }

        private void OnMessageFinishOpened() {
            closeMessageInput.SetTrigger("FadeIn");
            InputHandler.jumpStarted += messageInABottleUI.CloseMessage;
        }

        private void OnMessageClosed() {
            closeMessageInput.SetTrigger("FadeOut");
            InputHandler.jumpStarted -= messageInABottleUI.CloseMessage;
            playerStateMachine.Activate();
            //camera.enabled = true;
            camera.m_YAxis.m_MaxSpeed = 1.8f;
            camera.m_XAxis.m_MaxSpeed = 165f;
            postProcessing.ClearBlurScreen();
        }
    }
}