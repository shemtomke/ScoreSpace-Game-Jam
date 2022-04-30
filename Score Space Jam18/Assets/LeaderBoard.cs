using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LeaderBoard : MonoBehaviour
{
    int LeaderboardID = 2679;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator SubmitScoreRoutine(int scoretoupload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoretoupload, LeaderboardID, (Response) =>
        {
            if (Response.success)
            {
                Debug.Log("Uploaded Score");
                done = true;
            }
            else
            {
                Debug.Log("Failed to Upload Score" + Response.Error);
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }
}
