using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
public class title : MonoBehaviour {

    // Use this for initialization
    public void LevelsGo()
    {
        SceneManager.LoadScene(1);
    }

    public void AdsExtra()
    {
        if(Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = AdsAnalise });
          
        }

    }



    void AdsAnalise(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            ScoreManager.instance.ColetaMoedas(500);
        }
    }

}
