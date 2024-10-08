using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject EM;
    //
    public GameObject buttonManager;
    //�Ի��ı���csv��ʽ
    public TextAsset dialogDataFile;

    //���ͼ��
    public Image Imageleft;

    //�Ҳ�ͼ��
    public Image Imageright;

    //�����ı�
    public Text nameText;

    //�Ի������ı�
    public Text dialogText;

    //����ͼƬ
    public List<Sprite> Image = new List<Sprite>();

    //��ɫ���ƶ�ӦͼƬ
    Dictionary<string, Sprite> ImageDic = new Dictionary<string, Sprite>();

    //���浱ǰ�Ի�������ֵ
    public int dialogIndex;

    //�Ի��ı������зָ�
    public string[] dialogRows;

    //���ƾ����ƽ���ť
    public Button next;

    //����ѡ��İ�ť
    public GameObject select;

    //ѡ�ť���ڵ�,�����Զ�����
    public Transform buttonGroup;

    //���ò���ֵ���Ի��Ƿ����
    //public bool IsEND = false;

    public Button Skip;

    // Start is called before the first frame update
    private void Awake()
    {
        ImageDic["����"] = Image[0];
        ImageDic["���� "] = Image[1];
        ImageDic["����  "] = Image[2];
        ImageDic["����   "] = Image[3];
        ImageDic["sensei"] = Image[4];
        ImageDic["����˿"] = Image[5];
        ImageDic["С��"] = Image[6];
        ImageDic["������"] = Image[7];
        ImageDic["������"] = Image[8];
    }
    
    void Start()
    {
        
        Skip.onClick.AddListener(SKIP);
        //UpdateText("sensei", "�ҵ������");
        //UpdateImage("����",false);
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
        if(_position=="��"|| _position == "��")
        {
            Imageleft.sprite = ImageDic[_name];
        }
        //else if(_position=="��")
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
        //Debug.Log("��ȡ�ɹ�");
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
            //�󶨰�ť�¼�
            button.GetComponentInChildren<Text>().text = cells[4];
            //Ϊ��������¼�����
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
