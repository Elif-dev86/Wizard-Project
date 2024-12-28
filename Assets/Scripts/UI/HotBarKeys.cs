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

    private void Start() 
    {
        button = GetComponentInChildren<Button>();

        keyAction = actions.FindActionMap("gameplay").FindAction("hotbarKeys");

        keyAction.performed += OnKeyPerformed;
    }

    void OnKeyPerformed(InputAction.CallbackContext context)
    {
        if (ConvertPathToKeyCode(context.control.ToString()) == key || ConvertPathToAlphaKeyCode(context.control.ToString()) == key)
        {
            if (button != null)
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

    void OnEnable()
    {
        actions.FindActionMap("gameplay").Enable();
    }
    void OnDisable()
    {
        actions.FindActionMap("gameplay").Disable();
    }
}
