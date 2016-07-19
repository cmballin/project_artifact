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
    /// Interaction logic for healthHUD.xaml
    /// </summary>
    public partial class healthHUD : UserControl
    {
        private double internalCurrentLife;
        private double internalTotalLife;

        public healthHUD()
        {
            InitializeComponent();
        }
        public double currentLife
        {
            get
            {
                return internalCurrentLife;
            }
            set
            {
                currentLifeHUD.Text = value.ToString("########");
            }
        }
        public double totalLife
        {
            get
            {
                return internalTotalLife;
            }
            set
            {
                totalLifeHUD.Text = value.ToString("########");
            }
        }
    }
}
