using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Artifact
{
    class IntelligentKeyboardMatrix
    {

        private List<keyboardKey> keyboard = new List<keyboardKey>();
        private List<keyboardKey> selectableKeys = new List<keyboardKey>();

        private Random random = new Random();
        private char confirmationKey = '~';

        private String[] rows = {"    234789P     ",
                                 "   QWERU        ",
                                 "   ASDFJK       ",
                                 "    XCVM        ",
                                 "      56        ",
                                 "      TY        ",
                                 "      GH        ",
                                 "    Z BN   ~    "};                                           // the tild represents the enter key for now



        public IntelligentKeyboardMatrix()
        {

            //adding a keyboard matrix from an outside file would go in this spot

            createKeyboard();                                                                   //creates the keyboard | Who'd a thunk it.

        }

       

        private void resetKeyboard()                                                             //simply goes in and sets all the key's to be usable again.
        {
            if (keyboard == null) return;
            if (keyboard.Count == 0) return;

            foreach(keyboardKey key in keyboard)
            {
                key.blacklisted = false;
                key.inUse = false;
            }

            selectableKeys = new List<keyboardKey>(keyboard);

        }

        public string generateSnippet(int snippetLength, bool lastSnippet)
        {
            if (snippetLength <= 0) snippetLength = 1;


            List<keyboardKey> inProgressSnippet = new List<keyboardKey>();
            keyboardKey newKey;

            newKey = locateKeyByChar(confirmationKey);
            selectableKeys.Remove(newKey);

            if (lastSnippet == true)                                                 //if this is the last snippet, it needs to throw tild into the first slot of the snippet expression to blacklist it.      
                inProgressSnippet.Add(newKey);                                       //adding it to the list, should be enough to make sure it's considered by the other blacklists
                    
            for (int q = 0; q < snippetLength; q++)
            {
                newKey = findNextViableKey();

                if (newKey == null) break;                           //if there are no more keys, then leave with what we have collected so far.

                blacklist(newKey, inProgressSnippet);

                inProgressSnippet.Add(newKey);

                selectableKeys.Remove(newKey);
            }

            resetKeyboard();                                                                      //resets the keyboard when it is done generating the snippet.
            return convertSnippetToString(inProgressSnippet);
        }


        private string convertSnippetToString( List<keyboardKey> inProgressSnippet)
        {
            string snippetString ="";
            foreach(keyboardKey key in inProgressSnippet)
            {
                if (key.charValue == confirmationKey) continue;                         //makes sure the enter key doesn't get displayed with the string
  
                snippetString += key.charValue.ToString();
            }
            return snippetString;
        }

        private keyboardKey locateKeyByChar(char identifyingChar)                       //is it alright to have this many nulls? Whatever uses this code just needs to be able to handle them.
        {
            if (keyboard == null) return null;
            if (keyboard.Count == 0) return null;

            foreach (keyboardKey key in keyboard)
            {
                if (key.charValue == identifyingChar)
                    return key;                    
            }

            return null;
        }

        private void blacklist(keyboardKey key, List<keyboardKey> keyCheckList)
        {
            if (keyCheckList == null)        return;
            if (keyCheckList.Count == 0)     return;

            foreach(keyboardKey checkKey in keyCheckList)
            {
                if (key.charValue == checkKey.charValue) return;                                //makes sure it's not comparing itself to itself

                blacklistIncompatableKeys(key, checkKey);
            }      
                
        }
      
        private void blacklistIncompatableKeys(keyboardKey firstKey, keyboardKey secondKey)      //takes two keys and finds out which ones are incompatable and blacklists them
        {
            if (firstKey.charValue == secondKey.charValue) return;                               //cheacking again becasuse this might possibly get used elsewhere. Just making sure it isn't comparing itself to itself

            if( firstKey.xLocation == secondKey.xLocation)                                       //if the keys are on the same row | set their columns to blacklisted. 
            {
                foreach(keyboardKey key in keyboard)
                {
                    if (key.yLocation == firstKey.yLocation)
                    {
                        key.blacklisted = true;
                        selectableKeys.Remove(key);
                        continue;
                    }

                    if (key.yLocation == secondKey.yLocation)
                    {
                        key.blacklisted = true;
                        selectableKeys.Remove(key);
                    }
                         
                }

                return;
            }

            if (firstKey.yLocation == secondKey.yLocation)                                       //if the keys are in the same column | set their rows to blacklisted.
            {
                foreach (keyboardKey key in keyboard)
                {
                    if (key.xLocation == firstKey.xLocation)
                    {
                        key.blacklisted = true;
                        selectableKeys.Remove(key);
                        continue;
                    }

                    if (key.xLocation == secondKey.xLocation)
                    {
                        key.blacklisted = true;
                        selectableKeys.Remove(key);
                    }
                        
                }

                return;
            }

            //if they keys are not in the same row or column | blacklist their intersections | this is done by using the key's own row and the other's column

            keyboardKey firstIncompatableKey = findKeyByCoordinates(firstKey.xLocation, secondKey.yLocation);
            keyboardKey secondIncompatableKey = findKeyByCoordinates(secondKey.xLocation, firstKey.yLocation);

            if(firstIncompatableKey != null)
            {
                selectableKeys.Remove(firstIncompatableKey);
                firstIncompatableKey.blacklisted = true;
            }
                

            if(secondIncompatableKey != null)
            {
                selectableKeys.Remove(secondIncompatableKey);
                secondIncompatableKey.blacklisted = true;
            }
               

       
        }

        private keyboardKey findKeyByCoordinates(int x, int y)
        {
            keyboardKey correctKey = null;                                                //can return null if it doesn't find a key
            foreach (keyboardKey key in keyboard)
            {
                if (key.xLocation != x) continue;
                if (key.yLocation == y)
                {
                    correctKey = key;
                    break;
                }
            }
           
            return correctKey;
        }

        private keyboardKey findNextViableKey()
        {
            keyboardKey viableKey;
            int randomIndex;

            if (selectableKeys == null) return null;
            if (selectableKeys.Count == 0) return null;

            randomIndex = random.Next(selectableKeys.Count);

            viableKey = selectableKeys[randomIndex];
            viableKey.inUse = true;

           

            /*
            while (true)                                                              //possible that this will run forever. | Later, I might want to make a list of possible keys and pull from that. It would delete blacklisted ones.
            {
                randomIndex = random.Next(keyboard.Count);

                if (keyboard[randomIndex].inUse == true) continue;
                if (keyboard[randomIndex].blacklisted == true) continue;
                if (keyboard[randomIndex].charValue == confirmationKey) continue;                 //tild is currently being used to track the location of the enter key in the keyboard matrix.

                
                viableKey = keyboard[randomIndex];
                viableKey.inUse = true;
                break;
                                
            }
            */

            return viableKey;

        }


        private void createKeyboard()                                                        //takes the key multidemensional array and makes a list of keys | aka, the keyboard | ignores whitespaces
        {
            int keyX;
            int keyY;
            char keyChar;

            string currentRow = "";

            for (int q = 0; q < rows.Length; q++)                                         // goes through each row then goes through each character in order to create keys and populate the keyboard
            {
                currentRow = rows[q];
                keyX = q;

                for (int u = 0; u < currentRow.Length; u++)                               // goes through each letter in a row, makes the key when appropriate and then adds it to the list / keyboard
                {
                    if (currentRow[u].ToString() == " ") continue;                        //don't do anything if we see a whitespace

                    keyY = u;
                    keyChar = currentRow[u];

                    keyboardKey key = new keyboardKey(keyChar, keyX, keyY);
                    keyboard.Add(key);            

                }

            }
            selectableKeys = new List<keyboardKey>(keyboard);


        }

    }
}
