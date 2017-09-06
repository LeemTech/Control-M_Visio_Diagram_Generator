using System;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Linq;
using System.Threading;
using System.Collections;
using System.ComponentModel;

namespace Control_M_Visio_Generator
{
    #region Global Variables

    public partial class MainForm : Form
    {
        
        #endregion
        #region Class Variables

        public static string configXMLLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Control-M_Visio\\";
        public static string configFile = "Control_M_Visio_Config.XML";
        string vsdFilename;
        public static string outputLocation { get; set; }
        XDocument doc;
        XElement rootElement, folderElement, 
            section1ColourElement, 
            section2ColourElement, 
            section3ColourElement, 
            externalJobColourElement,
            outputElement,
            section1Element,
            section2Element,
            section3Element,
            xmlElement,
            flowElement,
            externalCheckElement;
        public static DataSet xmlDataSet { get; set; }
        public static string xmlPath { get; set; }
        public static string fileName { get; set; }
        public static bool externalJobs { get; set; }
        public static bool controlEnabled { get; set; }
        public static string section1 { get; set; }
        public static string section2 { get; set; }
        public static string section3 { get; set; }
        public static string section1Colour { get; set; }
        public static string section2Colour { get; set; }
        public static string section3Colour { get; set; }
        public static string externalJobColour { get; set; }
        public static int flowLayout { get; set; }
        public static MainForm _appendForm;
        GenerateVisio GenVisio;
        public static bool closePending;
        public static BackgroundWorker VisioBackgroundWorker = new BackgroundWorker();

        #endregion
       
        public MainForm()
        {
            InitializeComponent();
            _appendForm = this;
            controlEnabled = true;
            
            //Load config file
            if (File.Exists(configXMLLocation + configFile))
            {
                doc = XDocument.Load(configXMLLocation + configFile);
                rootElement = doc.Element("ROOT");
                folderElement = TryGetElementValue(parentEl: ref rootElement, elementName: "FOLDER");
                section1ColourElement = TryGetElementValue(parentEl: ref rootElement, elementName: "S1COLOUR");
                section2ColourElement = TryGetElementValue(parentEl: ref rootElement, elementName: "S2COLOUR");
                section3ColourElement = TryGetElementValue(parentEl: ref rootElement, elementName: "S3COLOUR");
                externalJobColourElement = TryGetElementValue(parentEl: ref rootElement, elementName: "EXCOLOUR");
                outputElement = TryGetElementValue(parentEl: ref rootElement, elementName: "OUTPUT");
                xmlElement = TryGetElementValue(parentEl: ref rootElement, elementName: "XML");
                section1Element = TryGetElementValue(parentEl: ref rootElement, elementName: "SECTION1");
                section2Element = TryGetElementValue(parentEl: ref rootElement, elementName: "SECTION2");
                section3Element = TryGetElementValue(parentEl: ref rootElement, elementName: "SECTION3");
                flowElement = TryGetElementValue(parentEl: ref rootElement, elementName: "FLOW");
                externalCheckElement = TryGetElementValue(parentEl: ref rootElement, elementName: "EXTCHECK");

                
                if (!(Directory.Exists(outputElement.Value)))
                {
                    outputElement.Value = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
                    doc.Save(configXMLLocation + configFile);
                }
                outputLocationTextbox.Text = outputElement.Value;

                if (!(Directory.Exists(folderElement.Value)))
                {
                    folderElement.Value = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
                    doc.Save(configXMLLocation + configFile);
                }

                folderLocationTextBox.Text = folderElement.Value;

            }
            else
            {
                if (!(Directory.Exists(configXMLLocation)))
                {
                    Directory.CreateDirectory(configXMLLocation);
                }
                
                //Build Item List XML structure
                doc = new XDocument();
                rootElement = new XElement("ROOT");
                folderElement = new XElement("FOLDER");
                section1ColourElement = new XElement("S1COLOUR");
                section2ColourElement = new XElement("S2COLOUR");
                section3ColourElement = new XElement("S3COLOUR");
                externalJobColourElement = new XElement("EXCOLOUR");
                outputElement = new XElement("OUTPUT");
                xmlElement = new XElement("XML");
                section1Element = new XElement("SECTION1");
                section2Element = new XElement("SECTION2");
                section3Element = new XElement("SECTION3");
                flowElement = new XElement("FLOW");
                externalCheckElement = new XElement("EXTCHECK");

                section1ColourElement.Value = "216, 216, 216";
                section2ColourElement.Value = "216, 216, 216";
                section3ColourElement.Value = "216, 216, 216";
                externalJobColourElement.Value = "237, 127, 54";

                folderElement.Value = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
                rootElement.Add(folderElement);
                rootElement.Add(section1ColourElement);
                rootElement.Add(section2ColourElement);
                rootElement.Add(section3ColourElement);
                rootElement.Add(externalJobColourElement);
                rootElement.Add(outputElement);
                rootElement.Add(xmlElement);
                rootElement.Add(section1Element);
                rootElement.Add(section2Element);
                rootElement.Add(section3Element);
                rootElement.Add(flowElement);
                rootElement.Add(externalCheckElement);


                doc.Add(rootElement);
                doc.Save(configXMLLocation + configFile);

            }

            VisioBackgroundWorker.WorkerSupportsCancellation = true;
            VisioBackgroundWorker.DoWork += new DoWorkEventHandler(VisioBackgroundWorker_DoWork);
            VisioBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(VisioBackgroundWorker_RunWorkerCompleted);
            FlowComboBox.SelectedItem = "Flowchart";
            

            int[] s1Colour = StringToInt(colour: section1ColourElement.Value);
            int[] s2Colour = StringToInt(colour: section2ColourElement.Value);
            int[] s3Colour = StringToInt(colour: section3ColourElement.Value);
            int[] extColour = StringToInt(colour: externalJobColourElement.Value);
            Section1ColourShape.FillColor = Color.FromArgb(red: s1Colour[0], green: s1Colour[1], blue: s1Colour[2]);
            Section2ColourShape.FillColor = Color.FromArgb(red: s2Colour[0], green: s2Colour[1], blue: s2Colour[2]);
            Section3ColourShape.FillColor = Color.FromArgb(red: s3Colour[0], green: s3Colour[1], blue: s3Colour[2]);
            ExternalColourShape.FillColor = Color.FromArgb(red: extColour[0], green: extColour[1], blue: extColour[2]);

            if (externalCheckElement.Value != "")
            {
                if (externalCheckElement.Value.ToString() == "Checked")
                {
                    extJobCheckBox.CheckState = CheckState.Checked;
                }
                else
                {
                    extJobCheckBox.CheckState = CheckState.Unchecked;
                }
            }
            else
            {
                extJobCheckBox.CheckState = CheckState.Checked;
            }

                      
            this.Size = new Size(568, 400);
            this.CenterToScreen();
        }


        #region formFunctions
      
        //Generate output for the user
        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            outputTextBox.Text += value + "\r\n";
        }

        public void folderLocationButton_Click(object sender, EventArgs e)
        {
           
            //Select folder location, and output contents to Combo Box
            FolderBrowserDialog folderLocation = new FolderBrowserDialog();
            folderLocation.SelectedPath = folderLocationTextBox.Text;

            //Get all files in a folder, and set the combobox to the first entry
            if (folderLocation.ShowDialog() == DialogResult.OK)
            {
                folderLocationTextBox.Text = folderLocation.SelectedPath;
                rootElement.Element("FOLDER").Value = folderLocation.SelectedPath;

                //Clear down XML data, and re-save with new config.
                folderElement.RemoveAll();
                folderElement.Value = folderLocation.SelectedPath.ToString();                             
                doc.Save(configXMLLocation + configFile);

            }

        }


        //Clear XML Combo box if folder changes.
        public void folderLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            xmlComboBox.Items.Clear();
            xmlComboBox.Text = "";

            string[] xmlFiles = Directory.GetFiles(folderLocationTextBox.Text, "*.xml");

            
            for (int i = 0; i < xmlFiles.Length; i++)
            {
                string xmlFile = Path.GetFileName(xmlFiles[i]);
                xmlComboBox.Items.Insert(i, xmlFile);
            }
            if (xmlComboBox.Items.Contains(xmlElement.Value.ToString()))
            {
                xmlComboBox.SelectedItem = xmlElement.Value;

            }
            else if (xmlComboBox.Items.Count != 0)
            {
                xmlComboBox.SelectedIndex = 0;
            }
            else
            {
                Console.WriteLine("No XML files found");
            }
        }

        public void outputLocationButton_Click(object sender, EventArgs e)
        {
            //Select Output Location
            FolderBrowserDialog outputLocation = new FolderBrowserDialog();

            if (outputLocation.ShowDialog() == DialogResult.OK)
            {
                outputLocationTextbox.Text = outputLocation.SelectedPath;
                outputElement.RemoveAll();
                outputElement.Value = outputLocation.SelectedPath;
                doc.Save(configXMLLocation + configFile);
            }
        }

        //This method is where the main function happens.
        public void generateVisioButton_Click(object sender, EventArgs e)
        {
            
            //Set accessors
            section1 = Section1ComboBox.Text;
            section2 = Section2ComboBox.Text;
            section3 = Section3ComboBox.Text;
            
            section1Colour = GetRGBValues((Color)Section1ColourShape.FillColor);
            section2Colour = GetRGBValues((Color)Section2ColourShape.FillColor);
            section3Colour = GetRGBValues((Color)Section3ColourShape.FillColor);
            externalJobColour = GetRGBValues((Color)ExternalColourShape.FillColor);

            switch (FlowComboBox.Text)
            {
                case "Flowchart":
                    flowLayout = 1;
                    break;

                case "Hierarchy":
                    flowLayout = 17;
                    break;
            }


            //Get paths, and file names
            xmlPath = folderLocationTextBox.Text + "\\" + xmlComboBox.Text;
            fileName = Path.GetFileNameWithoutExtension(xmlPath);
            vsdFilename = fileName + ".vsd";
            outputLocation = outputLocationTextbox.Text + "\\" + vsdFilename;
            
            //Error handling for improper input
            if (!File.Exists(xmlPath))
            {
                System.Windows.Forms.MessageBox.Show("XML does not exist, or the path is not valid");
                return;
            }

            if (!Directory.Exists(outputLocationTextbox.Text))
            {
                System.Windows.Forms.MessageBox.Show("Please choose a valid output location");
                return;
            }

            //Kill all Visio threads
            foreach (var process in Process.GetProcessesByName("VISIO"))
            {
                process.Kill();
            }

            if (extJobCheckBox.Checked == true)
            {
                externalJobs = true;
            }
            else
            {
                externalJobs = false;
            }

            
            VisioBackgroundWorker.RunWorkerAsync();
                   
      
        }

        public void xmlComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string xmlPath = folderLocationTextBox.Text + "\\" + xmlComboBox.Text;

            if ((string)xmlComboBox.SelectedItem != (string)xmlElement.Value)
            {
                var comboBox = sender as ComboBox;
                if ((string)comboBox.SelectedItem != (string)xmlElement.Value)
                {
                    xmlElement.Value = comboBox.SelectedItem.ToString();
                    doc.Save(configXMLLocation + configFile);
                }
            }
            Section1ComboBox.Items.Clear();
            Section1ComboBox.Text = "";
            Section2ComboBox.Items.Clear();
            Section2ComboBox.Text = "";
            Section3ComboBox.Items.Clear();
            Section3ComboBox.Text = "";

            //Begin XML handling - Initialise objects to read XML
            XmlDocument xmlDoc = new XmlDocument();
            StreamReader textReader = new StreamReader(xmlPath);

            //XML reader settings to avoid DTD in XML file
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = null;
            settings.DtdProcessing = DtdProcessing.Ignore;

            XmlReader xmlDataSetReader = XmlReader.Create(xmlPath, settings);
            DataSet xmlDataSet = new DataSet();
            xmlDataSet.ReadXml(xmlDataSetReader, XmlReadMode.Auto);

            for (int i = 0; i < xmlDataSet.Tables[1].Columns.Count; i++)
            {
                //Populate combo boxes with data
                DataColumn columnTitle = xmlDataSet.Tables[1].Columns[i];
                string sectionName = columnTitle.ColumnName;
                Section1ComboBox.Items.Insert(i, sectionName);
                Section2ComboBox.Items.Insert(i, sectionName);
                Section3ComboBox.Items.Insert(i, sectionName);
            }

            if (Section1ComboBox.Items.Contains(section1Element.Value))
            {
                Section1ComboBox.SelectedItem = section1Element.Value;
            }
            if (Section2ComboBox.Items.Contains(section2Element.Value))
            {
                Section2ComboBox.SelectedItem = section2Element.Value;
            }
            if (Section3ComboBox.Items.Contains(section3Element.Value))
            {
                Section3ComboBox.SelectedItem = section3Element.Value;
            }

            

             
        }

        private void diagTextBox_TextChanged(object sender, EventArgs e)
        {
            outputTextBox.SelectionStart = outputTextBox.Text.Length;
            outputTextBox.ScrollToCaret();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GenerateVisio sample = new GenerateVisio();
        }

        private void AdvancedButton_Click(object sender, EventArgs e)
        {
            if (this.Height == 400)
            {
                this.Size = new Size(568, 660);
                this.CenterToScreen();
            }
            else
            {
                this.Size = new Size(568, 400);
                this.CenterToScreen();
            }
        }
        private void Section1ColourButton_Click(object sender, EventArgs e)
        {
            if (colour_dialog.ShowDialog() == DialogResult.OK)
            {
                Section1ColourShape.FillColor = colour_dialog.Color;
                section1ColourElement.Value = (colour_dialog.Color.R + "," + colour_dialog.Color.G + "," + colour_dialog.Color.B).ToString();
                doc.Save(configXMLLocation + configFile);
            }
        }

        private void Section2ColourButton_Click(object sender, EventArgs e)
        {
            if (colour_dialog.ShowDialog() == DialogResult.OK)
            {
                Section2ColourShape.FillColor = colour_dialog.Color;
                section2ColourElement.Value = (colour_dialog.Color.R + "," + colour_dialog.Color.G + "," + colour_dialog.Color.B).ToString();
                doc.Save(configXMLLocation + configFile);
            }
        }

        private void Section3ColourButton_Click(object sender, EventArgs e)
        {
            if (colour_dialog.ShowDialog() == DialogResult.OK)
            {
                Section3ColourShape.FillColor = colour_dialog.Color;
                section3ColourElement.Value = (colour_dialog.Color.R + "," + colour_dialog.Color.G + "," + colour_dialog.Color.B).ToString();
                doc.Save(configXMLLocation + configFile);
            }
        }

        private void ExternalJobColourButton_Click(object sender, EventArgs e)
        {
            if (colour_dialog.ShowDialog() == DialogResult.OK)
            {
                ExternalColourShape.FillColor = colour_dialog.Color;
                externalJobColourElement.Value = (colour_dialog.Color.R + "," + colour_dialog.Color.G + "," + colour_dialog.Color.B).ToString();
                doc.Save(configXMLLocation + configFile);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (VisioBackgroundWorker.IsBusy)
            {
                closePending = true;
                e.Cancel = true;
                this.Hide(); // hides the form

                VisioBackgroundWorker.CancelAsync();

                return;
            }

        }

        private void VisioBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            GenVisio = new GenerateVisio();
            GenVisio.GenerateVisioDiagram();

        }

        private void VisioBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (closePending)
                this.Close();
            closePending = false;

        }

        private void Section1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if ((string)comboBox.SelectedItem != (string)section1Element.Value)
            {
                section1Element.Value = comboBox.SelectedItem.ToString();
                doc.Save(configXMLLocation + configFile);
            }
        }

        private void Section2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if ((string)comboBox.SelectedItem != (string)section2Element.Value)
            {
                section2Element.Value = comboBox.SelectedItem.ToString();
                doc.Save(configXMLLocation + configFile);
            }
        }

        private void Section3ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if ((string)comboBox.SelectedItem != (string)section3Element.Value)
            {
                section3Element.Value = comboBox.SelectedItem.ToString();
                doc.Save(configXMLLocation + configFile);
            }
        }


        #endregion

        public static XElement TryGetElementValue (ref XElement parentEl, string elementName)
        {
            XElement foundEl = parentEl.Element(elementName);

            if (foundEl != null)
            {
                return foundEl;
            }
            foundEl = new XElement(elementName);
            parentEl.Add(foundEl);
            return foundEl;
        }
        public int[] StringToInt(string colour)
        {           
            string[] colArray = colour.Split(',');
            int[] output = new int[colArray.Length];

            for (int i = 0; i < colArray.Length; i++)
            {
                output[i] = Int32.Parse(colArray[i]);
            }
                return output;
        }
        public static string GetRGBValues(Color ARGB)
        {
            string RGBValue;
            //This function exists to remove the Alpha value, really.
            Byte R = ARGB.R;
            Byte G = ARGB.G;
            Byte B = ARGB.B;

            return RGBValue = R + "," + G + "," + B;
        }

        public void disableAllControls()
        {
            //All calls will come from a separate thread
            if (InvokeRequired)
            {
                MethodInvoker method = new MethodInvoker(disableAllControls);
                Invoke(method);
                return;
            }
            if (controlEnabled)
                controlEnabled = false;
            else 
                controlEnabled = true;

            foreach (Control c in _appendForm.Controls)
            {
                if (c is Button || c is TextBox || c is ComboBox || c is CheckBox || c is RichTextBox)
                {
                    c.Enabled = controlEnabled;
                }
                
            }
        }

        private void FlowComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if ((string)comboBox.SelectedItem != (string)flowElement.Value)
            {
                flowElement.Value = comboBox.SelectedItem.ToString();
                doc.Save(configXMLLocation + configFile);
            }
        }

        private void extJobCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(externalCheckElement.Value.ToString() != extJobCheckBox.CheckState.ToString())
            {
                externalCheckElement.Value = extJobCheckBox.CheckState.ToString();
                doc.Save(configXMLLocation + configFile);
            }
        }


    }
}
