using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DB_manager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] PL_prefab;
    public Transform parentino;
    public InputField[] Inp_field;
    public InputField search;
    public Dropdown Dp;
    Vector3 temp;
    public Text info;


    public string Temp_UserName = "";
    public string recent_data = "";
    public Image img;
    //intial setup childs
    void Start()
    {
        temp = parentino.position;
        Temp_UserName = PlayerPrefs.GetString("SetName_string");
        Get_seT_childs();
    }
    /// Making player list & sliting their value from playerpref
   
   
    void Get_seT_childs()
    {
        for (int y = 0; y < PlayerPrefs.GetInt("Object_2_spon"); y++)
        {

            GameObject g = Instantiate(PL_prefab[0]) as GameObject;
            g.transform.parent = parentino;
        }



        fixRAWDATA_set();









    }
    void fixRAWDATA_set()
    {
        string Myrawdata = PlayerPrefs.GetString("Rawdata");

        string[] temp = Myrawdata.Split(char.Parse("|"));
        for (int y = 0; y < temp.Length; y++)
        {
            
            string[] temp_last = temp[y].Split(char.Parse(","));
           
           
                for (int i = 0; i < 3; i++)
                {
                    parentino.GetChild(y).GetChild(i).GetComponent<Text>().text = temp_last[i];
                parentino.GetChild(y).name = parentino.GetChild(y).GetChild(0).GetComponent<Text>().text;
                }
              
        }
        
        
    }
  
    /// </Adding child>
    public void AddList()
    {
      

       
        if (Inp_field[0].text.Length >= 4 && Inp_field[0].text.Length <=6)
        {
            if (Inp_field[1].text != "")
            {
                if (int.Parse(Inp_field[1].text) < 15 || int.Parse(Inp_field[1].text) > 55)
                {
                    info.text = "Invalid Age";
                    img.color = Color.yellow;
                    info.color = Color.yellow;

                }
                else
                {
                    GameObject g = Instantiate(PL_prefab[0]) as GameObject;
                    g.transform.parent = parentino;
                    g.gameObject.name = Inp_field[0].text;
                    fix(g);
                }
            }
            else
            {
                info.text = "Invalid Age";
                img.color = Color.yellow;
                info.color = Color.yellow;

            }
        }
        else
        {
            info.text = "namelength should be 4 to 8";
            img.color = Color.magenta;
            info.color = Color.magenta;

        }
    }
    // same name fix
    void fix(GameObject Go)
    {
        if(Temp_UserName.Contains(Go.name))
        {
            Destroy(Go);
            info.text = "Same Player";
            info.color = Color.red;
            Invoke("Reset_text", 3f);
            img.color = Color.red;

        }
        else
        {
            info.text = "Player Added";
            info.color = Color.green;
            Invoke("Reset_text", 3f);

            Temp_UserName = Temp_UserName +","+ Go.name;
            //for checking same player
            PlayerPrefs.SetString("SetName_string", Temp_UserName);
            SetRowData(Go);
            Getchild_No();
            img.color = Color.green;
        }
    }
    private void Reset_text()
    {
        info.text = ".....";
        info.color = Color.white;
        img.color = Color.white;

    }
    void Getchild_No()
    {
        PlayerPrefs.SetInt("Object_2_spon", parentino.transform.childCount);
    }
    private void FixedUpdate()
    {
        //Dynamic search
        DynaTry();


    }
    public void Reset_pos()
    {
        parentino.position = temp;
    }
    void DynaTry()
    {
        if (search.text != "")
        {
            foreach (Transform T in parentino)
            {

                if (T.gameObject.name.Contains(search.text))
                {
                   

                    T.gameObject.active = true;
                }
                else
                {
                    T.gameObject.active = false;

                }
            }
        }
        else
        {
            foreach (Transform T in parentino)
            {
                T.gameObject.active = true;

            }
        }
    }
    void SetRowData(GameObject jo)
    {
        //foreach(Transform T1 )
       // string space = "               ";
       
            recent_data = Inp_field[0].text +","+Inp_field[1].text +","+ Dp.options[Dp.value].text + "|";
            jo.transform.GetChild(0).GetComponent<Text>().text = Inp_field[0].text;
        jo.transform.GetChild(1).GetComponent<Text>().text = Inp_field[1].text;
        jo.transform.GetChild(2).GetComponent<Text>().text = Dp.options[Dp.value].text;

        PlayerPrefs.SetString("Rawdata", recent_data + PlayerPrefs.GetString("Rawdata"));
        
        
    }
}
