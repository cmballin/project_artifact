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
    /// Interaction logic for VisualKey.xaml
    /// </summary>
    public partial class VisualKey : UserControl
    {
        private bool isActivated;

        //private Color defaultBackgroundColor;
        //private Color defaultTextColor;

        //private Color currentBackgroundColor;
        //private Color currentTextColor;


        public VisualKey()
        {
            InitializeComponent();
        }

        public bool activated
        {
            get
            {
                return isActivated;
            }
            set
            {
                isActivated = value;
                activateButton();
            }
        }

        public String text
        {
            get
            {
                return keyText.Content.ToString();
            }
            set
            {
                keyText.Content = value;
            }
        }
        private void activateButton()
        {
            //if the button needs to be lit up
            if (isActivated == true)
            {
                keyBackground.Fill = Brushes.White;
                //black #CC000000
                 keyText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC000000"));
                return;
            }
            //if the button needs to go back into the foreground
            if (isActivated == false)
            {
                keyBackground.Fill = null;
                //white
                keyText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCFFFFFF"));
            }
            
        }

    }
}
