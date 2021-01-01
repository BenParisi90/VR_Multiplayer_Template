/***
 * Author: Yunhan Li
 * Any issue please contact yunhn.lee@gmail.com
 ***/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace VRKeyboard.Utils
{
    public class KeyboardManager : MonoBehaviour
    {
        #region Public Variables
        [SerializeField]
        NetworkingManager networkingManager;
        [Header("User defined")]
        [Tooltip("If the character is uppercase at the initialization")]
        public bool isUppercase = false;

        [Header("UI Elements")]
        public Text inputText;

        [Header("Essentials")]
        public Transform keys;
        #endregion

        #region Private Variables
        private string Input
        {
            get { return inputText.text; }
            set { inputText.text = value; }
        }
        private Key[] keyList;
        private bool capslockFlag;
        #endregion

        #region Monobehaviour Callbacks
        void Awake()
        {
            keyList = keys.GetComponentsInChildren<Key>();
        }

        void Start()
        {
            foreach (var key in keyList)
            {
                key.OnKeyClicked += GenerateInput;
            }
            capslockFlag = isUppercase;
            CapsLock();
        }
        #endregion

        #region Public Methods
        public void Enter()
        {
            networkingManager.JoinFriendsGame(Input);
        }

        public void Backspace()
        {
            if (Input.Length > 0)
            {
                Input = Input.Remove(Input.Length - 1);
            }
            else
            {
                return;
            }
        }

        public void Clear()
        {
            Input = "";
        }

        public void Exit()
        {
            //launcher.SelectOption(MenuOption.BACK_OUT_OF_MULTIPLAYER_KEYBOARD);
        }

        public void CapsLock()
        {
            foreach (var key in keyList)
            {
                if (key is Alphabet)
                {
                    key.CapsLock(capslockFlag);
                }
            }
            capslockFlag = !capslockFlag;
        }

        public void Shift()
        {
            foreach (var key in keyList)
            {
                if (key is Shift)
                {
                    key.ShiftKey();
                }
            }
        }

        public void GenerateInput(string s)
        {
            Input += s;
            if(Input.Length == 5)
            {
                networkingManager.JoinFriendsGame(Input);
                Clear();
            }
        }
        #endregion
    }
}