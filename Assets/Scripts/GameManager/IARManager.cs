using Google.Play.Review;
using System.Collections;
using UnityEngine;

public class IARManager : MonoBehaviour
{
    private ReviewManager _reviewManager;
    private PlayReviewInfo _playReviewInfo;

    public static bool isShownReview;

    private void Start()
    {
        _reviewManager = new ReviewManager();
    }

    public void ShowReview()
    {
        StartCoroutine(RequestReviews());
    }

    private IEnumerator RequestReviews()
    {
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
            yield break;

        _playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; 

        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
            yield break;

        isShownReview = true;
        Progress.Instance.progressData.isShownReview = true;
        Progress.Instance.Save();
    }
}
