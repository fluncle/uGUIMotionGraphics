using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CircleLineScene : MonoBehaviour {
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Transform _circleLinesParent;

    private void Start() {
        CircleLineView[] circleLines = new CircleLineView[_circleLinesParent.childCount];
        for(int i = 0; i < circleLines.Length; i++) {
            circleLines[i] = _circleLinesParent.GetChild(i).GetComponent<CircleLineView>();
        }

        for (int i = 0; i < circleLines.Length; i++) {
            CircleLineView circleLine = circleLines[i];
            int targetThickness = circleLine.Thickness;
            circleLine.Thickness = 0;

            Sequence seq = DOTween.Sequence();
            float interval = 1f + i * 0.05f;
            seq.AppendInterval(interval);
            seq.AppendCallback(() => { circleLine.Thickness = 30; });
            seq.Append(DOTween.To(() => circleLine.Thickness, thickness => circleLine.Thickness = thickness, targetThickness, 0.5f).SetEase(Ease.OutQuad));
            seq.Join(DOTween.To(() => circleLine.RightFillAmount, fillAmount => circleLine.RightFillAmount = fillAmount, 1f, 0.7f).SetEase(Ease.OutQuad));
            seq.Play();
        }

        float afterStartTime = 1f + circleLines.Length * 0.15f + 0.7f;

        for(int i = 0; i < circleLines.Length; i++) {
            CircleLineView circleLine = circleLines[i];

            Sequence seq = DOTween.Sequence();
            float interval = afterStartTime + i * 0.15f;
            seq.AppendInterval(interval);
            if(i == 0) {
                seq.AppendCallback(() => {
                    _camera.backgroundColor = Color.white;
                    _text.color = Color.black;
                });
            }
            seq.Append(DOTween.To(() => circleLine.Thickness, thickness => circleLine.Thickness = thickness, 0, 0.6f).SetEase(Ease.OutQuad));
            seq.Join(DOTween.To(() => circleLine.LeftFillAmount, fillAmount => circleLine.LeftFillAmount = fillAmount, 0f, 0.6f).SetEase(Ease.OutQuad));
            seq.Play();
        }
    }
}