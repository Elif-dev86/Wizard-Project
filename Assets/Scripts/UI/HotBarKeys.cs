using System.Collections;
using System.Collections.Generic;
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
        if (key == ConvertPathToKeyCode(context.control.ToString()))
        {
            button.onClick.Invoke();
        }
    }

    private KeyCode ConvertPathToKeyCode(string path)
    {
        string key = path.Replace("Key:/Keyboard/", "").ToUpper();

        if (System.Enum.TryParse(key, out KeyCode keyCode))
        {
            return keyCode;
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
