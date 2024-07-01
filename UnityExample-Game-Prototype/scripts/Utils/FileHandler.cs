using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public static class FileHandler
{
    public static void SaveToJSON<T>(List<T> toSave, string filename)
    {
        try
        {
            WriteFile(GetPath(filename), JsonHelper.ToJson<T>(toSave.ToArray()));
        }
        catch (System.Exception e)
        {

            Debug.LogError("Error saving JSON: " + e.Message);
        }
    }

    public static void SaveToJSON<T>(T toSave, string filename)
    {
        try
        {
            WriteFile(GetPath(filename), JsonUtility.ToJson(toSave));
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving JSON: " + e.Message);
        }
    }

    public static List<T> ReadListFromJSON<T>(string filename, string path = "")
    {
        try
        {
            string content = path == "" ? ReadFile(GetPath(filename)) : ReadFile(GetAssetPath(path));

            if (string.IsNullOrEmpty(content) || content == "{}")
            {
                return new List<T>();
            }

            List<T> res = JsonHelper.FromJson<T>(content).ToList();

            return res;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading JSON list: " + e.Message);
            return new List<T>();
        }
    }
    public class StringListWrapper
    {
        public List<string> items;
    }

    public static List<string> ReadFromJSON(string filename, string path = "")
    {
        try
        {

            string content = path == ""? ReadFile(GetPath(filename)) : ReadFile(GetAssetPath(path));

        

            if (string.IsNullOrEmpty(content) || content == "{}")
            {
                return new List<string>();
            }

            StringListWrapper wrapper = JsonUtility.FromJson<StringListWrapper>("{\"items\":" + content + "}");
            List<string> res = wrapper.items;
            Debug.Log(res.Count);
            return res;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading JSON: " + e.Message);
            return new List<string>();
        }
    }

    public static string GetPath(string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }

    public static string GetAssetPath(string filename)
    {
        return Application.dataPath + "/" + filename;
    }

    private static void WriteFile(string path, string content)
    {
        try
        {
            FileStream fileStream = new FileStream(path, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(content);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error writing to file: " + e.Message);
        }
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string content = reader.ReadToEnd();
                    return content;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error reading file: " + e.Message);
            }
        }
        return "";
    }
}
 