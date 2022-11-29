using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpareParts : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Stats stats = col.gameObject.GetComponent<Stats>();
            stats.SpareParts += 1;
            Destroy(gameObject);
        }
    }
}
