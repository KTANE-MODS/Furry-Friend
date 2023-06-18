using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend {

    public string ShortName {  get; private set; }
	public string FullName { get; private  set; }
    public static string SerialNumber { get; set; }
    public bool HangOut { get; set; } //if the friend has been seen already
	public Friend(string name, string fullName)
	{
		this.ShortName = name;
		this.FullName = fullName;
        HangOut = false;
	}

    public bool SpecialRuleApplies()
    {
        FullName = FullName.ToUpper();

        foreach (char c in SerialNumber)
        {
            if (FullName.Contains("" + c))
            {
                return true;
            }

        }
        return false;
    }


}
