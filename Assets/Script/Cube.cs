using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Cube : MonoBehaviour
{

    /// <summary>
    /// 相邻方块
    /// </summary>
    public GameObject[] nearbyCube = { };

    /// <summary>
    /// 拖动了方块的事件
    /// </summary>
    /// <param name="currentCube">被拖动的方块</param>
    public delegate void dragEventHandler(Cube currentCube);

    public static event dragEventHandler onDragEvent;

    private Vector3 offset;

    /// <summary>
    /// 已经变蓝的方块（防止重复）
    /// </summary>
    private List<GameObject> hasBlue = new List<GameObject>();

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
        // 发送事件，所有方块更新颜色
        // Debug.Log("OnMouseUp");
        onDragEvent?.Invoke(this);
    }

    private void dragEvent(Cube currentCube)
    {

        foreach (GameObject cube in nearbyCube)
        {
            cube.GetComponent<Renderer>().material.color = Color.white;
        }

        StartCoroutine(updateDelayed(currentCube));
    }

    /// <summary>
    /// 延迟后更新颜色，防止和白色冲突
    /// </summary>
    /// <param name="currentCube">当前更新的方块</param>
    /// <returns></returns>
    IEnumerator updateDelayed(Cube currentCube)
    {
        // Debug.Log("updateDelayed");
        yield return new WaitForSeconds(0.2f); // 延迟0.2秒

        if (currentCube == this)
        {
            blueCube(currentCube);
        }
    }

    private void blueCube(Cube currentCube)
    {
        foreach (GameObject cube in currentCube.nearbyCube)
        {
            // Debug.Log("[zzzz]判断方块" + cube.name + gameObject.name + "  y:" + cube.transform.position.y + "  " + transform.position.y);

            if (cube.transform.position.y == transform.position.y)
            {
                cube.GetComponent<Renderer>().material.color = Color.blue;
                if (!hasBlue.Contains(cube))
                {
                    hasBlue.Add(cube);
                    // cube.transform.SetParent(currentCube.transform);
                    blueCube(cube.GetComponent<Cube>());
                }
            }
            else
            {
                cube.GetComponent<Renderer>().material.color = Color.white;
            }
            // Debug.Log("[zzzz]判断完了");
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
        Cube.onDragEvent += dragEvent;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
