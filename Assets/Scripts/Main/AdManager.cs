using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : Singleton<AdManager>
{
#if MONETIZATION_SDKS
    const string AP_DEFAULT = "default"; // reward ad-nii placement-ee monitezation hariutssan ajiltandaa helj garguulj awna. 

    void Start()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.BOTTOM);
        AddEventListeners();
    }

    void AddEventListeners()
    {
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;

        LoadInterstitialAd();
    }

    void RemoveEventListeners()
    {
        IronSourceEvents.onInterstitialAdReadyEvent -= InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent -= InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent -= InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent -= InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent -= InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent -= InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent -= InterstitialAdClosedEvent;

        IronSourceEvents.onRewardedVideoAdOpenedEvent -= RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent -= RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent -= RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent -= RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent -= RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent -= RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent -= RewardedVideoAdShowFailedEvent;
    }

    void OnDestroy()
    {
        RemoveEventListeners();
    }


    // Update is called once per frame
    void Update()
    {

    }
    // Invoked when the initialization process has failed.
    // @param description - string - contains information about the failure.
    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
    }
    // Invoked when the ad fails to show.
    // @param description - string - contains information about the failure.
    void InterstitialAdShowFailedEvent(IronSourceError error)
    {

    }
    // Invoked when end user clicked on the interstitial ad
    void InterstitialAdClickedEvent()
    {
    }
    // Invoked when the interstitial ad closed and the user goes back to the application screen.
    void InterstitialAdClosedEvent()
    {

    }
    // Invoked when the Interstitial is Ready to shown after load function is called
    void InterstitialAdReadyEvent()
    {
    }
    // Invoked when the Interstitial Ad Unit has opened
    void InterstitialAdOpenedEvent()
    {
    }
    // Invoked right before the Interstitial screen is about to open.
    // NOTE - This event is available only for some of the networks. 
    // You should treat this event as an interstitial impression, but rather use InterstitialAdOpenedEvent
    void InterstitialAdShowSucceededEvent()
    {
    }

    void LoadInterstitialAd()
    {
        if (!IronSource.Agent.isInterstitialReady()) IronSource.Agent.loadInterstitial();
    }

    // Interstitial Ad duudah method
    public void ShowInterstitialAd()
    {
        if (IronSource.Agent.isInterstitialReady()) {
            IronSource.Agent.showInterstitial();
        } else {
            LoadInterstitialAd();
        }
    }

    public void ShowDefaultRV()
    {
        IronSource.Agent.showRewardedVideo(AP_DEFAULT);
    }

    //Invoked when the RewardedVideo ad view has opened.
    //Your Activity will lose focus. Please avoid performing heavy
    //tasks till the video ad will be closed.
    private void RewardedVideoAdOpenedEvent()
    {

    }

    //Invoked when the RewardedVideo ad view is about to be closed.
    //Your activity will now regain its focus.
    void RewardedVideoAdClosedEvent()
    {

    }

    //Invoked when there is a change in the ad availability status.
    //@param - available - value will change to true when rewarded videos are available.
    //You can then show the video by calling showRewardedVideo().
    //Value will change to false when no videos are available.
    void RewardedVideoAvailabilityChangedEvent(bool available)
    {

    }

    //Invoked when the user completed the video and should be rewarded. 
    //If using server-to-server callbacks you may ignore this events and wait for 
    // the callback from the  ironSource server.
    //@param - placement - placement object which contains the reward data 
    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        Debug.Log("BCDebug=> reward for ad placement: " + placement.getPlacementName());

        if (placement.getPlacementName() == AP_DEFAULT) {
            Debug.Log("BCDebug=> Give default reward");
        }
    }

    //Invoked when the Rewarded Video failed to show
    //@param description - string - contains information about the failure.
    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        Debug.Log("BCDebug=> RewardedVideoAdShowFailedEvent");
    }

    // ----------------------------------------------------------------------------------------
    // Note: the events below are not available for all supported rewarded video ad networks. 
    // Check which events are available per ad network you choose to include in your build. 
    // We recommend only using events which register to ALL ad networks you include in your build. 
    // ----------------------------------------------------------------------------------------
    //Invoked when the video ad starts playing. 
    void RewardedVideoAdStartedEvent()
    {
        Debug.Log("BCDebug=> RewardedVideoAdStartedEvent");
    }

    //Invoked when the video ad finishes playing. 
    void RewardedVideoAdEndedEvent()
    {
        Debug.Log("BCDebug=> RewardedVideoAdEndedEvent");
    }
#endif
}
