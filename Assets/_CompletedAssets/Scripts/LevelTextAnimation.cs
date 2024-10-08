using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextAnimation : MonoBehaviour
{
    public Text levelText; // �ؿ��ı�����  
    public float animationTime = 2f; // ����ʱ��  
    private Vector3 offScreenPosition; // ��Ļ��ĳ�ʼλ��  
    private Vector3 centerScreenPosition; // ��Ļ����λ��  

    void Start()
    {
        //offScreenPosition = (-1200, 45, 0);
        // ������Ļ��ĳ�ʼλ�ú���Ļ����λ��  
        offScreenPosition = new Vector3(Screen.width * 2, Screen.height / 2, 0); // ��Ļ�Ҳ���  
        centerScreenPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0); // ��Ļ����  
        levelText.transform.localPosition = offScreenPosition; // ���ı���ʼλ����������Ļ��  
    }

    public void ShowLevelText(string level)
    {
        // ������ʾ�Ĺؿ��ı�  
        levelText.text = "Level: " + level;
        // ��ʼ����Э��  
        StartCoroutine(MoveTextToCenterAndHide());
    }

    IEnumerator MoveTextToCenterAndHide()
    {
        // ���ı�����Ļ���ƶ�����Ļ����  
        float elapsedTime = 0;
        while (elapsedTime < animationTime)
        {
            levelText.transform.localPosition = Vector3.Lerp(offScreenPosition, centerScreenPosition, elapsedTime / animationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // �ڶ����������ı����û���Ļ�⣬�������ı�  
        levelText.transform.localPosition = offScreenPosition;
        levelText.gameObject.SetActive(false); // �����ı�����  
    }
}