using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using AsteroidRage.Game;

namespace TWM.Advertisements
{
    public class Advertisements : MonoBehaviour
    {
        int _counter = 0;
        int _intervalCounter = 0;

        public void IncrementCounter()
        {
            _counter++;
        }

        public void ShowVideoIfCounterAboveValue(int val)
        {
            if (_counter > val)
            {
                ShowVideoAd();
                _counter = 0;
            }
        }

        public void ShowVideoAdEveryInterval(int interval)
        {
            _intervalCounter++;

            if (interval <= 0)
                ShowVideoAd();
            else if (_intervalCounter % interval == 0)
                ShowVideoAd();
        }

        public void ShowVideoAd()
        {
            if (Advertisement.IsReady())
                Advertisement.Show("video");
        }

        public void ShowRewardedVideoAd()
        {
            if (Advertisement.IsReady())
                Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleShowResult });
        }

        void HandleShowResult(ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    Debug.Log("ad finished :)");
                    Currency.Instance._collectDouble = true;
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
