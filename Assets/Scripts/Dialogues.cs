using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Dialogues : MonoBehaviour
{
    private Story currentStory;
    private TextAsset inkJson;

    private GameObject dialoguePanel;
    private TextMeshProUGUI dialogueText;
    private TextMeshProUGUI characterNameText;
    [HideInInspector] public GameObject choiceButtonPanel;

    private GameObject choiceButton;
    private List<TextMeshProUGUI> choicesText = new();

    public bool DialogPlay { get; private set; }

    [Inject]
    public void Construct(DialogueInstaller dialogueInstaller)
    {
        inkJson = dialogueInstaller._inkJson;
        dialoguePanel = dialogueInstaller._dialoguePanel;
        dialogueText = dialogueInstaller._dialogueText;
        characterNameText = dialogueInstaller._characterNameText;
        choiceButtonPanel = dialogueInstaller._choiceButtonPanel;
        choiceButton = dialogueInstaller._choiceButton;
    }
    private void Awake()
    {
        DialogPlay = true;
        dialoguePanel.SetActive(true);
        currentStory = new Story(inkJson.text);
    }

    private void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        DialogPlay = true;
        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    public void ContinueStory(bool choiceBefore = false)
    {
        if (currentStory.canContinue) 
        {
            ShowDialogue();
            ShowChoiceButton();
        }
        else if (!choiceBefore)
        {
            ExitDialogue();
        }
    }

    private void ShowDialogue()
    {
        dialogueText.text = currentStory.Continue();
        characterNameText.text = (string)currentStory.variablesState["characterName"];
    }

    private void ShowChoiceButton()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        choiceButtonPanel.SetActive(currentChoices.Count != 0);
        if (currentChoices.Count <= 0) return;
        choiceButtonPanel.transform.Cast<Transform>().ToList().ForEach(child => Destroy(child.gameObject));
        for (int i = 0; i < currentChoices.Count; i++)
        {
            GameObject choice = Instantiate(choiceButton);
            choice.GetComponent<ButtonAction>().index = i;
            choice.transform.SetParent(choiceButtonPanel.transform);

            TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = currentChoices[i].text;
            choicesText.Add(choiceText);
        }
    }

    public void ChoseButtonAction(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory(true);
    }

    private void ExitDialogue()
    {
        DialogPlay = false;
        dialoguePanel.SetActive(false);
    }
}
