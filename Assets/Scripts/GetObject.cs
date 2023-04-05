using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetObject : MonoBehaviour {

    public Camera camera_object; //カメラを取得
    private RaycastHit hit; //レイキャストが当たったものを取得する入れ物
    private GameObject clickobject;
    ModelData _modeldata;
    public GameObject viewPanel;
    public TextMeshProUGUI nametext;
    public TextMeshProUGUI credittext;
    private string url;
    
    void Start() {
        viewPanel.SetActive(false);
    }

    void Update () {
        if (Input.GetMouseButtonDown(0)) //マウスがクリックされたら
        {
            Ray ray = camera_object.ScreenPointToRay(Input.mousePosition); //マウスのポジションを取得してRayに代入

                if(Physics.Raycast(ray,out hit))  //マウスのポジションからRayを投げて何かに当たったらhitに入れる
            {
                
                clickobject = hit.collider.gameObject;
                if (clickobject.tag == "model")
                {
                viewPanel.SetActive(true);
                _modeldata = clickobject.GetComponent<ModelData>();
                nametext.text = _modeldata.modelname;
                credittext.text = _modeldata.modelcredit;
                url = _modeldata.modelurl;
                }
               
            }
        }
        if(Input.GetKeyDown("joystick button 0")) {
            if (ObjectController.obj.tag == "model")
                {
                viewPanel.SetActive(true);
                _modeldata = ObjectController.obj.GetComponent<ModelData>();
                nametext.text = _modeldata.modelname;
                credittext.text = _modeldata.modelcredit;
                url = _modeldata.modelurl;
                }
        }
        if(Input.GetKeyDown("joystick button 1")) {
            if (viewPanel.activeSelf)
                {
                    urlbuttonclick();
                }
        }
        if(Input.GetKeyDown("joystick button 2")) {
            if (viewPanel.activeSelf)
                {
                    closepanel();
                }
        }
    }

    public void urlbuttonclick()
    {
        Application.OpenURL(url);
    }

    public void closepanel()
    {
        viewPanel.SetActive(false);
    }
}