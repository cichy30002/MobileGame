using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dna : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Stats stats = col.gameObject.GetComponent<Stats>();
            stats.Points += 100;
            Destroy(gameObject);
        }
    }
}
