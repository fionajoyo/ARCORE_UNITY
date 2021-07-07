using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //该脚本是用作测试使用
    //在arcore中，玩家移动手机会代替此脚本的功能
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Hor = Input.GetAxis("Horizontal");//当你按‘a’'d'键返回一个[-1，1]的值
        float Ver = Input.GetAxis("Vertical");//当你按‘w’'s'键返回一个[-1，1]的值
        transform.Translate(Vector3.forward * Ver * Time.deltaTime * 7);//前后移动
        transform.Rotate(Vector3.up * Hor * 20 * Time.deltaTime);//左右旋转
    }
}
