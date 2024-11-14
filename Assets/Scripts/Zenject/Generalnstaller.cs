using UnityEngine;
using Zenject;

public class Generalnstaller : MonoInstaller
{
    [SerializeField] private DialogueInstaller dialogueInstaller;
 
    public override void InstallBindings()
    {
        BindDialogueInstaller();
    }

    private void BindDialogueInstaller()
    {
        Container.Bind<DialogueInstaller>().FromInstance(dialogueInstaller);
    }
}
