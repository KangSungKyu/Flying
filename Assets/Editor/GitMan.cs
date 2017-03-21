using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;

public class GitMan : ScriptableWizard {

    public string bashPath = "C:/Program Files/Git/git-bash.exe";
    public string local = "";
    public string commitmsg = "";


    [MenuItem("Edit/Git Commit in Local")]
    static void GitCommitLocal()
    {
        ScriptableWizard.DisplayWizard("Commit in Local", typeof(GitMan), "Commit");
    }
    [MenuItem("Edit/Git Push to Hub")]
    static void GitPushHub()
    {

    }

    private void OnEnable()
    {
        
    }
    private void OnWizardCreate()
    {
        if (Selection.objects == null)
            return;

        string commit = "git commit -m \"" + commitmsg + "\"";

        Process bash = Process.Start(bashPath);

        bash.StandardInput.Write(commit);
    }

}
