using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 1f;
    public Rigidbody rigidbody;
    public float damage = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveBulletFoward();
    }

    private void moveBulletFoward() 
    {
        rigidbody.position += Vector3.forward * Time.deltaTime * Speed;
     }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy") 
        {
            collision.gameObject.GetComponent<Enemies>().takeDamage(damage);
            // play bullet collide animation.
            //destroy this bullet
            Destroy(this.gameObject);
        }
    }
}
