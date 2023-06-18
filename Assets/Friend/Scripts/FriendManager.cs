using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using KModkit;

public class FriendManager : MonoBehaviour
{
    private List<Friend> friendList;
    private KMBombInfo bomb;
    private List<string> bombModuleNames; //list of all module names on the bomb without spaces in between
    private List<ModuleInfo> repoModules; //all (non translated) modules found on the repo
    private MainScript main;
    private Friend aaron;


    public FriendManager(KMBombInfo bomb, List<ModuleInfo> repoModules, List<string> bombModuleNames)
    {
        main = GetComponent<MainScript>();
        this.bombModuleNames = bombModuleNames;
        this.bomb = bomb;
        this.repoModules = repoModules;
        Friend.SerialNumber = bomb.GetSerialNumber().ToUpper();

        aaron = new Friend("Aaron", "Aaron Kitty Boiii");

        friendList = new List<Friend>()
        {
            aaron,
            new Friend("Eltrick", "Eltrick"),
            new Friend("Kugel", "Kugel"),
            new Friend("Mas", "MasQueElite"),
            new Friend("Mico", "Mico"),
            new Friend("Razor", "RazorBlade"),
            new Friend("Skipz", "SkipzPlays"),
            new Friend("Tammy", "Tammy"),
            new Friend("Umbra", "Umbra Moruka"),
            new Friend("???", "???"),
        };
    }

    void SwapPositions(List<int> conditions)
    {

    }

    private bool GetConditionFriend(Friend f)
    {
        if (!f.SpecialRuleApplies())
        {
            return false;
        }

        switch (f.ShortName)
        {
            case "Aaron":
                return AaronCondition();
            case "Eltrick":
                return EltrickCondition();
            case "Kugel":
                return KugelCondition();
            case "Mas":
                return MasCondition();
            case "Mico":
                return MicoCondition();
            case "Razor":
                return RazorCondition();
            case "Skipz":
                return SkipzCondition();
            case "Tammy":
                return TammyCondition();
            case "Umbra":
                return UmbraCondition();
            default: // ???
                return QuestionCondition();
        }
    }

    private bool HasModule(string moduleName)
    {
        return bombModuleNames.Any(x => x.ToUpper() == moduleName);
    }



    private ModuleInfo FindModule(string moduleName)
    {
        for (int i = 0; i < repoModules.Count; i++)
        {
            if (repoModules[i].name == moduleName)
            {
                return repoModules[i];
            }
        }

        return null;
    }

    private bool AaronCondition()
    {
        return HasModule("NYA~") && HasModule("FLYSWATTING");
    }

    private bool EltrickCondition()
    {
        return bombModuleNames.Count(x => FindModule(x).contributors.Contains("Eltrick")) >= 3;
    }

    private bool RazorCondition()
    {
        return
        bomb.GetBatteryCount() == 1 && bomb.GetOffIndicators().Any(x => x.ToUpper() == "FRK") &&
        bomb.GetPorts().Any(x => x.ToUpper() == "SERIAL") && HasModule("RUBIK'S CUBE") && (main.ModuleContainsWord("GRAY") || main.ModuleContainsWord("GREY"));
    }

    private bool KugelCondition()
    {
        return bomb.GetBatteryCount() >= 6 && HasModule("KUGELBLITZ");
    }

    private bool MasCondition()
    {
        return HasModule("CHEEP CHECKOUT");
    }

    private bool MicoCondition()
    {
        return bomb.GetOnIndicators().Count() >= 5;
    }

    private bool SkipzCondition()
    {
        return bomb.GetOffIndicators().Count() >= 4 && main.ModuleContainsWord("PIANO") && friendList.IndexOf(aaron) < 3 && !aaron.HangOut;
    }

    private bool TammyCondition()
    {
        return main.ModuleContainsWord("DUCK") && friendList.IndexOf(aaron) < 3;
    }

    private bool UmbraCondition()
    {
        return HasModule("NYA~") && HasModule("THE GARNET THIEF");
    }

    private bool QuestionCondition()
    {
        return HasModule("...?") && HasModule("QUESTION MARK") && main.ModuleContainsWord("cipher");
    }

}
