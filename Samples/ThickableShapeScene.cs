using UnityEngine;
using DG.Tweening;

public class ThickableShapeScene : MonoBehaviour {
    [SerializeField]
    private ThickableShapeView[] _circles;
    [SerializeField]
    private ThickableShapeView[] _squares;
    [SerializeField]
    private ThickableShapeView[] _triangles;

    private void Start() {
        for (int i = 0; i < _circles.Length; i++) {
            ThickableShapeView circle = _circles[i];
            ThickableShapeView square = _squares[i];
            ThickableShapeView triangle = _triangles[i];

            circle.Size = 0;

            Sequence seq = DOTween.Sequence();
            float interval = 0.3f * i + Random.Range(1, 4) * 0.3f + 1f;
            seq.AppendInterval(interval);
            int endScale = 250 + Random.Range(0, 3) * 30;
            seq.Append(DOTween.To(() => circle.Size, size => circle.Size = size, endScale, 0.3f).SetEase(Ease.OutCubic));
            seq.Join(DOTween.To(() => circle.Thickness, thickness => circle.Thickness = thickness, 0, 1.5f).SetEase(Ease.OutCubic));

            SetChildShapeAnim(interval + 0.15f, seq, square);
            SetChildShapeAnim(interval + 0.2f, seq, triangle);

            seq.Play();
        }
    }

    private void SetChildShapeAnim(float atPosition, Sequence seq, ThickableShapeView shape) {
        shape.Size = 0;
        seq.Insert(atPosition, DOTween.To(() => shape.Size, size => shape.Size = size, 100, 1f).SetEase(Ease.OutCubic));
        seq.Insert(atPosition, DOTween.To(() => shape.Thickness, thickness => shape.Thickness = thickness, 0, 1.2f).SetEase(Ease.OutCubic));
        Vector3 endAnles = new Vector3(0f, 0f, Random.Range(120, 270) * (Random.Range(0, 10) % 2 == 0 ? 1 : -1));
        seq.Insert(atPosition, DOTween.To(() => shape.transform.eulerAngles, eulerAngles => shape.transform.eulerAngles = eulerAngles, endAnles, 1.2f).SetEase(Ease.OutCubic));
        Vector3 endPosition = shape.transform.localPosition + new Vector3(Random.Range(-2, 3), Random.Range(-2, 3)) * 40f;
        seq.Insert(atPosition, shape.transform.DOLocalMove(endPosition, 1f).SetEase(Ease.OutCubic));
    }
}