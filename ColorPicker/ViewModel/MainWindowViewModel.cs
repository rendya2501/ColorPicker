using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataAnnotationsExtensions;


namespace ColorPicker.ViewModel
{
    public partial class MainWindowViewModel : ObservableValidator
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(R))]
        [NotifyPropertyChangedFor(nameof(G))]
        [NotifyPropertyChangedFor(nameof(B))]
        [NotifyPropertyChangedFor(nameof(A))]
        [NotifyPropertyChangedFor(nameof(BorderBackground))]
        public Color? color;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Color))]
        [NotifyDataErrorInfo]
        [Range(0, 255)]
        public int r;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Color))]
        [NotifyDataErrorInfo]
        [Range(0, 255)]
        public int g;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Color))]
        [NotifyDataErrorInfo]
        [Range(0, 255)]
        public int b;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Color))]
        [NotifyDataErrorInfo]
        [Range(0, 255)]
        public int a;

        [ObservableProperty]
        public int int_32BitARGB;

        [ObservableProperty]
        public string? hexadecimal;

        [ObservableProperty]
        public string? html;

        [ObservableProperty]
        public Brush borderBackground;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel() { }


        partial void OnGChanged(int value)
        {
            _ = 1;
        }

        partial void OnColorChanged(Color? value)
        {
            System.Windows.Media.Color mediaColor = value.Value;
            System.Drawing.Color drawingColor = System.Drawing.Color.FromArgb(mediaColor.R, mediaColor.G, mediaColor.B);

            // 
            borderBackground = new SolidColorBrush(mediaColor);
            // RGBA
            r = mediaColor.R;
            g = mediaColor.G;
            b = mediaColor.B;
            a = mediaColor.A;
            // 32BitARGB
            Int_32BitARGB = drawingColor.ToArgb();
            // 16進数
            Hexadecimal = "#" + drawingColor.Name.ToUpper();
            // X2:16進数2桁表示
            Html = "#" + mediaColor.R.ToString("X2") + mediaColor.G.ToString("X2") + mediaColor.B.ToString("X2");
        }
    }




    ///// <summary>
    ///// int入力
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //private void _Int_LostFocus(object sender, RoutedEventArgs e)
    //{
    //    if (sender is TextBox textBox)
    //    {
    //        Execute(textBox.Text);
    //    }
    //}
    ///// <summary>
    ///// int入力
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //private void _Int_KeyDown(object sender, KeyEventArgs e)
    //{
    //    if (sender is TextBox textBox)
    //    {
    //        if (e.Key == Key.Enter)
    //        {
    //            Execute(textBox.Text);
    //        }
    //    }
    //}

    //private void Execute(string value)
    //{
    //    if (!int.TryParse(value, out int argb))
    //    {
    //        MessageBox.Show("かた");
    //        return;
    //    }
    //    //IntからRGBに変換
    //    System.Drawing.Color c = System.Drawing.Color.FromArgb(argb);
    //    System.Windows.Media.Color color = System.Windows.Media.Color.FromRgb(c.R, c.G, c.B);

    //    _Border.Background = new SolidColorBrush(color);
    //    _R.Text = color.R.ToString();
    //    _G.Text = color.G.ToString();
    //    _B.Text = color.B.ToString();
    //    _A.Text = color.A.ToString();
    //    // カラーピッカー
    //    _ColorPoicker.SelectedColor = color;
    //    // 16進数
    //    _Hexadecimal.Text = "#" + c.Name.ToUpper();
    //    // X2:16進数2桁表示
    //    _HTML.Text = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
    //}

    //private void _ColorPoicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
    //{
    //    System.Windows.Media.Color c = e.NewValue.Value;
    //    System.Drawing.Color color = System.Drawing.Color.FromArgb(c.R, c.G, c.B);

    //    _Border.Background = new SolidColorBrush(c);
    //    _R.Text = color.R.ToString();
    //    _G.Text = color.G.ToString();
    //    _B.Text = color.B.ToString();
    //    _A.Text = color.A.ToString();
    //    // 32BitARGB
    //    _Int.Text = color.ToArgb().ToString("D");
    //    // 16進数
    //    _Hexadecimal.Text = "#" + color.Name.ToUpper();
    //    // X2:16進数2桁表示
    //    _HTML.Text = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
    //}

    // RGB to 16
    // Argb to 16
    // 16 to RGM
    // 16 to Argb

    // https://it-h.net/m/14
    // https://www.osadasoft.com/%E3%82%AB%E3%83%A9%E3%83%BC%E3%82%B3%E3%83%BC%E3%83%89%E3%82%9216%E9%80%B2%E6%95%B0%E8%A1%A8%E7%A4%BA%E3%81%99%E3%82%8B/
}
