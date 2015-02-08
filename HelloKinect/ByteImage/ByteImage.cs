using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace HelloKinect.ByteImage
{
    [TemplatePart(Name = "PART_image", Type = typeof(Image))]
    public class ByteImage : Control
    {
        private Image image;
        private WriteableBitmap colourBitmap;

        public ByteImage()
        {
            DefaultStyleKey = typeof (ByteImage);
        }

        public static DependencyProperty ByteSourceProperty 
            = DependencyProperty.Register("ByteSource", typeof (byte[]), typeof (ByteImage), new PropertyMetadata(new byte[0], SourceUpdated));

        public byte[] ByteSource
        {
            get
            {
                return (byte[]) GetValue(ByteSourceProperty);
            }
            set
            {
                SetValue(ByteSourceProperty, value);
            }
        }

        protected override void OnApplyTemplate()
        {
            this.image = (Image) GetTemplateChild("PART_image");
            colourBitmap = new WriteableBitmap(1920, 1080);
            this.image.Source = colourBitmap;
        }

        private static async void SourceUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ByteImage instance = (ByteImage) d;

            if (((byte[])e.OldValue).Length > 0 || e.OldValue == e.NewValue || instance.image == null)
            {
                return;
            }

            await instance.CreateSource();
        }

        private async Task CreateSource()
        {
            using (Stream stream = this.colourBitmap.PixelBuffer.AsStream())
            {
                await stream.WriteAsync(ByteSource, 0, ByteSource.Length - 1);
            }
        }
    }
}
