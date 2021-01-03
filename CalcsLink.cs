using System;



namespace HCMCalc_UrbanStreets
{

    public class LinkRunningTimeCalcParameters
    {
        float _startUpLostTime;
        float _proximityAdjFact;
        float _turningDelayPerAccess;
        float _totalTurningDelay;
        float _otherDelay;
        int[] _midBlockPctTurnsDefault; // Planning variable

        public LinkRunningTimeCalcParameters()
        {
            _startUpLostTime = 2.0f;  //use HCM default
            _midBlockPctTurnsDefault = new int[4] { 7, 5, 3, 2 }; // { 8, 6, 4, 2 };
        }

        public float StartUpLostTime { get => _startUpLostTime; set => _startUpLostTime = value; }
        public float ProximityAdjFact { get => _proximityAdjFact; set => _proximityAdjFact = value; }
        public float TurningDelayPerAccess { get => _turningDelayPerAccess; set => _turningDelayPerAccess = value; }
        public float TotalTurningDelay { get => _totalTurningDelay; set => _totalTurningDelay = value; }
        public float OtherDelay { get => _otherDelay; set => _otherDelay = value; }
        public int[] MidBlockPctTurnsDefault { get => _midBlockPctTurnsDefault; set => _midBlockPctTurnsDefault = value; }
    }

    public class BaseFreeFlowSpeedCalcParameters
    {
        float _baseFreeFlowSpeed;
        float _baseFFScalibFactor;
        float _speedConstant;
        float _crossSectAdjFact;
        float _accessPointsAdjFact;
        float _onStreetParkingAdjFact;

        public BaseFreeFlowSpeedCalcParameters()
        {
            _baseFFScalibFactor = 0.0f;
        }

        public float BaseFreeFlowSpeed { get => _baseFreeFlowSpeed; set => _baseFreeFlowSpeed = value; }
        public float BaseFFScalibFactor { get => _baseFFScalibFactor; set => _baseFFScalibFactor = value; }
        public float SpeedConstant { get => _speedConstant; set => _speedConstant = value; }
        public float CrossSectAdjFact { get => _crossSectAdjFact; set => _crossSectAdjFact = value; }
        public float AccessPointsAdjFact { get => _accessPointsAdjFact; set => _accessPointsAdjFact = value; }
        public float OnStreetParkingAdjFact { get => _onStreetParkingAdjFact; set => _onStreetParkingAdjFact = value; }

        //float _propSegRestrictMedian;
        //float _propSegWithCurb;

    }

    public class SegmentCalcs
    {

        public static float BaseFreeFlowSpeed(AnalysisMode projectMode, AreaType area, int postedSpeed, int numThruLanes, float linkLengthFt, MedianType median, float propRestrictMedian, float propSegCurbRightSide, float pctOnStreetParking, float numAccessPointsSubDir, float numAccessPointsOppDir)
        {
            BaseFreeFlowSpeedCalcParameters BaseFFS = new BaseFreeFlowSpeedCalcParameters();

            /*if (projectMode == AnalysisMode.LOSPLAN)
            {
                //------- Artplan ---------
                //segment length in ARTPLAN is the same as link length in HCM 2010; thus, segment length is passed into method and assigned to variable 'linklength' 
                //float PropSegWithCurb = ParmRanges.PropSegWithCurbDefault[Convert.ToInt32(area)];
                //float NumAccessPts = AccessPointsArtPlan(linkLength);
                //numAccessPointsSubDir = NumAccessPts;
                //numAccessPointsOppDir = NumAccessPts;      //for planning purposes, assume opposing direction has same number of access points as subject direction
                //float PropSegRestrictMedian = 0;   //valid for 'none' and 'non-restrictive' median types
                //if (median == MedianType.Restrictive)
                //    PropSegRestrictMedian = 1.0f;
                //------- Artplan ---------
            }*/

            //Equation 18-3 terms -------------------
            //See notes of Exhibit 18-11
            BaseFFS.SpeedConstant = 25.6f + 0.47f * postedSpeed;

            /*if (median == MedianType.Restrictive)
                BaseFFS.CrossSectAdjFact = 1.5f * propRestrictMedian - 0.47f * propSegCurbRightSide - 3.7f * propRestrictMedian * propSegCurbRightSide;
            else if (median == MedianType.Nonrestrictive || median == MedianType.None)
                BaseFFS.CrossSectAdjFact = 0;*/

            BaseFFS.CrossSectAdjFact = 1.5f * propRestrictMedian - 0.47f * propSegCurbRightSide - 3.7f * propRestrictMedian * propSegCurbRightSide; // Used temporarily but the above code should be used instead

            float AccessDensity = 5280 * (numAccessPointsSubDir + numAccessPointsOppDir) / linkLengthFt;

            BaseFFS.AccessPointsAdjFact = -0.078f * (AccessDensity / numThruLanes);
            
            BaseFFS.OnStreetParkingAdjFact = -3.0f * (pctOnStreetParking / 100);

            return BaseFFS.SpeedConstant + BaseFFS.CrossSectAdjFact + BaseFFS.AccessPointsAdjFact;
        }


        public static float FreeFlowSpeed(float BaseFreeFlowSpeed, float segmentLength)
        {
            //Equation 18-4 -------------------
            float SignalSpacingAdjFact = (float)(Math.Min(1.02 - 4.7 * ((BaseFreeFlowSpeed - 19.5) / Math.Max(segmentLength, 400)), 1.0));  //this equation requires a segment length of at least 400 ft, and max value is 1.0

            //Equation 18-5 -------------------
            return BaseFreeFlowSpeed * SignalSpacingAdjFact;
        }

        public static LinkRunningTimeCalcParameters GetRunningTimeParms(AnalysisMode projectMode, AreaType area, float freeFlowSpeedMPH, float midSegVol, int numSegThruLanes, float segmentLength, bool onStreetParking, ParkingActivityLevel parkingActivity, float numAccessPointsSubDir, float numAccessPointsOppDir)
        {
            LinkRunningTimeCalcParameters RunningTimeParms = new LinkRunningTimeCalcParameters();

            int MidBlockPctTurns = 0;
            //if (projectMode == AnalysisMode.LOSPLAN)
            if (projectMode == AnalysisMode.Planning)
            {
                float NumAccessPts = AccessPointsArtPlan(segmentLength);
                numAccessPointsSubDir = NumAccessPts;
                numAccessPointsOppDir = NumAccessPts;      //for planning purposes, assume opposing direction has same number of access points as subject direction

                MidBlockPctTurns = RunningTimeParms.MidBlockPctTurnsDefault[Convert.ToInt32(area)];
            }

            // CNB: Needs review
            if (midSegVol / numSegThruLanes > 1000)
                midSegVol = 1000;

            //Equation 18-6 -------------------
            RunningTimeParms.ProximityAdjFact = (float)(2 / (1 + Math.Pow(1 - (midSegVol / (52.8 * numSegThruLanes * freeFlowSpeedMPH)), 0.21)));
            //if (float.IsNaN(RunningTimeParms.ProximityAdjFact))
            //    RunningTimeParms.ProximityAdjFact = 1;

            if (onStreetParking == false)
                RunningTimeParms.OtherDelay = 0;
            else
            {
                if (parkingActivity == ParkingActivityLevel.Low)
                    RunningTimeParms.OtherDelay = (2.0f / (numSegThruLanes));
                else if (parkingActivity == ParkingActivityLevel.Medium)
                    RunningTimeParms.OtherDelay = (4.0f / (numSegThruLanes));
                else //high parking activity
                    RunningTimeParms.OtherDelay = (6.0f / (numSegThruLanes));
            }

            //turning delay calculations
            if (numAccessPointsSubDir + numAccessPointsOppDir > 0)
            {
                //equations below are based on regression fits to data from HCM 2016 Exhibit 18-13
                if (numSegThruLanes == 1)
                    //TurningDelayPerAccess = 0.00000107 * Math.Pow(midSegVol, 2) - 0.0002843 * midSegVol / numSegThruLanes + 0.0597143;
                    RunningTimeParms.TurningDelayPerAccess = (float)(0.0208 * Math.Exp(0.0022 * midSegVol));
                else if (numSegThruLanes == 2)
                    RunningTimeParms.TurningDelayPerAccess = (float)(0.00014325313 * midSegVol / numSegThruLanes);
                else
                    //TurningDelayPerAccess = 0.016 * Math.Exp(0.0055 * midSegVol / numSegThruLanes);
                    RunningTimeParms.TurningDelayPerAccess = (float)(0.000109151 * midSegVol / numSegThruLanes);

                /*
                //set maximum value
                if (numSegThruLanes > 2 & (midSegVol / numSegThruLanes) >= 400)  //0.15 is upper limit for 3 or more lanes and flow rate per lane >= 400
                    TurningDelayPerAccess = 0.15;
                */

                RunningTimeParms.TurningDelayPerAccess *= MidBlockPctTurns / 7;  //HCM 2016 Exhibit 18-13 assumes 10% left and 10% right turns at the access point.  Values are adjusted proportionally for different turning percentages.  7% is used as base in Florida

                RunningTimeParms.TotalTurningDelay = RunningTimeParms.TurningDelayPerAccess * (numAccessPointsSubDir + numAccessPointsOppDir);
            }

            return RunningTimeParms;
        }

        /// <summary>
        /// Returns the running time for a segment.
        /// </summary>
        /// <param name="runningTimeParms"></param>
        /// <param name="freeFlowSpeedMPH"></param>
        /// <param name="segmentLengthFt"></param>
        /// <param name="intersectionWidth"></param>
        /// <returns></returns>
        public static float RunningTime(LinkRunningTimeCalcParameters runningTimeParms, float freeFlowSpeedMPH, float segmentLengthFt, AnalysisMode ProjectAnalMode)
        {
            //Equation 18-7 -------------------
            //return (float)(((6 - runningTimeParms.StartUpLostTime) / (0.0025 * (linkLength + intersectionWidth))) + ((3600 * (linkLength + intersectionWidth)) / (5280 * freeFlowSpeedMPH)) * runningTimeParms.ProximityAdjFact + runningTimeParms.TotalTurningDelay + runningTimeParms.OtherDelay);

            float CalibStartUpTime = 0.0025f;
            float StartUpTime = (float)((6 - runningTimeParms.StartUpLostTime) / (CalibStartUpTime * segmentLengthFt));
            float T_ff = (3600 * segmentLengthFt / (5280 * freeFlowSpeedMPH));
            float f_v = runningTimeParms.ProximityAdjFact;
            float d_ap = runningTimeParms.TotalTurningDelay;
            float d_other = runningTimeParms.OtherDelay;
            float sum = StartUpTime + T_ff * f_v + d_ap + d_other;
            return sum;
        }

        /// <summary>
        /// Returns the number of access points on a segment depending on the link length.
        /// </summary>
        /// <param name="segmentLength"></param>
        /// <returns></returns>
        public static float AccessPointsArtPlan(float segmentLength)
        {
            if (segmentLength < 660)
                return 0;
            else
                return 2 * segmentLength / 1320;
        }

        /// <summary>
        /// Returns the base free flow travel time for a segment.
        /// </summary>
        /// <param name="SegLengthFeet"></param>
        /// <param name="BaseFreeFlowSpeed"></param>
        /// <returns></returns>
        public static float BaseFFTravTime(float SegLengthFeet, float BaseFreeFlowSpeed)
        {
            return 3600 * (SegLengthFeet / 5280) / BaseFreeFlowSpeed;  //units of seconds
        }

        /// <summary>
        /// Returns the free flow travel time for a segment.
        /// </summary>
        /// <param name="SegLengthFeet"></param>
        /// <param name="FreeFlowSpeed"></param>
        /// <returns></returns>
        public static float FFTravTime(float SegLengthFeet, float FreeFlowSpeed)
        {
            return 3600 * (SegLengthFeet / 5280) / FreeFlowSpeed;  //units of seconds
        }

        /// <summary>
        /// Returns the average speed for a segment.
        /// </summary>
        /// <param name="SegLengthFeet"></param>
        /// <param name="SegTravTimeSeconds"></param>
        /// <returns></returns>
        public static float SegAvgSpeed(float SegLengthFeet, float SegTravTimeSeconds)
        {
            return 3600 * (SegLengthFeet / 5280) / SegTravTimeSeconds; //units of mi/h
        }

        /// <summary>
        /// Returns the LOS for a segment depending on the average speed comapred to the thresholds speeds defined in <param name="ParmRanges"></param>.
        /// </summary>
        /// <param name="AvgSpeed"></param>
        /// <returns></returns>
        public static string LOSsegmentAuto(float AvgSpeed, int[] ThreshSpeed)
        {   //Determine Segment LOS
            string LOS;

            if (AvgSpeed > ThreshSpeed[0])
                LOS = "A";
            else if (AvgSpeed <= ThreshSpeed[0] && AvgSpeed > ThreshSpeed[1])
                LOS = "B";
            else if (AvgSpeed <= ThreshSpeed[1] && AvgSpeed > ThreshSpeed[2])
                LOS = "C";
            else if (AvgSpeed <= ThreshSpeed[2] && AvgSpeed > ThreshSpeed[3])
                LOS = "D";
            else if (AvgSpeed <= ThreshSpeed[3] && AvgSpeed > ThreshSpeed[4])
                LOS = "E";
            else
                LOS = "F";

            return LOS;
        }
    }
}
