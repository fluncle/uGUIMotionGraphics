using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 丸縁の線を制御するクラス
/// </summary>
public class RoundedLineView : MonoBehaviour {
    #region パラメータ
    // 長さ
    public int Length = 150;
    private int _currentLength;
    // 太さ
    public int Thickness = 15;
    private int _currentThickness;
    // 色
    public Color Color = Color.white;
    private Color _currentColor;
    // 右サイドからのマスク
    [Range(0f, 1f)]
    public float RightFillAmount = 1f;
    private float _currentRightFillAmount;
    // 左サイドからのマスク
    [Range(0f, 1f)]
    public float LeftFillAmount = 1f;
    private float _currentLeftFillAmount;
    #endregion

    // サイズ制御
    [SerializeField]
    private RectTransform _sizeRect;

    // 線のマスク
    [SerializeField]
    private Image _rightMask;
    [SerializeField]
    private Image _leftMask;

    // 線の末端
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
        _currentLength = Length;
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
            _currentLength != Length ||
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
    private void OnValidate() {
        Length = Mathf.Max(Length, 0);
        Thickness = Mathf.Max(Thickness, 0);

        // 縁の長さ、太さ設定
        _sizeRect.sizeDelta = new Vector2(Length, Thickness);

        // 線の端の太さ設定
        _rightEndImage.rectTransform.sizeDelta = new Vector2(Thickness, Thickness);
        _leftEndImage.rectTransform.sizeDelta = new Vector2(Thickness, Thickness);

        // 色の設定
        _rightMask.color = Color;
        _rightEndImage.color = Color;
        _leftEndImage.color = Color;

        // 線のマスク設定
        _rightMask.fillAmount = RightFillAmount;
        _rightEndImage.rectTransform.anchoredPosition = new Vector3(Mathf.Lerp(-Length, 0f, RightFillAmount), 0f);
        _leftMask.fillAmount = LeftFillAmount;
        _leftEndImage.rectTransform.anchoredPosition = new Vector3(Mathf.Lerp(Length, 0f, LeftFillAmount), 0f);
    }
}