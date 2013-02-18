using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

public class jsonDynamicParser {
	public jsonDynamicParser(Dictionary<string, object> jsonItem) {
		FieldInfo[] fieldinfo = GetType().GetFields();
		foreach(FieldInfo field_info in fieldinfo)
		{
			string field_name	= field_info.Name;
			if(jsonItem.ContainsKey(field_name) == true) {	
				object field_value	= jsonItem[field_name];
				field_info.SetValue(this, field_value);
			}
			else{
				Debug.Log("error jsonDynamicParser() - not found key : " + field_name);
			}
		}
	}
}

public class jsonUtill 
{
	public static string ReadJsonFile(string filename) 
	{
		string readBuffer;
			
#if UNITY_ANDROID || UNITY_IPHONE
		if(filename.Contains(".txt"))
		{
			string strSparator	= ".";
			char[] charSparator = strSparator.ToCharArray();
			string[] splits = filename.Split(charSparator);
			filename = splits[0];
		}
			
		TextAsset textAsset = Resources.Load(filename) as TextAsset;
		//check utf-8
		if(textAsset.bytes[0] == 0xef && textAsset.bytes[1] == 0xbb)
			readBuffer =  textAsset.text.Remove(0, 2); //bom 3byte remove
		else
			readBuffer =  textAsset.text;
#else
		
		string assetPath 	= Application.dataPath;
		string dataPath		= "/_Lobby/UnlimitedUI/Resources/" + filename;
		readBuffer	= File.ReadAllText(assetPath + dataPath);
#endif 
		
		return readBuffer;
	}
}