// #define ANALYTICS_SDKS

using UnityEngine;
using UnityEngine.SceneManagement;

#if ANALYTICS_SDKS
using GameAnalyticsSDK;
#endif

public class Initializer : MonoBehaviour
{
#if ANALYTICS_SDKS
    const float SecondsToWait = 2f;

    float timer = 0f;

    void Start()
    {
        InitGA();
        InitAds();
    }

    void InitGA()
    {
        GameAnalytics.Initialize();
    }

    void InitAds()
    {
#if MONETIZATION_SDKS
    #if UNITY_ANDROID
        IronSource.Agent.init(""); // Android App Key
        IronSource.Agent.validateIntegration();
    #elif UNITY_IOS
        IronSource.Agent.init(""); // iOS App Key
        IronSource.Agent.validateIntegration();
    #endif
#endif
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (GameAnalytics.IsRemoteConfigsReady() || timer >= SecondsToWait)
        {
            print("GA:IsRemoteConfigsReady: " + GameAnalytics.IsRemoteConfigsReady());
            print("GA:GetRemoteConfigsContentAsString: " + GameAnalytics.GetRemoteConfigsContentAsString());

            ABTestManager.Instance.Init();

            SceneManager.LoadScene("Main");
        }
    }
#endif
}