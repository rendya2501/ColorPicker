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

Xceed Toolkit  

wpfには標準で[ColorPicker]に相当するコントロールは存在しない模様。(WinFormにはある見たい)  
さすがに何かあるだろうと探したらxceedで作ってた。  

``` VB
Dim c As Color = oColorPicker.SelectedColor.Value
Dim DrawingColor = System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B)
Dim MediaColor = System.Windows.Media.Color.FromArgb(c.A, c.R, c.G, c.B)
Dim MediaColor As System.Windows.Media.Color = oColorPicker.SelectedColor.Value
```

---

## ソース

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Color
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// int入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Int_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                Execute(textBox.Text);
            }
        }
        /// <summary>
        /// int入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Int_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (e.Key == Key.Enter)
                {
                    Execute(textBox.Text);
                }
            }
        }

        private void Execute(string value)
        {
            if (!int.TryParse(value, out int argb))
            {
                MessageBox.Show("かた");
                return;
            }
            //IntからRGBに変換
            System.Drawing.Color c = System.Drawing.Color.FromArgb(argb);
            System.Windows.Media.Color color = System.Windows.Media.Color.FromRgb(c.R, c.G, c.B);

            _Border.Background = new SolidColorBrush(color);
            _R.Text = color.R.ToString();
            _G.Text = color.G.ToString();
            _B.Text = color.B.ToString();
            _A.Text = color.A.ToString();
            // カラーピッカー
            _ColorPoicker.SelectedColor = color;
            // 16進数
            _Hexadecimal.Text = "#" + c.Name.ToUpper();
            // X2:16進数2桁表示
            _HTML.Text = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private void _ColorPoicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            System.Windows.Media.Color c = e.NewValue.Value;
            System.Drawing.Color color = System.Drawing.Color.FromArgb(c.R, c.G, c.B);

            _Border.Background = new SolidColorBrush(c);
            _R.Text = color.R.ToString();
            _G.Text = color.G.ToString();
            _B.Text = color.B.ToString();
            _A.Text = color.A.ToString();
            // 32BitARGB
            _Int.Text = color.ToArgb().ToString("D");
            // 16進数
            _Hexadecimal.Text = "#" + color.Name.ToUpper();
            // X2:16進数2桁表示
            _HTML.Text = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        // RGB to 16
        // Argb to 16
        // 16 to RGM
        // 16 to Argb

        // https://it-h.net/m/14
        // https://www.osadasoft.com/%E3%82%AB%E3%83%A9%E3%83%BC%E3%82%B3%E3%83%BC%E3%83%89%E3%82%9216%E9%80%B2%E6%95%B0%E8%A1%A8%E7%A4%BA%E3%81%99%E3%82%8B/
    }
}
```

``` XML
<Window
    x:Class="Color.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:local="clr-namespace:Color"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:xctk="http://schemas.xceed.com/wpf/xaml/toolkit"
    Title="MainWindow"
    Width="800"
    Height="450"
    mc:Ignorable="d">
    <Grid>
        <xctk:ColorPicker
            x:Name="_ColorPoicker"
            Width="193"
            Height="29"
            Margin="60,173,539,217"
            SelectedColorChanged="_ColorPoicker_SelectedColorChanged" />
        <TextBox
            Name="_Int"
            Width="193"
            Height="29"
            Margin="60,37,0,0"
            HorizontalAlignment="Left"
            VerticalAlignment="Top"
            KeyDown="_Int_KeyDown"
            LostFocus="_Int_LostFocus"
            Text=""
            TextWrapping="Wrap" />
        <TextBox
            Name="_R"
            Width="35"
            Height="29"
            Margin="60,139,0,0"
            HorizontalAlignment="Left"
            VerticalAlignment="Top"
            Text="R"
            TextWrapping="Wrap" />
        <Border
            Name="_Border"
            Width="136"
            Height="166"
            Margin="258,37,0,0"
            HorizontalAlignment="Left"
            VerticalAlignment="Top"
            BorderBrush="Black"
            BorderThickness="1" />
        <TextBox
            x:Name="_G"
            Width="35"
            Height="29"
            Margin="112,139,0,0"
            HorizontalAlignment="Left"
            VerticalAlignment="Top"
            Text="G"
            TextWrapping="Wrap" />
        <TextBox
            x:Name="_B"
            Width="35"
            Height="29"
            Margin="166,139,0,0"
            HorizontalAlignment="Left"
            VerticalAlignment="Top"
            Text="B"
            TextWrapping="Wrap" />
        <TextBox
            x:Name="_A"
            Width="35"
            Height="29"
            Margin="218,139,0,0"
            HorizontalAlignment="Left"
            VerticalAlignment="Top"
            Text="A"
            TextWrapping="Wrap" />
        <TextBox
            x:Name="_Hexadecimal"
            Width="193"
            Height="29"
            Margin="60,71,0,0"
            HorizontalAlignment="Left"
            VerticalAlignment="Top"
            KeyDown="_Int_KeyDown"
            LostFocus="_Int_LostFocus"
            Text=""
            TextWrapping="Wrap" />
        <TextBox
            x:Name="_HTML"
            Width="193"
            Height="29"
            Margin="60,105,0,0"
            HorizontalAlignment="Left"
            VerticalAlignment="Top"
            KeyDown="_Int_KeyDown"
            LostFocus="_Int_LostFocus"
            Text=""
            TextWrapping="Wrap" />
    </Grid>
</Window>
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

---

## 嵌ったところ

ValidationAnnotationを使いたい場合`ObservableValidator`を継承する。  
こいつは`ObservableObject`を継承しているので問題なく`ObservableProperty`を使える。  

intのValidationAnnotationのMax,Minは使えない。  
代わりにRangeを使う。  

<https://twitter.com/SergioPedri/status/1377228872979263493/photo/1>
