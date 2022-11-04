# 色変換ツール

---

## 概要

任意の整数値を色に変換するためのお手軽なツールがなかったので、実務で作りたいなーと思いながら作ってたら変換までできたので、気が済むところまで作ることにした。  

とりあえず変換できるだけで十分だったのでエラー処理とかまでは作っていない。  
後でちゃんとまとめる。  

[【VB.NET】【WPF】Extended WPF Toolkit ColorPicker の使い方](https://elleneast.com/?p=12894#ColorPicker_xaml)  
[WPFでColorPickerを作った話(前編)](https://recruit.cct-inc.co.jp/tecblog/csharp/wpf-colorpicker/)  
ここの更新も楽しみである。  

---

## nugetからインストールするパッケージ  

- System.Drawing.Color
- Extended WPF Toolkit

wpfには標準で[ColorPicker]に相当するコントロールは存在しない模様。(WinFormにはある見たい)  
さすがに何かあるだろうと探したらxceedで作ってた。  

``` VB
Dim c As Color = oColorPicker.SelectedColor.Value
Dim DrawingColor = System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B)
Dim MediaColor = System.Windows.Media.Color.FromArgb(c.A, c.R, c.G, c.B)
Dim MediaColor As System.Windows.Media.Color = oColorPicker.SelectedColor.Value
```

---

## 32BitArgbの変換の参考にしたコンバーター

``` C# : 32BitArgbの変換の参考にしたコンバーター
using System;
using System.Globalization;
using System.Windows.Data;

    /// <summary>
    /// 色コンバータ
    /// </summary>
    public class BrushConverter : IValueConverter
    {
        /// <summary>
        /// デフォルトカラー
        /// </summary>
        public System.Drawing.Color DefaultColor { get; set; }

        /// <summary>
        /// IntからColorに変換する
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                if (DefaultColor != null)
                {
                    var dc = DefaultColor;
                    return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(dc.A, dc.R, dc.G, dc.B));
                }
                return null;
            }
            if (!(value is int)) throw new Exception(string.Format(Message.Invalid, "型"));
            //IntからRGBに変換
            var c = System.Drawing.Color.FromArgb((int)value);

            return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(c.R, c.G, c.B));
        }

        /// <summary>
        /// ColorからIntに変換する
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (!(value is System.Windows.Media.SolidColorBrush)) throw new Exception(string.Format(Message.Invalid, "型"));

            var valueColor = ((System.Windows.Media.SolidColorBrush)value).Color;
            var c = System.Drawing.Color.FromArgb(valueColor.R, valueColor.G, valueColor.B);
            return c.ToArgb();
        }
    }
```
