using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// テクスチャボタンのクラス
    /// </summary>
    [Serializable]
    public class ImageButton : ButtonBase
    {
        private Vector2F uv0;
        private Vector2F uv1;
        /// <summary>
        /// 色を取得または設定する
        /// </summary>
        public Color Color { get; set; } = new Color(255, 255, 255);
        /// <summary>
        /// 縁の太さを取得または設定する
        /// </summary>
        public int FrameSize { get; set; }
        /// <summary>
        /// 拡大率を取得または設定する
        /// </summary>
        public Vector2F Scale { get; set; } = new Vector2F(1f, 1f);
        /// <summary>
        /// サイズを取得または設定する
        /// </summary>
        public Vector2F Size
        {
            get => _src.Size * Scale;
            set
            {
                var size = _src.Size;
                Scale = size.X == 0 || size.Y == 0 ? default : value / size;
            }
        }
        /// <summary>
        /// テクスチャの描画範囲を取得または設定する
        /// </summary>
        public RectF Src
        {
            get => _src;
            set
            {
                if (_src == value) return;
                _src = value;
                var size = _texture?.Size ?? new Vector2F(1f, 1f);
                if (size.X == 0 || size.Y == 0)
                {
                    uv0 = default;
                    uv1 = new Vector2F(1f, 1f);
                }
                else
                {
                    uv0 = value.Position / size;
                    uv1 = uv0 + value.Size / size;
                }
                Size = value.Size;
            }
        }
        private RectF _src = new RectF(default, new Vector2F(1f, 1f));
        /// <summary>
        /// 描画するテクスチャを取得または設定する
        /// </summary>
        public TextureBase Texture
        {
            get => _texture;
            set
            {
                if (_texture == value) return;
                _texture = value;
                Src = new RectF(default, value?.Size ?? default);
            }
        }
        private TextureBase _texture;
        /// <summary>
        /// <see cref="ImageButton"/>の新しいインスタンスを生成する
        /// </summary>
        public ImageButton() { }
        internal override void Update()
        {
            if (Engine.Tool.ImageButton(Texture, Size, uv0, uv1, FrameSize, Color, Color)) OnClicked(EventArgs.Empty);
        }
    }
}
