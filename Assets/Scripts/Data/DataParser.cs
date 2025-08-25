using System.Data;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class DataParser
{
    private static string _xmlPath = "Resources/Xml/";
    private static string _jsonPath = "Resources/Json/";

    public static void WriteXMLData<T>(string fileName, T data)
    {
        XmlSerializer xml = new(typeof(T));
        FileStream stream = new(_xmlPath + fileName, FileMode.OpenOrCreate, FileAccess.Write);
        xml.Serialize(stream, data);
    }
    public static T ParseXMLData<T>(string fileName)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        FileStream stream = new(_xmlPath + fileName, FileMode.Open, FileAccess.Read);
        return (T)xml.Deserialize(stream);
    }

    public static void WriteJsonData<T>(string fileName, T data)
    {
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(_jsonPath + fileName, json);
    }
    public static T ParseJsonData<T>(string fileName)
    {
        return JsonUtility.FromJson<T>(File.ReadAllText(_jsonPath + fileName));
    }
}
