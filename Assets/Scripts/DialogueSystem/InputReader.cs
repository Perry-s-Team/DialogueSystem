using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IDialogueActions
{
    Controls inputActons;
    Dialogues dialogues;

    private void OnEnable()
    {
        dialogues = FindObjectOfType<Dialogues>();

        if (inputActons != null)
            return;

        inputActons = new Controls();
        inputActons.Dialogue.SetCallbacks(this);
        inputActons.Dialogue.Enable();
    }

    private void OnDisable()
    {
        inputActons.Dialogue.Disable();
    }

    public void OnNextPhrase(InputAction.CallbackContext context)
    {
        if (context.started && dialogues.DialogPlay)
        {
            dialogues.ContinueStory(dialogues.choiceButtonPanel.activeInHierarchy);
        }
    }
}
