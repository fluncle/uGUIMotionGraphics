using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 指定したスプライトで大きさ・太さを可変な図形を制御するクラス
/// </summary>
public class ThickableShapeView : MonoBehaviour {
    #region パラメータ
    // スプライト
    public Sprite SourceImage;
    private Sprite _currentSourceImage;
    // サイズ
    public int Size = 150;
    private int _currentSize;
    // 太さ
    public int Thickness = 15;
    private int _currentThickness;
    // 色
    public Color Color = Color.white;
    private Color _currentColor;
    #endregion

    // 画像
    [SerializeField]
    private Image _inImage;

    // マスク画像
    [SerializeField]
    private Image _maskImage;
    [SerializeField]
    private Image _unmaskImage;

    // サイズ制御
    [SerializeField]
    private RectTransform _sizeRect;

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
        _currentSourceImage = SourceImage;
        _currentSize = Size;
        _currentThickness = Thickness;
        _currentColor = Color;
    }

    /// <summary>
    /// 現在のパラメータに変化があったかどうか
    /// </summary>
    private bool IsChangedValue() {
        return
            _currentSourceImage != SourceImage ||
            _currentSize != Size ||
            _currentThickness != Thickness ||
            _currentColor != Color;
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
        Size = Mathf.Max(Size, 0);
        Thickness = Mathf.Max(Thickness, 0);

        // 画像を設定
        _maskImage.sprite = SourceImage;
        _unmaskImage.sprite = SourceImage;
        _inImage.sprite = SourceImage;

        // 縁の大きさ設定
        _sizeRect.sizeDelta = new Vector2(Size, Size);

        // 太さ設定
        _unmaskImage.rectTransform.offsetMin = new Vector2(Thickness, Thickness); // new Vector2(left, bottom);
        _unmaskImage.rectTransform.offsetMax = new Vector2(-Thickness, -Thickness); // new Vector2(-right, -top);
        _inImage.rectTransform.offsetMin = new Vector2(-Thickness, -Thickness);
        _inImage.rectTransform.offsetMax = new Vector2(Thickness, Thickness);

        // 色の設定
        _inImage.color = Color;
    }
}