using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TypewriterEffect : MonoBehaviour, ITimeControl
{
    [SerializeField]
    private float typewriterSpeed = 50f;

    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>() {'.', '!', '?'}, 0.6f),
        new Punctuation(new HashSet<char>() {',', ';', ':'}, 0.3f),
    };

    private string fullText;
    private TMP_Text textLabel;
    private Coroutine typingCoroutine;

    public Coroutine Run(string textToType, TMP_Text label)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        SetText(textToType, label);
        typingCoroutine = StartCoroutine(TypeText(textToType, label));
        return typingCoroutine;
    }

    public void SetText(string textToType, TMP_Text label)
    {
        fullText = textToType;
        textLabel = label;
        if (textLabel != null)
        {
            textLabel.text = string.Empty;
        }
    }

    public void SetTime(double time)
    {
        if (textLabel == null || string.IsNullOrEmpty(fullText)) return;

        int charIndex = Mathf.FloorToInt((float)time * typewriterSpeed);
        charIndex = Mathf.Clamp(charIndex, 0, fullText.Length);

        textLabel.text = fullText.Substring(0, charIndex);
    }

    public void OnControlTimeStart()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        if (textLabel != null)
        {
            textLabel.text = string.Empty;
        }
    }

    public void OnControlTimeStop()
    {
        if (textLabel != null)
        {
            textLabel.text = fullText;
        }
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;
        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;
            t += Time.deltaTime * typewriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            for (int i = lastCharIndex; i < charIndex; i++)
            {
                textLabel.text = textToType.Substring(0, i + 1);

                bool isLast = i == textToType.Length - 1;
                if (IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }
            yield return null;
        }
        textLabel.text = textToType;
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach (Punctuation punctuationCategory in punctuations)
        {
            if (punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
