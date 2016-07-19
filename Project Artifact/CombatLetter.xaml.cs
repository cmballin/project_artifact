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
    /// Interaction logic for CombatLetter.xaml
    /// </summary>
    public partial class CombatLetter : UserControl
    {
        public CombatLetter()
        {
            InitializeComponent();
        }

        //needs to keep track of whether or not it is highlighted
        //also needs to keep a record of it's previous color, so that it can revert back if it isn't highlighted
    }
}
