using System;

namespace HCMCalc_UrbanStreets
{
    class FDOTplanning_Methods
    {

        /// <summary>
        /// Returns the weighted gC of the arterial.
        /// </summary>
        /// <param name="NumSegs"></param>
        /// <param name="ArtTotgC"></param>
        /// <param name="CritgC"></param>
        /// <returns></returns>
        public static float WeightedgC(int NumSegs, float ArtTotgC, float CritgC)
        {
            if (NumSegs == 1)
                return CritgC;
            else
                return (CritgC + (ArtTotgC - CritgC) / (NumSegs - 1)) / 2;
        }

        /// <summary>
        /// Returns the FFS Delay as a function of the arterial travel time minus the arterial free flow travel time, both in units of seconds.
        /// </summary>
        /// <param name="ArtTravTimeSeconds"></param>
        /// <param name="ArtFFTravTimeSeconds"></param>
        /// <returns></returns>
        public static float FFSDelay(float ArtTravTimeSeconds, float ArtFFTravTimeSeconds)
        {
            return ArtTravTimeSeconds - ArtFFTravTimeSeconds;
        }

        /// <summary>
        /// Returns the threshold delay of the arterial.
        /// </summary>
        /// <param name="Area"></param>
        /// <param name="ArtTravTimeSeconds"></param>
        /// <param name="ArtLengthMiles"></param>
        /// <returns></returns>
        public static float ThresholdDelay(AreaType Area, float ArtTravTimeSeconds, float ArtLengthMiles)
        {
            FDOTplanning_ParmRanges ParmRanges = new FDOTplanning_ParmRanges();
            float ThreshDelay;

            if (Area == AreaType.RuralDeveloped) //LOS C threshold from HCM2000 Exhibit 15-2
                ThreshDelay = ArtTravTimeSeconds - (ArtLengthMiles / ParmRanges.ThreshSpeed[3] * 3600);
            else    //other area types use LOS D threshold
                ThreshDelay = ArtTravTimeSeconds - (ArtLengthMiles / ParmRanges.ThreshSpeed[4] * 3600);

            if (ThreshDelay < 0)
                ThreshDelay = 0;

            return ThreshDelay;
        }

        /// <summary>
        /// Calculates the percentage of turns on an exclusion lane (a turn bay).
        /// </summary>
        /// <param name="LGType"></param>
        /// <param name="PctLT"></param>
        /// <param name="PctRT"></param>
        /// <returns></returns>
        public static float PctTurnsExcLanes(LaneMovementsAllowed LGType, float PctLT, float PctRT)
        {

            if (LGType == LaneMovementsAllowed.LeftRightShared) // CNB: There is no Right and Left Turn bays with Thru lane, do we need to create it?
                return PctLT + PctRT;
            else if (LGType == LaneMovementsAllowed.ThruAndLeftTurnBay)
                return PctLT;
            else if (LGType == LaneMovementsAllowed.ThruAndRightTurnBay)
                return PctRT;
            else
                return 0;

            /*
            if (LTlane == true && RTlane == true)
            {
                PctRT = Convert.ToInt16(PctRT * ArterialData.RTadj);
                return PTXL = PctLT + PctRT;
            }
            else if (LTlane == true && RTlane == false)
                return PTXL = PctLT;
            else if (LTlane == false && RTlane == true)
            {
                PctRT = Convert.ToInt16(PctRT * ArterialData.RTadj);
                return PTXL = PctRT;
            }
            else
                return PTXL = 0;
            */
        }

        public static float ThruVolume(float PHF, float DDHV, float PTXL)
        {
            return (DDHV / PHF) * (1 - PTXL);
        }

        /// <summary>
        /// Calculates gC based off of cycle length.
        /// </summary>
        /// <param name="cycle"></param>
        /// <returns></returns>
        public static float CalcGreenToCycleLengthRatio(int cycle)
        {
            float gC = 0;

            //gC = Math.Round(0.1005 * Math.Log(cycle) - 0.0571, 2, MidpointRounding.AwayFromZero);

            if (cycle < 90)
            {
                gC = (float)(Math.Max(0.000000926 * Math.Pow(cycle, 3) - 0.000220635 * Math.Pow(cycle, 2) + 0.018685184 * (cycle) - 0.156190459, 0.1));
            }

            else if (cycle >= 90 && cycle < 110)
                gC = 0.42f;
            else if (cycle >= 110 && cycle < 120)
                gC = 0.43f;
            else if (cycle >= 120 && cycle < 140)
                gC = 0.44f;
            else if (cycle >= 140 && cycle < 160)
                gC = 0.45f;
            else if (cycle >= 160 && cycle < 200)
                gC = 0.46f;
            else if (cycle >= 200)
                gC = 0.47f;

            return gC;
        }

        public static float QSratio(float AdjSatFlowThru, float SegDDHV, float PHF, int PctLT, SigControlType SigControl, int CycleLen, float gClt, float PreviousVC, int NumLTlanes, int LTbayLen, PhasingType LTphasing)
        {
            //Based on Appendix G of HCM 2000
            float QSR = 0;     //queue storage ratio
            float VolLT;
            float AdjSatFlowLT;
            float CapacityLT;
            float FlowRatioLT;
            float vcLT;
            float Rp = 1.0f;    //assumed arrival type of 3
            float PF2;
            float Q1;
            float Q2;
            float Ivalue;
            float kb;

            //QbL = 0    //assumed that there is no initial queue at start of analysis period--this also simplifies some equations
            //T = 0.25   //15-min analysis period, used in equation for Q2
            //Lh = 25 ft (default vehicle spacing in queue)

            //Note: do calculations in terms of one LT lane
            AdjSatFlowLT = AdjSatFlowThru * 0.95f;  //5% reduction from adjacent through lanes
            VolLT = (SegDDHV / PHF) * (float)(PctLT) / 100 / NumLTlanes;

            if (LTphasing == PhasingType.Protected)
                CapacityLT = AdjSatFlowLT * gClt;
            else  //protected + permitted phasing
            {
                float NumPhasesPerHour = 3600 / CycleLen;
                CapacityLT = AdjSatFlowLT * gClt + (NumPhasesPerHour * 2.5f);  //increase capacity by 2.5 vehicles per phase
            }
            FlowRatioLT = VolLT / AdjSatFlowLT;
            vcLT = VolLT / CapacityLT;

            PF2 = ((1 - Rp * gClt) * (1 - FlowRatioLT)) / ((1 - gClt) * (1 - Rp * FlowRatioLT));
            Q1 = PF2 * (((VolLT * CycleLen) / 3600) * (1 - gClt) / (1 - vcLT * gClt));

            if (PreviousVC >= 1)
                Ivalue = 0.09f;
            else
                Ivalue = (float)(1 - 0.91 * Math.Pow(PreviousVC, 2.68));

            if (SigControl == SigControlType.Pretimed)
                kb = (float)(0.12 * Ivalue * Math.Pow((AdjSatFlowLT * gClt * CycleLen) / 3600, 0.7));
            else    //Semiactuated or Fully Actuated
                kb = (float)(0.1 * Ivalue * Math.Pow((AdjSatFlowLT * gClt * CycleLen) / 3600, 0.6));

            Q2 = (float)(0.25 * CapacityLT * 0.25 * ((vcLT - 1) + Math.Pow(Math.Pow((vcLT - 1), 2) + (8 * kb * vcLT) / (CapacityLT * 0.25), 0.5)));

            QSR = 25 * (Q1 + Q2) / LTbayLen;  //(LTbayLen / NumLTlanes);  LTbayLen is now total storage length, not per lane length

            //if (QSR > 1)
            //CalcsArterial.LTMessage = true;   //turn on warning message for LT lane spillover

            return QSR;
        }

        public static float AdjustedThruVol(float ApproachDemand, int PctLT, int CycleLen, float gCth, float gClt, float NumThruLanes, int NumLTlanes, int LTbayLen)
        {
            //this method implmements the results of the FDOT project on left turn spillover, BD-545-84

            float AdjThruput;
            float GreenThru = gCth * CycleLen;
            float GreenLT = gClt * CycleLen;

            float LTstorage = LTbayLen / 25;   //convert storage bay length (which accounts for the # of LT lanes) from feet to vehicles, 25 ft is assumed intervehicle spacing

            if (NumThruLanes == 1)
            {
                AdjThruput = (float)(799.0094 - 6.8054 * PctLT - 43.8500 * LTstorage - 30.9825 * GreenLT + 1.3245 * GreenThru + 0.9251 * CycleLen + 0.4918 * ApproachDemand
                    + 0.6805 * PctLT * LTstorage + 0.9152 * PctLT * GreenLT - 0.2896 * PctLT * GreenThru + 0.0338 * PctLT * CycleLen - 0.0161 * PctLT * ApproachDemand
                    + 0.6493 * LTstorage * GreenLT + 0.1148 * LTstorage * GreenThru + 0.0241 * LTstorage * ApproachDemand + 0.0571 * GreenLT * GreenThru
                    + 0.0109 * GreenLT * ApproachDemand + 0.0056 * GreenThru * ApproachDemand - 0.0045 * CycleLen * ApproachDemand);
            }
            else
            {
                AdjThruput = (float)(932.6415 - 21.6749 * PctLT - 41.9322 * LTstorage - 100.4621 * GreenLT - 39.4056 * GreenThru + 8.8626 * CycleLen + 0.5795 * ApproachDemand + 731.7854 * NumThruLanes
                    + 0.9569 * PctLT * LTstorage + 1.5033 * PctLT * GreenLT - 0.5604 * PctLT * GreenThru + 0.0732 * PctLT * CycleLen - 0.0314 * PctLT * ApproachDemand - 5.0604 * PctLT * NumThruLanes
                    + 0.2749 * LTstorage * CycleLen + 0.5900 * GreenLT * GreenThru + 0.0281 * GreenLT * ApproachDemand + 5.5910 * GreenLT * NumThruLanes + 0.0586 * GreenThru * CycleLen
                    + 0.0293 * GreenThru * ApproachDemand + 6.8871 * GreenThru * NumThruLanes - 0.0151 * CycleLen * ApproachDemand - 3.9624 * CycleLen * NumThruLanes + 0.1671 * ApproachDemand * NumThruLanes);
            }

            //implement constraints
            //if (AdjThruput > ApproachThruDemand) //if AdjThruput > Approach Thru Demand, use Approach Thru Demand
            //    AdjThruput = ApproachThruDemand;
            //if AdjThruput > ThruCapacity, use ThruCapacity

            return AdjThruput;
        }

        public static float UnsignalizedGCratio(UnsignalControlType Control, bool LTBays, float MainStreetVol, float PctLeftTurns)
        {
            float CrossStreetVol;
            float UnsignalizedGCratio;

            if (Control == UnsignalControlType.TwoWayStop)  //Two-way stop control
            {
                CrossStreetVol = 1.5f * MainStreetVol;  //if major street is stop-controlled and cross street is not, then cross street likely has higher volume

                if (LTBays == false)    //Without Left-Turn Bays
                    UnsignalizedGCratio = (float)(0.556666 + 0.000968 * MainStreetVol - 0.000006 * Math.Pow(MainStreetVol, 2) + 0.000446 * CrossStreetVol - 0.000003 * Math.Pow(CrossStreetVol, 2) - 0.413692 * (PctLeftTurns / 100) + 0.707765 * Math.Pow((PctLeftTurns / 100), 2));
                else   //With Left-Turn Bays
                    UnsignalizedGCratio = (float)(0.501495 + 0.000989 * MainStreetVol - 0.000005 * Math.Pow(MainStreetVol, 2) + 0.000578 * CrossStreetVol - 0.000003 * Math.Pow(CrossStreetVol, 2) - 0.136783 * (PctLeftTurns / 100) + 0.756259 * Math.Pow((PctLeftTurns / 100), 2));
            }
            else   //All-way stop control
            {
                CrossStreetVol = 1.0f * MainStreetVol;  //if it is all-way stop-controlled intersection, assume that the volumes are balanced betwee the major and cross streets

                if (LTBays == false)    //Without Left-Turn Bays
                    UnsignalizedGCratio = (float)(0.05336429 + 0.00403063 * MainStreetVol - 0.00001033 * Math.Pow(MainStreetVol, 2) + 0.00136678 * CrossStreetVol - 0.00000291 * Math.Pow(CrossStreetVol, 2) + 0.37614667 * (PctLeftTurns / 100) - 1.25347703 * Math.Pow((PctLeftTurns / 100), 2));
                else   //With Left-Turn Bays
                    UnsignalizedGCratio = (float)(0.637963 + 0.000971 * MainStreetVol - 0.000004 * Math.Pow(MainStreetVol, 2) - 0.000440 * CrossStreetVol + 0.0000000424 * Math.Pow(CrossStreetVol, 2) + 0.140119 * (PctLeftTurns / 100) + 1.196012 * Math.Pow((PctLeftTurns / 100), 2));
            }

            if (UnsignalizedGCratio < 0)
                UnsignalizedGCratio = 0.1f;  //g/C input field does not allow a value less than 0.1
            return UnsignalizedGCratio;
        }

        public static void SetDefaultValues() // Arterial
        {
            FDOTplanning_ParmRanges ParmRanges = new FDOTplanning_ParmRanges();
            ParmRanges.CycleLengthDefault[0, 1] = 150;  //Large Urbanized, Class 1
            ParmRanges.CycleLengthDefault[0, 2] = 120;  //Large Urbanized, Class 2 
            ParmRanges.CycleLengthDefault[1, 1] = 150;  //Other Urbanized, Class 1
            ParmRanges.CycleLengthDefault[1, 2] = 120;  //Other Urbanized, Class 2
            ParmRanges.CycleLengthDefault[2, 1] = 120;  //Transitioning, Class 1
            ParmRanges.CycleLengthDefault[2, 2] = 120;  //Transitioning, Class 2
            ParmRanges.CycleLengthDefault[3, 1] = 90;   //Rural Developed, Class 1
            ParmRanges.CycleLengthDefault[3, 2] = 90;   //Rural Developed, Class 2

            //signal control type defaults
            //ParmRanges.SigControlDefault[0, 1] = SigControlType.CoordinatedActuated;  //Large Urbanized, Class 1
            //ParmRanges.SigControlDefault[0, 2] = SigControlType.Pretimed;             //Large Urbanized, Class 2 
            //ParmRanges.SigControlDefault[1, 1] = SigControlType.CoordinatedActuated;  //Other Urbanized, Class 1
            //ParmRanges.SigControlDefault[1, 2] = SigControlType.Pretimed;             //Other Urbanized, Class 2
            //ParmRanges.SigControlDefault[2, 1] = SigControlType.CoordinatedActuated;  //Transitioning, Class 1
            //ParmRanges.SigControlDefault[2, 2] = SigControlType.CoordinatedActuated;  //Transitioning, Class 2
            //ParmRanges.SigControlDefault[3, 1] = SigControlType.FullyActuated;        //Rural Developed, Class 1
            //ParmRanges.SigControlDefault[3, 2] = SigControlType.CoordinatedActuated;  //Rural Developed, Class 2

            ParmRanges.SigControlDefault[1] = SigControlType.FullyActuated;  //Class 1
            ParmRanges.SigControlDefault[2] = SigControlType.CoordinatedActuated;        //Class 2 

            //signal arrival type defaults
            //ParmRanges.ArrTypeDefault[0, 1] = 4;  //Large Urbanized, Class 1
            //ParmRanges.ArrTypeDefault[0, 2] = 4;  //Class 2
            //ParmRanges.ArrTypeDefault[1, 1] = 4;  //Other Urbanized, Class 1
            //ParmRanges.ArrTypeDefault[1, 2] = 4;  //Class 2
            //ParmRanges.ArrTypeDefault[2, 1] = 4;  //Transitioning, Class 1
            //ParmRanges.ArrTypeDefault[2, 2] = 4;  //Class 2
            //ParmRanges.ArrTypeDefault[3, 1] = 3;  //Rural Developed, Class 1
            //ParmRanges.ArrTypeDefault[3, 2] = 4;  //Class 2

        }

        public void SetDefaultValues(AreaType area, int artClass, SigControlType sigControl) // Intersection
        {
            /*
            _cycleLen = ParmRanges.CycleLengthDefault[(int)area, artClass];
            //_gCthru = ParmRanges.gCThruDefault[(int)area];
            _gCthru = SignalCalcs.CalcGreenToCycleLengthRatio(_cycleLen);
            _arvType = ParmRanges.ArrTypeDefault[(int)sigControl];
            _numThLanes = ParmRanges.NumThruLanesDefault[(int)area];
            _pctLT = ParmRanges.PctLTDefault[(int)area];
            _pctRT = ParmRanges.PctRTDefault[(int)area];
            _excLTlane = ParmRanges.ExclLTlaneDefault[(int)area];
            _phasing = ParmRanges.LTphasing;
            _numLTlanes = ParmRanges.NumLTLanesDefault[(int)area];
            _LTlen = ParmRanges.LTbayLenDefault[(int)area];
            _gCleft = ParmRanges.gCLeftDefault[(int)area];
            _excRTlane = ParmRanges.ExclRTlaneDefault[(int)area];
            _width = ParmRanges.IntWidthDefault[(int)area];
            _overCap = false;
            */
        }

        public void SetDefaultValuesArtPlan(AreaType area, int artClass)
        {
            FDOTplanning_ParmRanges ParmRanges = new FDOTplanning_ParmRanges();
            int _lengthFt = ParmRanges.SegLengthDefault[artClass - 1];  //program starts with Class 1 (index 0)
            long _AADT = ParmRanges.AADTDefault[(int)area];
            float _ddhv = ParmRanges.AADTDefault[(int)area] * ParmRanges.KfactDefault[(int)area] * ParmRanges.DfactDefault[(int)area];
            int _numLanes = ParmRanges.NumThruLanesDefault[(int)area];
            int _postSpeedMPH = ParmRanges.PostedSpeedDefault[artClass - 1];
            int _freeFlowSpeedMPH = ParmRanges.FFSpeedDefault[artClass - 1];
            MedianType _medType = ParmRanges.MedianDefault[(int)area];
            bool _onStreetParkingExists = false;
            ParkingActivityLevel _parkingActivity = ParkingActivityLevel.NotApplicable;
        }
    }

    public class ResultsAutoArtPlan
    {
        float _wtdgC;
        float _totalgC;
        float _ffsDelay;       //free-flow speed delay
        float _threshDelay;    //LOS threshold delay
        float _avgLanes;   //length weighted average number of lanes for the arterial

        public ResultsAutoArtPlan()
        {
            _wtdgC = 0;
            _ffsDelay = 0;
            _threshDelay = 0;
            _avgLanes = 0;
        }

        public float WeightedgC { get => _wtdgC; set => _wtdgC = value; }
        public float TotalgC { get => _totalgC; set => _totalgC = value; }
        public float FFSdelay { get => _ffsDelay; set => _ffsDelay = value; }
        public float ThresholdDelay { get => _threshDelay; set => _threshDelay = value; }
        public float AvgLanes { get => _avgLanes; set => _avgLanes = value; }
    }


    public class ResultsMultimodalData
    {
        //Calculated multimodal values
        float _totBikePoints;
        string _totBikeLOS;
        float _totBikeSidePathPoints;
        string _totBikeSidePathLOS;
        bool _sidePathContinuousYN;   //is there a continuous side path along the full length of the arterial section?
        float _totPedPoints;
        string _totPedLOS;
        float _totBusPoints;
        string _totBusLOS;

        public ResultsMultimodalData()
        {
            _totBikePoints = 0;
            _totBikeLOS = "";
            _totBikeSidePathPoints = 0;
            _totBikeSidePathLOS = "";
            _sidePathContinuousYN = false;
            _totPedPoints = 0;
            _totPedLOS = "";
            _totBusPoints = 0;
            _totBusLOS = "";

        }

        public float TotBikePoints { get => _totBikePoints; set => _totBikePoints = value; }
        public string TotBikeLOS { get => _totBikeLOS; set => _totBikeLOS = value; }
        public float TotBikeSidePathPoints { get => _totBikeSidePathPoints; set => _totBikeSidePathPoints = value; }
        public string TotBikeSidePathLOS { get => _totBikeSidePathLOS; set => _totBikeSidePathLOS = value; }
        public bool SidePathContinuousYN { get => _sidePathContinuousYN; set => _sidePathContinuousYN = value; }
        public float TotPedPoints { get => _totPedPoints; set => _totPedPoints = value; }
        public string TotPedLOS { get => _totPedLOS; set => _totPedLOS = value; }
        public float TotBusPoints { get => _totBusPoints; set => _totBusPoints = value; }
        public string TotBusLOS { get => _totBusLOS; set => _totBusLOS = value; }
    }
}
