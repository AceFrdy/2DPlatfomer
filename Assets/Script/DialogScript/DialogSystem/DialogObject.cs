using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/DialogObject")]
public class DialogObject : ScriptableObject
{
    [System.Serializable]
    public struct DialogEntry
    {
        public string characterName;
        [TextArea] public string dialog;
    }

    [SerializeField] private DialogEntry[] dialogEntries;

    public DialogEntry[] DialogEntries => dialogEntries;
}
