using UnityEngine;
using TMPro;
using System.Collections;

public class DialogUI : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogBox;
    
    [SerializeField]
    private TMP_Text characterLabel;
    
    [SerializeField]
    private TMP_Text textLabel;

    public bool IsOpen { get; private set;}
    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogBox();
    }

    public void ShowDialog(DialogObject dialogObject)
    {
        IsOpen = true;
        dialogBox.SetActive(true);
        StartCoroutine(StepThroughDialog(dialogObject));
    }

    private IEnumerator StepThroughDialog(DialogObject dialogObject)
    {
        if (dialogObject.DialogEntries == null || dialogObject.DialogEntries.Length == 0)
        {
            Debug.LogError("DialogObject has no dialog entries or is null.");
            yield break;
        }

        foreach (var entry in dialogObject.DialogEntries)
        {
            characterLabel.text = entry.characterName;
            yield return typewriterEffect.Run(entry.dialog, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        CloseDialogBox();
    }

    private void CloseDialogBox()
    {
        IsOpen = false;
        dialogBox.SetActive(false);
        textLabel.text = string.Empty;
        characterLabel.text = string.Empty;
    }
}
