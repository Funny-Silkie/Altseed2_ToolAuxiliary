﻿using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// スライドをして<see cref="int"/>型の値を操作するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public abstract class SliderIntBase : ToolComponent
    {
        /// <summary>
        /// 設定可能な<see cref="Max"/>の最大値
        /// </summary>
        public const int RuledMaximum = int.MaxValue / 2;
        /// <summary>
        /// 設定可能な<see cref="Min"/>の最小値
        /// </summary>
        public const int RuledMinimum = int.MinValue / 2;
        /// <summary>
        /// 最大値を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が<see cref="Min"/>未満または<see cref="RuledMaximum"/>を超える</exception>
        public int Max
        {
            get => _max;
            set
            {
                if (_max == value) return;
                if (value < _min || RuledMaximum < value) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が範囲を逸脱しています\n許容される範囲：{_min}～{RuledMaximum}\n実際の値：{value}");
                _max = value;
            }
        }
        private int _max = RuledMaximum;
        /// <summary>
        /// 最小値を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が<see cref="Max"/>を超えるまたは<see cref="RuledMinimum"/>未満</exception>
        public int Min
        {
            get => _min;
            set
            {
                if (_min == value) return;
                if (value < RuledMinimum || _max < value) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が範囲を逸脱しています\n許容される範囲：{RuledMinimum}～{_max}\n実際の値：{value}");
                _min = value;
            }
        }
        private int _min = RuledMinimum;
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// <see cref="SliderIntBase"/>の新しいインスタンスを生成する
        /// </summary>
        protected SliderIntBase() { }
    }
}
