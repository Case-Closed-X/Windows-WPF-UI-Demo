using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI_Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
            int uAction,
            int uParam,
            string lpvParam,
            int fuWinIni
            );

        public static DoubleAnimation DoubleAnimate(UIElement obj, string property, double to)
        {
            DoubleAnimation anim = new()
            {
                To = to,
                Duration = TimeSpan.FromSeconds(0.6),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut },
                FillBehavior = FillBehavior.HoldEnd,
            };
            Storyboard.SetTarget(anim, obj);
            Storyboard.SetTargetProperty(anim, new PropertyPath(property));
            return anim;
        }
        
        public MainWindow()
        {
            InitializeComponent();

            border.MouseEnter += (s, e) => MouseEnterAnimate(border);
            border.MouseLeave += (s, e) => MouseLeaveAnimate(border);

            buttonSetWallpaper.Click += delegate
            {
                SystemParametersInfo(20, 1, $@"{AppDomain.CurrentDomain.BaseDirectory}/Test.jpg", 1);
            };

            buttonList.Click += ButtonList_Click;

            toggleButtonTheme.Click += (s, e) =>
            {
                if (toggleButtonTheme.IsChecked == true)
                {
                    grid.Background = new SolidColorBrush(Colors.Wheat);
                }
                else
                {
                    grid.Background = new SolidColorBrush(Colors.AliceBlue);
                }
            };
        }

        private void ButtonList_Click(object sender, RoutedEventArgs e)
        {
            new ListDemoWindow().Show();
        }

        private void MouseEnterAnimate(Border border)
        {
            Storyboard storyboard = new();

            storyboard.Children.Add(DoubleAnimate(border, "Height", this.ActualHeight));
            storyboard.Children.Add(DoubleAnimate(border, "Width", this.ActualWidth));

            var anim = new ObjectAnimationUsingKeyFrames();
            var currentCornerRadius = border.CornerRadius.TopLeft;
            for (double i = 0; i <= 40; i++)
            {
                anim.KeyFrames.Add(new DiscreteObjectKeyFrame { Value = new CornerRadius(currentCornerRadius - i), KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(15 * i)) });
            }
            Storyboard.SetTarget(anim, border);
            Storyboard.SetTargetProperty(anim, new PropertyPath("CornerRadius"));
            storyboard.Children.Add(anim);

            ThicknessAnimation marginAnimation = new()
            {
                To = new Thickness(0, 0, 0, 0),
                Duration = TimeSpan.FromSeconds(0.6),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut },
                FillBehavior = FillBehavior.HoldEnd,
            };

            border.BeginAnimation(MarginProperty, marginAnimation);

            storyboard.Begin();
        }

        private void MouseLeaveAnimate(Border border)
        {
            Storyboard storyboard = new();

            storyboard.Children.Add(DoubleAnimate(border, "Height", 210));
            storyboard.Children.Add(DoubleAnimate(border, "Width", 360));

            var anim = new ObjectAnimationUsingKeyFrames();
            var currentCornerRadius = border.CornerRadius.TopLeft;
            for (double i = currentCornerRadius; i <= 40; i++)
            {
                anim.KeyFrames.Add(new DiscreteObjectKeyFrame { Value = new CornerRadius(i), KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(15 * i)) });
            }
            Storyboard.SetTarget(anim, border);
            Storyboard.SetTargetProperty(anim, new PropertyPath("CornerRadius"));
            storyboard.Children.Add(anim);

            ThicknessAnimation marginAnimation = new()
            {
                To = new Thickness(0, 20, 0, 0),
                Duration = TimeSpan.FromSeconds(0.6),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut },
                FillBehavior = FillBehavior.HoldEnd,
            };

            border.BeginAnimation(MarginProperty, marginAnimation);

            storyboard.Begin();
        }
    }
}
