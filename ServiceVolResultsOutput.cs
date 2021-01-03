using System;
using System.Collections.Generic;
using System.IO;


namespace HCMCalc_UrbanStreets
{
    public class ServiceVolResultsOutput
    {

        /*public static void WriteServiceVolResultsData(string filename, List<ServiceVolumes> serVolResults)
        {
            try
            {
                //bool DoesFileCurrentlyExist = File.Exists(filename);

                StreamWriter swLinkResults = new StreamWriter(filename, true);

                /*if (DoesFileCurrentlyExist == false)
                {
                    //WriteLinkResultsDataHeader(Filename);
                    swLinkResults.Write("Scenario, SubScenario, Replication, Link Id, Link Type, Density (veh/mi/ln), Avg. Speed (mi/h), Avg. Travel Time (s), Accel Noise-Mean Zero (ft/s2), Accel Noise-Mean NonZero (ft/s2), Flow Rate (veh/h/ln), Num PC, % PC, Num SUT, % SUT, Num IMST");
                    swLinkResults.WriteLine();
                }*

                swLinkResults.Write(",,Volumes by LOS");
                if (serVolResults.Count > 1)
                    swLinkResults.Write(",,,,,,,% Difference Between Ranges,,,,,");
                swLinkResults.WriteLine();
                if (serVolResults.Count > 1)
                    swLinkResults.Write(serVolResults[0].TestParameterLabel + ",");
                swLinkResults.Write("Lanes,");
                swLinkResults.Write("LOS A,");
                swLinkResults.Write("LOS B,");
                swLinkResults.Write("LOS C,");
                swLinkResults.Write("LOS D,");
                swLinkResults.Write("LOS E,");
                swLinkResults.Write(",");
                if (serVolResults.Count > 1)
                {
                    swLinkResults.Write("Range,");
                    swLinkResults.Write("Lanes,");
                    swLinkResults.Write("LOS A,");
                    swLinkResults.Write("LOS B,");
                    swLinkResults.Write("LOS C,");
                    swLinkResults.Write("LOS D,");
                    swLinkResults.Write("LOS E,");
                }
                swLinkResults.Write(",Legend");
                swLinkResults.WriteLine();

                foreach (ServiceVolumes serVolResult in serVolResults)
                {
                    for (int lanes = 0; lanes < 5; lanes++)
                    {
                        // Service Volumes by LOS
                        if (serVolResult.TestParameterLabel != "None")
                            swLinkResults.Write(Math.Round(serVolResult.TestParameterValue,2) + ",");
                        else if (serVolResults.Count > 1)
                            swLinkResults.Write(",");
                        swLinkResults.Write((lanes+1) + ",");
                        for (int LOSLevel = 0; LOSLevel < 5; LOSLevel++)
                        {
                            if (serVolResult.PkDirVol[lanes, LOSLevel] >= 0)
                                swLinkResults.Write(serVolResult.PkDirVol[lanes, LOSLevel] + ",");
                            else if (serVolResult.PkDirVol[lanes, LOSLevel] == -1)
                                swLinkResults.Write("a*,");
                            else if (serVolResult.PkDirVol[lanes, LOSLevel] == -2)
                                swLinkResults.Write("b*,");
                            else if (serVolResult.PkDirVol[lanes, LOSLevel] == -3)
                                swLinkResults.Write("c*,");
                        }
                        swLinkResults.Write(",");

                        // % Difference Between Highest and Lowest Values
                        if (serVolResults.IndexOf(serVolResult) == 0 && serVolResults.Count > 1)
                        {
                            if (serVolResults.Count > 1)
                                swLinkResults.Write("Min to Max,");
                            swLinkResults.Write((lanes + 1) + ",");
                            for (int LOSLevel = 0; LOSLevel < 5; LOSLevel++)
                            {
                                float lowestValue = 9999999;
                                float highestValue = 0;
                                foreach (ServiceVolumes serVolResultCheckValues in serVolResults)
                                {
                                    if (serVolResultCheckValues.PkDirVol[lanes, LOSLevel] > 0)
                                    {
                                        lowestValue = Math.Min(serVolResultCheckValues.PkDirVol[lanes, LOSLevel], lowestValue);
                                        highestValue = Math.Max(serVolResultCheckValues.PkDirVol[lanes, LOSLevel], highestValue);
                                    }
                                }
                                if (highestValue == 0 && lowestValue == 9999999)
                                    swLinkResults.Write("N/A,");
                                else
                                    swLinkResults.Write(Math.Round((highestValue - lowestValue) * 100 / lowestValue, 2) + "%,");
                            }
                        }
                        else if (serVolResults.IndexOf(serVolResult) > 0) // % Difference Between Bound Iterations
                        {
                            if (serVolResults.Count > 1)
                                swLinkResults.Write(Math.Round(serVolResults[serVolResults.IndexOf(serVolResult) - 1].TestParameterValue,2) + " to " + Math.Round(serVolResult.TestParameterValue,2) + ",");
                            swLinkResults.Write((lanes + 1) + ",");
                            for (int LOSLevel = 0; LOSLevel < 5; LOSLevel++)
                            {
                                if (serVolResults.IndexOf(serVolResult) == 0)
                                    swLinkResults.Write("N/A,");
                                else
                                {
                                    if (serVolResult.PkDirVol[lanes, LOSLevel] > 0 && serVolResults[serVolResults.IndexOf(serVolResult) - 1].PkDirVol[lanes, LOSLevel] > 0)
                                        swLinkResults.Write(Math.Round((float)(serVolResult.PkDirVol[lanes, LOSLevel] - serVolResults[serVolResults.IndexOf(serVolResult) - 1].PkDirVol[lanes, LOSLevel]) * 100 / (float)serVolResults[serVolResults.IndexOf(serVolResult) - 1].PkDirVol[lanes, LOSLevel], 2) + "%,");
                                    else
                                        swLinkResults.Write("N/A,");
                                }
                            }
                            
                        }
                        if (serVolResults.IndexOf(serVolResult)==0)
                        {
                            if (lanes == 0)
                                swLinkResults.Write(",a*,LOS Level cannot be achieved");
                            else if (lanes == 1)
                                swLinkResults.Write(",b*,Service volume is constrained by capacity");
                            else if (lanes == 2)
                                swLinkResults.Write(",c*,Service volume same for previous LOS level");
                        }
                        swLinkResults.WriteLine();
                    }
                }
                swLinkResults.Close();
            }
            catch //(IOException ex)
            {
                //ex.Message
            }
        }*/

        public static void WriteLOSHeaders(StreamWriter swLinkResults, ServiceVolumes serVolResult, List<ServiceVolumes> serVolResults)
        {
            swLinkResults.WriteLine();

            if (serVolResults[0].TestParameterLabel != "None")
                swLinkResults.Write(",");
            swLinkResults.Write(serVolResult.SerVolAreaType);
            if (serVolResult.SerVolAreaType != AreaType.RuralDeveloped)
                swLinkResults.Write(",," + serVolResult.Class + ",,");
            else
                swLinkResults.Write(",,,,");
            swLinkResults.Write("Peak Hour Dir. Vol,,,");

            if (serVolResults[0].TestParameterLabel != "None")
                swLinkResults.Write(",");
            swLinkResults.Write(serVolResult.SerVolAreaType);
            if (serVolResult.SerVolAreaType != AreaType.RuralDeveloped)
                swLinkResults.Write(",," + serVolResult.Class + ",,");
            else
                swLinkResults.Write(",,,,");
            swLinkResults.Write("Peak Hour Both. Vol,,,");

            if (serVolResults[0].TestParameterLabel != "None")
                swLinkResults.Write(",");
            swLinkResults.Write(serVolResult.SerVolAreaType);
            if (serVolResult.SerVolAreaType != AreaType.RuralDeveloped)
                swLinkResults.Write(",," + serVolResult.Class + ",,");
            else
                swLinkResults.Write(",,,,");
            swLinkResults.Write("AADT");
            if (serVolResult.TestParameterLabel != "None")
            {
                swLinkResults.Write(",,,," + serVolResult.SerVolAreaType);
                if (serVolResult.SerVolAreaType != AreaType.RuralDeveloped)
                    swLinkResults.Write(",," + serVolResult.Class + ",,");
                else
                    swLinkResults.Write(",,,,");
                swLinkResults.Write("% Difference Pk. Dir");
            }
            swLinkResults.WriteLine();
            if (serVolResults[0].TestParameterLabel != "None")
                swLinkResults.Write(serVolResults[0].TestParameterLabel + ",");
            swLinkResults.Write("Lanes,");
            swLinkResults.Write("LOS A,");
            swLinkResults.Write("LOS B,");
            swLinkResults.Write("LOS C,");
            swLinkResults.Write("LOS D,");
            swLinkResults.Write("LOS E,");
            swLinkResults.Write(",");
            if (serVolResults[0].TestParameterLabel != "None")
                swLinkResults.Write(serVolResults[0].TestParameterLabel + ",");
            swLinkResults.Write("Lanes,");
            swLinkResults.Write("LOS A,");
            swLinkResults.Write("LOS B,");
            swLinkResults.Write("LOS C,");
            swLinkResults.Write("LOS D,");
            swLinkResults.Write("LOS E,");
            swLinkResults.Write(",");
            if (serVolResults[0].TestParameterLabel != "None")
                swLinkResults.Write(serVolResults[0].TestParameterLabel + ",");
            swLinkResults.Write("Lanes,");
            swLinkResults.Write("LOS A,");
            swLinkResults.Write("LOS B,");
            swLinkResults.Write("LOS C,");
            swLinkResults.Write("LOS D,");
            swLinkResults.Write("LOS E,");
            if (serVolResult.TestParameterLabel != "None")
            {
                swLinkResults.Write(",");
                swLinkResults.Write("Range,");
                swLinkResults.Write("Lanes,");
                swLinkResults.Write("LOS A,");
                swLinkResults.Write("LOS B,");
                swLinkResults.Write("LOS C,");
                swLinkResults.Write("LOS D,");
                swLinkResults.Write("LOS E,");
            }
            swLinkResults.WriteLine();
        }

        public static void WriteSerVolValues(StreamWriter swLinkResults, int[,] analysisArray, int lanes, String testLabel, float testValue)
        {
            if (testLabel != "None")
                swLinkResults.Write(Math.Round(testValue, 2) + ",");
            if (lanes < 4)
                swLinkResults.Write((lanes + 1) + ",");
            else
                swLinkResults.Write("*,");

            for (int LOSLevel = 0; LOSLevel < 5; LOSLevel++)
            {
                if (analysisArray[lanes, LOSLevel] >= 0)
                    swLinkResults.Write(analysisArray[lanes, LOSLevel] + ",");
                else if (analysisArray[lanes, LOSLevel] == -1)
                    swLinkResults.Write("a*,");
                else if (analysisArray[lanes, LOSLevel] == -2)
                    swLinkResults.Write("b*,");
                else if (analysisArray[lanes, LOSLevel] == -3)
                    swLinkResults.Write("c*,");
            }
            swLinkResults.Write(",");
        }

        public static void WriteServiceVolResultsDataNew(string filename, List<ServiceVolumes> serVolResults, int numberOfTestParmsPerTable)
        {
            try
            {
                if (File.Exists(filename) == true)
                    File.Delete(filename);

                StreamWriter swLinkResults = new StreamWriter(filename, true);

                /*swLinkResults.Write(",,Volumes by LOS");
                if (serVolResults[0].TestParameterLabel != "None")
                    swLinkResults.Write(",,,,,,,% Difference Between Ranges,,,,,");
                swLinkResults.WriteLine();*/

                foreach (ServiceVolumes serVolResult in serVolResults)
                {
                    if (serVolResults.IndexOf(serVolResult) == 0)
                        WriteLOSHeaders(swLinkResults, serVolResult, serVolResults);
                    else if (serVolResults.IndexOf(serVolResult) > 0)
                        if (serVolResult.Class != serVolResults[serVolResults.IndexOf(serVolResult) - 1].Class || serVolResult.SerVolAreaType != serVolResults[serVolResults.IndexOf(serVolResult) - 1].SerVolAreaType)
                            WriteLOSHeaders(swLinkResults, serVolResult, serVolResults);

                    for (int lanes = 0; lanes < 5; lanes++)
                    {
                        WriteSerVolValues(swLinkResults, serVolResult.PkDirVol, lanes, serVolResult.TestParameterLabel, serVolResult.TestParameterValue);
                        WriteSerVolValues(swLinkResults, serVolResult.BothDirVol, lanes, serVolResult.TestParameterLabel, serVolResult.TestParameterValue);
                        WriteSerVolValues(swLinkResults, serVolResult.AADT, lanes, serVolResult.TestParameterLabel, serVolResult.TestParameterValue);

                        // % Difference Between Highest and Lowest Values
                        if (serVolResults.IndexOf(serVolResult) % numberOfTestParmsPerTable == 0 && serVolResult.TestParameterLabel != "None")
                        {
                            swLinkResults.Write("Min to Max,");
                            if (lanes < 4)
                                swLinkResults.Write((lanes + 1) + ",");
                            else
                                swLinkResults.Write("*,");
                            for (int LOSLevel = 0; LOSLevel < 5; LOSLevel++)
                            {
                                float lowestValue = 9999999;
                                float highestValue = 0;
                                foreach (ServiceVolumes serVolResultCheckValues in serVolResults)
                                {
                                    if (serVolResultCheckValues.PkDirVol[lanes, LOSLevel] > 0)
                                    {
                                        lowestValue = Math.Min(serVolResultCheckValues.PkDirVol[lanes, LOSLevel], lowestValue);
                                        highestValue = Math.Max(serVolResultCheckValues.PkDirVol[lanes, LOSLevel], highestValue);
                                    }
                                }
                                if (highestValue == 0 && lowestValue == 9999999)
                                    swLinkResults.Write("N/A,");
                                else
                                    swLinkResults.Write(Math.Round((highestValue - lowestValue) * 100 / lowestValue, 2) + "%,");
                            }
                            swLinkResults.Write(",");
                        }
                        else if (serVolResults.IndexOf(serVolResult) > 0 && serVolResult.TestParameterLabel != "None") // % Difference Between Bound Iterations
                        {
                            swLinkResults.Write(Math.Round(serVolResults[serVolResults.IndexOf(serVolResult) - 1].TestParameterValue, 2) + " to " + Math.Round(serVolResult.TestParameterValue, 2) + ",");
                            if (lanes < 4)
                                swLinkResults.Write((lanes + 1) + ",");
                            else
                                swLinkResults.Write("*,");
                            for (int LOSLevel = 0; LOSLevel < 5; LOSLevel++)
                            {
                                if (serVolResults.IndexOf(serVolResult) == 0)
                                    swLinkResults.Write("N/A,");
                                else
                                {
                                    if (serVolResult.PkDirVol[lanes, LOSLevel] > 0 && serVolResults[serVolResults.IndexOf(serVolResult) - 1].PkDirVol[lanes, LOSLevel] > 0)
                                        swLinkResults.Write(Math.Round((float)(serVolResult.PkDirVol[lanes, LOSLevel] - serVolResults[serVolResults.IndexOf(serVolResult) - 1].PkDirVol[lanes, LOSLevel]) * 100 / (float)serVolResults[serVolResults.IndexOf(serVolResult) - 1].PkDirVol[lanes, LOSLevel], 2) + "%,");
                                    else
                                        swLinkResults.Write("N/A,");
                                }
                            }
                            swLinkResults.Write(",");
                        }
                        if (serVolResults.IndexOf(serVolResult) == 0)
                        {
                            if (lanes == 0)
                                swLinkResults.Write("Legend");
                            else if (lanes == 1)
                                swLinkResults.Write("a*,LOS Level cannot be achieved");
                            else if (lanes == 2)
                                swLinkResults.Write("b*,Service volume is constrained by capacity");
                            else if (lanes == 3)
                                swLinkResults.Write("c*,Service volume same for previous LOS level");
                        }
                        swLinkResults.WriteLine();
                    }
                }
                swLinkResults.Close();
            }
            catch //(IOException ex)
            {
                //ex.Message
            }
        }
    }
}
