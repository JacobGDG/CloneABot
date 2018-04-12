using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {
    Vector3 initialPosition;
    Vector3 targetPosition;

    [HideInInspector]
    public bool activate = false;

    public float maxX;
    public float maxY;
    public float activateSpeed;
    public float deactivateSpeed;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(maxX, maxY, 0);
    }

    void Update()
    {
        if (activate && transform.position != targetPosition)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, activateSpeed * Time.deltaTime);
        else if(!activate && transform.position != initialPosition)
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, deactivateSpeed * Time.deltaTime);
    }
}
