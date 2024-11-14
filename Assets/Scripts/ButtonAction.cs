using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    private Button button;
    private Dialogues dialogues;
    private UnityAction clickAction;
    public int index;

    private void Start()
    {
        button = GetComponent<Button>();
        dialogues = FindAnyObjectByType<Dialogues>();
        clickAction = new UnityAction(() => dialogues.ChoseButtonAction(index));
        button.onClick.AddListener(clickAction);
    }

    private void ShowLog()
    {
        Debug.Log("ClickLKM");
    }
}
