using System;
using Visio = Microsoft.Office.Interop.Visio;

namespace Control_M_Visio_Generator
{
    public partial class VisioDrawer
    {
        //Initalise variables
        //---------------------------------
        public Visio.InvisibleApp VisApp;

        //the collection of shape masters
        //these are the collections you can choose from when drawing in the visio application
        public Visio.Documents MastersDocuments;

        //the document that holders the masters we will be working with
        public Visio.Document MasterDoc;

        //the masters collection we will get shapes from
        public Visio.Masters Masters;

        //The document we will be working with
        public Visio.Document ActiveDoc;

        //Document Legend Page
        public Visio.Page LegendPage;

        //The page we will put shapes on
        //public Visio.Page ActivePage;

        public Visio.Master connectionMaster, missingJobMaster, fullShapeMaster, containerMaster;
        public Visio.VisAutoConnectDir connectParams = Visio.VisAutoConnectDir.visAutoConnectDirNone;
        public Visio.Selection selection;
        public Visio.Shape upstreamShape;
        public Visio.Shape downstreamShape;
        public Visio.Shape soloContainerShape;
        public Visio.Shape connectContainerShape;
        public Visio.Shape selectShape;
        public bool soloContainerExists = false;
        public bool connectContainerExists = false;
        public string section1Colour;
        public string section2Colour;
        public string section3Colour;
        public string externalJobColour;
        public int flowCode = MainForm.flowLayout;
        public VisioDrawer()
        {

            //These variable allow Visio to run quickly, and quietly
            //Defer Recalc needs to be set to 0 after processing is done
            VisApp = new Visio.InvisibleApp();
            VisApp.UndoEnabled = false;
            VisApp.LiveDynamics = false;
            VisApp.AutoLayout = false;
            VisApp.DeferRecalc = -1;
            VisApp.DeferRelationshipRecalc = true;

            //Open the page holding the master collection so we can use it
            string executingSource = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string executingFolder = System.IO.Path.GetDirectoryName(executingSource);

            //Add a new document - this becomes the active document
            //if we do not do this, we get throw an exception
            VisApp.Documents.Add(executingFolder + "\\Config\\VisioTemplate.vsd");


            MastersDocuments = VisApp.Documents;
            MasterDoc = MastersDocuments.OpenEx(executingFolder + "\\Config\\Stencil.vss", (short)Visio.VisOpenSaveArgs.visOpenHidden);

            //Now get a masters collection to use
            Masters = MasterDoc.Masters;

            //now get the active document
            ActiveDoc = VisApp.ActiveDocument;

            connectionMaster = GetMaster(@"Arrow");
            missingJobMaster = GetMaster(@"External Job");
            fullShapeMaster = GetMaster(@"FullShapeTemplate");
            containerMaster = GetMaster(@"Container");

            
        }

        public int GenerateShape(string field1, string field2, string field3, string jobName, bool soloJob, bool externalJob)
        {       
            #region Grouping Info
            //Info on selection. More available online
            /* Name         Value       Description
                * visDeselect  1           Cancels the selection of a shape but leaves the rest of the selection unchanged.
                * visSelect    2           Selects a shape but leaves the rest of the selection unchanged.
    `           */
            #endregion 

            //Deselect all shapes from previous run

            //"FullShapeJobSection" - Top Section
            //"FullShapeRectangleSection" - Middle Section
            //"FullShapeSection3" - Bottom Section

            Visio.Shape fullShape = VisApp.ActivePage.Drop(fullShapeMaster, 0.0, 0.0);

            //Determine Container
            if (soloJob == true)
            {

                if (soloContainerExists == false)
                {
                    soloContainerShape = VisApp.ActivePage.DropContainer(containerMaster, fullShape);

                    soloContainerExists = true;
                }
                else
                {
                    soloContainerShape.ContainerProperties.AddMember(fullShape, Visio.VisMemberAddOptions.visMemberAddExpandContainer);
                }
            }
            else
            {

                if (connectContainerExists == false)
                {
                    connectContainerShape = VisApp.ActivePage.DropContainer(containerMaster, fullShape);
                    connectContainerShape.Text = "Connected Job";
                    connectContainerExists = true;
                }
                else
                {
                    connectContainerShape.ContainerProperties.AddMember(fullShape, Visio.VisMemberAddOptions.visMemberAddExpandContainer);
                }
            }

            if (externalJob)
            {
                section1Colour = MainForm.externalJobColour;
                section2Colour = MainForm.externalJobColour;
                section3Colour = MainForm.externalJobColour;
            }
            else
            {
                section1Colour = MainForm.section1Colour;
                section2Colour = MainForm.section2Colour;
                section3Colour = MainForm.section3Colour;
            }

            fullShape.Shapes["FullShapeJobSection"].get_Cells("FillBkgnd").Formula = "=THEMEGUARD(RGB(" + section1Colour + "))";
            fullShape.Shapes["FullShapeJobSection"].get_Cells("FillForegnd").Formula = "=THEMEGUARD(RGB(" + section1Colour + "))";
            fullShape.Shapes["FullShapeRectangleSection"].get_Cells("FillBkgnd").Formula = "=THEMEGUARD(RGB(" + section2Colour + "))";
            fullShape.Shapes["FullShapeRectangleSection"].get_Cells("FillForegnd").Formula = "=THEMEGUARD(RGB(" + section2Colour + "))";
            fullShape.Shapes["FullShapeSection3"].get_Cells("FillBkgnd").Formula = "=THEMEGUARD(RGB(" + section3Colour + "))";
            fullShape.Shapes["FullShapeSection3"].get_Cells("FillForegnd").Formula = "=THEMEGUARD(RGB(" + section3Colour + "))";
            

            fullShape.Shapes["FullShapeJobSection"].Text = field1;
            fullShape.Shapes["FullShapeJobSection"].Name = "Section1 + " + jobName;

            fullShape.Shapes["FullShapeRectangleSection"].Text = field2;
            fullShape.Shapes["FullShapeRectangleSection"].Name = "Section2 + " + jobName;

            fullShape.Shapes["FullShapeSection3"].Text = field3;
            fullShape.Shapes["FullShapeSection3"].Name = "Section3 + " + jobName;

            //Configure Text
            ConfigureText(RGB: section1Colour, shape: fullShape.Shapes["Section1 + " + jobName]);
            ConfigureText(RGB: section2Colour, shape: fullShape.Shapes["Section2 + " + jobName]);
            ConfigureText(RGB: section3Colour, shape: fullShape.Shapes["Section3 + " + jobName]);


            fullShape.Name = jobName;
            Console.WriteLine("Added Job: " + jobName);

            return fullShape.ID;
        }

        public void ConnectShapes(int upstreamJob, int downstreamJob)
        {
            //Link jobs
           
            upstreamShape = this.VisApp.ActivePage.Shapes.get_ItemFromID(upstreamJob);
            downstreamShape = this.VisApp.ActivePage.Shapes.get_ItemFromID(downstreamJob);

            if (upstreamJob != downstreamJob)
            {
                upstreamShape.AutoConnect(downstreamShape, connectParams, connectionMaster);
            }
        }

        public Visio.Master GetMaster(string masterName)
        {
            return Masters.get_ItemU(masterName);
        }

        public void ArrangeGraph(Visio.Selection selection)
        {
            //Sets various organisation elements)
            // set 'PlaceStyle'

            /*Visio Layout Code
             * 1. Flowchart = 1
             * 2. Hierarchy = 17
             * 3. CompactTree = 7
             * 4. Circular = 6
             * 5. Radial = 3
             * */

            
            var placeStyleCell = VisApp.ActivePage.PageSheet.get_CellsSRC(
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowPageLayout,
                (short)Visio.VisCellIndices.visPLOPlaceStyle).ResultIU = flowCode; //Flow layout code goes here

            // set 'RouteStyle'
            var routeStyleCell = VisApp.ActivePage.PageSheet.get_CellsSRC(
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowPageLayout,
                (short)Visio.VisCellIndices.visPLORouteStyle).ResultIU = 5;
            // set 'PageShapeSplit'
            var pageShapeSplitCell = VisApp.ActivePage.PageSheet.get_CellsSRC(
                (short)Visio.VisSectionIndices.visSectionObject,
                (short)Visio.VisRowIndices.visRowPageLayout,
                (short)Visio.VisCellIndices.visPLOSplit).ResultIU = 1;

            if (selection.Count != 0)
            {
                selection.Layout();
            }
            
             
        }

        public void SelectShape(int shapeID)
        {
            
            selectShape = VisApp.ActivePage.Shapes.get_ItemFromID(shapeID);
            selection.Select(selectShape, 2);
        }

        public void Save(string outputLocation)
        {
            //Defer recalculation until the work is done 0 equals false.
            VisApp.Application.DeferRecalc = 0;

            string newDocPath = @outputLocation;
            //try to save the file.

            this.VisApp.ActiveDocument.SaveAsEx(newDocPath, (short)Microsoft.Office.Interop.Visio.VisOpenSaveArgs.visSaveAsListInMRU);
        }

        public static void ConfigureText(string RGB, Visio.Shape shape)
        {
            //Function checks if the colour is dark
            //in order to change the text colour
            
            string[] colourValues = RGB.Split(',');

            int sum = 0;
            foreach (string num in colourValues)
            {
                sum += Int32.Parse(num.ToString());
            }

            if (sum < 200)
            {
                shape.get_Cells("Char.Color").Formula = "=THEMEGUARD(RGB(255,255,255))";
            }

            //Reduce text size if too long
            if (shape.Text.Length > 28)
            {
                shape.get_Cells("Char.Size").Formula = "4 pt";
            }
           
        }
    }
}
