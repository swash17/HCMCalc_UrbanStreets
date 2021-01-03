using System;
using System.Collections.Generic;


namespace HCMCalc_UrbanStreets
{
    class CalcsServiceVolumes
    {

        public static List<SerVolTablesByClass> WriteFDOTServiceVolInputFile()
        {
            List<SerVolTablesByClass> SerVolTables = new List<SerVolTablesByClass>();
            string Filename = "FDOTServiceVolsInputData.xml";

            foreach (AreaType areaType in Enum.GetValues(typeof(AreaType)))
            {
                if (areaType == AreaType.Transitioning || areaType == AreaType.LargeUrbanized || areaType == AreaType.RuralDeveloped)
                {
                    SerVolTablesByClass TablesForClass = new SerVolTablesByClass();
                    foreach (ArterialClass artClass in Enum.GetValues(typeof(ArterialClass)))
                    {
                        if (!(areaType == AreaType.RuralDeveloped && artClass == ArterialClass.Class_II)) // Ignore Class II Rural since it doesn't exist
                        {
                            TablesForClass.SerVolTablesMultiLane.Add(CreateArterial_FDOTSerVols.CreateServiceVolTableFDOT(areaType, artClass));
                        }
                    }
                    TablesForClass.ArtAreaType = areaType;
                    SerVolTables.Add(TablesForClass);
                }
            }
            FileIOserviceVols.SerializeServiceVolInputData(Filename, SerVolTables);
            return SerVolTables;
        }

        /// <summary>
        /// Creates the Service Volumes for an arterial based on input parameters.
        /// </summary>
        /// <param name="Project"></param>
        /// <param name="SerVol"></param>
        /// <param name="inputsOneLane"></param>
        /// <param name="inputsMultiLane"></param>
        /*public static void ServiceVolsAuto(ProjectData Project, ServiceVolumes SerVol, ServiceVolumeArterialInputs
         * inputsOneLane, ServiceVolumeArterialInputs inputsMultiLane)
        {

            float NumberOfLoops = 5 * 5;  //This accounts for LOS and Lanes, does not account for volume increments; Type is float just to avoid extra cast in calculations below
            int LoopCounter = 0;
            frmMain.bgwRunServiceVolsCalcs.ReportProgress(0);

            ArterialData Arterial = new ArterialData();
            for (int lanes = 0; lanes < 5; lanes++) // 2,4,6,8,*
            {
                int vol = 40; // Starting volume (volume entering upstream of first intersection)
                bool threshExceeded = false;

                // Variable access points

                ServiceVolumeArterialInputs inputsForArterialCreation = inputsOneLane;
                if (lanes > 0)
                    inputsForArterialCreation = inputsMultiLane;

                if (lanes != 4)
                    Arterial = CreateArterial_FDOTSerVols.NewArterial(inputsForArterialCreation, vol, lanes + 1);
                else
                    Arterial = CreateArterial_FDOTSerVols.NewArterial(inputsForArterialCreation, vol);

                for (int LOSlevel = 0; LOSlevel < 5; LOSlevel++) // LOS A-E
                {
                    LoopCounter++;

                    int[] ServiceVolsArray;
                    do
                    {
                        CreateArterial_FDOTSerVols.ChangeArterialVolume(ref Arterial, vol);
                        Arterial.TestSerVol = vol;
                        CalcsArterial.CalcResults(Project, ref Arterial);

                        if (!Arterial.OverCapacity)
                        {
                            // Check whether LOS speed threshold has been exceeded
                            if (Arterial.Results.AverageSpeed < Arterial.Thresholds.Speed[LOSlevel])  // Avg. speed below LOS threshold speed
                            {
                                vol -= 10;
                                threshExceeded = true;
                                if (vol <= 0)
                                {
                                    vol = 10;
                                    SerVol.PkDirVol[lanes, LOSlevel] = -1;
                                    SerVol.BothDirVol[lanes, LOSlevel] = -1;
                                    SerVol.AADT[lanes, LOSlevel] = -1;
                                    SerVol.Found[lanes, LOSlevel] = true;
                                }
                            }
                            else
                            {
                                if (threshExceeded == false) // Threshold not exceeded, increment volume
                                {
                                    if ((Arterial.Results.AverageSpeed - Arterial.Thresholds.Speed[LOSlevel]) > 5)
                                        vol += (30 * (lanes + 1) * (LOSlevel + 1));
                                    else
                                        vol += 10;
                                }
                                else // Threshold exceeded, record volume for LOS level
                                {
                                    if (LOSlevel > 0)
                                        ServiceVolsArray = SetServiceVolumes(vol, Project.Period, Arterial.Dfactor, Arterial.Kfactor, LOSlevel, SerVol.PkDirVol[lanes, LOSlevel - 1]);
                                    else
                                        ServiceVolsArray = SetServiceVolumes(vol, Project.Period, Arterial.Dfactor, Arterial.Kfactor, LOSlevel, -1);
                                    SerVol.PkDirVol[lanes, LOSlevel] = ServiceVolsArray[0];
                                    SerVol.BothDirVol[lanes, LOSlevel] = ServiceVolsArray[1];
                                    SerVol.AADT[lanes, LOSlevel] = ServiceVolsArray[2];
                                    SerVol.Found[lanes, LOSlevel] = true;
                                    vol += 10; // Increment volume for start of next LOS level
                                    threshExceeded = false;
                                }
                            }
                        }
                        else  // Check to see if there is a constraining intersection due to (v/c * PHF) > 1
                        {
                            vol -= 10; // Return to the previous non-capacity-constrained volume
                            for (int capCheckLOSlevel = LOSlevel + 1; capCheckLOSlevel < 5; capCheckLOSlevel++) // LOS A-E
                            {
                                SerVol.PkDirVol[lanes, capCheckLOSlevel] = -2;
                                SerVol.BothDirVol[lanes, capCheckLOSlevel] = -2;
                                SerVol.AADT[lanes, capCheckLOSlevel] = -2;
                                SerVol.Found[lanes, capCheckLOSlevel] = true;
                            }
                            if (LOSlevel > 0)
                                ServiceVolsArray = SetServiceVolumes(vol, Project.Period, Arterial.Dfactor, Arterial.Kfactor, LOSlevel, SerVol.PkDirVol[lanes, LOSlevel - 1]);
                            else
                                ServiceVolsArray = SetServiceVolumes(vol, Project.Period, Arterial.Dfactor, Arterial.Kfactor, LOSlevel, -1);
                            SerVol.PkDirVol[lanes, LOSlevel] = ServiceVolsArray[0];
                            SerVol.BothDirVol[lanes, LOSlevel] = ServiceVolsArray[1];
                            SerVol.AADT[lanes, LOSlevel] = ServiceVolsArray[2];
                            SerVol.Found[lanes, LOSlevel] = true;
                        }
                    }
                    while (SerVol.Found[lanes, LOSlevel] == false);     //loop until service vol reached for given LOS
                    if (Arterial.OverCapacity)
                    {
                        LoopCounter = 5 * (lanes + 1);
                        frmMain.bgwRunServiceVolsCalcs.ReportProgress((int)(Math.Round(LoopCounter / NumberOfLoops * 100, 0, MidpointRounding.AwayFromZero)));
                        break;
                    }
                    frmMain.bgwRunServiceVolsCalcs.ReportProgress((int)(Math.Round(LoopCounter / NumberOfLoops * 100, 0, MidpointRounding.AwayFromZero)));
                } // BP here to view service volumes by LOS
            } // BP here to view service volumes by number of lanes
        } // BP here to view all service volumes*/

        public static void ServiceVolsAutoNew(ProjectData Project, ServiceVolumes SerVol, ServiceVolumeTableFDOT inputsOneLane, ServiceVolumeTableFDOT inputsMultiLane)
        {

            float NumberOfLoops = 5 * 5;  //This accounts for LOS and Lanes, does not account for volume increments; Type is float just to avoid extra cast in calculations below
            int LoopCounter = 0;
            frmMain.bgwRunServiceVolsCalcs.ReportProgress(0);

            ArterialData Arterial = new ArterialData();
            for (int lanes = 0; lanes < 5; lanes++) // 2,4,6,8,*
            {
                int vol = 40; // Starting volume (volume entering upstream of first intersection)
                bool threshExceeded = false;

                // Variable access points

                ServiceVolumeTableFDOT inputsForArterialCreation = inputsOneLane;
                if (lanes > 0)
                    inputsForArterialCreation = inputsMultiLane;

                if (lanes != 4)
                    Arterial = CreateArterial_FDOTSerVols.NewArterial(inputsForArterialCreation, Project.AnalMode, vol, lanes + 1);
                else
                    Arterial = CreateArterial_FDOTSerVols.NewArterial(inputsForArterialCreation, Project.AnalMode, vol);

                for (int LOSlevel = 0; LOSlevel < 5; LOSlevel++) // LOS A-E
                {
                    LoopCounter++;

                    int[] ServiceVolsArray;
                    do
                    {
                        CreateArterial_FDOTSerVols.ChangeArterialVolume(ref Arterial, vol);
                        Arterial.TestSerVol = vol;
                        CalcsArterial.CalcResults(Project, ref Arterial);

                        if (!Arterial.OverCapacity)
                        {
                            // Check whether LOS speed threshold has been exceeded
                            if (Arterial.Results.AverageSpeed < Arterial.Thresholds.Speed[LOSlevel])  // Avg. speed below LOS threshold speed
                            {
                                vol -= 10;
                                threshExceeded = true;
                                if (vol <= 0)
                                {
                                    vol = 10;
                                    SerVol.PkDirVol[lanes, LOSlevel] = -1;
                                    SerVol.BothDirVol[lanes, LOSlevel] = -1;
                                    SerVol.AADT[lanes, LOSlevel] = -1;
                                    SerVol.Found[lanes, LOSlevel] = true;
                                }
                            }
                            else
                            {
                                if (threshExceeded == false) // Threshold not exceeded, increment volume
                                {
                                    if ((Arterial.Results.AverageSpeed - Arterial.Thresholds.Speed[LOSlevel]) > 5)
                                        vol += (30 * (lanes + 1) * (LOSlevel + 1));
                                    else
                                        vol += 10;
                                }
                                else // Threshold exceeded, record volume for LOS level
                                {
                                    //vol += 10;
                                    if (LOSlevel > 0)
                                        ServiceVolsArray = SetServiceVolumes(vol, Project.Period, Arterial.Dfactor, Arterial.Kfactor, LOSlevel, SerVol.PkDirVol[lanes, LOSlevel - 1]);
                                    else
                                        ServiceVolsArray = SetServiceVolumes(vol, Project.Period, Arterial.Dfactor, Arterial.Kfactor, LOSlevel, -1);
                                    SerVol.PkDirVol[lanes, LOSlevel] = ServiceVolsArray[0];
                                    SerVol.BothDirVol[lanes, LOSlevel] = ServiceVolsArray[1];
                                    SerVol.AADT[lanes, LOSlevel] = ServiceVolsArray[2];
                                    SerVol.Found[lanes, LOSlevel] = true;
                                    vol += 10; // Increment volume for start of next LOS level
                                    threshExceeded = false;
                                }
                            }
                        }
                        else  // Check to see if there is a constraining intersection due to (v/c * PHF) > 1
                        {
                            vol -= 10; // Return to the previous non-capacity-constrained volume
                            for (int capCheckLOSlevel = LOSlevel + 1; capCheckLOSlevel < 5; capCheckLOSlevel++) // LOS A-E
                            {
                                SerVol.PkDirVol[lanes, capCheckLOSlevel] = -2;
                                SerVol.BothDirVol[lanes, capCheckLOSlevel] = -2;
                                SerVol.AADT[lanes, capCheckLOSlevel] = -2;
                                SerVol.Found[lanes, capCheckLOSlevel] = true;
                            }
                            if (LOSlevel > 0)
                                ServiceVolsArray = SetServiceVolumes(vol, Project.Period, Arterial.Dfactor, Arterial.Kfactor, LOSlevel, SerVol.PkDirVol[lanes, LOSlevel - 1]);
                            else
                                ServiceVolsArray = SetServiceVolumes(vol, Project.Period, Arterial.Dfactor, Arterial.Kfactor, LOSlevel, -1);
                            SerVol.PkDirVol[lanes, LOSlevel] = ServiceVolsArray[0];
                            SerVol.BothDirVol[lanes, LOSlevel] = ServiceVolsArray[1];
                            SerVol.AADT[lanes, LOSlevel] = ServiceVolsArray[2];
                            SerVol.Found[lanes, LOSlevel] = true;
                        }
                    }
                    while (SerVol.Found[lanes, LOSlevel] == false);     //loop until service vol reached for given LOS
                    threshExceeded = false;
                    if (Arterial.OverCapacity)
                    {
                        LoopCounter = 5 * (lanes + 1);
                        frmMain.bgwRunServiceVolsCalcs.ReportProgress((int)(Math.Round(LoopCounter / NumberOfLoops * 100, 0, MidpointRounding.AwayFromZero)));
                        break;
                    }
                    frmMain.bgwRunServiceVolsCalcs.ReportProgress((int)(Math.Round(LoopCounter / NumberOfLoops * 100, 0, MidpointRounding.AwayFromZero)));
                } // BP here to view service volumes by LOS
            } // BP here to view service volumes by number of lanes
        } // BP here to view all service volumes


        private static int[] SetServiceVolumes(int vol, StudyPeriod period, float dFactor, float kFactor, int LOSlevel, int previousPkDirVol)
        {
            int[] ServiceVols = new int[3];
            ServiceVols[0] = (int)vol;
            if (period != StudyPeriod.PeakHour)     // Kn/a study period
            {
                ServiceVols[1] = Convert.ToInt32(((float)vol / dFactor) / 10f + 0.5f) * 10;
                ServiceVols[2] = Convert.ToInt32(((float)vol / (dFactor * kFactor)) / 100f + 0.5f) * 100;
            }
            if (LOSlevel > 0)
            {
                if (Math.Abs(ServiceVols[0] - previousPkDirVol) <= 10)
                    ServiceVols[0] = -3; // Volume is same as for previous LOS level, use footnote 3 star
            }
            else
            {
                if (ServiceVols[0] == 0)
                    ServiceVols[0] = -3; // Volume is 0, use footnote 3 star
            }
            if (vol < 50)
            {
                ServiceVols[0] = -1;
            }
            return ServiceVols;
        }

        /*public static void ServiceVolsSignalOnly(ProjectData Project, ArterialData[] Art, List<IntersectionData> Int, List<LinkData> Seg, ref ServiceVolumes SerVol)
        {
            //------------------------------------------------------------------------------------------
            //      Find AADT for each LOS
            //------------------------------------------------------------------------------------------
            float ThruVol;
            float PrevVC;
            float IntLanes;

            for (int lanes = 1; lanes <= 5; lanes++)      //2,4,6,8,*
            {
                float vol = 10;                //starting volume

                for (int LOSlevel = 1; LOSlevel <= 5; LOSlevel++)      //LOS A-E
                {
                    int VCCheck = 0;
                    float MaxVC = 0;

                    SerVol.PkDirVol[lanes, LOSlevel] = 0;
                    SerVol.BothDirVol[lanes, LOSlevel] = 0;
                    SerVol.AADT[lanes, LOSlevel] = 0;
                    SerVol.Found[lanes, LOSlevel] = false;

                    do
                    {
                        IntLanes = lanes;

                        Int[2].PTXL = SignalCalcs.PctTurnsExcLanes(Int[2].isLTbay, Int[2].isRTbay, Int[2].PctLT, Int[2].PctRT);
                        ThruVol = SignalCalcs.ThruVolume(Arterial.PHF, vol, Int[2].PTXL);  //check index number for segment
                        Int[2].AdjSatFlow = SatFlowRateCalculations.LOSPLAN(Project.Mode, Arterial.Area, OutLaneWidth.Typical, 12, ThruVol, Arterial.BaseSatFlow, Arterial.PctHeavyVeh, IntLanes, Seg[1].MedType, Seg[1].PostSpeedMPH, Int[2].isLTbay, Int[2].isRTbay, Int[2].PctLT, Int[2].PctRT, Int[2].CycleLen, Seg[1].PctGrade);
                        Int[2].Capacity = SignalCalcs.Capacity(Int[2].AdjSatFlow, Int[2].gCthru, IntLanes);
                        Int[2].vcRatio = SignalCalcs.vcRatio(ThruVol, Int[2].Capacity);
                        PrevVC = Int[2].vcRatio;
                        Int[2].CtrlDelay = SignalCalcs.SigDelay(Arterial.SigControl, Int[2].CycleLen, Int[2].gCthru, Int[2].ArvType, ThruVol, Int[2].AdjSatFlow, lanes, Int[2].vcRatio, PrevVC);

                        if (Int[2].vcRatio * Arterial.PHF >= 1)
                        {
                            if (MaxVC < Int[2].vcRatio)
                            {
                                VCCheck = 2;
                                MaxVC = Int[2].vcRatio;
                            }
                        }

                        //Determine whether control delay value exceeds threshold value

                        if (Int[2].CtrlDelay > ParmRanges.ThreshDelay[LOSlevel])
                        {
                            vol = vol - 10;  //return vol to previous value before LOS threshold was exceeded
                            SerVol.PkDirVol[lanes, LOSlevel] = Convert.ToInt32(vol / 10 + 0.5) * 10;
                            if (Arterial.Kfactor != 0)     // Kn/a study period
                            {
                                SerVol.BothDirVol[lanes, LOSlevel] = Convert.ToInt32((vol / Arterial.Dfactor) / 10 + 0.5) * 10;
                                SerVol.AADT[lanes, LOSlevel] = Convert.ToInt32((vol / (Arterial.Dfactor * Arterial.Kfactor)) / 100 + 0.5) * 100;
                            }
                            if (SerVol.PkDirVol[lanes, LOSlevel] == SerVol.PkDirVol[lanes, LOSlevel - 1])
                                SerVol.PkDirVol[lanes, LOSlevel] = -3;  //volume is same as for previous LOS level, use footnote 3 star
                            SerVol.Found[lanes, LOSlevel] = true;       //service vol reached for given LOS

                            if (vol < 50)
                            {                                
                                SerVol.PkDirVol[lanes, LOSlevel] = -2;
                            }
                            vol = vol + 20;  //increment volume for start of next LOS level
                        }
                        else
                        {
                            //threshold not exceeded, increment volume
                            vol = vol + 10;
                        }

                        //Check to see if there is a constraining intersection due to (v/c * PHF) > 1
                        if (VCCheck != 0)
                        {
                            if (lanes != 5)
                                IntLanes = lanes;
                            else
                                IntLanes = Int[VCCheck].NumThLanes;

                            vol = (1 / Arterial.PHF) * (Int[VCCheck].AdjSatFlow * Int[VCCheck].gCthru * IntLanes);

                            vol = vol / (1 - (Int[VCCheck].PTXL / 100)) * Arterial.PHF;
                            SerVol.PkDirVol[lanes, LOSlevel] = Convert.ToInt32(vol / 10 + 0.5) * 10;
                            if (Arterial.Kfactor != 0)     // Kn/a study period
                            {
                                SerVol.BothDirVol[lanes, LOSlevel] = Convert.ToInt32((vol / Arterial.Dfactor) / 10 + 0.5) * 10;
                                SerVol.AADT[lanes, LOSlevel] = Convert.ToInt32((vol / (Arterial.Dfactor * Arterial.Kfactor)) / 100 + 0.5) * 100;
                            }
                            SerVol.Found[lanes, LOSlevel] = true;       //service vol reached for given LOS
                        }
                    }
                    while (SerVol.Found[lanes, LOSlevel] == false);     //loop until service vol reached for given LOS
                }
            }
        }

        */
    }
}
