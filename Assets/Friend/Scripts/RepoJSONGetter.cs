using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class RepoJSONGetter : MonoBehaviour {

	public static bool Success = false;
	public static bool Loading = false;
	public static bool LoadingDone = false;

    //Only stores the ModuleInfos which have their isUsable property
    public static List<ModuleInfo> UsableModules;

    //Container class for JsonConvert.DeserializeObject<T>.
    private class ktaneData
	{
		public List<Dictionary<string, object>> KtaneModules { get; set; }
	}

    //Loads the data from the json
    public IEnumerator LoadData(string url)
    {
        Loading = true;

        //Stores the raw text of the grabbed json.
        string dataString;

        WWW request = new WWW(url);
        //Waits until the WWW request returns the JSON file.
        yield return request;

        if (request.error != null)
        {
            Success = false;
            Debug.Log("Failed to get data!");
        }

        else
        {
            Debug.Log("Gotten info!");
            dataString = request.text;
            Success = true;

            //Turns the raw JSON into an instance of the container class, which contains a List of Dictionaries.
            ktaneData deserial = JsonConvert.DeserializeObject<ktaneData>(dataString);

            UsableModules = new List<ModuleInfo>();

            foreach (Dictionary<string, object> dict in deserial.KtaneModules)
            {
                ModuleInfo info = new ModuleInfo(dict);

                if (info.isUsable)
                    UsableModules.Add(info);
            }
        }

        Loading = false;
        LoadingDone = true;
    }
}
