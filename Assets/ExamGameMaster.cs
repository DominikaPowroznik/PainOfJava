using UnityEngine;
using System.Collections;

public class ExamGameMaster : MonoBehaviour {

    //we want to have only 1 instance of GM
    public static ExamGameMaster egm;

    void Awake()
    {
        if (egm == null)
        {
            egm = GameObject.FindGameObjectWithTag("ExamGameMaster").GetComponent<ExamGameMaster>(); ;
        }
    }
}
