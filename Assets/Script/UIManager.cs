using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;
    private SoundManager soundManager;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
        soundManager = FindObjectOfType<SoundManager>(); // Mengambil referensi ke SoundManager
    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damageRecieved)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = damageRecieved.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    }

    public void PlayClickSound()
    {
        if (soundManager != null)
        {
            soundManager.PlayClickSound();
        }
    }

    public void PlayHoverSound()
    {
        if (soundManager != null)
        {
            soundManager.PlayHoverSound();
        }
    }

    public void PlayPickupSound()
    {
        if (soundManager != null)
        {
            soundManager.PlayPickupSound();
        }
    }

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + " ; " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif

#if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
            Application.Quit();
#elif (UNITY_WEBGL)
            SceneManager.LoadScene("QuitScane");
#endif
        }
    }
}
