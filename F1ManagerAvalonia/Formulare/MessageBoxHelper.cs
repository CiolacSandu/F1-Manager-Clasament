using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;

namespace F1ManagerAvalonia.Formulare
{
    public static class MessageBoxHelper
    {
        public static async Task ShowInfo(string message, string title = "Info")
        {
            var dialog = new Window
            {
                Title = title,
                Width = 400,
                Height = 200,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                CanResize = false,
                Background = new SolidColorBrush(Color.FromRgb(30, 30, 46))
            };

            var stackPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Spacing = 15
            };

            stackPanel.Children.Add(new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                FontSize = 16,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                MaxWidth = 350
            });

            var okButton = new Button
            {
                Content = "OK",
                Width = 100,
                Height = 35,
                Background = new SolidColorBrush(Color.FromRgb(225, 6, 0)),
                Foreground = Brushes.White,
                FontSize = 14,
                FontWeight = FontWeight.Bold,
                BorderThickness = new Thickness(0),
                Cursor = new Cursor(StandardCursorType.Hand)
            };

            okButton.Click += async (_, __) => dialog.Close();

            stackPanel.Children.Add(okButton);

            var border = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(30, 30, 46)),
                Child = stackPanel
            };

            dialog.Content = border;

            var lifetime = Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;

            if (lifetime != null && lifetime.MainWindow != null)
            {
                await dialog.ShowDialog(lifetime.MainWindow);
            }
            else
            {
                dialog.Show();
            }
        }

        public static async Task<bool> ShowConfirm(string message, string title = "Confirmare")
        {
            var tcs = new TaskCompletionSource<bool>();

            var dialog = new Window
            {
                Title = title,
                Width = 400,
                Height = 200,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                CanResize = false,
                Background = new SolidColorBrush(Color.FromRgb(30, 30, 46))
            };

            var stackPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Spacing = 15
            };

            stackPanel.Children.Add(new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                FontSize = 16,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                MaxWidth = 350
            });

            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Spacing = 15
            };

            var daButton = new Button
            {
                Content = "Da",
                Width = 100,
                Height = 35,
                Background = new SolidColorBrush(Color.FromRgb(225, 6, 0)),
                Foreground = Brushes.White,
                FontSize = 14,
                FontWeight = FontWeight.Bold,
                BorderThickness = new Thickness(0),
                Cursor = new Cursor(StandardCursorType.Hand)
            };

            daButton.Click += (_, __) =>
            {
                tcs.TrySetResult(true);
                dialog.Close();
            };

            var nuButton = new Button
            {
                Content = "Nu",
                Width = 100,
                Height = 35,
                Background = new SolidColorBrush(Color.FromRgb(60, 60, 60)),
                Foreground = Brushes.White,
                FontSize = 14,
                FontWeight = FontWeight.Bold,
                BorderThickness = new Thickness(0),
                Cursor = new Cursor(StandardCursorType.Hand)
            };

            nuButton.Click += (_, __) =>
            {
                tcs.TrySetResult(false);
                dialog.Close();
            };

            buttonPanel.Children.Add(daButton);
            buttonPanel.Children.Add(nuButton);
            stackPanel.Children.Add(buttonPanel);

            var border = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(30, 30, 46)),
                Child = stackPanel
            };

            dialog.Content = border;

            var lifetime = Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;

            if (lifetime != null && lifetime.MainWindow != null)
            {
                await dialog.ShowDialog(lifetime.MainWindow);
            }
            else
            {
                dialog.Show();
            }

            return await tcs.Task;
        }
    }
}