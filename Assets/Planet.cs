using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    void Start()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.startPlaying.AddListener(RemovePlanet);
    }
    private void RemovePlanet()
    {
        rb.velocity = Vector2.down * speed;
        Destroy(gameObject,1f);
    }
    

}
