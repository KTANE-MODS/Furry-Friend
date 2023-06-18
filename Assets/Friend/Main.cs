using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;

    RepoJSONGetter json;
    const string url = "";


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
        

    }

    void SetUpModule()
    {
        if (RepoJSONGetter.Success)
        {
            RepoJSONGetter.UsableModules.ForEach(x => Debug.Log(x.ToString()));
        }
    }

    void Update()
    {

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
