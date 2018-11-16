using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.FFMPEG.Genral.Media
{
    public class MediaPlayer : Control
    {
        static MediaPlayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MediaPlayer), new FrameworkPropertyMetadata(typeof(MediaPlayer)));
        }


        public Uri MediaSource
        {
            get { return (Uri)GetValue(MediaSourceProperty); }
            set { SetValue(MediaSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaSourceProperty =
            DependencyProperty.Register("MediaSource", typeof(Uri), typeof(MediaPlayer), new PropertyMetadata(default(Uri), (d, e) =>
            {
                MediaPlayer control = d as MediaPlayer;

                if (control == null) return;

                Uri config = e.NewValue as Uri;

            }));



    }
}
