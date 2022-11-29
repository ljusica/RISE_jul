using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    public InputControls inputControls;
    [HideInInspector] public InputAction horizontal, vertical, interact;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Destroy(instance);
        }

        inputControls = new InputControls();
        inputControls.Enable();
        vertical = inputControls.Actions.Vertical;
        horizontal = inputControls.Actions.Horizontal;
        interact = inputControls.Actions.Interact;
    }

    private void OnDisable()
    {
        inputControls.Disable();
    }
}
