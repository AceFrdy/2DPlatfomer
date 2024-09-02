using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform posA, posB;
    public float Speed;
    public float waitTime = 1.0f;  // Waktu jeda dalam detik
    private Vector2 targetPos;
    private bool isWaiting = false;

    void Start()
    {
        targetPos = posB.position;
    }

    void FixedUpdate()
    {
        if (!isWaiting)
        {
            if (Vector2.Distance(transform.position, posA.position) < .1f)
            {
                targetPos = posB.position;
                StartCoroutine(WaitBeforeMoving());
            }
            else if (Vector2.Distance(transform.position, posB.position) < .1f)
            {
                targetPos = posA.position;
                StartCoroutine(WaitBeforeMoving());
            }

            transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator WaitBeforeMoving()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(DetachPlayer(collision.transform));
        }
    }

    private IEnumerator DetachPlayer(Transform playerTransform)
    {
        yield return null; // Menunggu satu frame
        playerTransform.SetParent(null);
    }
}
