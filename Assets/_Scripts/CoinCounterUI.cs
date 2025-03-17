using UnityEngine;
using System.Collections;
using DG.Tweening;
using TMPro;

public class CoinCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private float duration;
    [SerializeField] private Transform coinTextContainter;
    private float containerInitPosition;
    private float moveAmount;
    [SerializeField] private Ease animationCurve;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Canvas.ForceUpdateCanvases();
        current.SetText("0");
        toUpdate.SetText("0");
        containerInitPosition = coinTextContainter.localPosition.y;
        moveAmount = current.rectTransform.rect.height;
    }
    void Update()
    {

    }

    // Update is called once per frame
    public void UpdateScore(int score)
    {
        toUpdate.SetText($"{score}");
        coinTextContainter.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(animationCurve);
        StartCoroutine(ResetCoinContainer(score));
    }
    private IEnumerator ResetCoinContainer(int score)
    {
        yield return new WaitForSeconds(duration);
        current.SetText($"{score}");
        Vector3 localPosition = coinTextContainter.localPosition;
        coinTextContainter.localPosition = new Vector3(coinTextContainter.localPosition.x, containerInitPosition, coinTextContainter.localPosition.z);
    }
}
