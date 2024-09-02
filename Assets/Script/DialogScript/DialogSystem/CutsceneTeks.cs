using UnityEngine;
using TMPro;
using System.Collections;

public class CutsceneTeks : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textLabel;

    [SerializeField]
    private float timeBetweenDialogs = 2f; // Waktu jeda antar dialog

    [SerializeField]
    private DialogObject dialogObject; // Menambahkan DialogObject sebagai komponen

    public bool IsOpen { get; private set; }
    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogBox();

        // Memulai dialog otomatis saat cutscene dimulai jika diperlukan
        if (dialogObject != null)
        {
            ShowDialog(0); // Memulai dari dialog pertama
        }
    }

    public void ShowDialog(int startIndex)
    {
        IsOpen = true;
        Debug.Log("ShowDialog started.");
        StartCoroutine(StepThroughDialog(startIndex));
    }

    private IEnumerator StepThroughDialog(int startIndex)
    {
        if (dialogObject.DialogEntries == null || dialogObject.DialogEntries.Length == 0)
        {
            Debug.LogError("DialogObject has no dialog entries or is null.");
            yield break;
        }

        for (int i = startIndex; i < dialogObject.DialogEntries.Length; i++)
        {
            var entry = dialogObject.DialogEntries[i];
            Debug.Log($"Dialog: {entry.dialog}");

            // Menjalankan efek typewriter
            yield return typewriterEffect.Run(entry.dialog, textLabel);

            yield return new WaitForSeconds(timeBetweenDialogs); // Waktu jeda antar dialog
        }

        CloseDialogBox();
    }

    private void CloseDialogBox()
    {
        IsOpen = false;
        textLabel.text = string.Empty;
    }

    public void TriggerDialog(int dialogIndex)
    {
        if (dialogObject != null && dialogIndex < dialogObject.DialogEntries.Length)
        {
            ShowDialog(dialogIndex); // Memulai dialog dari indeks tertentu
        }
        else
        {
            Debug.LogWarning("Invalid dialog index or DialogObject is null.");
        }
    }
}
