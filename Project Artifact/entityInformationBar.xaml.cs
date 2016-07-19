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
    /// Interaction logic for entityInformationBar.xaml
    /// </summary>
    public partial class entityInformationBar : UserControl
    {
        private bool isReversed;

        public entityInformationBar()
        {
            InitializeComponent();
        }
        
        public ImageSource image
        {
            get
            {
                return characterPortrait.image;
            }
            set
            {
                characterPortrait.image = value;
            }
        }
        
        public double percentage
        {
            get
            {
                return healthBar.percentage;
            }
            set
            {
                healthBar.percentage = value;
            }
        }

        public double currentLife
        {
            get
            {
                return healthInfo.currentLife;
            }
            set
            {
                healthInfo.currentLife = value;
            }
        }

        public double totalLife
        {
            get
            {
                return healthInfo.totalLife;
            }
            set
            {
                healthInfo.totalLife = value;
            }
        }

     
  

        //"characterPortrait" image
        //"healthBar" percentage
        //"healthInfo" currentLife="4450" totalLife="1000000"
    }
}
