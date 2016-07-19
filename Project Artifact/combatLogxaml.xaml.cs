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
    /// Interaction logic for combatLogxaml.xaml
    /// </summary>
    public partial class combatLogxaml : UserControl
    {
        public combatLogxaml()
        {
            InitializeComponent();
        }

        //takes a string and adds it to the richTextBox | don't need dis shit waaaa!
        private void addLine(String line)
        {

            Paragraph par = new Paragraph();
            Run run = new Run(line);

            //adds the new run to the paragraph
            par.Inlines.Add(run);

            //adds the paragraph to the rickTextBox
            textContainer.Document.Blocks.Add(par);
        }

        //takes a string that may or may not have emphasize codes. Splits the string up into multiple runs if needed, styles them and then poofs the final paragraph out to the rich textbox
        private void emphasize(String line)
        {
            String codeIdentifier = "";
            String runString = "";

            Run run = new Run(null);
            int lastPosition = 0;
            int codeIdentifierLength = 1; //not sure about having this variable
            

            //paragraph to store my runs, and that will hold the final shipment to the richTextBox
            Paragraph finalPar = new Paragraph();
            finalPar.LineHeight = 1;

            while (true)
            {
                int codePosition = line.IndexOf('~', lastPosition);

                //If it doesn't find any more codes. Only happens at the end.
                if (codePosition == -1) codePosition = line.Length;

                
                //gets the string snippet and saves it to a String
                runString = line.Substring(lastPosition, codePosition - lastPosition);

                //if the code isn't the first found character , saves off off the characters that are found up until the point of the code and adds it to the paragraph
                if (runString != "")
                {
                    run = new Run(runString);
                    
                    //check for codeIdentifier and modify the run appropriately
                    switch(codeIdentifier)
                    {
                        case "0": break;

                        case "1":        
                            run.Foreground = Brushes.LightGreen;
                            run.FontSize = 22;
                            break;

                        case "2":
                            run.Foreground = Brushes.Red;
                            run.FontSize = 22;
                            break;
                    }
                    //makes a run and gives it to the paragraph
                    finalPar.Inlines.Add(run);
                    textContainer.ScrollToEnd();

                }

                //if we are not at the end, aka if we saved off the last run, get out | not sure if this is the right way to do it.
                if (line.Length == codePosition) break;

               //the +1 is to move down the positon track by one
                lastPosition = codePosition + codeIdentifierLength + 1;
                //the +1 just refers to a starting point of the next position
                codeIdentifier = line.Substring(codePosition + 1, codeIdentifierLength);

            }
            //finally add that paragraph to the rich text container.
            textContainer.Document.Blocks.Add(finalPar);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            emphasize("You hit the enemy for ~1 326 ~0 deeps");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            emphasize("Enemy hits you for ~2 200 ~0 damages, oh noes!!!");
        }
    }
}
