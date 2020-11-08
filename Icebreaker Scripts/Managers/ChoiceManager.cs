using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public ChoiceConfig choiceConfig;
    // Start is called before the first frame update

    public void RecordChoice(int choiceNum, string choice, string time)
    {
        choiceConfig.SetChoice(choiceNum, choice, time);    
    }

    public int CheckChoice(int choiceNumber)
    {
        if (choiceConfig.choices.Count>= choiceNumber)
        {
            bool valid = int.TryParse(choiceConfig.choices[choiceNumber - 1].choice, out int c);

            if (valid)
            {
                return c;
            }

            else
            {
                return 1;
            }
        }

        return 1;

    }

}


