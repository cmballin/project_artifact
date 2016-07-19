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
    /// Interaction logic for CombatSnippet.xaml
    /// </summary>
    public partial class CombatSnippet : UserControl
    {
        private bool inFocus;
        private bool isComplete;
        private String snippetString = "";

        //might add some colors and stuff for customization
        private Color highlightColor = Colors.Black;
        private Color defaultColorForeground = Colors.White;
        private Color deactivatedLetterColor = Colors.LightGray;

        private Thickness defaultPadding = new Thickness(0);
        private Thickness defaultMargin = new Thickness(0, 0, 5, 0);
        

        public CombatSnippet(string str)
        {
            InitializeComponent();
            snippetString = str;

            displaySnippet();
            
        }

        public bool focus
        {
            get
            {
                return inFocus;
            }
            set
            {
                inFocus = value;

                if (inFocus)
                    activate();
                else
                    deactivate();
            }
        }
        //goes through each label, if any are not tagged as pressed, it cancels. If they are all pressed it sets the isComplete bool to true
        public bool allPressed
        {
            get
            {
                foreach (Label l in snippetContainer.Children)
                {
                    if ((String)l.Tag != "pressed")
                        return false;
                }

                return true;
            }

        }

        public bool complete
        {
            get
            {
                return isComplete;
            }
            set
            {
                isComplete = value;

                if (isComplete == true)
                {
                    makeInvisible();
                    inFocus = false;
                }

                else
                    makeVisible();
            }
        }

        //runs through all of the labels and sets thier properties to invisible
        private void makeInvisible()
        {
            foreach (Label l in snippetContainer.Children)
            {
                l.Visibility = Visibility.Hidden;
            }

        }
        //runs through all of the labels and sets thier properties to visible
        private void makeVisible()
        {
            foreach (Label l in snippetContainer.Children)
            {
                l.Visibility = Visibility.Visible;
            }

        }

        //will grey the snippet out when not in use or in focus
        private void deactivate()
        {
            foreach (Label l in snippetContainer.Children)
            {
                l.Foreground = new SolidColorBrush(deactivatedLetterColor);
            }
        }

        //will bring the snippet back to it's normal set of colors
        private void activate()
        {
            //The individual labels
            foreach(Label l in snippetContainer.Children)
            {
                l.Foreground = new SolidColorBrush(defaultColorForeground);
            }
            
        }

        //will find an individual key and highlight it's label
        private void highLightKey(Key key)
        {
            Label keyLabel = findLabel(key);

            if (keyLabel != null)
            {
                //assigns a flag on each label that says if it is pressed
                keyLabel.Tag = "pressed";
                keyLabel.Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    BlurRadius = 5,
                    Color = highlightColor,
                    Direction = 360,
                    ShadowDepth = 0,
                    Opacity = 1
                };

            }

        }
        //will find an individual key and remove the highlight from it's label
        private void unHighLightKey(Key key)
        {
            Label keyLabel = findLabel(key);

            if (keyLabel != null)
            {
                //gets rid of any assigned flag that might be present on a label
                keyLabel.Tag = null;
                keyLabel.Effect = null;

            }
        }
        private void unHighLightAll()
        {
            foreach (Label l in snippetContainer.Children)
            {
                l.Tag = null;
                l.Effect = null;
            }        
        }

        private Label findLabel(Key key)
        {
            foreach (Label label in snippetContainer.Children)
            {
                if (convertKey(key) == label.Content.ToString())
                {
                    return label;
                }
            }

            return null;
        }

        private String convertKey(Key key)
        {
            switch (key.ToString())
            {
                case "D0": return "0";
                case "D1": return "1";
                case "D2": return "2";
                case "D3": return "3";
                case "D4": return "4";
                case "D5": return "5";
                case "D6": return "6";
                case "D7": return "7";
                case "D8": return "8";
                case "D9": return "9";

                case "NumPad0": return "0";
                case "NumPad1": return "1";
                case "NumPad2": return "2";
                case "NumPad3": return "3";
                case "NumPad4": return "4";
                case "NumPad5": return "5";
                case "NumPad6": return "6";
                case "NumPad7": return "7";
                case "NumPad8": return "8";
                case "NumPad9": return "9";
            }
            return key.ToString();
        }

        private void displaySnippet()
        {
            //clears the container
            snippetContainer.Children.Clear();

            //for each of the substring sequences, display 
            foreach(Char ch in snippetString)
            {
                Label individualLetter = new Label();
                individualLetter.Content = ch;
                snippetContainer.Children.Add(individualLetter);

                //setting the individual letter's default label settings
                individualLetter.Foreground = new SolidColorBrush(defaultColorForeground);
                individualLetter.Padding = defaultPadding;
                individualLetter.Margin = defaultMargin;

            }
            if (inFocus)
                activate();
            else
                deactivate();

        }

       

        //takes keys from the parent and decides how to handle the input
        public void keyDownRead(Key key)
        {
            //deals with highlighting and then checks if complete. If complete, it turns invisible and gets rid of the focus | might do more stuff later

            //if correct key is pressed | if we find a matching key, highlight it and get out.
            foreach (char ch in snippetString)
            {
                if (convertKey(key) == ch.ToString())
                {
                    highLightKey(key);
                    return;
                }           
            }

            //if wrong key is pressed | unhiglight all keys
            unHighLightAll();

        }
        public void keyUpRead(Key key)
        {
            //deals with highlighting and then checks if complete. If complete, it turns invisible and gets rid of the focus | might do more stuff later
            unHighLightKey(key);

        }




    }
}
