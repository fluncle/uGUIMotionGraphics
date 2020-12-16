using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RoundedLineScene : MonoBehaviour {
    [SerializeField]
    private Transform _underViewsParent;
    [SerializeField]
    private Transform _overViewsParent;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Camera _camera;

    private void Start() {
        List<RoundedLineView> underViews = GetViews(_underViewsParent);
        List<RoundedLineView> overViews = GetViews(_overViewsParent);

        float delay = 0.7f;
        float interval = 0.2f;
        for (int i = 0; i < underViews.Count; i++) {
            RoundedLineView view = underViews[i];
            view.RightFillAmount = 0;

            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(delay + interval * i);
            seq.Append(DOTween.To(() => view.RightFillAmount, amount => view.RightFillAmount = amount, 1f, 0.8f));
            seq.SetEase(Ease.OutQuad);
            seq.Play();
        }

        float offset = 0.3f;
        for (int i = 0; i < overViews.Count; i++) {
            RoundedLineView view = overViews[i];

            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(delay + interval * i + offset);
            seq.Append(DOTween.To(() => view.RightFillAmount, amount => view.RightFillAmount = amount, 1f, 1f));
            seq.SetEase(Ease.OutQuad);
            seq.Play();
        }

        float afterStartTime = delay + interval * overViews.Count + offset + 0.7f;
        interval = 0.1f;
        for(int i = 0; i < overViews.Count; i++) {
            RoundedLineView view = overViews[i];

            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(interval * i + afterStartTime);
            if(i == 0) {
                seq.AppendCallback(() => {
                    _camera.backgroundColor = Color.black;
                    _text.color = Color.white;
                    for(int j = 0; j < underViews.Count; j++) {
                        underViews[j].Thickness = 0;
                    }
                });
            }
            seq.Append(DOTween.To(() => view.LeftFillAmount, amount => view.LeftFillAmount = amount, 0f, 0.25f));
            seq.SetEase(Ease.OutQuad);
            seq.Play();
        }
    }

    private List<RoundedLineView> GetViews(Transform viewParent) {
        List<RoundedLineView> views = new List<RoundedLineView>();
        for (int i = 0; i < viewParent.childCount; i++) {
            RoundedLineView view = viewParent.GetChild(i).GetComponent<RoundedLineView>();
            if (view != null) {
                views.Add(view);
            }
        }
        return views;
    }
}