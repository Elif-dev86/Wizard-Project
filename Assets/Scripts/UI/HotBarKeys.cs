using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HotBarKeys : MonoBehaviour
{
    
    public InputActionAsset actions;

    InputAction keyAction;

    Button button;

    public KeyCode key;

    public bool isReady = true;

    private void Start() 
    {

        keyAction = actions.FindActionMap("gameplay").FindAction("hotbarKeys");

        keyAction.performed += OnKeyPerformed;
    }

    void OnKeyPerformed(InputAction.CallbackContext context)
    {

        FindObjectOfType<HotBarKeys>();

        if (ConvertPathToKeyCode(context.control.ToString()) == key || ConvertPathToAlphaKeyCode(context.control.ToString()) == key)
        {

            button = this.GetComponentInChildren<Button>();

            if (button != null && isReady)
            {
                button.onClick.Invoke();
            }
            else
            {
                return;
            }
        }
    }

    private KeyCode ConvertPathToKeyCode(string path)
    {
        string keyPath = path.Replace("Key:/Keyboard/", "").ToUpper();

        if (System.Enum.TryParse(keyPath, out KeyCode keyCode))
        {
            return keyCode;
        }

        return KeyCode.None;
    }

    private KeyCode ConvertPathToAlphaKeyCode(string path)
    {
        string keyNum = path.Replace("Key:/Keyboard/", "Alpha");

        if (System.Enum.TryParse(keyNum, out KeyCode numCode))
        {
            return numCode;
        }

        return KeyCode.None;
    }

    private void OnDestroy() 
    {
        if (keyAction != null)
        {
            keyAction.performed -= OnKeyPerformed;
        }
    }

    void OnEnable()
    {
        actions.FindActionMap("gameplay").Enable();
    }
    void OnDisable()
    {
        actions.FindActionMap("gameplay").Disable();
    }
}
