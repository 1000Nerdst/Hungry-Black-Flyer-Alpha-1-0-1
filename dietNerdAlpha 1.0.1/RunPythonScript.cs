using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace dietNerdAlpha_1._0._1
{
    class RunPythonScript
    {
        //static void Main()
        //{

        //}
        public void chooseServingSize()
        {
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python37_64\python.exe";

            var script = @"C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\Python Files\getServingOptions.py";
            //var script = @"C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\Python Files\getServingOptions\getServingOptions\getServingOptions\getServingOptions.py";


            //inputs arrguments

            //psi.Arguments = $"\"{script}\"\"{start}\"\"{end}\"";
            psi.Arguments = $"\"{script}\"";

            //process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            //execute process and get outputs
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();

            }

            string resultingString = results;

            string numberOfOptionsString = resultingString.Substring(0, resultingString.IndexOf('\r'));
            int numberOfOptions = int.Parse(numberOfOptionsString);

            string[] servingUnitArray = new string[numberOfOptions];
            float[] multiplerArray = new float[numberOfOptions];

            int indexOfNewLine = resultingString.IndexOf('\n');
            resultingString = resultingString.Remove(0, indexOfNewLine + 1);
            int i = 0;
            while (resultingString.Length > 2)
            {
                string sizeOfServing = resultingString.Substring(0, resultingString.IndexOf('\r'));
                indexOfNewLine = resultingString.IndexOf('\n');
                resultingString = resultingString.Remove(0, indexOfNewLine + 1);
                string servingMultipler = resultingString.Substring(0, resultingString.IndexOf('\r'));
                indexOfNewLine = resultingString.IndexOf('\n');
                resultingString = resultingString.Remove(0, indexOfNewLine + 1);

                float multipler = float.Parse(servingMultipler);

                servingUnitArray[i] = sizeOfServing;
                multiplerArray[i] = multipler;

                i++;
            }

            getPythonOptions pythonOptions = new getPythonOptions();
            pythonOptions.servingUnit = servingUnitArray;
            pythonOptions.multipler = multiplerArray;

            creatOptionsCSV(pythonOptions, numberOfOptions);
        }

        public void GetIngrendentInfromation()
        {
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python37_64\python.exe";

            var script = @"C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\Python Files\scrapeIngredentInfromation.py";

            psi.Arguments = $"\"{script}\"";

            //process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            //execute process and get outputs
            var errors = "";
            var results = "";
            int indexOfNewLine;

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();

            }

            string resultingString = results;
            string[] servingUnitArray = new string[45];

            for (int i = 0;  i < 45; i++)
            {
                string nutrientString = resultingString.Substring(0, resultingString.IndexOf('\r'));
                indexOfNewLine = resultingString.IndexOf('\n');
                resultingString = resultingString.Remove(0, indexOfNewLine + 1);

                servingUnitArray[i] = nutrientString;
            }

            loadToNewIngredentsXML(servingUnitArray);
        }

        private void loadToNewIngredentsXML(string[] stringArray)
        {
            string xmlPath = @"C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\Config Files\newIngredient.xml";
            XmlTextWriter xmlWriter = new XmlTextWriter(xmlPath, Encoding.UTF8);

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("Ingredient");
            xmlWriter.WriteElementString("Calories", stringArray[0]);
            xmlWriter.WriteElementString("Fats", stringArray[1]);
            xmlWriter.WriteElementString("Carbohydrates", stringArray[2]);
            xmlWriter.WriteElementString("Protein", stringArray[3]);
            xmlWriter.WriteElementString("Cholesterol", stringArray[4]);
            xmlWriter.WriteElementString("Trans_Fats", stringArray[5]);
            xmlWriter.WriteElementString("Surgar", stringArray[6]);
            xmlWriter.WriteElementString("Sta_Fats", stringArray[7]);
            xmlWriter.WriteElementString("Fiber", stringArray[8]);
            xmlWriter.WriteElementString("VitaminA", stringArray[9]);
            xmlWriter.WriteElementString("VitaminB1", stringArray[10]);
            xmlWriter.WriteElementString("VitaminB2", stringArray[11]);
            xmlWriter.WriteElementString("VitaminB3", stringArray[12]);
            xmlWriter.WriteElementString("VitaminB5", stringArray[13]);
            xmlWriter.WriteElementString("VitaminB6", stringArray[14]);
            xmlWriter.WriteElementString("VitaminB7", stringArray[15]);
            xmlWriter.WriteElementString("VitaminB9", stringArray[16]);
            xmlWriter.WriteElementString("VitaminB12", stringArray[17]);
            xmlWriter.WriteElementString("VitaminC", stringArray[18]);
            xmlWriter.WriteElementString("VitaminD", stringArray[19]);
            xmlWriter.WriteElementString("VitaminE", stringArray[20]);
            xmlWriter.WriteElementString("VitaminK", stringArray[21]);
            xmlWriter.WriteElementString("Choline", stringArray[22]);
            xmlWriter.WriteElementString("Calcium", stringArray[23]);
            xmlWriter.WriteElementString("Chloride", stringArray[24]);
            xmlWriter.WriteElementString("Chromium", stringArray[25]);
            xmlWriter.WriteElementString("Copper", stringArray[26]);
            xmlWriter.WriteElementString("Fluoride", stringArray[27]);
            xmlWriter.WriteElementString("Iodine", stringArray[28]);
            xmlWriter.WriteElementString("Iron", stringArray[29]);
            xmlWriter.WriteElementString("Magnesium", stringArray[30]);
            xmlWriter.WriteElementString("Manganese", stringArray[31]);
            xmlWriter.WriteElementString("Molybdenum", stringArray[32]);
            xmlWriter.WriteElementString("Phosphorus", stringArray[33]);
            xmlWriter.WriteElementString("Potassium", stringArray[34]);
            xmlWriter.WriteElementString("Selenium", stringArray[35]);
            xmlWriter.WriteElementString("Sodium", stringArray[36]);
            xmlWriter.WriteElementString("Sulfur", stringArray[37]);
            xmlWriter.WriteElementString("Zinc", stringArray[38]);
            xmlWriter.WriteElementString("Omega3", stringArray[39]);
            xmlWriter.WriteElementString("Omega6", stringArray[40]);
            xmlWriter.WriteElementString("ALA", stringArray[41]);
            xmlWriter.WriteElementString("EPA", stringArray[42]);
            xmlWriter.WriteElementString("DPA", stringArray[43]);
            xmlWriter.WriteElementString("DHA", stringArray[44]);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();
        }

        private void creatOptionsCSV(getPythonOptions pythonOptions, int numberOfOptions)
        {
            StringBuilder csvContent = new StringBuilder();
            StringBuilder tempContent = new StringBuilder();
            string csvPath = @"C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\Config Files\servingOptions.csv";
            //make and empty file
            tempContent.AppendLine(" ");
            File.AppendAllText(csvPath, tempContent.ToString());
            //delete the empty file
            File.Delete(csvPath);

            csvContent.AppendLine("Serving Unit, Size");

            string[] servingUnitArray = pythonOptions.servingUnit;
            float[] multiplerArray = pythonOptions.multipler;

            for (int i = 0; i < numberOfOptions; i++)
            {
                string unit = servingUnitArray[i];
                float mult = multiplerArray[i];
                string multString = mult.ToString();

                string singleLine = unit + "," + multString;

                csvContent.AppendLine(singleLine);

            }

            File.AppendAllText(csvPath, csvContent.ToString()); 
            //throw new NotImplementedException();
        }

        public void execProcess()
        {
            //create process info
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python37_64\python.exe";

            //provide the scrip and arguments
            //script
            var script = @"C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\Ingredents to Database\Test for dietNerd\testForDietNerd\testForDietNerd\testForDietNerd.py";

            //inputs arrguments

            //psi.Arguments = $"\"{script}\"\"{start}\"\"{end}\"";
            psi.Arguments = $"\"{script}\"";

            //process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            //execute process and get outputs
            var errors = "";
            var results = "";

            using(var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }


            Console.WriteLine();
            Console.WriteLine(results);
            Console.WriteLine();

        }
    }
}
