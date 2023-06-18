using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;

public class MainScript : MonoBehaviour
{

    public KMBombInfo Bomb;
    public KMAudio Audio;

    RepoJSONGetter json;
    const string url = "";

    #region Edgework
    string serialNumber;
    List<char> serialNumberLetters;
    List<int> serialNumberDigits;
    List<string> indicators;
    List<string> litIndicators;
    List<string> unlitIndicators;
    List<string> ports;


    #endregion

    private List<string> bombModuleNamesNoSpace; //list of all module names on the bomb without spaces in between
    private List<string> bombModuleNames; //list of all module names on the bomb without spaces in between


    static int ModuleIdCounter = 1;
    int ModuleId;
    private bool ModuleSolved;


    void Awake()
    {
        ModuleId = ModuleIdCounter++;
        /*
        foreach (KMSelectable object in keypad) {
            object.OnInteract += delegate () { keypadPress(object); return false; };
        }
        */

        //button.OnInteract += delegate () { buttonPress(); return false; };
    }

    IEnumerator Start()
    {
        //if data is not done loaded
        if (!RepoJSONGetter.LoadingDone)
        {
            //if not already loading, load
            if (!RepoJSONGetter.Loading)
            {
                yield return json.LoadData(url);
            }

            //if aleady loading, wait until loading is done
            else
            {
                do
                {
                    yield return new WaitForSeconds(0.1f);

                } while (!RepoJSONGetter.LoadingDone);
            }
        }

        SetUpModule();

        GetEdgework();
    }

    void Update()
    {

    }

    void GetEdgework()
    {
        Bomb.GetModuleNames().Select(x => x).ToList();

        serialNumber = Bomb.GetSerialNumber().ToUpper();
        serialNumberLetters = Bomb.GetSerialNumberLetters().ToList();
        serialNumberDigits = Bomb.GetSerialNumberNumbers().ToList();

        indicators = Bomb.GetIndicators().ToList();
        litIndicators = Bomb.GetOnIndicators().ToList();
        unlitIndicators = Bomb.GetOffIndicators().ToList();

        ports = Bomb.GetPorts().ToList();
    }

    void SetUpModule()
    {
        if (RepoJSONGetter.Success)
        {
            RepoJSONGetter.UsableModules.ForEach(x => Debug.Log(x.ToString()));
        }
    }

    /// <summary>
    /// Getting what conditions applied in order to figure out who to swap
    /// </summary>
    private List<int> GetConditions()
    {
        List<int> conditions = new List<int>();

        if (ContainsIndictor("FRK"))
        {
            conditions.Add(1);
        }

        string indicatorLetters = string.Join("", indicators.ToArray()).ToUpper();

        int aCount = 0;

        foreach (char c in indicatorLetters)
        {
            if (c == 'A')
            {
                aCount++;
            }
        }

        foreach (char c in serialNumber)
        {
            if (c == 'A')
            {
                aCount++;
            }
        }

        if (aCount >= 2)
        {
            conditions.Add(2);
        }

        if (ContainsIndictor("TRN"))
        {
            conditions.Add(3);
        }

        if () //cant do this rule until maunal is worded better
        {
            conditions.Add(4);
        }

        if (ContainsIndictor("FRQ"))
        {
            conditions.Add(5);
        }

        if ()  //cant do this rule until maunal is worded better
        {
            conditions.Add(6);
        }

        if (ContainsIndictor("IND"))
        {
            conditions.Add(7);
        }

        if (serialNumberLetters.Count == 4)
        {
            conditions.Add(8);
        }

        if (ContainsIndictor("SND"))
        {
            conditions.Add(9);
        }

        if (serialNumberDigits.Count() == 2 || serialNumberDigits.Count() == 4)
        {
            conditions.Add(10);
        }

        if (ContainsIndictor("CAR"))
        {
            conditions.Add(11);
        }

        if (ModuleContainsWord("fur"))
        {
            conditions.Add(12);
        }

        if (ContainsIndictor("SIG"))
        {
            conditions.Add(13);
        }

        if (ModuleContainsWord("cat"))
        {
            conditions.Add(14);
        }

        if (ContainsIndictor("BOB"))
        {
            conditions.Add(15);
        }

        if (litIndicators.Count > unlitIndicators.Count)
        {
            conditions.Add(16);
        }

        if (unlitIndicators.Count > litIndicators.Count)
        {
            conditions.Add(17);
        }

        if (unlitIndicators.Count == litIndicators.Count)
        {
            conditions.Add(18);
        }

        if (indicators.Count == 0)
        {
            conditions.Add(19);
        }

        if (ContainsIndictor("MSA"))
        {
            conditions.Add(20);
        }

        if (serialNumber.Contains("E") && (serialNumber.Contains("A") || serialNumber.Contains("U")))
        {
            conditions.Add(21);
        }

        if (ContainsIndictor(""))
        {
            conditions.Add(22);
        }

        if (conditions.Count >= 5)
        {
            conditions.Add(23);
        }

        if (ContainsIndictor("NSA"))
        {
            conditions.Add(24);
        }

        if (GetPortCount("DVI") >= 3 ||
           GetPortCount("RJ45") >= 3 ||
           GetPortCount("STEREORCA") >= 3 ||
           GetPortCount("PARALLEL") >= 3 ||
           GetPortCount("PS2") >= 3 ||
           GetPortCount("SERIAL") >= 3)
        { 
            conditions.Add(25);
        }


        return conditions;
    }

    private int GetPortCount(string portName)
    {
        return ports.Count(x => x.ToUpper() == portName.ToUpper());
    }

    private bool ContainsIndictor(string name)
    {
        return indicators.Any(x => x.ToUpper() == name);
    }

    public bool ModuleContainsWord(string word)
    {
        return bombModuleNamesNoSpace.Any(x => x.Contains(word.ToUpper()));
    }

    public void GetModuleNames()
    {
        bombModuleNames = Bomb.GetModuleNames();

        bombModuleNamesNoSpace = new List<string>();

        foreach (string s in bombModuleNames)
        {
            string temp = s.Replace(" ", "").ToUpper();

            bombModuleNamesNoSpace.Add(temp);
        }
    }



#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string Command)
    {
        yield return null;
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;
    }
}
