using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class moveControl : MonoBehaviour
{
    //**
    //该脚本实现利用ray类对物体实施操控
    //**
    public static moveControl instance;//单例模式
    public GameObject set;//设置界面
    public GameObject musicOpen;//音乐开键
    public GameObject musicOff;//音乐关键
    public GameObject obj;//高亮物体
    
    public Material highLight;//高亮
    public GameObject cancelUI = null;//取消按钮

    //以下是私有变量，策划无需关心
    private GameObject mainCamera = null;//主摄像机
    private GameObject item = null;//绑定的组件  
    private static bool screenTouch = true;//允许点击屏幕有效
    private static bool rayOne = false;//控制仅生成一条射线
    private GameObject highlightObj;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))//调试端使用,读取鼠标左键状态
        //if(Input.touches[0].phase ==TouchPhase.Began)//手机端使用,读取当前点击状态
        {
            RayOut();//每帧检测
        }
        
    }

    //射线发射
    private void RayOut()
    {
        if (screenTouch)//如果屏幕点击有效
        {
            if (!rayOne)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//定义射线-电脑调试
                //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);//定义射线-手机端
                Debug.DrawLine(ray.origin, ray.direction, Color.red,10);//划线调试
                if (Physics.Raycast(ray, out RaycastHit hitInfo))//接受射线命中的物体
                {
                    item = hitInfo.transform.gameObject;//绑定射线命中的物体

                    //---------------------------高亮实现
                    //Material[] materials = item.GetComponent<MeshRenderer>().materials;
                    //item.GetComponent<Renderer>().materials = new Material[2] { materials[0], highLight };
                    //GameObject highlightObj = (GameObject)Instantiate(obj, item.gameObject.transform.position, item.gameObject.transform.rotation);

                    //----------------------------

                    screenTouch = false;//屏幕点击无效
                    cancelUI.SetActive(true);//取消按钮可见
                    rayOne = true;//命中物体才不让继续生成射线
                    Debug.Log("命中物体");
                }
                Debug.Log("已经点击屏幕");
            }
        }
        else 
        {
            try
            {
                item.transform.parent = mainCamera.transform;//物体设为主摄子物体
            }
            catch { }
        }
    }

    //取消按钮
    public void BottomControl()
    {
        Debug.Log("点击取消按钮");
  
        screenTouch = true;//点击屏幕有效
        cancelUI.SetActive(false);//取消按钮消失
        item = null;//绑定物体空
        mainCamera.transform.DetachChildren();//解除父子
        rayOne = false;//允许生成射线
        try
        {
                  //---------------------------高亮消失
        //Material[] materials = item.GetComponent<MeshRenderer>().materials;//获取自身shader
        //materials[1] = materials[0];//0是默认shader,1是高亮shader,将高亮shader的值赋值为默认（即取消）
        //item.GetComponent<Renderer>().materials = materials;//更新shader
        //----------------------------
            //Destroy(highlightObj);
        }
        catch
        {

        }
    }
    //绑定物体

    //以下全为按钮点击事件
    public void Setopen()//控制台界面
    {
        //BGM控制
        //
        set.gameObject.SetActive(true);//设置界面可见
    }

    public void Setoff()//控制台界面
    {
        //BGM控制
        //
        set.gameObject.SetActive(false);//设置界面不可见
    }

    public void musicopen()//BGM开
    {
        //AudioSource AS = gameObject.GetComponent<AudioSource>();
        //AS.Play();
        musicOpen.gameObject.SetActive(true);
        musicOff.gameObject.SetActive(false);
    }

    public void musicoff()//BGM关
    {
        //AudioSource AS = gameObject.GetComponent<AudioSource>();
        //AS.Stop();
        musicOpen.gameObject.SetActive(false);
        musicOff.gameObject.SetActive(true);
    }
    public void BackToMain()
    {
        //回到主界面
        SceneManager.LoadScene("Scene0");
    }
}
