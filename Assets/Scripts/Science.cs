using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Science : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Stats stats = col.gameObject.GetComponent<Stats>();
            stats.Points += 100;
            Destroy(gameObject);
        }
    }
}
