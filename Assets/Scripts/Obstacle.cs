using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 30f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject boomParticle;
    [SerializeField] private GameObject miniBoomParticle;

    private Vector2 _dir;

    private void Start()
    {
        _dir = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
        _dir.Normalize();
        rb.angularVelocity = Random.Range(-6f, 6f);
        
        InvokeRepeating(nameof(ChangeDirection), 1f,1f);
    }

    private void Update()
    {
        rb.velocity = _dir * (speed *Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Instantiate(miniBoomParticle, (col.transform.position + transform.position) / 2, Quaternion.identity);
            Stats stats = col.gameObject.GetComponent<Stats>();
            stats.Hp -= damage;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Barrel"))
        {
            Instantiate(boomParticle, (col.transform.position + transform.position) / 2, Quaternion.identity);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        if (col.gameObject.CompareTag("SpareParts") || col.gameObject.CompareTag("DNA"))
        {
            Instantiate(miniBoomParticle, (col.transform.position + transform.position) / 2, Quaternion.identity);
            Destroy(col.gameObject);
        }
    }

    private void ChangeDirection()
    {
        Vector2 currVelocity = Vector2.zero;
        const float smoothTime = 0.3f;
        Vector2.SmoothDamp(_dir, RandomRotateUnitVector2(_dir), ref currVelocity, smoothTime);
    }
    private static Vector2 RandomRotateUnitVector2(Vector2 vec)
    {
        double theta = Math.Asin(vec.y);
        theta += Random.Range(-0.5f, 0.5f);
        return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)).normalized;
    }
}
