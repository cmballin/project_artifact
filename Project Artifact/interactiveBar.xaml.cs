using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        private Storyboard barStoryboard;


        private GridLengthAnimation barGridLengthAnimation;
        private GridLengthAnimation companionGridLengthAnimation;


        private DateTime endingTime = new DateTime();

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
            Loaded += InteractiveBar_Loaded;


        }

        private void InteractiveBar_Loaded(object sender, RoutedEventArgs e)
        {
            barStoryboard = new Storyboard();
            

            if (isHealthBar == false)
                start(0, 100, TimeSpan.FromSeconds(6));                     //temporary so we can see them running.
         

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
            if (internalPercentage >= 100)
                internalPercentage = 100;

          
             // barContainer.ColumnDefinitions[0].Width = new GridLength( internalPercentage, GridUnitType.Star);                //sets the length of the bar
             // barContainer.ColumnDefinitions[1].Width = new GridLength(100 - internalPercentage, GridUnitType.Star);

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

        public void start( double startPercentage, double endPercentage, TimeSpan time )              //uses start and end so that I can get a reverse in the bar. without a reverse property
        {
            endingTime = DateTime.Now + time;
            percentage = startPercentage;                                   //restarts the percentage 



            barGridLengthAnimation = new GridLengthAnimation();                                  // controls the left side of the bar i.e. the colored part
            barGridLengthAnimation.From = new GridLength (startPercentage, GridUnitType.Star);
            barGridLengthAnimation.To = new GridLength(endPercentage, GridUnitType.Star);
            barGridLengthAnimation.Duration = time;


            companionGridLengthAnimation = new GridLengthAnimation();                                // controls the right side of the bar i.e. the unfilled part
            companionGridLengthAnimation.From = new GridLength(100 - startPercentage, GridUnitType.Star);
            companionGridLengthAnimation.To = new GridLength(100 - endPercentage, GridUnitType.Star);
            companionGridLengthAnimation.Duration = time;

            //  barGridLengthAnimation.Completed += BarGridLengthAnimation_Completed;
            barGridLengthAnimation.changeEvent += BarGridLengthAnimation_changeEvent;

            barColumnOne.BeginAnimation(ColumnDefinition.WidthProperty, barGridLengthAnimation);
            barColumnTwo.BeginAnimation(ColumnDefinition.WidthProperty, companionGridLengthAnimation);

            
            //barStoryboard.Children.Add(barGridLengthAnimation);
            //Storyboard.SetTargetName(barGridLengthAnimation, barColumnOne.Name);
            //Storyboard.SetTargetProperty(barGridLengthAnimation, new PropertyPath(ColumnDefinition.WidthProperty));


            //barStoryboard.Children.Add(companionGridLengthAnimation);
            //Storyboard.SetTargetName(companionGridLengthAnimation, barColumnTwo.Name);
            //Storyboard.SetTargetProperty(companionGridLengthAnimation, new PropertyPath(ColumnDefinition.WidthProperty));



            //barStoryboard.Begin(this);
                                     

        }

        private void BarGridLengthAnimation_changeEvent(double percentage)
        {
            internalPercentage = percentage;
            updateBar();
        }

        private void BarGridLengthAnimation_Completed(object sender, EventArgs e)
        {
            activated = true;   //this actually needs to be time based so that we can have it trigger before the animation is totally complete
               
        }
    }
}
