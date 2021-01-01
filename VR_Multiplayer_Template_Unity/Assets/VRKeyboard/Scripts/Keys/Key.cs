using UnityEngine;
using UnityEngine.UI;
namespace VRKeyboard.Utils
{
    public class Key : MonoBehaviour
    {
        protected Text key;

        public delegate void OnKeyClickedHandler(string key);

        // The event which other objects can subscribe to
        // Uses the function defined above as its type
        public event OnKeyClickedHandler OnKeyClicked;

        public virtual void Awake()
        {
            key = transform.Find("Text").GetComponent<Text>();
            GetComponent<Button>().onClick.AddListener(() =>
            {

                OnKeyClicked(key.text);
            });
        }

        public virtual void CapsLock(bool isUppercase) { }
        public virtual void ShiftKey() { }
    };
}