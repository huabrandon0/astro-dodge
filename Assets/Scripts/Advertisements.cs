using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace TWM.Advertisements
{
    public class Advertisements : MonoBehaviour
    {
        public void ShowVideoAd()
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show("video", new ShowOptions() { resultCallback = HandleShowResult });
            }
        }

        void HandleShowResult(ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    Debug.Log("ad finished :)");
                    break;
                case ShowResult.Skipped:
                    Debug.Log("ad skipped :(");
                    break;
                case ShowResult.Failed:
                    Debug.Log("ad failed :O");
                    break;
            }
        }
    }
}
