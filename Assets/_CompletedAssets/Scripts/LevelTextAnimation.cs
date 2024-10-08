using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextAnimation : MonoBehaviour
{
    public Text levelText; // 关卡文本对象  
    public float animationTime = 2f; // 动画时间  
    private Vector3 offScreenPosition; // 屏幕外的初始位置  
    private Vector3 centerScreenPosition; // 屏幕中心位置  

    void Start()
    {
        //offScreenPosition = (-1200, 45, 0);
        // 设置屏幕外的初始位置和屏幕中心位置  
        offScreenPosition = new Vector3(Screen.width * 2, Screen.height / 2, 0); // 屏幕右侧外  
        centerScreenPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0); // 屏幕中心  
        levelText.transform.localPosition = offScreenPosition; // 将文本初始位置设置在屏幕外  
    }

    public void ShowLevelText(string level)
    {
        // 更新显示的关卡文本  
        levelText.text = "Level: " + level;
        // 开始动画协程  
        StartCoroutine(MoveTextToCenterAndHide());
    }

    IEnumerator MoveTextToCenterAndHide()
    {
        // 将文本从屏幕外移动到屏幕中心  
        float elapsedTime = 0;
        while (elapsedTime < animationTime)
        {
            levelText.transform.localPosition = Vector3.Lerp(offScreenPosition, centerScreenPosition, elapsedTime / animationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 在动画结束后将文本设置回屏幕外，并隐藏文本  
        levelText.transform.localPosition = offScreenPosition;
        levelText.gameObject.SetActive(false); // 隐藏文本对象  
    }
}