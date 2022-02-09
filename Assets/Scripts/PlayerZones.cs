using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZones : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        }

        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "Finish")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("PlayerFinished", true);
        }
    }
}
