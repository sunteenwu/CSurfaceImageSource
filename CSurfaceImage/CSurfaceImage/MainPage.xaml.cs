using MyImageSourceComponent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CSurfaceImage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        uint imageWidth;
        uint imageHeight;
        MyImageSource myImageSource;
        public MainPage()
        {
            this.InitializeComponent();
            imageWidth = (uint)this.MyImage.Width;
            imageHeight = (uint)this.MyImage.Height;
            myImageSource = new MyImageSource(imageWidth, imageHeight, true);
            this.MyImage.Source = myImageSource;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.FileTypeChoices.Add("Png file", new List<string>() { ".png" });
            savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite);
                myImageSource.SaveSurfaceImageToFile(stream);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Begin updating the surfaceImageSource
            myImageSource.BeginDraw();

            // Clear background
            myImageSource.Clear(Colors.Black);

            // Draw something...
            Rect rect;
            float startPointX = 0.0f;
            float startPointY = 0.0f;
            float deltaX = 3.0f;
            float deltaY = 3.0f;

            rect.X = startPointX;
            rect.Y = startPointY;
            rect.Width = (imageWidth - deltaX * 2) / 2.0f;
            rect.Height = (imageHeight - deltaY * 2) / 2.0f;
            myImageSource.FillSolidRect(Color.FromArgb(255, 250, 67, 5), rect);

            rect.X = startPointX + rect.Width + 2 * deltaX;
            myImageSource.FillSolidRect(Color.FromArgb(255, 96, 176, 6), rect);

            rect.X = startPointX;
            rect.Y = startPointY + rect.Height + 2 * deltaY;
            myImageSource.FillSolidRect(Color.FromArgb(255, 14, 179, 241), rect);

            rect.X = startPointX + rect.Width + 2 * deltaX;
            myImageSource.FillSolidRect(Color.FromArgb(255, 247, 199, 36), rect);

            // Stop updating the SurfaceImageSource and draw its contents 
            myImageSource.EndDraw();
        }


    }
}
