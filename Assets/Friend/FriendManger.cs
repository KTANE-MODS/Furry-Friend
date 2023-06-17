using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using KModkit;

public class FriendManger  {
    private List<Friend> friendList;
    private KMBombInfo bomb;
    private List<string> moduleNames; //list of all module names without spaces in between
    public FriendManger(KMBombInfo bomb)
    {
        moduleNames = 
        Friend.SerialNumber = bomb.GetSerialNumber().ToUpper();

        friendList = new List<Friend>()
        {
            new Friend("Aaron", "Aaron Kitty Boiii", HasModule("NYA~") && HasModule("FLYSWATTING")),
            new Friend("Eltrick", "Eltrick", ),
            new Friend("Kugel", "Kugel", bomb.GetBatteryCount() >= 6 && HasModule("KUGELBLITZ")),
            new Friend("Mas", "MasQueElite", HasModule("CHEEP CHECKOUT")),
            new Friend("Mico", "Mico", bomb.GetOnIndicators().Count() >= 5),
            new Friend("Razor", "RazorBlade", & ),
            new Friend("Skipz", "SkipzPlays"),
            new Friend("Tammy", "Tammy"),
            new Friend("Umbra", ),
            new Friend("???", ),


        }
    }

    private bool HasModule(string moduleName)
    {
        return bomb.GetModuleNames().Any(x => x.ToUpper() == moduleName);
    }

    private bool RazorCondition()
    {
        bomb.GetBatteryCount() == 1 && bomb.GetOffIndicators().Any(x => x == "FRK") && 
        bomb.GetPorts().Any(x => x == "SERIAL") && HasModule("RUBIK'S CUBE") && bomb.GetModuleNames().Any(x => x.ToUpper().Contains("GRA"))
    }

    private void GetModuleNames() {
        moduleNames = new List<string>();
    }

    private bool ModuleContainsWord(string word)
    { 
        List
    }

    
}
