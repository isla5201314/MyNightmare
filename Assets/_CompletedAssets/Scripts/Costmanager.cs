using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 确保你已经导入了UI命名空间来使用Slider  

public class Costmanager : MonoBehaviour
{
    public float CostIncreaseSpeed = 0.3f;
    public float CurrentlyCost = 0f;
    public float CostMinNum = 0f;
    public float CostMaxNum = 10f;
    public Slider Cost; // 确保你在Unity编辑器中已经将这个Slider拖放到这个字段上  
    public Text CostNum;

    // Start is called before the first frame update  
    void Start()
    {
        // 可以在这里初始化CurrentlyCost的值，如果需要的话  
        CurrentlyCost = Cost.value; // 假设你想要从Slider的初始值开始  
        CostNum.text = "Cost:" + CurrentlyCost.ToString("F0");
    }

    // Update is called once per frame  
    void Update()
    {

        // 假设每一帧CurrentlyCost都会增加，增加的速度是CostIncreaseSpeed  
        CurrentlyCost += CostIncreaseSpeed * Time.deltaTime; // 使用Time.deltaTime来确保增加的速度与帧率无关  

        // 使用Mathf.Clamp来确保CurrentlyCost的值在[CostMinNum, CostMaxNum]之间  
        CurrentlyCost = Mathf.Clamp(CurrentlyCost, CostMinNum, CostMaxNum);

        // 更新Slider的值以反映CurrentlyCost的当前值  
        Cost.value = (CurrentlyCost - CostMinNum) / (CostMaxNum - CostMinNum);
        CostNum.text = "Cost:" + CurrentlyCost.ToString("F0");
    }
}