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

namespace Project_Artifact
{
    class MaskImage:Image
    {
        private Size demensions;
        private ImageSource iMask;
        private ImageSource iImage;

       


        protected override void OnRender(DrawingContext dc)
        {
            // base.OnRender(dc);

            int picMaxWidth = (int)demensions.Width;
            int picMaxHeight = (int)demensions.Height;

           // if (picMaxWidth < 100) picMaxWidth = 100;
           // if (picMaxHeight < 100) picMaxHeight = 100;

            //if there is a parent. if flowing right to left do something

            Rect demensionRect = new Rect(0, 0, picMaxWidth, picMaxHeight);
           
            ImageBrush opacityBrush = new ImageBrush(iMask);
            opacityBrush.Stretch = Stretch.Uniform;
          
            dc.PushOpacityMask(opacityBrush);

            ImageBrush imgBrush = new ImageBrush(image);
            imgBrush.Stretch = Stretch.UniformToFill;

            dc.DrawRectangle(imgBrush, null, demensionRect);

            dc.DrawRectangle(imgBrush, null, demensionRect);

            
            dc.Pop();
           
          
          

        }
        protected override Size MeasureOverride(Size constraint)
        {
            demensions = constraint;
            return base.MeasureOverride(constraint);
        }

        public ImageSource mask
        {
            get
            {
                return iMask;
            }
            set
            {
                iMask = value;
                Source = value;
            }
        }

        public ImageSource image
        {
            get
            {
                return iImage;
            }
            set
            {
                iImage = value;
            }
        }



    }
}
