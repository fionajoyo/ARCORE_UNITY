using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colliderPoint : MonoBehaviour
{
    public GameObject item;//目标组件
    

    //以下是私有变量，策划无需关心
    private float rotate_speed=0.1f;//碰撞后旋转时的角速度,设为0.1挺合适
    private float move_speed=0.041667f;//碰撞后移动的速度,设为1/24挺合适
    private int NoItem;//第几号硬件
    private bool iscollide = false;//归位动画播放允许
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (iscollide)//丝滑代码
        {
            MoveSilky();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == item)
        {
            iscollide = true;//允许丝滑movesilky函数调用
            //归位
            collider.gameObject.layer = 2;//忽视射线
            moveControl.instance.BottomControl();//调用取消按钮点击函数
            Debug.Log("归位" + item.name);
            //播放成功音乐
            //SupremeControl.instance.Music(true);//调用成功音乐播放
            //SupremeControl.instance.Finish(item);//传入成功组件
            
            //这里写归位的代码(旧版，已淘汰)
            //item.transform.position = gameObject.transform.position;//原三维向量赋值给位置
            //item.transform.rotation = gameObject.transform.rotation;//角度
            //
            //这里是未来 动画归位 的地方
            //
        }
        else
        {
            //播放失败音乐
            //SupremeControl.instance.Music(false);//调用失败音乐播放
        }
    }

    private void MoveSilky()
    {
        item.transform.rotation = Quaternion.Slerp(item.transform.rotation, transform.rotation, rotate_speed); //碰撞后旋转
        if (Mathf.Abs(item.transform.position.x - gameObject.transform.position.x) < 0.001 && Mathf.Abs(item.transform.position.y - gameObject.transform.position.y) < 0.001 && Mathf.Abs(item.transform.position.z - gameObject.transform.position.z) < 0.001)//足够近的时候就直接吸住，不然它老是跳动
        { item.transform.position = gameObject.transform.position; }
        else//距离不够近就移动过去
        { item.transform.position += (gameObject.transform.position - item.transform.position) * move_speed; }//碰撞后移动
    }
}
