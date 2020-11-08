using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Scene Config", menuName = "Config/SceneConfig")]
public class SceneConfig : ScriptableObject
{
    public List<SceneList> allGameScenes;

    public int GetNextScene(Enums.GameMode gameMode, int currSceneIndex)
    {
        foreach(SceneList sl in allGameScenes)
        {
            if(sl.gameMode == gameMode)
            {
                for(int i = 0; i<sl.sceneBuildIndex.Count-1; ++i)
                {
                    if(sl.sceneBuildIndex[i] == currSceneIndex)
                    {
                        return sl.sceneBuildIndex[i + 1];
                    }
                }

            }
        }

        return 0;
    }
}

[System.Serializable]
public class SceneList
{
    public Enums.GameMode gameMode;
    public List<int> sceneBuildIndex;
}
