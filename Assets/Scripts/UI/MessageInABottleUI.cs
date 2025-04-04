using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace LMO {

    public class MessageInABottleUI : MonoBehaviour {

        [SerializeField] private Animator animator;
        [SerializeField] private TextMeshProUGUI messageTextBox;

        [SerializeField] private float timeToOpenMessage;
        private WaitForSeconds openMessageTime;
        [SerializeField] private float timeToCloseMessage;
        private WaitForSeconds closeMessageTime;

        [Header("Close Message UI")]
        [SerializeField] private GameObject closeMessageInput;
        public GameObject CloseMessageInput => closeMessageInput;

        public static Action OnMessageFinishOpened;
        public static Action OnMessageClosed;

        private void Start() {
            openMessageTime = new WaitForSeconds(timeToOpenMessage);
            closeMessageTime = new WaitForSeconds(timeToCloseMessage);
        }

        public void DisplayMessage(string message) {
            StartCoroutine(DisplayMessageLogic(message));   
        }

        public void CloseMessage() {
            StartCoroutine(CloseMessageLogic());
        }

        private IEnumerator DisplayMessageLogic(string message) {
            messageTextBox.text = message;
            animator.SetTrigger("Open");
            yield return openMessageTime;
            OnMessageFinishOpened?.Invoke();
        }

        private IEnumerator CloseMessageLogic() {
            animator.SetTrigger("Close");
            yield return closeMessageTime;
            OnMessageClosed?.Invoke();
        }
    }
}