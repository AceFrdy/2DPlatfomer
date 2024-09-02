using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent NoCollidersRemain;
    public List<Collider2D> detectedColliders;
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);
    }

    // Update is called once per frame
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);

        if(detectedColliders.Count == 0)
        {
            NoCollidersRemain.Invoke();
        }
    }
}
