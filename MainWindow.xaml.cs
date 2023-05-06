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
using System.IO;
using Microsoft.Toolkit.Uwp.Notifications;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {

        public bool saved = false;
        public bool isStrikedThrough = false;
        public bool isUnderLined = false;
        public bool isOverLined = false;
        public bool isItalic = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            var notifySaved = new ToastContentBuilder();
            notifySaved.AddAppLogoOverride(new Uri(@"E:\Text Editor\WpfApp1\WpfApp1\save.png"));

            string path = $@"{(path_element.Text).ToString()}";
            notifySaved.AddText($"Файл успешно сохранён в {path}");
            try
            {
                File.WriteAllText(path, (main_text.Text).ToString());
                this.saved = true;
                notifySaved.Show();
                this.Title = $"{path} - Текстовый редактор";
            }
            catch { MessageBox.Show($"Не найдено такого пути: {path}"); }
        }

        private void ChangeFont(object sender, RoutedEventArgs e)
        {
            string font = (string)(((MenuItem)e.OriginalSource).Header);
            main_text.FontFamily = new FontFamily(font);
        }

        private void SizeTo(object sender, RoutedEventArgs e)
        {
            uint size;
            if (sender is TextBox)
            {
                try
                {
                    size = uint.Parse((((TextBox)e.OriginalSource).Text).ToString());
                    main_text.FontSize = size;
                }
                catch
                {
                    main_text.FontSize = 14;
                }
            }
            else if (sender is MenuItem)
            {
                size = uint.Parse((((MenuItem)e.OriginalSource).Header).ToString());
                main_text.FontSize = size;
            }
        }

        private void ReplaceTo(object sender, RoutedEventArgs e)
        {
            string text = ((main_text.Text).ToLower()).ToString();
            string search = ((searching_text.Text).ToLower()).ToString();
            string replace = (replace_text.Text).ToString();
            int idx = text.IndexOf(search);
            string newText = (main_text.Text).ToString();
            if (!string.IsNullOrEmpty(replace))
            {
                newText = newText.Remove(idx, search.Length);
                newText = newText.Insert(idx, replace);
                main_text.Text = newText;
            }
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            string text = ((main_text.Text).ToLower()).ToString();
            string search = ((searching_text.Text).ToLower()).ToString();
            int idx = text.IndexOf(search);
            try { main_text.Select(idx, search.Length); }
            catch { }
        }

        private void HandleText(object sender, RoutedEventArgs e)
        {
            string text = (main_text.Text).ToString();
            int wordsAmount = 0;
            int linesAmount = 0;
            string path = $@"{(path_element.Text).ToString()}";
            string[] words_arr = text.Split(' ');
            if (this.saved == false && text != "")
            {
                this.Title = "*Новый текстовый файл";
            }
            foreach (string word in words_arr)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    wordsAmount++;
                }
            }
            linesAmount = main_text.LineCount;
            words_element.Header = $"Количество слов: {wordsAmount}";
            lines_element.Header = $"Количество строк: {linesAmount}";
            try
            {
                string fileText = File.ReadAllText(path);
                if ((main_text.Text).ToString() != fileText)
                {
                    this.Title = $"*{path} - Текстовый редактор";
                }
                if ((main_text.Text).ToString() == fileText)
                {
                    this.Title = $"{path} - Текстовый редактор";
                }
            }
            catch { }
        }

        public void StrikeThrough(object sender, RoutedEventArgs e)
        {
            if (!this.isStrikedThrough)
            {
                main_text.TextDecorations = TextDecorations.Strikethrough;
                this.isStrikedThrough = true;
            }
            else
            {
                main_text.TextDecorations = null;
                this.isStrikedThrough = false;
            }
        }

        public void Underline(object sender, RoutedEventArgs e)
        {
            if (!this.isUnderLined)
            {
                main_text.TextDecorations = TextDecorations.Underline;
                this.isUnderLined = true;
            }
            else
            {
                main_text.TextDecorations = null;
                this.isUnderLined = false;
            }
        }

        public void OverLine(object sender, RoutedEventArgs e)
        {
            if (!this.isOverLined)
            {
                main_text.TextDecorations = TextDecorations.OverLine;
                this.isOverLined = true;
            }
            else
            {
                main_text.TextDecorations = null;
                this.isOverLined = false;
            }
        }

        public void MakeItalics(object sender, RoutedEventArgs e)
        {
            if (!this.isItalic)
            {
                main_text.FontStyle = FontStyles.Italic;
                this.isItalic = true;
            }
            else
            {
                main_text.FontStyle = FontStyles.Normal;
            }
        }

        public void ChangeColorToRed(object sender, RoutedEventArgs e)
        {
            main_text.Foreground = Brushes.Red;
            color_element.Foreground = Brushes.Red;
        }

        public void ChangeColorToGreen(object sender, RoutedEventArgs e)
        {
            main_text.Foreground = Brushes.Green;
            color_element.Foreground = Brushes.Green;
        }

        public void ChangeColorToBlue(object sender, RoutedEventArgs e)
        {
            main_text.Foreground = Brushes.Blue;
            color_element.Foreground = Brushes.Blue;
        }

        public void ChangeColorToDefault(object sender, RoutedEventArgs e)
        {
            main_text.Foreground = Brushes.Black;
            color_element.Foreground = Brushes.Black;
        }

    }
}