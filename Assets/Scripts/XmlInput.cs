using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class XmlInput : MonoBehaviour
{
    [SerializeField] private SplineController splineController;
    private void Awake()
    {
        List<Vector3> positions = new List<Vector3>();
        List<int> splinesCount = new List<int>();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(Application.dataPath+"/Spline.xml");

        XmlNode dotsNode = xmlDocument.SelectSingleNode("/Сплайн");
        XmlNodeList dotsNodeList = dotsNode.ChildNodes;
        string filter(string str)
        {
            string resultStr = "";

            foreach (char c in str)
            {
                if ((c >= 48 && c <= 57) || c == 45)
                {
                    resultStr += c;
                }
                else if (c == 44 || c == 46)
                {
                    resultStr += ',';
                }
            }
            return resultStr;
        }

        foreach (XmlNode segmentNode in dotsNodeList)
        {
            int temp = 0;
            foreach (XmlNode dotNode in segmentNode)
            {
                string x = filter(dotNode.Attributes.GetNamedItem("x").Value);
                string y = filter(dotNode.Attributes.GetNamedItem("y").Value);
                string z = filter(dotNode.Attributes.GetNamedItem("z").Value);
                positions.Add(new Vector3(ConvertToFloat(x), ConvertToFloat(y), ConvertToFloat(z)));
                temp++;
            }
            splinesCount.Add(temp);
        }
        splineController.CreateSplines(positions,splinesCount);
    }
    private float ConvertToFloat(string text)
    {
        float temp;
        float.TryParse(text, out temp);
        return temp; 
    }
}
