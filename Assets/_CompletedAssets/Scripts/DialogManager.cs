using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject EM;
    //
    public GameObject buttonManager;
    //对话文本，csv格式
    public TextAsset dialogDataFile;

    //左侧图像
    public Image Imageleft;

    //右侧图像
    public Image Imageright;

    //名字文本
    public Text nameText;

    //对话内容文本
    public Text dialogText;

    //人物图片
    public List<Sprite> Image = new List<Sprite>();

    //角色名称对应图片
    Dictionary<string, Sprite> ImageDic = new Dictionary<string, Sprite>();

    //保存当前对话的索引值
    public int dialogIndex;

    //对话文本，按行分隔
    public string[] dialogRows;

    //控制剧情推进按钮
    public Button next;

    //控制选项的按钮
    public GameObject select;

    //选项按钮父节点,用于自动排列
    public Transform buttonGroup;

    //设置布尔值检测对话是否结束
    //public bool IsEND = false;

    public Button Skip;

    // Start is called before the first frame update
    private void Awake()
    {
        ImageDic["优香"] = Image[0];
        ImageDic["优香 "] = Image[1];
        ImageDic["优香  "] = Image[2];
        ImageDic["优香   "] = Image[3];
        ImageDic["sensei"] = Image[4];
        ImageDic["爱丽丝"] = Image[5];
        ImageDic["小桃"] = Image[6];
        ImageDic["白洲梓"] = Image[7];
        ImageDic["阿罗那"] = Image[8];
    }
    
    void Start()
    {
        
        Skip.onClick.AddListener(SKIP);
        //UpdateText("sensei", "我叼你嘛的");
        //UpdateImage("优香",false);
        readtext(dialogDataFile);
        ShowDialogRow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SKIP()
    {
        EnemyManager enemyManager = EM.GetComponent<EnemyManager>();
        enemyManager.IsCreat = false;
        enemyManager.Isstart = true;
        enemyManager.level = 1;
        buttonManager.gameObject.SetActive(false);
    }

    public void UpdateText(string _name,string _text)
    {
        nameText.text = _name;
        dialogText.text = _text;
    }

    public void UpdateImage(string _name,string _position)
    {
        if(_position=="右"|| _position == "左")
        {
            Imageleft.sprite = ImageDic[_name];
        }
        //else if(_position=="左")
        //{
        //    Imageright.sprite = ImageDic[_name];
        //}
    }

    public void readtext(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
        //foreach (var row in rows)
        //{
        //    string[] cell = row.Split(',');
        //}
        //Debug.Log("读取成功");
    }

    public void ShowDialogRow()
    {
        for(int i=0;i<dialogRows.Length;i++)
        {
            string[] cells = dialogRows[i].Split(',');
            if (cells[0]=="#" && int.Parse(cells[1]) == dialogIndex)
            {
                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);

                dialogIndex = int.Parse(cells[5]);

                next.gameObject.SetActive(true);
                break;
            }
            else if (cells[0]=="&" && int.Parse(cells[1]) == dialogIndex)
            {
                next.gameObject.SetActive(false);
                GenerateOption(i);
            }
            else if(cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                //IsEND = true;
                SKIP();
            }

        }

    }

    public void OnClickNext()
    {
        ShowDialogRow();
    }

    public void GenerateOption(int _index)
    {
        string[] cells = dialogRows[_index].Split(',');
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(select, buttonGroup);
            //绑定按钮事件
            button.GetComponentInChildren<Text>().text = cells[4];
            //为按键添加事件函数
            button.GetComponent<Button>().onClick.AddListener(delegate { OnOptionClick(int.Parse(cells[5])); });

            GenerateOption(_index + 1);
        }
    }

    public void OnOptionClick(int _id)
    {
        dialogIndex = _id;
        ShowDialogRow();
        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        } 

    }

}
