using System;
using System.Data;
using System.Collections.Generic;
using System.Xml;

namespace Control_M_Visio_Generator
{
    public enum TypeOfJob
    {
        External,
        Connected,
        Solo
    };
    public class JobInfo
    {
        public int JobID { get; set; }
        public int OnID { get; set; }
        public string Name { get; set; }
        //Fields 1-3 are user choice of job data
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string[] InConditions { get; set; }
        public string[] OutConditions { get; set; }
        public TypeOfJob JobType { get; set; }
        public int ShapeID { get; set; }
    }
    public class ConditionInfo
    {
        public string Condition { get; set; }
        public HashSet<int> IssuingConditions { get; set; }
        public HashSet<int> ReceivingConditions { get; set; }
    }
    public class JobList : Dictionary<int, JobInfo>
    {
        public void AddJob(JobInfo job)
        {
            Add(job.JobID, job);
        }

    }
    public class ConditionList : Dictionary<string, ConditionInfo>
    {
        public void AddToTable(string condition, int jobID, string type)
        {
            ConditionInfo conditionInfo = new ConditionInfo();
            bool newEntry = false;
            if (!(this.ContainsKey(condition)))
            {                   
                conditionInfo.Condition = condition;
                conditionInfo.IssuingConditions = new HashSet<int>();
                conditionInfo.ReceivingConditions = new HashSet<int>();
                newEntry = true;
            }
            else
            {
                conditionInfo = this[condition];                  
            }


            if (type == "INCOND")
            {                 
                conditionInfo.ReceivingConditions.Add(jobID);
            }
            else
            {
                conditionInfo.IssuingConditions.Add(jobID);
            }
            if (newEntry)
            {
                this.Add(key: condition, value: conditionInfo);
            }
                
        }
    }
    

    public partial class GenerateVisio
    {
        public DataSet xmlDataSet = MainForm.xmlDataSet;
        public string xmlPath = MainForm.xmlPath;
        public ConditionList conditionList = new ConditionList();
        public static JobList externalJobList;
        public string outputLocation = MainForm.outputLocation;
        public string fileName = MainForm.fileName;
        public bool soloJob = false;
        public VisioDrawer Drawer;
        public JobList jobList;
        public string section1 = MainForm.section1;
        public string section2 = MainForm.section2;
        public string section3 = MainForm.section3;

        //Constructor
        public GenerateVisio()
        {
            
        }
        public static string GetConditionJobName(string cond, string type)
        {

            /*
             * ------------------
             * Standard condition
             * ------------------
             * ConditionA-ConditionB
             * 
             * ------------------
             *Old Standard
             *------------------
             *ConditionA-TO-ConditionB
             *
             *------------------
             *Non-standard
             *------------------
             *Anything else!
             */

            bool newStandard = true;
            string condName = cond;

            //Remove maybe condition if required
            int condPlace = condName.IndexOf("-");
            if (condPlace == 1)
            {
                condName = condName.Remove(0, condPlace + 1);
            }

            int hyphenCount = 0;
            foreach (char c in condName)
            {
                if (c == '-')
                    hyphenCount++;
            }
            //If this name contains more than two Hyphens, it is not standard and should not be parsed.
            if (hyphenCount > 2)
            {
                //Return the original string
                return cond;
            }

            //Take away the " characters
            string[] jobNames = condName.Split('-');

            //Find out if this is a old standard condition
            if ((condName.Contains("-TO-")) || ((condName.Contains("-to-"))))
            {
                newStandard = false;
            }

            //If this is an in-condition
            if (type == "INCOND")
            {
                condName = jobNames[0];
                return condName = jobNames[0];
            }

            //If this is an Out-Condition
            if (type == "OUTCOND")
            {
                //If the condition is old standard
                if (newStandard == false)
                {
                    //inCond-to-outCond
                    condName = jobNames[2];
                }
                else
                {
                    //inCond - outCond
                    if (jobNames.Length == 2)
                    {
                        condName = jobNames[1];
                    }
                    else
                    {
                        //If it gets to this, it's non-standard, and weird.
                        condName = jobNames[0];
                    }
                        
                }
            }

            return condName;
        }

        
        public string[] GatherConditions(int num, string table)
        {
            List<string> list = new List<String>();
            int conditionJobID;

            foreach (DataRow row in xmlDataSet.Tables[table].Rows)
            {
                conditionJobID = Int32.Parse(row["JOB_Id"].ToString());

                //Get the condition if the JobID's match
                if (num == conditionJobID)
                {
                    list.Add(row["NAME"].ToString());

                    //Append Conditions table
                    conditionList.AddToTable(condition: row["NAME"].ToString(), jobID: num, type: table);

                }
                //If the table jobID is higher than our parameter ID,
                //There are no more conditions, and return.
                if (num < conditionJobID)
                {
                    break;
                }
            }
            //Migrate all conditions to string array
            string[] conditionArray = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                conditionArray[i] = list[i];
            }
            return conditionArray;
        }

        public void CleanTables(string tableName)
        {
            DataTable table = xmlDataSet.Tables[tableName];
            for (int i = table.Rows.Count - 1; i > 0; i--)
            {
                //Remove DEL Condition
                if (table.Columns.Contains("SIGN") && table.Rows[i]["SIGN"].ToString() == "DEL" || table.Columns.Contains("SIGN") && table.Rows[i]["SIGN"].ToString() == "-")
                {
                    table.Rows[i].Delete();
                }
                //Remove Smart Condition
                else if (table.Rows[i]["JOB_Id"] == DBNull.Value)
                {
                    table.Rows[i].Delete();
                }
            }
        }

        public int HandleExternalJobs(string type, string condition)
        {
            string jobName = GetConditionJobName(cond: condition, type: type);

            foreach (KeyValuePair<int, JobInfo> jobentry in jobList)
            {
                //If the job already exists, no need to make it.
                if (jobentry.Value.Name == jobName)
                {                     
                    return jobentry.Value.JobID;    
                }
            }

            //If this runs, then no existing entry was found.
            JobInfo externalJob = new JobInfo
            {
                Name = jobName,
                Field1 = jobName,
                Field2 = condition,
                Field3 = "External Job",
                JobType = TypeOfJob.External,

                ShapeID = Drawer.GenerateShape(
                field1: jobName,
                field2: condition,
                field3: "External",
                jobName: jobName,
                soloJob: false,
                externalJob: true),
            };
            externalJob.JobID = jobList.Count;
            jobList.AddJob(externalJob);
            MainForm._appendForm.AppendTextBox(externalJob.Name + " added");
            return externalJob.JobID;
            

        }
        public void GenerateVisioDiagram()
        {
                MainForm._appendForm.disableAllControls();
                //Set a start time for entire process
                DateTime startTime = DateTime.Now;

                MainForm._appendForm.AppendTextBox("----------------------------------------------------------------------------");
                MainForm._appendForm.AppendTextBox("Begin generating " + fileName + " Visio Diagram");
                MainForm._appendForm.AppendTextBox("----------------------------------------------------------------------------");

                Drawer = new VisioDrawer();
                //XML reader settings to avoid DTD in XML file
                //XML will not be read otherwise
                XmlDocument xmlDoc = new XmlDocument();
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.XmlResolver = null;
                settings.DtdProcessing = DtdProcessing.Ignore;
                XmlReader xmlDataSetReader = XmlReader.Create(xmlPath, settings);
                xmlDataSet = new DataSet();
                xmlDataSet.ReadXml(xmlDataSetReader, XmlReadMode.Auto);

                //Remove Delete conditions from tables
                //Remove Smart Table conditions
                if (xmlDataSet.Tables.Contains("OUTCOND"))
                {
                    CleanTables(tableName: "OUTCOND");
                }
                if (xmlDataSet.Tables.Contains("INCOND"))
                {
                    CleanTables(tableName: "INCOND");
                }

                //Build Job, and Condition List
                /* Notes:
                 * - Build dictionary of jobs with relevant details
                 * - Build dictionary of Out-Condition Master list
                 * - Build dictionary of In-Condition Master list
                 * 
                 */
                JobInfo job;
                jobList = new JobList();

                for (int i = 0; i < xmlDataSet.Tables["JOB"].Rows.Count; i++)
                {
                    job = new JobInfo
                    {
                        Name = xmlDataSet.Tables["JOB"].Rows[i]["JOBNAME"].ToString(),
                        Field1 = xmlDataSet.Tables["JOB"].Rows[i][section1].ToString(),
                        Field2 = xmlDataSet.Tables["JOB"].Rows[i][section2].ToString(),
                        Field3 = xmlDataSet.Tables["JOB"].Rows[i][section3].ToString(),
                        JobID = Int32.Parse(xmlDataSet.Tables["JOB"].Rows[i]["JOB_Id"].ToString()),
                        JobType = TypeOfJob.Connected,
                        InConditions = GatherConditions(num: i, table: "INCOND"),
                        OutConditions = GatherConditions(num: i, table: "OUTCOND"),
                    };

                    //Add to Solo job container
                    if ((job.InConditions.Length == 0) && (job.OutConditions.Length == 0))
                    {
                        soloJob = true;
                        job.JobType = TypeOfJob.Solo;
                    }

                    job.ShapeID = Drawer.GenerateShape(
                        field1: job.Field1,
                        field2: job.Field2,
                        field3: job.Field3,
                        jobName: job.Name,
                        soloJob: soloJob,
                        externalJob: false
                        );

                    jobList.AddJob(job);
                    MainForm._appendForm.AppendTextBox(job.Name + " added");
                    soloJob = false;
                }

                //---------------------------------
                //External Jobs
                //---------------------------------
                if (MainForm.externalJobs == true)
                {
                    externalJobList = new JobList();

                    //Find the entries with no in/out conditions, and generate jobs
                    foreach (KeyValuePair<string, ConditionInfo> entry in conditionList)
                    {
                        //In Conditions
                        if (entry.Value.IssuingConditions.Count == 0)
                        {
                            entry.Value.IssuingConditions.Add(HandleExternalJobs(type: "INCOND", condition: entry.Value.Condition));
                        }
                        //Out Conditions
                        if (entry.Value.ReceivingConditions.Count == 0)
                        {
                            entry.Value.ReceivingConditions.Add(HandleExternalJobs(type: "OUTCOND", condition: entry.Value.Condition));
                        }
                    }
                }
                //---------------------------------
                //Connect all jobs in Visio Diagram
                //---------------------------------

                //Condition Table loop
                foreach (KeyValuePair<string, ConditionInfo> entry in conditionList)
                {
                    //Out Condition loop        
                    foreach (var upStreamItem in entry.Value.IssuingConditions)
                    {
                        var upstreamJob = jobList[upStreamItem];
                        int upstreamShape = upstreamJob.ShapeID;

                        //In condition loop
                        foreach (var downstreamItem in entry.Value.ReceivingConditions)
                        {
                            var downstreamJob = jobList[downstreamItem];
                            int downstreamShape = downstreamJob.ShapeID;

                            Drawer.ConnectShapes(upstreamJob: upstreamShape, downstreamJob: downstreamShape);
                            MainForm._appendForm.AppendTextBox("Linked " + upstreamJob.Name + " to " + downstreamJob.Name);
                        }
                    }
                }

                MainForm._appendForm.AppendTextBox("Arranging Layout...");

                Drawer.selection = Drawer.VisApp.ActiveWindow.Selection;

                //Organise connected jobs
                //Organise solo jobs
                foreach (KeyValuePair<int, JobInfo> selectJob in jobList)
                {
                    if (selectJob.Value.JobType == TypeOfJob.Connected)
                    {
                        Drawer.selectShape = Drawer.VisApp.ActivePage.Shapes.get_ItemFromID(selectJob.Value.ShapeID);
                        Drawer.selection.Select(Drawer.selectShape, 2);

                    }
                }
                Drawer.ArrangeGraph(selection: Drawer.selection);
                Drawer.selection.DeselectAll();

                //Organise solo jobs
                foreach (KeyValuePair<int, JobInfo> selectJob in jobList)
                {
                    if (selectJob.Value.JobType == TypeOfJob.Solo)
                    {
                        Drawer.selectShape = Drawer.VisApp.ActivePage.Shapes.get_ItemFromID(selectJob.Value.ShapeID);
                        Drawer.selection.Select(Drawer.selectShape, 2);

                    }
                }

                //Solo Jobs
                Drawer.ArrangeGraph(selection: Drawer.selection);
                Drawer.selection.DeselectAll();



                //Save Document, and Organise the document shapes
                MainForm._appendForm.AppendTextBox("Resizing Document...");
                Drawer.VisApp.ActivePage.ResizeToFitContents();

                MainForm._appendForm.AppendTextBox("Saving Document...");
                Drawer.Save(outputLocation);
                Drawer.ActiveDoc.Close();


                //Calculate run time
                DateTime endTime = DateTime.Now;
                TimeSpan span = endTime.Subtract(startTime);

                MainForm._appendForm.AppendTextBox("Complete. Saving, and closing Visio.");
                MainForm._appendForm.AppendTextBox("----------------------------------------------------------------------------");
                MainForm._appendForm.AppendTextBox("Completed generating " + fileName + " Visio Diagram");
                MainForm._appendForm.AppendTextBox("----------------------------------------------------------------------------");
                MainForm._appendForm.AppendTextBox("");
                MainForm._appendForm.AppendTextBox("Document saved at: " + outputLocation);
                MainForm._appendForm.AppendTextBox("Run Time = " + span);

                MainForm._appendForm.disableAllControls();
                
            
        }
    }
}
