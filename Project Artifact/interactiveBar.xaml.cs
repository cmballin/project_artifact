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
    /// <summary>
    /// Interaction logic for interactiveBar.xaml
    /// </summary>
    public partial class interactiveBar : UserControl
    {
        private double internalPercentage;

        private bool internalActivated;
        private bool isHealthBar;

        private Color internalBarColorEmergency  = (Color)ColorConverter.ConvertFromString("#FFFF3737");
        private Color internalBarColorLow        = (Color)ColorConverter.ConvertFromString("#FFFFCF7A");
        private Color internalBarColorHigh       = (Color)ColorConverter.ConvertFromString("#FF7AFF92");

        private string internalLabel;

        private Color highlightColor = Colors.White;

        public interactiveBar()
        {
            InitializeComponent();
        }

        public double percentage
        {
            get
            {
                return internalPercentage;
            }
            set
            {
                internalPercentage = value;
                updateBar();
            }
        }
        
        public Color barColorLow
        {
            get
            {
                return internalBarColorLow;
            }
            set
            {
                internalBarColorLow = value;
                mainBar.Fill = new SolidColorBrush(internalBarColorLow);
            }
        }

        public Color barColorHigh
        {
            get
            {
                return internalBarColorHigh;
            }
            set
            {
                internalBarColorHigh = value;
                mainBar.Fill = new SolidColorBrush(internalBarColorHigh);
            }
        }

        public Color barColorEmergency
        {
            get
            {
                return internalBarColorEmergency;
            }
            set
            {
                internalBarColorEmergency = value;
                mainBar.Fill = new SolidColorBrush(internalBarColorEmergency);
            }
        }

        public bool healthBar
        {
            get
            {
                return isHealthBar;
            }
            set
            {
                isHealthBar = value;
            }
        }

        public bool activated
        {
            get
            {
                return internalActivated;
            }
            set
            {
                internalActivated = value;

                if (internalActivated == true)
                {
                    mainBar.Effect = new System.Windows.Media.Effects.DropShadowEffect
                    {
                        BlurRadius = 30,
                        Color = highlightColor,
                        Direction = 360,
                        ShadowDepth = 0,
                        Opacity = 1
                    };
                }

                if(internalActivated == false)
                {
                    mainBar.Effect = null;
                }
               
            }
        }

        public string barLabel
        {
            get
            {
                return internalLabel;
            }
            set
            {
                label.Text = value;
                internalLabel = value;
            }
        }

        private void updateBar()
        {
            barContainer.ColumnDefinitions[0].Width = new GridLength(internalPercentage, GridUnitType.Star);                //sets the length of the bar
            barContainer.ColumnDefinitions[1].Width = new GridLength(100 - internalPercentage, GridUnitType.Star);

            if (internalPercentage <= 15)
            {
                barColorEmergency = internalBarColorEmergency;   //default red color
                return;
            }

            if (internalPercentage <= 35)
            {                
                barColorLow = internalBarColorLow;          //default orange color   
                return;
            }
  
            barColorHigh = internalBarColorHigh;            //default green color

            if (isHealthBar == true)
                return;

            if(internalPercentage >= 98)
            {
                activated = true;
                return;
            }

            activated = false;

        }






    }
}
