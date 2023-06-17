using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend {

    private string ShortName {  get;  set; }
	private string FullName { get; set; }
	private bool SpecialCondition { get; set; }
    public static string SerialNumber { get; set; }
	public Friend(string name, string fullName, bool specialCondition)
	{
		this.ShortName = name;
		this.FullName = fullName;
		this.SpecialCondition = SpecialRuleApplies() && specialCondition;
	}

    private bool SpecialRuleApplies()
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
