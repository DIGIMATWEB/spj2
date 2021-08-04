using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public string[] items;
   
IEnumerator Start()
    {
        WWW itemsData = new WWW("https://databasechroma.000webhostapp.com/itemsData.php");
        yield return itemsData;
        string itemsDataString = itemsData.text;
        print(itemsDataString);
        items = itemsDataString.Split(';');
        print(GetDataValue(items[0], "Mail:"));
     }
    string GetDataValue(string data,string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))value = value.Remove(value.IndexOf("|"));
        return value;
    }

}
