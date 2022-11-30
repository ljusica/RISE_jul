using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
            Destroy(this);
        }

        inputControls = new InputControls();
        inputControls.Enable();
        vertical = inputControls.Actions.Vertical;
        horizontal = inputControls.Actions.Horizontal;
        interact = inputControls.Actions.Interact;
        inputControls.Actions.Restart.performed += RestartLevel;
    }

    private void OnDisable()
    {
        inputControls.Disable();
    }

    void RestartLevel(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
