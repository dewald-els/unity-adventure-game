using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesStickToCamera : MonoBehaviour
{
    [SerializeField] private float _smoothFactor = 5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, cameraPosition, _smoothFactor);
        transform.position = smoothPosition;
    }
}
