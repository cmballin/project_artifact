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
    /// Interaction logic for CombatSequence.xaml
    /// </summary>
    public partial class CombatSequence : UserControl
    {
        private String combatString;
        private List<string> combatList = new List<string>();
        private List<CombatSnippet> snippetList = new List<CombatSnippet>();

        private int currentSnippetIndex = 0;

        private String confirmationKey = "Return";
        private String alternateConfirmationKey = "Enter";

        private Color highlightColor = Colors.Black;
        private Color separatorColor = Colors.Black;

        public CombatSequence()
        {
            InitializeComponent();

            //Needs to wait for window to be created before it can attach an keypress handler/listener to it.
            //This if statement just checks to make sure that we don't try to run the key listeners in design mode, which is having issues running before the window is instanciated
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this) == false)
                Loaded += CombatSequence_Loaded;
            
            
        }

        //when it is time to add the CombatSequence element to the page, this code will run and assign the listener. This keeps the code organized in the child, and not the parent
        private void CombatSequence_Loaded(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.PreviewKeyDown += ParentWindow_PreviewKeyDown;
            parentWindow.PreviewKeyUp += ParentWindow_PreviewKeyUp;

            // TODO get rid of these hard coded numbers
            sequenceGenerator ginnyTheGenerator = new sequenceGenerator(2, 3, 2, 4);
            text = ginnyTheGenerator.sequence;


        }

        //code runs evertime a key is lifted and gets rid of the highlighting
        private void ParentWindow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
           //check which snippet is inFocus
           foreach(CombatSnippet snippet in snippetList)
            {
                if (snippet.focus == true)
                    snippet.keyUpRead(e.Key);

                //probably need to move this
                confimationKeyUI.activated = false;
            }
        }

        //code runs everytime a key is pressed and highlights the key
        private void ParentWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsRepeat == true)     return;
            if (snippetList == null)    return;
            if (snippetList.Count == 0) return;

            CombatSnippet currentSnippet = snippetList[currentSnippetIndex];

            if (currentSnippet.focus == false)  return;

            
            //if it is the last snippet in the sequence check to see if confirmation key is pressed | or whatever it ends up being | needs to trigger first, before keyDownRead
            if (currentSnippetIndex >= snippetList.Count - 1)
            {
                //if the confirmation key is pressed, check for pompletion before processing the key with the child
                if ((e.Key.ToString() == confirmationKey || e.Key.ToString() == alternateConfirmationKey) && currentSnippet.allPressed)
                {
                    if (combatBar.activated == true)
                    {
                        //need to set some sort of buffer timer so that there is a button pressing grace period
                        //bar needs a way to trigger an event that will make the confirmation key light up. Should it only be controlled by the bar? or should the held down keys play a role?
                        //needs to check with bar length to see if it can be pressed.

                        combatBar.start(0, 100, TimeSpan.FromSeconds(6));
                        combatBar.activated = false;

                        currentSnippet.complete = true;
                        //get a new string or execute damage or something | start snippet index over

                        // TODO get rid of these hard coded numbers
                        sequenceGenerator ginnyTheGenerator = new sequenceGenerator(2, 3, 2, 4);
                        text = ginnyTheGenerator.sequence;



                        confimationKeyUI.activated = false;
                        return;
                    }
           
                }

                //give key to child to process
                currentSnippet.keyDownRead(e.Key);

                if (currentSnippet.allPressed)
                {
                    //light up the enterkey
                    confimationKeyUI.activated = true;
                }

                      
                return;

            }  

            //if this isn't the last snippet
            currentSnippet.keyDownRead(e.Key);
                    
            //check if it is complete--all the snippet keys pressed down--, if it is, then set a new focus and make the old one invisible | complete does this
            if (currentSnippet.allPressed)
            {
                currentSnippet.complete = true;
                //focus next snippet | this should only be reached if the current snippet isn't the last

                currentSnippetIndex += 1;
                snippetList[currentSnippetIndex].focus = true;
            }

            return;    
                         
        }

  
        public String confirmKeyText
        {
            get
            {
                return confimationKeyUI.text;
            }
            set
            {
                confimationKeyUI.text = value;
            }
        }

        public bool confirmKeyActivated
        {
            get
            {
                return confimationKeyUI.activated;
            }
            set
            {
                confimationKeyUI.activated = value;
            }
        }

        public String text
        {
            get
            {
                return combatString;
            }
            //This needs to create a for loop to peel letters off of the string and then add the invdividual labels/textboxes to the screen
            set
            {
                currentSnippetIndex = 0;

                combatString = value;
                makeCollections(combatString);

                displayCombatSequence();
            }
        }

        public double barPercentage
        {
            get
            {
                return combatBar.percentage;
            }
            set
            {
                combatBar.percentage = value;
            }
        }

        public Color barColorLow
        {
            get
            {
                return combatBar.barColorLow;
            }
            set
            {
                combatBar.barColorLow = value;
            }
        }

        public Color barColorHigh
        {
            get
            {
                return combatBar.barColorHigh;
            }
            set
            {
                combatBar.barColorHigh = value;
            }
        }

        public Color barColorEmergency
        {
            get
            {
                return combatBar.barColorEmergency;
            }
            set
            {
                combatBar.barColorEmergency = value;
            }
        }

        public bool isBarHealthBar
        {
            get
            {
                return combatBar.healthBar;
            }
            set
            {
               combatBar.healthBar = value;
            }
        }

        public bool barActivated
        {
            get
            {
                return combatBar.activated;
            }
            set
            {
                combatBar.activated = value;

            }
        }

        public string barLabel
        {
            get
            {
                return combatBar.barLabel;
            }
            set
            {
                combatBar.barLabel = value;
            }
        }

        //need some sort of parser that takes a string (P5Q|LME|97V), with dividers, and makes an array of substring sections like ---> [P5Q, LME, 97V] Uses these to build UI component

        //makes a list to keep track of the parts of a combat sequence
        private void makeCollections(String rawString)
        {
            combatList.Clear();

            //separates the sting parts and adds them to a list for easy access
            string[] combatArray = rawString.Split('|');
            foreach (string s in combatArray)
                combatList.Add(s);

            //sets up the snippets
            snippetList.Clear();
            foreach (string snip in combatList)
            {
                CombatSnippet snippet = new CombatSnippet(snip);
                //adds newly created snippet (which is a stack panel) to a list of snippets
                snippetList.Add(snippet);
            }
            //puts the first snippet into focus to begin with
            snippetList[currentSnippetIndex].focus = true;
        }

        private void displayCombatSequence()
        {
            //clears the container
            sequenceContainer.Children.Clear();

            for (int k = 0; k < snippetList.Count; k++)
            {
                //adds snippet to the outer stack panel, while adding separators!
                sequenceContainer.Children.Add(snippetList[k]);

                //add separator
                //if I'm not at the end | make a new label separator
                if (k + 1 != snippetList.Count)
                {
                    Label separator = new Label();

                    //properties of the separator | making it black to see if the code is working
                    separator.Foreground = new SolidColorBrush(separatorColor);
                    separator.Padding = new Thickness(0);
                    separator.Margin = new Thickness(0, 0, 5, 0);

                    separator.Content = '|';

                    sequenceContainer.Children.Add(separator);

                }
            

            }
           
        }









    }
}
