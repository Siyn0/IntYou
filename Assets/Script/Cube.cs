using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    private Vector3 offset;

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - new Vector3(0, GetMouseWorldPos().y, 0);
    }

    private void OnMouseDrag()
    {
        transform.position = new Vector3(0, GetMouseWorldPos().y, 0) + offset;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
