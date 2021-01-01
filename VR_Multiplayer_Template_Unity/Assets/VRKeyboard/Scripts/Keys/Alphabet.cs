using UnityEngine;
using UnityEngine.UI;

namespace VRKeyboard.Utils
{
    public class Alphabet : Key
    {
        public override void CapsLock(bool isUppercase)
        {
            if (isUppercase)
            {
                key.text = key.text.ToUpper();
            }
            else
            {
                key.text = key.text.ToLower();
            }
        }
    }
}