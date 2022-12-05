using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    public InputControls inputControls;
    [HideInInspector] public InputAction horizontal, vertical, interact, escape;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

        inputControls = new InputControls();
        inputControls.Enable();
        vertical = inputControls.Actions.Vertical;
        horizontal = inputControls.Actions.Horizontal;
        interact = inputControls.Actions.Interact;
        escape = inputControls.Actions.Escape;
    }

    private void OnDisable()
    {
        inputControls.Disable();
    }
}
