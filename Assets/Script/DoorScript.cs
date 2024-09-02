using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public GameObject player;
    public Sprite OpenDoorImage;
    public Sprite CloseDoorImage;
    public float TimeBeforeNextScene;
    public bool PlayerIsAtTheDoor;
    private string sceneToLoad;
    public Animator transition;
    public float transitionTime = 1f;

    // Referensi ke LevelLoader
    public LevelLoader levelLoader;

    void Start()
    {
        sceneToLoad = SceneManager.GetActiveScene().name;
        // MusicManager.Instance.PlayMusic("DalamRumah");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerIsAtTheDoor == true)
        {
            StartCoroutine(_OpenDoor());
        }
    }

    public IEnumerator _OpenDoor()
    {
        transform.GetComponent<SpriteRenderer>().sprite = OpenDoorImage;
        SoundManager.Instance.PlaySound2D("Door");
        yield return new WaitForSeconds(TimeBeforeNextScene);

        player.SetActive(false);
        yield return new WaitForSeconds(TimeBeforeNextScene);

        transform.GetComponent<SpriteRenderer>().sprite = CloseDoorImage;
        SoundManager.Instance.PlaySound2D("Door");
        yield return new WaitForSeconds(TimeBeforeNextScene);

        if (levelLoader != null)
        {
            if (sceneToLoad == "Level1")
            {
                // MusicManager.Instance.PlayMusic("DalamRumah");
                levelLoader.LoadLevelByName("DalamRumah");
            }
            else if (sceneToLoad == "DalamRumah")
            {
                // MusicManager.Instance.PlayMusic("Stage1");
                levelLoader.LoadLevelByName("Level1");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerIsAtTheDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerIsAtTheDoor = false;
        }
    }
}
