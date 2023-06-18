using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ModuleInfo {

    //I STOLE THIS FROM MODDLE CODE (HALF OF IT ISN'T GOING TO GET USED)

    //Stores the json represented as a dictionary as made by Newtonsoft.Json
    public Dictionary<string, object> json { get; private set; }

    //Stores the display name of the module this represents.
    public string name { get; private set; }

    //Stores the list of contributors to the module, under the "contributors" field.
    public string[] contributors { get; private set; }
    
    //Stores the date that the module was released.
    public DateTime date { get; private set; }

    public bool isUsable { get; private set; }

    public ModuleInfo(Dictionary<string, object> json)
    {
        this.json = json;

        isUsable = true;

        name = (string)json["Name"];

        //Use the first author.
        contributors = ((string)json["Author"]).Split(new[] { ", " }, System.StringSplitOptions.RemoveEmptyEntries);
        
        //Take only the year of the publish date.
        date = DateTime.ParseExact((string)json["Published"], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

        //Do not allow translations in the mod.
        if (json.ContainsKey("TranslationOf"))
            isUsable = false;
    }

    public override string ToString()
    {
        return string.Format("{0} by {1}, published on {2}",
            name,
            contributors.Join(", "),
            date.ToString("yyyy-MM-dd")); //Date format used by the repo.
    }
}
