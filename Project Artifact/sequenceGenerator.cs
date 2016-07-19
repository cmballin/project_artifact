using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Artifact
{
    class sequenceGenerator
    {
        private String iSequence = "";
        private List<String> snippetList = new List<String>();  //don't actually need this as of now.

        Random random = new Random();
        IntelligentKeyboardMatrix keyboard = new IntelligentKeyboardMatrix();

        private int snippetQuantityMin;
        private int snippetQuantityMax;

        private int snippetLengthMin;
        private int snippetLengthMax;


       // private int difficultyLevel;


        //right now, it won't take any properties. | later it can take things like difficulty level or complexity
        public sequenceGenerator(int quantityMin, int quantityMax, int lengthMin, int lengthMax)
        {
            //if min is zero, make it one
            if (quantityMin <= 0)   quantityMin = 1;
            if (lengthMin <= 0)     lengthMin = 1;

            //if max is less than min, max = min
            if (quantityMax < quantityMin)      quantityMax = quantityMin;
            if (lengthMax < lengthMin)          lengthMax = lengthMin;

            snippetQuantityMin = quantityMin;
            snippetQuantityMax = quantityMax;

            snippetLengthMin = lengthMin;
            snippetLengthMax = lengthMax;


            generateSequence();
            
        }

        public String sequence
        {
            get
            {
                return iSequence;
            }
            set
            {
            }
        }

        private String generateSnippet(bool lastSnippet)                                                 //moving this part over to the intelligent keyboard matrix so that it knows which keys are viable. 
        {
            string snippet = "";

            int snippetLength = random.Next(snippetLengthMin, snippetLengthMax + 1);

            snippet = keyboard.generateSnippet(snippetLength, lastSnippet);

   
            return snippet;
        }

        public void generateSequence()
        {
            int snippetQuantity = random.Next(snippetQuantityMin, snippetQuantityMax + 1);
            string tempSequence = "";
            bool lastSnippet = false;

            for (int q = 0; q < snippetQuantity; q++)
            {
                if (q + 1 == snippetQuantity)                        //if it is the last snippet
                    lastSnippet = true;

                tempSequence += generateSnippet(lastSnippet);    //add the generated snippet to a string

                if ( q + 1 != snippetQuantity)                    //if it's not the last snippet, add a separator as well
                    tempSequence += '|';
            }

            iSequence = tempSequence;
        }



    }



}
