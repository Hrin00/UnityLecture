using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class DataManager : MonoBehaviour
{
#if UNITY_EDITOR
    private StreamWriter sw;
#elif UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void FileDownLoad(string str, string fileName);
#endif

    private string dirPath;
    private string fileName;
    private string[] header;
    public static List<string> dataStrs;


    // Start is called before the first frame update
    void Start()
    {
        dirPath = Application.dataPath + "/Scripts/CVS/";
        header = new string[] { "Score", "Time" };
        dataStrs = new List<String>();
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickOutputButton()
    {
        fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
#if UNITY_EDITOR
        sw = new StreamWriter(dirPath + fileName + ".csv", false, Encoding.GetEncoding("UTF-8"));
#endif

#if UNITY_EDITOR
        sw.WriteLine(string.Join(",", header));
        foreach (string dataStr in dataStrs)
        {
            sw.WriteLine(dataStr);
        }
        sw.Close();
#elif UNITY_WEBGL
        FileDownLoad(string.Join(",", header) + "\n" + string.Join("\n", dataStrs), fileName);
#endif
        Debug.Log(dirPath + fileName + ".csv has been created");
    }

    public void UpdateData(string dataStr)
    {
        dataStrs.Add(dataStr);
    }

}
