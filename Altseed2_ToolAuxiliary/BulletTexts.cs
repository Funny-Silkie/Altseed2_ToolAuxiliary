using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 列挙されたテキストのツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class BulletTexts : ToolComponent
    {
        private readonly static string[] defaultArray = new[] { "Bullet1", "Bullet2" };
        /// <summary>
        /// 描画される文字列を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentNullException">設定しようとした値がnull</exception>
        public string[] Texts
        {
            get => _texts;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value), "引数がnullです");
                if (_texts == value) return;
                _texts = value;
            }
        }
        private string[] _texts;
        /// <summary>
        /// 既定の文字列を持つ<see cref="BulletTexts"/>の新しいインスタンスを生成する
        /// </summary>
        public BulletTexts() : this(defaultArray) { }
        /// <summary>
        /// 指定した文字列を持つ<see cref="BulletTexts"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="texts">描画する文字列</param>
        /// <exception cref="ArgumentNullException"><paramref name="texts"/>がnull</exception>
        public BulletTexts(string[] texts)
        {
            _texts = texts ?? throw new ArgumentNullException(nameof(texts), "引数がnullです");
        }
        internal override void Update()
        {
            for (int i = 0; i < _texts.Length; i++) Engine.Tool.BulletText(_texts[i]);
        }
    }
}
