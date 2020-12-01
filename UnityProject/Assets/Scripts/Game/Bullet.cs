using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 target;
    public float speed;
    public float damage;
    public float lifeTime;
    public LayerMask enemyLayer;

    float StartTime;
    private void Start()
    {
        StartTime = Time.time;
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
    }

    void FixedUpdate()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward * speed * Time.deltaTime, out hit, enemyLayer))
        {
            bool hitChar = hit.transform.gameObject.TryGetComponent<Character1>(out Character1 character);
            if (hitChar) End(character);
            Destroy(this.gameObject);
        }

        if (Time.time - StartTime > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool hitChar = collision.transform.gameObject.TryGetComponent<Character1>(out Character1 character);
        if (hitChar) End(character);
    }
    private void OnTriggerEnter(Collider other)
    {
        bool hitChar = other.transform.gameObject.TryGetComponent<Character1>(out Character1 character);
        if (hitChar) End(character);
    }

    void End(Character1 character)
    {
        character.Health -= damage;
        Destroy(this.gameObject);
    }
}
