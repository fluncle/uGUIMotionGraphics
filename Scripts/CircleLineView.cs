using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 丸縁の円形の線を制御するクラス
/// </summary>
public class CircleLineView : MonoBehaviour {
    #region パラメータ
    // サイズ
    public int Size = 150;
    private int _currentSize;
    // 太さ
    public int Thickness = 15;
    private int _currentThickness;
    // 色
    public Color Color = Color.white;
    private Color _currentColor;
    // 右サイドからの回転マスク
    [Range(0f, 1f)]
    public float RightFillAmount = 1f;
    private float _currentRightFillAmount;
    // 左サイドからの回転マスク
    [Range(0f, 1f)]
    public float LeftFillAmount = 1f;
    private float _currentLeftFillAmount;
    #endregion

    // サイズ制御
    [SerializeField]
    private RectTransform _sizeRect;

    // 太さ制御
    [SerializeField]
    private RectTransform _unmaskRect;
    [SerializeField]
    private Image _inImage;

    // 線のマスク
    [SerializeField]
    private Image _rightRollCircle;
    [SerializeField]
    private Image _leftRollCircle;

    // 線の末端
    [SerializeField]
    private RectTransform _rightEndAngle;
    [SerializeField]
    private RectTransform _leftEndAngle;
    [SerializeField]
    private Image _rightEndImage;
    [SerializeField]
    private Image _leftEndImage;

    /// <summary>
    /// 起動時の処理
    /// </summary>
    private void Awake() {
        OnValidate();
        UpdateCurrentValue();
    }

    /// <summary>
    /// 現在のパラメータ更新
    /// </summary>
    private void UpdateCurrentValue() {
        _currentSize = Size;
        _currentThickness = Thickness;
        _currentColor = Color;
        _currentRightFillAmount = RightFillAmount;
        _currentLeftFillAmount = LeftFillAmount;
    }

    /// <summary>
    /// 現在のパラメータに変化があったかどうか
    /// </summary>
    private bool IsChangedValue() {
        return
            _currentSize != Size ||
            _currentThickness != Thickness ||
            _currentColor != Color ||
            _currentRightFillAmount != RightFillAmount ||
            _currentLeftFillAmount != LeftFillAmount;
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update() {
        if (IsChangedValue()) {
            OnValidate();
            UpdateCurrentValue();
        }
    }

    /// <summary>
    /// Inspectorの値が変更されたときのコールバック
    /// パラメータを図形に反映する処理
    /// </summary>
    private void OnValidate()　{
        Size = Mathf.Max(Size, 0);
        Thickness = Mathf.Max(Thickness, 0);

        // 縁の大きさ設定
        _sizeRect.sizeDelta = new Vector2(Size, Size);

        // 太さ設定
        _unmaskRect.offsetMin = new Vector2(Thickness, Thickness); // new Vector2(left, bottom);
        _unmaskRect.offsetMax = new Vector2(-Thickness, -Thickness); // new Vector2(-right, -top);
        _inImage.rectTransform.offsetMin = new Vector2(-Thickness, -Thickness);
        _inImage.rectTransform.offsetMax = new Vector2(Thickness, Thickness);
        _rightEndImage.rectTransform.sizeDelta = new Vector2(Thickness, Thickness);
        _leftEndImage.rectTransform.sizeDelta = new Vector2(Thickness, Thickness);

        // 色の設定
        _rightRollCircle.color = Color;
        _rightEndImage.color = Color;
        _leftEndImage.color = Color;

        // 線のマスク設定
        _rightRollCircle.fillAmount = RightFillAmount;
        _rightEndAngle.localEulerAngles = new Vector3(0f, 0f, RightFillAmount * 360f);
        _leftRollCircle.fillAmount = LeftFillAmount;
        _leftEndAngle.localEulerAngles = new Vector3(0f, 0f, LeftFillAmount * -360f);
    }
}