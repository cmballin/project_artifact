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
    /// Interaction logic for DefenseArea.xaml
    /// </summary>
    public partial class DefenseArea : UserControl
    {
        private ImageSource shieldImage;
        private bool iBlockQueued;

        private Color highlightColor = Colors.Black;

        private string textBlockQueued = "Block queued";
        private string textBlockNotQueued = "No block queued";

        private string iText;

        public DefenseArea()
        {
            InitializeComponent();
        }

        
        public bool blockQueued
        {
            get
            {
                return iBlockQueued;
            }
            set
            {
                iBlockQueued = value;

                if (iBlockQueued == true)
                {
                    iText = textBlockQueued;
                    imageShield.Effect = new System.Windows.Media.Effects.DropShadowEffect
                    {
                        BlurRadius = 10,
                        Color = highlightColor,
                        Direction = 360,
                        ShadowDepth = 0,
                        Opacity = 1
                    };
                }
                   
                if (iBlockQueued == false)
                {
                    iText = textBlockNotQueued;
                    imageShield.Effect = null;
                }
                   

                blockTextContainer.Text = iText;
                
            }
        }

        public ImageSource image
        {
            get
            {
                return shieldImage;
            }
            set
            {
                shieldImage = value;
                imageShield.Source = shieldImage;
            
            }
        }




    }
}
