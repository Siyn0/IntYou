using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cube : MonoBehaviour
{

    /// <summary>
    /// 相邻方块
    /// </summary>
    public GameObject[] nearbyCube = { };

    private Vector3 offset;

    private void OnMouseDown()
    {
        // 让移动的y值在{-2，-1，0，1，2}范围内
        offset = gameObject.transform.position - new Vector3(0, (int)Math.Min(Math.Max(GetMouseWorldPos().y, -2), 2), 0);
    }

    private void OnMouseDrag()
    {
        // 让移动的y值在{-2，-1，0，1，2}范围内
        transform.position = new Vector3(0, (int)Math.Min(Math.Max(GetMouseWorldPos().y, -2), 2), 0) + offset;
    }

    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        foreach (GameObject cube in nearbyCube)
        {
            if (cube.transform.position.y == transform.position.y)
            {
                cube.GetComponent<Renderer>().material.color = Color.blue;
            }
            else
            {
                cube.GetComponent<Renderer>().material.color = Color.white;
            }
        }
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
