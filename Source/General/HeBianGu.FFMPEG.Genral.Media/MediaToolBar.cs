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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.FFMPEG.Genral.Media
{
    public class MediaToolBar : Control
    {
        static MediaToolBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MediaToolBar), new FrameworkPropertyMetadata(typeof(MediaToolBar)));
        }



        public MediaElement MediaElement
        {
            get { return (MediaElement)GetValue(MediaElementProperty); }
            set { SetValue(MediaElementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaElementProperty =
            DependencyProperty.Register("MediaElement", typeof(MediaElement), typeof(MediaToolBar), new PropertyMetadata(default(MediaElement), (d, e) =>
             {
                 MediaToolBar control = d as MediaToolBar;

                 if (control == null) return;

                 MediaElement config = e.NewValue as MediaElement;

             }));



        public MediaToolBar()
        {
            //  Message：播放
            CommandBinding commandBinding = new CommandBinding();
            commandBinding.Command = MediaCommands.Play;
            commandBinding.CanExecute += (l, k) =>
            {
                k.CanExecute = true;
                return;
            };
            commandBinding.Executed += (l, k) =>
            {
                 this.MediaElement.Play();
             };

            this.CommandBindings.Add(commandBinding);

            //  Message：暫停
            commandBinding = new CommandBinding();
            commandBinding.Command = MediaCommands.Pause;
            commandBinding.CanExecute += (l, k) =>
            {
                k.CanExecute = true;
                return;

                if (this.MediaElement == null)
                {
                    k.CanExecute = false;
                    return;
                }

                if (this.MediaElement.Clock == null)
                {
                    k.CanExecute = false;
                    return;
                }

                k.CanExecute = this.MediaElement.Clock.CurrentState == ClockState.Active;
            };
            commandBinding.Executed += (l, k) =>
            {
                this.MediaElement.Pause();
            };

            this.CommandBindings.Add(commandBinding);

            //  Message：停止
            commandBinding = new CommandBinding();
            commandBinding.Command = MediaCommands.Stop;
            commandBinding.CanExecute += (l, k) =>
            {
                k.CanExecute = true;
                return;

                if (this.MediaElement == null)
                {
                    k.CanExecute = false;
                    return;
                }

                if (this.MediaElement.Clock == null)
                {
                    k.CanExecute = false;
                    return;
                }

                k.CanExecute = this.MediaElement.Clock.CurrentState == ClockState.Active;
            };
            commandBinding.Executed += (l, k) =>
            {
                this.MediaElement.Stop();
            };

            this.CommandBindings.Add(commandBinding);


        }
    }
}
