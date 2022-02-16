using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKilledByEnemy : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject == null) return;

        if (collision.gameObject.tag == "Enemy")
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    }
}
