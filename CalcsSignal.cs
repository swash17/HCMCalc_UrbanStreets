using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace HCMCalc_UrbanStreets
{
    /// <summary>
    /// Parameters related to signalization control.
    /// </summary>
    public class SignalControlParameters
    {
        float _flowRateGreen;
        float _flowRateRed;
        float _redTime;
        float _initialQueue;
        float _finalQueue;
        float _queueServeTime;
        float _queueClearTime;
        float _uniformStops_h1;        //H1 uniform stop rate, stops/veh
        float _incrementalStops_h2;    //H2 incremental stop rate, stops/veh
        float _residualStops_h3;       //H3 third-term stopped vehicles, stops/veh
        float _uniformDelay_d1;
        float _incrementalDelay_d2;
        float _residualDelay_d3;
        float _totalUniformDelay;
        float _avgOverallDelay;
        float _controlType_k;   // signal control parameter, corresponding to pretimed or actuated control
        float _kMin;
        float _passageTime;
        float _availCapacity;
        float _saturatedDelay;
        float _saturatedCapacity;
        float _saturatedStops;
        float _upstreamMetering_I;   // filtering/metering factor
        float _platoonRatio_Rp;
        float[] _platoonRatioValues;

        /// <summary>
        /// Parameterless constructor needed for XML de/serialization. This should not be referenced.
        /// </summary>
        public SignalControlParameters()
        {
            //Parameterless constructor needed for XML de/serialization
        }
        /// <summary>
        /// Inputs parameters for signal control and calculates the platoon ratio based on the arrival type.
        /// </summary>
        public SignalControlParameters(int arvType)
        {
            _passageTime = 2.0f;
            _platoonRatioValues = new float[] { 0.333f, 0.667f, 1.0f, 1.333f, 1.667f, 2.0f };
            _platoonRatio_Rp = _platoonRatioValues[arvType - 1];
        }

        public float FlowRateGreen { get => _flowRateGreen; set => _flowRateGreen = value; }
        public float FlowRateRed { get => _flowRateRed; set => _flowRateRed = value; }
        public float RedTime { get => _redTime; set => _redTime = value; }
        public float QueueClearTime { get => _queueClearTime; set => _queueClearTime = value; }
        public float ControlType_k { get => _controlType_k; set => _controlType_k = value; }
        public float kMin { get => _kMin; set => _kMin = value; }
        public float PassageTime { get => _passageTime; set => _passageTime = value; }
        public float UpstreamMetering_I { get => _upstreamMetering_I; set => _upstreamMetering_I = value; }
        [XmlIgnore]
        public float[] PlatoonRatioValues { get => _platoonRatioValues; set => _platoonRatioValues = value; }
        public float PlatoonRatio_Rp { get => _platoonRatio_Rp; set => _platoonRatio_Rp = value; }
        public float QueueServeTime { get => _queueServeTime; set => _queueServeTime = value; }
        public float UniformStops { get => _uniformStops_h1; set => _uniformStops_h1 = value; }
        public float IncrementalStops { get => _incrementalStops_h2; set => _incrementalStops_h2 = value; }
        public float H3Stops { get => _residualStops_h3; set => _residualStops_h3 = value; }
        public float InitialQueue { get => _initialQueue; set => _initialQueue = value; }
        public float FinalQueue { get => _finalQueue; set => _finalQueue = value; }
        public float AvailCapacity { get => _availCapacity; set => _availCapacity = value; }
        public float SaturatedDelay { get => _saturatedDelay; set => _saturatedDelay = value; }
        public float SaturatedCapacity { get => _saturatedCapacity; set => _saturatedCapacity = value; }
        public float SaturatedStops { get => _saturatedStops; set => _saturatedStops = value; }
        public float UniformDelay_d1 { get => _uniformDelay_d1; set => _uniformDelay_d1 = value; }
        public float IncrementalDelay_d2 { get => _incrementalDelay_d2; set => _incrementalDelay_d2 = value; }
        public float ResidualDelay_d3 { get => _residualDelay_d3; set => _residualDelay_d3 = value; }
        public float TotalUniformDelay { get => _totalUniformDelay; set => _totalUniformDelay = value; }
        public float AvgOverallDelay { get => _avgOverallDelay; set => _avgOverallDelay = value; }        
    }

    public class SignalPhaseParms
    {
        float _arrivalsToMaxOut;
        float _extensionsToMaxOut;
        float _avgHdwyGivenLessThanMAH;
        float _temp1;
        float _temp2;
        float _queueClearTime;
        float _maxQueueClearTime;
        float _probOfGreenExtension;
        float _equilibGreenExtension;
        float _grnCall;
        float _grnUnbalanced;
        float _durationUnbalanced;
        float _durationCrossStreet;
        float _adjustedTimerDuration;
        float _extensionByOnePhase;
        float _volumeRatio;

        float[] _flowForTimer = new float[8];
        float[] _flowXMAHForTimer = new float[8];
        SignalPhaseData _conflictingPhase;

        public SignalPhaseParms()
        {

        }

        public float ArrivalsToMaxOut { get => _arrivalsToMaxOut; set => _arrivalsToMaxOut = value; }
        public float ExtensionsToMaxOut { get => _extensionsToMaxOut; set => _extensionsToMaxOut = value; }
        public float AvgHdwyGivenLessThanMAH { get => _avgHdwyGivenLessThanMAH; set => _avgHdwyGivenLessThanMAH = value; }
        public float Temp1 { get => _temp1; set => _temp1 = value; }
        public float Temp2 { get => _temp2; set => _temp2 = value; }
        public float QueueClearTime { get => _queueClearTime; set => _queueClearTime = value; }
        public float MaxQueueClearTime { get => _maxQueueClearTime; set => _maxQueueClearTime = value; }
        public float ProbOfGreenExtension { get => _probOfGreenExtension; set => _probOfGreenExtension = value; }
        public float EquilibGreenExtension { get => _equilibGreenExtension; set => _equilibGreenExtension = value; }
        public float GreenCall { get => _grnCall; set => _grnCall = value; }
        public float GreenUnbalanced { get => _grnUnbalanced; set => _grnUnbalanced = value; }
        public float DurationUnbalanced { get => _durationUnbalanced; set => _durationUnbalanced = value; }
        public float DurationCrossStreet { get => _durationCrossStreet; set => _durationCrossStreet = value; }
        public float AdjustedTimerDuration { get => _adjustedTimerDuration; set => _adjustedTimerDuration = value; }
        public float ExtensionByOnePhase { get => _extensionByOnePhase; set => _extensionByOnePhase = value; }
        public float VolumeRatio { get => _volumeRatio; set => _volumeRatio = value; }
        public float[] FlowForTimer { get => _flowForTimer; set => _flowForTimer = value; }
        public float[] FlowXMAHForTimer { get => _flowXMAHForTimer; set => _flowXMAHForTimer = value; }
        [XmlIgnore]
        public SignalPhaseData ConflictingPhase { get => _conflictingPhase; set => _conflictingPhase = value; }
    }

    /// <summary>
    /// Calculations related to signalization.
    /// </summary>
    public class SignalCalcs
    {
        /// <summary>
        /// Calculates capacity (per lane) for a lane group.
        /// </summary>
        /// <param name="adjSatFlow"></param>
        /// <param name="gCratio"></param>
        /// <param name="numLanes"></param>
        /// <returns></returns>
        public static float Capacity(float adjSatFlow, float gCratio, float numLanes)
        {
            return adjSatFlow * gCratio * numLanes;
        }

        /// <summary>
        /// Calculates the v/c ratio for a lane group.
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="capacity"></param>
        /// <returns></returns>
        public static float vcRatio(float volume, float capacity)
        {
            return volume / capacity;
        }

        /// <summary>
        /// Returns true if the v/c ratio is greater than the inverse of the PHF.
        /// </summary>
        /// <param name="vcRatio"></param>
        /// <param name="PHF"></param>
        /// <returns></returns>
        public static bool OverCapacityCheck(float vcRatio, float PHF)
        {
            if (vcRatio > (1 / PHF))
            {
                return true;
            }
            else
                return false;
        }



        /// <summary>
        /// Handles all calculations related to signal delay, returning a <param name="SignalControlParameters"></param>
        /// </summary>
        /// <param name="control"></param>
        /// <param name="cycle"></param>
        /// <param name="gC"></param>
        /// <param name="arvType"></param>
        /// <param name="volume"></param>
        /// <param name="adjSatFlowRatePerLane"></param>
        /// <param name="numLanes"></param>
        /// <param name="capacity"></param>
        /// <param name="vc"></param>
        /// <param name="prevVC"></param>
        /// <param name="portionOnGreen"></param>
        /// <param name="NemaMvmtID"></param>
        /// <returns></returns>
        public static SignalControlParameters SigDelay(SigControlType control, int cycle, float gC, int arvType, float volume, float adjSatFlowRatePerLane, float numLanes, float capacity, float vc, float prevVC, float portionOnGreen, NemaMovementNumbers NemaMvmtID)
        {
            SignalControlParameters SigDelayParms = new SignalControlParameters(arvType);

            //Equation 19-15
            //SigDelayParms.ProportionVehsArriveGreen = (float)Math.Min(SigDelayParms.PlatoonRatioValues[arvType - 1] * gC, 1.0);
            //SigDelayParms.ProportionVehsArriveGreen = (float)Math.Min(SigDelayParms.PlatoonRatio_Rp * gC, 1.0);

            //SigDelayParms.ProportionVehsArriveGreen = 0.447f;

            if (cycle == 0 || cycle == 1)  //two-way or all-way stop-controlled intersection
                cycle = 120;    //assumed value for cycle length in development of g/C estimation equations for unsignalized intersections

            SigDelayParms.FlowRateGreen = (volume / 3600) * portionOnGreen / gC;
            SigDelayParms.FlowRateRed = (volume / 3600) * (1 - portionOnGreen) / (1 - gC);

            SigDelayParms.RedTime = cycle - (cycle * gC);

            //Equation 19-16
            //SigDelayParms.Capacity = Capacity(adjSatFlowRate, gC, numLanes);

            //Equations 19-19 - 19-21
            SigDelayParms.QueueClearTime = SigDelayParms.FlowRateRed * SigDelayParms.RedTime / ((adjSatFlowRatePerLane * numLanes) / 3600 - SigDelayParms.FlowRateGreen);
            SigDelayParms.TotalUniformDelay = (float)((0.5 * SigDelayParms.FlowRateRed * Math.Pow(SigDelayParms.RedTime, 2)) + (0.5 * SigDelayParms.FlowRateRed * SigDelayParms.RedTime * SigDelayParms.QueueClearTime));
            SigDelayParms.UniformDelay_d1 = SigDelayParms.TotalUniformDelay / (cycle * volume / 3600);

            //Equation 19-26
            if (NemaMvmtID == NemaMovementNumbers.EBThru || NemaMvmtID == NemaMovementNumbers.WBThru)
            {
                SigDelayParms.ControlType_k = 0.5f;
                if (prevVC >= 1)
                    SigDelayParms.UpstreamMetering_I = 0.09f;
                else
                    SigDelayParms.UpstreamMetering_I = (float)(1 - 0.91 * Math.Pow(prevVC, 2.68));
            }
            else //if (control == SigControlType.FullyActuated)
            {
                SigDelayParms.kMin = (float)Math.Max(-0.375 + 0.354 * SigDelayParms.PassageTime - 0.091 * Math.Pow(SigDelayParms.PassageTime, 2) + 0.00889 * Math.Pow(SigDelayParms.PassageTime, 3), 0.04);      //Kmin must be greater than or equal to 0.04
                SigDelayParms.ControlType_k = (float)Math.Max((1 - 2 * SigDelayParms.kMin) * (vc - 0.5) + SigDelayParms.kMin, SigDelayParms.kMin);
                SigDelayParms.ControlType_k = (float)Math.Min(SigDelayParms.ControlType_k, 0.5);
                SigDelayParms.UpstreamMetering_I = 1.0f;
            }
            //capacity /= numLanes; // To match VBA code - which is incorrectly adjusting for # of lanes
            SigDelayParms.IncrementalDelay_d2 = (float)(900 * 0.25 * ((vc - 1) + Math.Sqrt((vc - 1) * (vc - 1) + (8 * SigDelayParms.ControlType_k * SigDelayParms.UpstreamMetering_I * vc) / (0.25 * capacity))));

            if (gC != 1)
            {
                //PF = (1 - (1.33 * gC)) / (1 - gC);  //from Exhibit 31-46, p. 31-93 
                SigDelayParms.AvgOverallDelay = SigDelayParms.UniformDelay_d1 + SigDelayParms.IncrementalDelay_d2;
            }
            else
                SigDelayParms.AvgOverallDelay = 0;

            return SigDelayParms;
        }

        /// <summary>
        /// Calculates LOS for an intersection based off of the threshold delays.
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public static string LOSintersection(float delay, int[] ThreshDelay)
        {   //Determine Intersection LOS
            string LOS;

            if (delay <= ThreshDelay[0])
                LOS = "A";
            else if (delay > ThreshDelay[0] && delay <= ThreshDelay[1])
                LOS = "B";
            else if (delay > ThreshDelay[1] && delay <= ThreshDelay[2])
                LOS = "C";
            else if (delay > ThreshDelay[2] && delay <= ThreshDelay[3])
                LOS = "D";
            else if (delay > ThreshDelay[3] && delay <= ThreshDelay[4])
                LOS = "E";
            else
                LOS = "F";

            return LOS;
        }

        //Actuated Signal Timing Methods

        //public static SignalPhaseData QAP_ProtectedLane(int CycleLengthSec, int Movement, SignalPhaseData Phase)
        public static SignalPhaseData QAP_ProtectedLane(int CycleLengthSec, int Movement, LaneGroupData laneGroup)
        //SSW Revised 4/21/21
        //Changed everything that was previously "Phase." to "laneGroup." or "laneGroup.SignalPhase", and "Phase.AssociatedLaneGroup." to "laneGroup."
        {
            float LaneVolume;
            float phaseCapacity;
            float Que_r = 0;
            float QueAtEnd = 0;
            float TimeToClear = 0;

            laneGroup.SignalPhase.RedEffectiveSec = Math.Max(CycleLengthSec - laneGroup.SignalPhase.GreenEffectiveSec, 0);

            phaseCapacity = laneGroup.SignalPhase.GreenEffectiveSec / CycleLengthSec * laneGroup.AnalysisResults.SatFlowRate.AdjustedValueVehHrLane; // veh/h/ln
            LaneVolume = Math.Min(laneGroup.DemandVolumeVehPerHrSplit[Movement] / laneGroup.Lanes.Count, phaseCapacity);
            laneGroup.AnalysisResults.CapacityPerLane = phaseCapacity * laneGroup.Lanes.Count; // veh/h

            // Flow rate on red and green, veh/s/ln
            if (laneGroup.SignalPhase.RedEffectiveSec > 0)
                laneGroup.SignalPhase.Timer.RedFlow = (1 - laneGroup.PortionOnGreen) * LaneVolume / 3600 * CycleLengthSec / laneGroup.SignalPhase.RedEffectiveSec;
            else
                laneGroup.SignalPhase.Timer.RedFlow = 0;
            if (laneGroup.SignalPhase.GreenEffectiveSec > 0)
                laneGroup.SignalPhase.Timer.GreenFlow = laneGroup.PortionOnGreen * LaneVolume / 3600 * CycleLengthSec / laneGroup.SignalPhase.GreenEffectiveSec;
            else
                laneGroup.SignalPhase.Timer.GreenFlow = 0;

            QueueStatusChange(0, (0 - laneGroup.SignalPhase.Timer.RedFlow), laneGroup.SignalPhase.RedEffectiveSec, ref Que_r, ref TimeToClear);

            // Time required for left-turn queue service during protected phase, g_s, s
            laneGroup.AnalysisResults.DischargeVolume = laneGroup.AnalysisResults.SatFlowRate.AdjustedValueVehHrLane / 3600 - laneGroup.SignalPhase.Timer.GreenFlow;
            QueueStatusChange(Que_r, laneGroup.AnalysisResults.DischargeVolume, laneGroup.SignalPhase.GreenEffectiveSec, ref QueAtEnd, ref TimeToClear);
            laneGroup.SignalPhase.Timer.QueueServeTime = TimeToClear;
            laneGroup.SignalPhase.Timer.QueueClearTime = TimeToClear;
            
            return laneGroup.SignalPhase;
        }

        //public static SignalPhaseData QAP_ProtectedSharedLane(int CycleLengthSec, SignalPhaseData Phase)
        //SSW Revised 4/21/21
        //Changed everything that was previously "Phase." to "laneGroup." or "laneGroup.SignalPhase", and "Phase.AssociatedLaneGroup." to "laneGroup."
        public static SignalPhaseData QAP_ProtectedSharedLane(int CycleLengthSec, LaneGroupData laneGroup)
        {
            float LaneVolume;
            float PortionOnGreenFlow;
            float ApproachCapacity;
            float TimeToClear = 0;
            float QueAtEnd = 0;
            float Que_r = 0;

            //APPLICABLE TO SPLIT PHASING
            //Phase.RedEffectiveSec = Math.Max(CycleLengthSec - Phase.AssociatedLaneGroup.SignalPhase.GreenEffectiveSec, 0);
            laneGroup.SignalPhase.RedEffectiveSec = Math.Max(CycleLengthSec - laneGroup.SignalPhase.GreenEffectiveSec, 0);

            /*AvailGreen = Timer.Duration - Timer.Intergreen;
            if ((Timer.MaxGreen > AvailGreen) & (Phase.NemaPhaseId != 2 & Phase.NemaPhaseId != 6))
                AvailGreen = Timer.MaxGreen;
            AvailGreen -= Phase.StartUpLostTimeSec + Phase.EndUseSec;*/

            if (laneGroup.Type == LaneMovementsAllowed.ThruAndRightTurnBay || laneGroup.Type == LaneMovementsAllowed.ThruRightShared) // T+R, OUTSIDE SHARED LANE ANALYSIS
            {
                LaneVolume = Math.Min(laneGroup.DemandVolumeVehPerHr, laneGroup.SignalPhase.GreenEffectiveSec / CycleLengthSec * laneGroup.BaseSatFlow); // veh/h/ln

                // Flow rate on red and green, veh/s/ln
                PortionOnGreenFlow = ((laneGroup.DemandVolumeVehPerHr - laneGroup.DemandVolumeVehPerHr) * laneGroup.PortionOnGreen + laneGroup.DemandVolumeVehPerHr * laneGroup.PortionOnGreen) / (laneGroup.DemandVolumeVehPerHr + 0.001f);

                if (laneGroup.SignalPhase.RedEffectiveSec > 0)
                    laneGroup.SignalPhase.Timer.RedFlow = (1 - PortionOnGreenFlow) * LaneVolume / 3600 * CycleLengthSec / laneGroup.SignalPhase.RedEffectiveSec;
                else
                    laneGroup.SignalPhase.Timer.RedFlow = 0;
                if (laneGroup.SignalPhase.GreenEffectiveSec > 0)
                    laneGroup.SignalPhase.Timer.GreenFlow = PortionOnGreenFlow * LaneVolume / 3600 * CycleLengthSec / laneGroup.SignalPhase.GreenEffectiveSec;
                else
                    laneGroup.SignalPhase.Timer.GreenFlow = 0;

                QueueStatusChange(0, (0 - laneGroup.SignalPhase.Timer.RedFlow), laneGroup.SignalPhase.RedEffectiveSec, ref Que_r, ref TimeToClear);
                laneGroup.AnalysisResults.DischargeVolume = laneGroup.BaseSatFlow / 3600 - laneGroup.SignalPhase.Timer.GreenFlow;
                QueueStatusChange(Que_r, laneGroup.AnalysisResults.DischargeVolume, laneGroup.SignalPhase.GreenEffectiveSec, ref QueAtEnd, ref TimeToClear);
                laneGroup.SignalPhase.Timer.QueueServeTime = TimeToClear;
                laneGroup.SignalPhase.Timer.QueueClearTime = TimeToClear;

            }
            else // T,  MIDDLE THROUGH LANE(S) ANALYSIS
            {
                LaneVolume = Math.Min(laneGroup.DemandVolumeVehPerHr / laneGroup.Lanes.Count, laneGroup.SignalPhase.GreenEffectiveSec / CycleLengthSec * laneGroup.BaseSatFlow); // veh/h/ln

                if (laneGroup.SignalPhase.RedEffectiveSec > 0)
                    laneGroup.SignalPhase.Timer.RedFlow = (1 - laneGroup.PortionOnGreen) * LaneVolume / 3600 * CycleLengthSec / laneGroup.SignalPhase.RedEffectiveSec;
                else
                    laneGroup.SignalPhase.Timer.RedFlow = 0;
                if (laneGroup.SignalPhase.GreenEffectiveSec > 0)
                    laneGroup.SignalPhase.Timer.GreenFlow = laneGroup.PortionOnGreen * LaneVolume / 3600 * CycleLengthSec / laneGroup.SignalPhase.GreenEffectiveSec;
                else
                    laneGroup.SignalPhase.Timer.GreenFlow = 0;

                QueueStatusChange(0, (0 - laneGroup.SignalPhase.Timer.RedFlow), laneGroup.SignalPhase.RedEffectiveSec, ref Que_r, ref TimeToClear);
                laneGroup.AnalysisResults.DischargeVolume = laneGroup.BaseSatFlow / 3600 - laneGroup.SignalPhase.Timer.GreenFlow;
                QueueStatusChange(Que_r, laneGroup.AnalysisResults.DischargeVolume, laneGroup.SignalPhase.GreenEffectiveSec, ref QueAtEnd, ref TimeToClear);
                laneGroup.SignalPhase.Timer.QueueServeTime = TimeToClear;
                laneGroup.SignalPhase.Timer.QueueClearTime = TimeToClear;
            }

            //CALCULATE MOVEMENT CAPACITY
            laneGroup.AnalysisResults.CapacityPerLane = laneGroup.SignalPhase.GreenEffectiveSec * laneGroup.SignalPhase.Timer.MvmtSatFlow[2] / (CycleLengthSec * laneGroup.Lanes.Count);

            ApproachCapacity = laneGroup.SignalPhase.GreenEffectiveSec * laneGroup.BaseSatFlow * laneGroup.Lanes.Count / CycleLengthSec;
            laneGroup.AnalysisResults.CapacityPerLane = ApproachCapacity - laneGroup.AnalysisResults.CapacityPerLane;
            return laneGroup.SignalPhase;
        }

        public static List<SignalPhaseData> ComputeQAPolygon(List<SignalPhaseData> Phases, int CycleLengthSec, IntersectionData intersection)        
        {
            for (int PhaseIndex = 0; PhaseIndex < Phases.Count; PhaseIndex++)
            {
                SignalPhaseData Phase = Phases[PhaseIndex];

                //SSW Revised 4/21/21
                LaneGroupData laneGroup = MovementData.GetLaneGroupFromNemaMovementNumber(intersection, Phase.NemaMvmtId); //or should this be 'Phase.NemaPhaseId'?

                if (laneGroup.Lanes.Count > 0)
                {
                    //Phase.GreenEffectiveSec = Math.Max(Phase.Timer.Duration - Phase.Timer.IntergreenTimeSec - Phase.AssociatedLaneGroup.SignalPhase.StartUpLostTimeSec + Phase.AssociatedLaneGroup.SignalPhase.EndUseSec, 0.1f);
                    //SSW Revised 4/21/21
                    Phase.GreenEffectiveSec = Math.Max(Phase.Timer.Duration - Phase.Timer.IntergreenTimeSec - Phase.StartUpLostTimeSec + Phase.EndUseSec, 0.1f);

                    if (Phase.Phasing == PhasingType.Protected) // PROTECTED LEFT MODE, CASE 2: L:x:x (Analysis Lane Group: Left)
                    {
                        Phase = QAP_ProtectedLane(CycleLengthSec, 0, laneGroup);
                    }
                    else if (Phase.Phasing == PhasingType.ThruAndRight) //THROUGH-RIGHT MOVEMENTS ON APPROACHES WITH PROTECTED OR PROTECTED-PERMITTED LEFT MODE //through and right turn movements (no lefts) 
                    {
                        if (laneGroup.Type == LaneMovementsAllowed.ThruAndRightTurnBay) // CASE 3:   x:_:R or x:T:R
                        {
                            Phase = QAP_ProtectedLane(CycleLengthSec, 1, laneGroup);
                            Phase = QAP_ProtectedLane(CycleLengthSec, 2, laneGroup); // R
                        }
                        else if (laneGroup.Type != LaneMovementsAllowed.ThruAndRightTurnBay) // CASE 4:  x:T:T+R  or  x:T:_  or  x:_:T+R
                        {
                            if (laneGroup.DemandVolumeVehPerHrSplit[2] > 0) // T:T+R  or  _:T+R
                            {
                                Phase = QAP_ProtectedSharedLane(CycleLengthSec, laneGroup);
                            }
                            else
                            {
                                Phase = QAP_ProtectedLane(CycleLengthSec, 1, laneGroup);
                            }
                        }
                    }
                }
            }
            return Phases;
        }

        public static void QueueStatusChange(float QueAtStart, float QueChangeRate, float QueChangeDuration, ref float QueAtEnd, ref float TimeToClear)
        {
            if (QueChangeRate > 0)
                TimeToClear = QueAtStart / QueChangeRate;
            else
                TimeToClear = 999;

            if (TimeToClear > QueChangeDuration) { TimeToClear = QueChangeDuration; }
            if (TimeToClear < 0) { TimeToClear = 0; }
            QueAtEnd = QueAtStart - QueChangeRate * QueChangeDuration;
            if (QueAtEnd < 0) { QueAtEnd = 0; }
        }

        public static List<SignalPhaseData> VolumeComputations(List<SignalPhaseData> Phases, IntersectionData intersection)
        {
            SignalPhaseData ConcurrentPhase;
            float Denominator;

            //A. Call Rate to Extend Green
            //Determine Extending Call Rate Parameters
            //See variable definitions for Eq. 31-5

            foreach (SignalPhaseData Phase in Phases)
            {
                //SSW Revised 4/21/21
                LaneGroupData laneGroup = MovementData.GetLaneGroupFromNemaMovementNumber(intersection, Phase.NemaMvmtId); //or should this be 'Phase.NemaPhaseId'?

                TimerData Timer = Phase.Timer;
                int phaseIndex = Phases.IndexOf(Phase);
                for (int LaneGroupIndex = 0; LaneGroupIndex < 3; LaneGroupIndex++)
                {
                    if (laneGroup.DemandVolumeVehPerHrSplit[LaneGroupIndex] > 0)
                    {
                        if (laneGroup.Lanes.Count == 1)
                        {
                            Timer.Delta[LaneGroupIndex] = 1.5f;
                            Timer.BunchFactor[LaneGroupIndex] = 0.6f;
                        }
                        else if (laneGroup.Lanes.Count == 2)
                        {
                            Timer.Delta[LaneGroupIndex] = 0.5f;
                            Timer.BunchFactor[LaneGroupIndex] = 0.5f;
                        }
                        else if (laneGroup.Lanes.Count > 2)
                        {
                            Timer.Delta[LaneGroupIndex] = 0.5f;
                            Timer.BunchFactor[LaneGroupIndex] = 0.8f;
                        }
                        else
                        {
                            Timer.Delta[LaneGroupIndex] = 0;
                            Timer.BunchFactor[LaneGroupIndex] = 0;
                        }

                        //Eq. 31-5
                        Timer.PortionFree[LaneGroupIndex] = (float)Math.Exp(-1 * Timer.BunchFactor[LaneGroupIndex] * Timer.Delta[LaneGroupIndex] * Phase.AssociatedLaneGroup.DemandVolumeVehPerHrSplit[LaneGroupIndex] / 3600);

                        //Eq. 31-4 denominator
                        Denominator = 1 - Timer.Delta[LaneGroupIndex] * laneGroup.DemandVolumeVehPerHrSplit[LaneGroupIndex] / 3600;

                        //Eq. 31-4
                        if (Denominator > 0)
                            Timer.UnbunchedFlow[LaneGroupIndex] = Timer.PortionFree[LaneGroupIndex] * laneGroup.DemandVolumeVehPerHrSplit[LaneGroupIndex] / 3600 / Denominator;
                        else
                            Timer.UnbunchedFlow[LaneGroupIndex] = 99;

                        Timer.PhaseFlowRate[LaneGroupIndex] = laneGroup.DemandVolumeVehPerHrSplit[LaneGroupIndex] / 3600;    //veh/s
                    }
                }
            }

            //DETERMINE PHASE EXTENDING CALL RATE
            foreach (SignalPhaseData Phase in Phases)
            {
                TimerData Timer = Phase.Timer;
                int phaseIndex = Phases.IndexOf(Phase);

                Timer.Delta[0] = 0;
                Timer.PortionFree[0] = 1;
                Timer.UnbunchedFlow[0] = 0;

                for (int LaneGroup = 0; LaneGroup < 3; LaneGroup++)
                {
                    if (Phase.AssociatedLaneGroup.DemandVolumeVehPerHrSplit[LaneGroup] > 0)
                    {
                        Timer.Delta[0] += Timer.Delta[LaneGroup] * Timer.UnbunchedFlow[LaneGroup];
                        Timer.PortionFree[0] *= Timer.PortionFree[LaneGroup];
                        Timer.UnbunchedFlow[0] += Timer.UnbunchedFlow[LaneGroup];
                        Timer.PhaseFlowRate[0] += Timer.PhaseFlowRate[LaneGroup];
                    }
                }

                if (Phase.GapOutMode == true && (phaseIndex == 1 || phaseIndex == 3 || phaseIndex == 5 || phaseIndex == 7)) // Phase associated with timer is set for simultaneous gap out
                {
                    ConcurrentPhase = Phases[5]; // phaseIndex == 1
                    if (phaseIndex == 3) { ConcurrentPhase = Phases[7]; }
                    if (phaseIndex == 5) { ConcurrentPhase = Phases[1]; }
                    if (phaseIndex == 7) { ConcurrentPhase = Phases[3]; }
                    for (int LaneGroup = 1; LaneGroup <= 3; LaneGroup++)
                    {
                        if (ConcurrentPhase.AssociatedLaneGroup.DemandVolumeVehPerHrSplit[LaneGroup] > 0)
                        {
                            Timer.Delta[0] += ConcurrentPhase.Timer.Delta[LaneGroup] * ConcurrentPhase.Timer.UnbunchedFlow[LaneGroup];
                            Timer.PortionFree[0] *= ConcurrentPhase.Timer.PortionFree[LaneGroup];
                            Timer.UnbunchedFlow[0] += ConcurrentPhase.Timer.UnbunchedFlow[LaneGroup];
                            Timer.PhaseFlowRate[0] += ConcurrentPhase.Timer.PhaseFlowRate[LaneGroup];
                        }
                    }
                }
                if (Timer.UnbunchedFlow[0] > 0) { Timer.Delta[0] = Timer.Delta[0] / Timer.UnbunchedFlow[0]; }
            }

            //B. DETERMINE CALL RATE TO ACTIVATE A PHASE
            //i. Determine Phase Vehicle Flow Rate
            foreach (SignalPhaseData Phase in Phases)
            {
                TimerData Timer = Phase.Timer;
                if (Phase.AssociatedLaneGroup.Type == LaneMovementsAllowed.LeftOnly) // Timer serves left only, and in an exclusive lane
                {
                    Phase.AssociatedLaneGroup.DemandVolumeVehPerHr = Phase.AssociatedLaneGroup.DemandVolumeVehPerHrSplit[0];
                }
                else if (Phase.AssociatedLaneGroup.Type == LaneMovementsAllowed.ThruAndRightTurnBay || Phase.AssociatedLaneGroup.Type == LaneMovementsAllowed.ThruRightShared) // Timer serves through and right only
                {
                    Phase.AssociatedLaneGroup.DemandVolumeVehPerHr = Phase.AssociatedLaneGroup.DemandVolumeVehPerHrSplit[1] + Phase.AssociatedLaneGroup.DemandVolumeVehPerHrSplit[2];
                }
                else if (Phase.AssociatedLaneGroup.Type == LaneMovementsAllowed.All) // Timer serves left, thru, and right; with left-turns served in Permissive mode or with Split phasing
                {
                    Phase.AssociatedLaneGroup.DemandVolumeVehPerHr = Phase.AssociatedLaneGroup.DemandVolumeVehPerHrSplit[0] + Phase.AssociatedLaneGroup.DemandVolumeVehPerHrSplit[1] + Phase.AssociatedLaneGroup.DemandVolumeVehPerHrSplit[2];
                }
            }

            //ii. Determine Activating Vehicle Call Rate
            foreach (SignalPhaseData Phase in Phases)
            {
                TimerData Timer = Phase.Timer;
                int phaseIndex = Phases.IndexOf(Phase);

                ConcurrentPhase = Phases[5]; // phaseIndex == 0
                if (phaseIndex == 1) { ConcurrentPhase = Phases[5]; }
                if (phaseIndex == 2) { ConcurrentPhase = Phases[7]; }
                if (phaseIndex == 3) { ConcurrentPhase = Phases[7]; }
                if (phaseIndex == 4) { ConcurrentPhase = Phases[1]; }
                if (phaseIndex == 5) { ConcurrentPhase = Phases[1]; }
                if (phaseIndex == 6) { ConcurrentPhase = Phases[3]; }
                if (phaseIndex == 7) { ConcurrentPhase = Phases[3]; }

                if (Phase.AssociatedLaneGroup.SignalPhase.DualEntryMode == true)
                    Timer.CallingFlow = (Phase.AssociatedLaneGroup.DemandVolumeVehPerHr + ConcurrentPhase.AssociatedLaneGroup.DemandVolumeVehPerHr + Phases[phaseIndex-1].AssociatedLaneGroup.DemandVolumeVehPerHr) / 3600;
                else
                    Timer.CallingFlow = Phase.AssociatedLaneGroup.DemandVolumeVehPerHr / 3600;
            }
            return Phases;
        }

        public static List<SignalPhaseData> MaximumAllowableHeadway(List<SignalPhaseData> Phases, int PostedSpeed)
        {
            SignalPhaseParms SigParms = new SignalPhaseParms();

            foreach (SignalPhaseData Phase in Phases)
            {
                TimerData Timer = Phase.Timer;
                int phaseIndex = Phases.IndexOf(Phase);

                if ((Phase.AssociatedLaneGroup.Lanes.Count > 0) & (Phase.NemaPhaseId != 2 & Phase.NemaPhaseId != 6))
                {
                    // Don't compute MAH for coordinated phases
                    float MAH_Left = GetMaxAllowableHeadway(PostedSpeed, Phase.AssociatedLaneGroup);
                    float MAH_Left_Adj = (Phase.AssociatedLaneGroup.AnalysisResults.SatFlowRate.AdjFact.LeftTurnEquivalency - 1) * 3600 / Phase.AssociatedLaneGroup.AnalysisResults.SatFlowRate.BaseValuePCHrLane;

                    if (Phase.Phasing == PhasingType.Protected) // CASE 2: L:x:x
                    {
                        SigParms.FlowXMAHForTimer[phaseIndex] = (MAH_Left + MAH_Left_Adj) * Timer.UnbunchedFlow[0];
                        SigParms.FlowForTimer[phaseIndex] = Timer.UnbunchedFlow[0];
                    }
                }
                if (SigParms.FlowForTimer[phaseIndex] > 0)
                    Timer.MaxAllowHdwy = SigParms.FlowXMAHForTimer[phaseIndex] / SigParms.FlowForTimer[phaseIndex];
                else
                    Timer.MaxAllowHdwy = 0;
            }

            // REVISE TIMER MAH if (SIMULTANEOUS GAP OUT IS PRESENT
            for (int phaseIndex = 1; phaseIndex < 8; phaseIndex += 2)
            {
                SignalPhaseData Phase = Phases[phaseIndex];
                TimerData Timer = Phase.Timer;
                int ConcurrentTimerIndex = 0;
                if (phaseIndex == 1) { ConcurrentTimerIndex = 5; }
                if (phaseIndex == 3) { ConcurrentTimerIndex = 7; }
                if (phaseIndex == 5) { ConcurrentTimerIndex = 1; }
                if (phaseIndex == 7) { ConcurrentTimerIndex = 3; }
                if ((Phase.GapOutMode == true) & (phaseIndex == 1 | phaseIndex == 3 | phaseIndex == 5 | phaseIndex == 7))
                {
                    // Phase associated with timer is set for simultaneous gap out
                    float TotalUnbunchedFlow = SigParms.FlowForTimer[phaseIndex] + SigParms.FlowForTimer[ConcurrentTimerIndex];
                    if ((TotalUnbunchedFlow > 0) & (SigParms.FlowForTimer[phaseIndex] > 0) & (Phase.NemaPhaseId != 2 & Phase.NemaPhaseId != 6))
                        Timer.MaxAllowHdwy = (SigParms.FlowXMAHForTimer[phaseIndex] + SigParms.FlowXMAHForTimer[ConcurrentTimerIndex]) / TotalUnbunchedFlow;
                    else
                        Timer.MaxAllowHdwy = 0;
                }
            }
            return Phases;
        }

        public static float GetMaxAllowableHeadway(int PostedSpeed, LaneGroupData LaneGroup)
        {
            UrbanStreetParameters DefaultValues = new UrbanStreetParameters();

            float AverageSpeedMPH = (25.6f + 0.47f * PostedSpeed) * DefaultValues.AverageSpeedtoFFSRatio;
            float DetectedVehLength = DefaultValues.StoredLaneLength - DefaultValues.DistBetweenStoredVeh;

            if (LaneGroup.DetectorLength > 1 && PostedSpeed > 0)
                return LaneGroup.SignalPhase.PassTime + (DetectedVehLength + LaneGroup.DetectorLength) / (AverageSpeedMPH * 5280 / 3600); // Presence-mode detection
            else if (LaneGroup.DetectorLength == 1 && PostedSpeed > 0)
                return LaneGroup.SignalPhase.PassTime; // Pulse-mode detection
            else
                return 0; // No detection
        }

        public static List<SignalPhaseData> ComputeEquivalentMaxGreen(IntersectionData Intersection)
        {
            List<SignalPhaseData> Phases = Intersection.Signal.Phases;
            SignalPhaseData ReferencePhase = GetPhaseFromPhaseNumber(Phases, Intersection.Signal.ReferencePhaseID);
            int ReferenceTimerIndex = Intersection.Signal.Phases.IndexOf(ReferencePhase);

            float sumSplits;
            float GreenTime;

            // Determine boundary points for each timer.  Boundary points coincide with start and end of split window.
            // Boundary point at start of window coincides with start of green.
            // Boundary point at end of window coincides with end of green.
            // Boundary point at end of window for phases 2 and 6 coincide with Yield point
            // Boundary point at end of window for phases 1,3,4,5,7,8 coincide with Force-Off point

            if (Intersection.Signal.ReferencePoint == 1)  // Offset referenced to beginning of green
            {
                sumSplits = Intersection.Signal.OffsetSec % Intersection.Signal.CycleLengthSec; // CNB: Is sumSplits just Splits?
            }
            else // Offset referenced to end of green
            {
                GreenTime = ReferencePhase.Splits - ReferencePhase.Timer.IntergreenTimeSec;
                if (GreenTime < 0) { GreenTime = 0; }
                sumSplits = (Intersection.Signal.OffsetSec - GreenTime + Intersection.Signal.CycleLengthSec) % Intersection.Signal.CycleLengthSec;
            }

            if (ReferencePhase.NemaPhaseId == 2)
            {
                for (int ring1Iterate = 0; ring1Iterate < 4; ring1Iterate += 2) // Define times in Ring 1
                {
                    SignalPhaseData Phase = Intersection.Signal.Phases[((ring1Iterate + ReferenceTimerIndex) % 4)]; // Phase number, starting with the coordinated phase in Ring 2.

                    Phase.Timer.StartWindow = sumSplits;
                    sumSplits = (sumSplits + Phase.Splits) % Intersection.Signal.CycleLengthSec;
                    GreenTime = Phase.Splits - Phase.Timer.IntergreenTimeSec;
                    if (GreenTime < 0) { GreenTime = 0; }
                    Phase.Timer.EndWindow = (Phase.Timer.StartWindow + GreenTime) % Intersection.Signal.CycleLengthSec;
                }
                for (int phaseIndex = 0; phaseIndex < 4; phaseIndex += 2) // Define times in Ring 2
                {
                    // Match times that occur a barriers, Starts: T1->T5, T3->T7; Ends: T2->T6, T4->T8
                    SignalPhaseData Phase = Intersection.Signal.Phases[phaseIndex];
                    Phases[phaseIndex + 4].Timer.StartWindow = Phase.Timer.StartWindow;
                    Phases[phaseIndex + 5].Timer.EndWindow = Phases[phaseIndex + 1].Timer.EndWindow;

                    // Use timer duration to determine times at non-barrier points, Starts: T6, T8; Ends: T5, T7
                    GreenTime = Phases[phaseIndex + 4].Splits - Phases[phaseIndex + 4].Timer.IntergreenTimeSec;
                    if (GreenTime < 0) { GreenTime = 0; }
                    Phases[phaseIndex + 4].Timer.EndWindow = (Phases[phaseIndex + 4].Timer.StartWindow + GreenTime) % Intersection.Signal.CycleLengthSec;

                    GreenTime = Phases[phaseIndex + 5].Splits - Phases[phaseIndex + 5].Timer.IntergreenTimeSec;
                    if (GreenTime < 0) { GreenTime = 0; }
                    Phases[phaseIndex + 5].Timer.StartWindow = (Phases[phaseIndex + 5].Timer.EndWindow - GreenTime + Intersection.Signal.CycleLengthSec) % Intersection.Signal.CycleLengthSec;
                }
            }
            else  // ReferencePhase = 6
            {
                for (int ring2Iterate = 0; ring2Iterate < 4; ring2Iterate += 2) // Define times in Ring 2
                {
                    SignalPhaseData Phase = Intersection.Signal.Phases[((ring2Iterate + ReferenceTimerIndex) % 4) + 4]; // Timer number, starting with the coordinated phase in Ring 2.
                    TimerData Timer = Phase.Timer;

                    Timer.StartWindow = sumSplits;
                    sumSplits = (sumSplits + Phase.Splits) % Intersection.Signal.CycleLengthSec;
                    GreenTime = Phase.Splits - Phase.Timer.IntergreenTimeSec;
                    if (GreenTime < 0) { GreenTime = 0; }
                    Timer.EndWindow = (Timer.StartWindow + GreenTime) % Intersection.Signal.CycleLengthSec;
                }
            }
            for (int phaseIndex = 0; phaseIndex < 4; phaseIndex += 2)            // Define times in Ring 1
            {
                SignalPhaseData Phase = Intersection.Signal.Phases[phaseIndex];
                SignalPhaseData NextPhase = Intersection.Signal.Phases[phaseIndex+1];

                // Match times that occur a barriers, Starts: T5->T1, T7->T3; Ends: T6->T2, T8->T4
                Phase.Timer.StartWindow = Phases[phaseIndex + 4].Timer.StartWindow;
                NextPhase.Timer.EndWindow = Phases[phaseIndex + 5].Timer.EndWindow;

                // Use timer duration to determine times at non-barrier points, Starts: T2, T4; Ends: T1, T3
                GreenTime = Phase.Splits - Phase.Timer.IntergreenTimeSec;
                if (GreenTime < 0) { GreenTime = 0; }
                Phase.Timer.EndWindow = (Phase.Timer.StartWindow + GreenTime) % Intersection.Signal.CycleLengthSec;

                GreenTime = NextPhase.Splits - NextPhase.Timer.IntergreenTimeSec;
                if (GreenTime < 0) { GreenTime = 0; }
                NextPhase.Timer.StartWindow = (NextPhase.Timer.EndWindow - GreenTime + Intersection.Signal.CycleLengthSec) % Intersection.Signal.CycleLengthSec;
            }

            //COMPUTE EQUIVALENT MAXIMUM GREEN FOR NON-COORDINATED PHASES

            foreach (SignalPhaseData Phase in Intersection.Signal.Phases)
            {
                TimerData Timer = Phase.Timer;
                int phaseIndex = Intersection.Signal.Phases.IndexOf(Phase);
                
                TimerData PreviousTimer = Intersection.Signal.Phases[3].Timer; // phaseIndex == 0
                if (phaseIndex == 1) { PreviousTimer = Intersection.Signal.Phases[0].Timer; }
                if (phaseIndex == 2) { PreviousTimer = Intersection.Signal.Phases[1].Timer; }
                if (phaseIndex == 3) { PreviousTimer = Intersection.Signal.Phases[2].Timer; }
                if (phaseIndex == 4) { PreviousTimer = Intersection.Signal.Phases[7].Timer; }
                if (phaseIndex == 5) { PreviousTimer = Intersection.Signal.Phases[4].Timer; }
                if (phaseIndex == 6) { PreviousTimer = Intersection.Signal.Phases[5].Timer; }
                if (phaseIndex == 7) { PreviousTimer = Intersection.Signal.Phases[6].Timer; }

                if (Phase.NemaPhaseId != 2 && Phase.NemaPhaseId != 6 && Phase.NemaPhaseId != 0) // Don't compute max. green for coordinated phases
                {
                    if (Intersection.Signal.ForceMode == true) // FIXED FORCE MODE
                        Timer.MaxGreen = (Timer.EndWindow - PreviousTimer.EndTime + Intersection.Signal.CycleLengthSec) % Intersection.Signal.CycleLengthSec;
                    else // FLOATING FORCE MODE
                        Timer.MaxGreen = Phase.AssociatedLaneGroup.SignalPhase.Splits - Phase.AssociatedLaneGroup.SignalPhase.Timer.IntergreenTimeSec;
                }
            }
            return Phases;
        }

        public static List<SignalPhaseData> ComputeAveragePhaseDuration(List<SignalPhaseData> Phases, int CycleLengthSec)
        {
            // Method Parms
            // Delta = Delta_i, either 1.5 or 0.5, see variable definitions for Eq. 31-5, set in VolumeComputations method

            float durationCrossStreet;

            //COMPUTE NUMBER OF ARRIVALS TO REACH MAXOUT n

            foreach (SignalPhaseData Phase in Phases)
            {
                TimerData Timer = Phase.Timer;
                int phaseIndex = Phases.IndexOf(Phase);
                if ((Timer.MaxAllowHdwy > 0) & (Timer.UnbunchedFlow[0] > 0) & (Phase.NemaPhaseId != 2) & (Phase.NemaPhaseId != 6))
                {
                    // Don't compute MAH for coordinated phases

                    //UnbunchedFlow = bunching factor (b)
                    //Delta = headway of bunched vehicle stream in lane group i

                    //Eq. 31-29 second term
                    Timer.PhaseParms.Temp2 = (float)(Timer.PortionFree[0] * Math.Exp(-1 * Timer.UnbunchedFlow[0] * (Timer.MaxAllowHdwy - Timer.Delta[0])));
                    //Eq. 31-29
                    Timer.PhaseParms.ProbOfGreenExtension = 1 - Timer.PhaseParms.Temp2;
                    if (Timer.PhaseParms.ProbOfGreenExtension < 0.0001) { Timer.PhaseParms.ProbOfGreenExtension = 0.0001f; }
                    if (Timer.PhaseParms.ProbOfGreenExtension > 0.9999) { Timer.PhaseParms.ProbOfGreenExtension = 0.9999f; }

                    if (Timer.PhaseParms.Temp2 <= Timer.PortionFree[0])
                    {
                        //Eq. 31-45 numerator; check parentheses, HCM chapter shows open paren before MAH, but no closing paren
                        Timer.PhaseParms.Temp1 = Timer.Delta[0] + Timer.PortionFree[0] / Timer.UnbunchedFlow[0] - (Timer.MaxAllowHdwy + 1 / Timer.UnbunchedFlow[0]) * Timer.PhaseParms.Temp2;
                        
                        //Eq. 31-45
                        Timer.PhaseParms.AvgHdwyGivenLessThanMAH = Timer.PhaseParms.Temp1 / (1 - Timer.PhaseParms.Temp2);
                    }
                    else
                    {
                        Timer.PhaseParms.AvgHdwyGivenLessThanMAH = Timer.Delta[0];
                    }

                    // Find movement that requires the largest queue service time
                    Timer.PhaseParms.MaxQueueClearTime = 0;
                    foreach (int moveDir in Enum.GetValues(typeof(MovementDirection)))
                    {
                        if (Phase.AssociatedLaneGroup.DetectorLength > 0) // Only movements with detection can extent the phase
                            Timer.PhaseParms.QueueClearTime = Timer.MaxQueueClearTime + Phase.StartUpLostTimeSec;
                        else // No movement assigned to this lane group
                            Timer.PhaseParms.QueueClearTime = 0;

                        if (Timer.PhaseParms.QueueClearTime > Timer.PhaseParms.MaxQueueClearTime) { Timer.PhaseParms.MaxQueueClearTime = Timer.PhaseParms.QueueClearTime; }
                    }

                    Timer.MaxQueueClearTime = Timer.PhaseParms.MaxQueueClearTime;
                    Timer.PhaseParms.Temp1 = Timer.MaxGreen - Timer.MaxAllowHdwy - Timer.PhaseParms.MaxQueueClearTime;

                    //Eq. 31-44, Number of arrivals to needed to max out
                    //Following equation is inverse of Eq. 31-45; is this equivalent to Eq. 31-44? This variable is used below in Eq. 31-43
                    Timer.PhaseParms.ArrivalsToMaxOut = Timer.PhaseParms.Temp1 / (Timer.PhaseParms.AvgHdwyGivenLessThanMAH + 0.00001f);
                    if (Timer.PhaseParms.ArrivalsToMaxOut < 0) { Timer.PhaseParms.ArrivalsToMaxOut = 0; }

                    //Eq. 31-43, Probability of phase termination by max out
                    Timer.ProbMaxOut = (float)Math.Pow(Timer.PhaseParms.ProbOfGreenExtension, Timer.PhaseParms.ArrivalsToMaxOut);

                    //Eq. 31-28, Number of extensions before max out                    
                    Timer.PhaseParms.ExtensionsToMaxOut = Timer.PhaseFlowRate[0] * (Timer.MaxGreen - Timer.PhaseParms.MaxQueueClearTime);
                    if (Timer.PhaseParms.ExtensionsToMaxOut < 0) { Timer.PhaseParms.ExtensionsToMaxOut = 0; }

                    //Eq. 31-30, Green extension time grn_e, s
                    //EquilibGreenExtension calculation is 'p' divided by denominator of Eq. 31-30
                    Timer.PhaseParms.EquilibGreenExtension = Timer.PhaseParms.ProbOfGreenExtension / (1 - Timer.PhaseParms.ProbOfGreenExtension) / Timer.PhaseFlowRate[0];
                    //Remainder of Eq. 31-30 calculation
                    Timer.GreenExtension = (float)(Timer.PhaseParms.EquilibGreenExtension * Timer.PhaseParms.ProbOfGreenExtension * (1 - Math.Pow(Timer.PhaseParms.ProbOfGreenExtension, Timer.PhaseParms.ExtensionsToMaxOut)));

                    //Green interval when call is from vehicle
                    //Eq. 31-35
                    Timer.PhaseParms.GreenCall = Timer.PhaseParms.MaxQueueClearTime + Timer.GreenExtension;
                    if (Timer.PhaseParms.GreenCall < Phase.MinGreenIntervalSec) { Timer.PhaseParms.GreenCall = Phase.MinGreenIntervalSec; }

                    //Unbalanced green duration
                    if (Phase.RecallMinMode == true)  //min recall on 
                    {
                        Timer.ProbOfPhaseCall = 1;
                        Timer.PhaseParms.GreenUnbalanced = Timer.PhaseParms.GreenCall;
                    }
                    else if (Phase.RecallMaxMode == true)  //max recall on
                    {
                        Timer.ProbOfPhaseCall = 1;
                        Timer.PhaseParms.GreenUnbalanced = Timer.MaxGreen;
                    }
                    else    //no recalls set on
                    {
                        //Eq. 31-32
                        Timer.ProbOfPhaseCall = (float)(1 - Math.Exp(-1 * Timer.CallingFlow * CycleLengthSec));
                        //Eq. 31-34, with probability of pedestrian call (p_p) assumed to be zero
                        Timer.PhaseParms.GreenUnbalanced = Timer.PhaseParms.GreenCall * Timer.ProbOfPhaseCall;
                    }
                    if (Timer.PhaseParms.GreenUnbalanced > Timer.MaxGreen) { Timer.PhaseParms.GreenUnbalanced = Timer.MaxGreen; }
                    //Eq. 31-37, Unbalanced phase duration
                    Timer.PhaseParms.DurationUnbalanced = Timer.PhaseParms.GreenUnbalanced + Phase.Timer.IntergreenTimeSec;
                }
            }

            //  APPROXIMATE ADJUSTMENT FOR SIMULTANEOUS GAP OUT WHEN ONE PHASE REACHES MAX OUT BEFORE ITS CONCURRENT PHASE

            for (int phaseIndex = 1; phaseIndex < 8; phaseIndex += 2)    //timer number
            {
                SignalPhaseData Phase = Phases[phaseIndex];
                TimerData Timer = Phase.Timer;
                int concurrentTimerIndex = 0;
                if ((Timer.MaxAllowHdwy > 0) & (Timer.UnbunchedFlow[0] > 0) & (Phase.NemaPhaseId != 2) & (Phase.NemaPhaseId != 6))
                {
                    if (phaseIndex == 1) { concurrentTimerIndex = 5; }
                    if (phaseIndex == 3) { concurrentTimerIndex = 7; }
                    if (phaseIndex == 5) { concurrentTimerIndex = 1; }
                    if (phaseIndex == 7) { concurrentTimerIndex = 3; }
                    TimerData ConcurrentTimer = Phases[concurrentTimerIndex].Timer;

                    if ((Phase.GapOutMode == true) & (Phase.RecallMaxMode == false))
                    {
                        //phase associated with timer is set for simultaneous gap out

                        //time of max-out for concurrent phase, relative to previous barrier
                        
                        Timer.PhaseParms.Temp1 = Phases[concurrentTimerIndex - 1].Timer.PhaseParms.DurationUnbalanced + ConcurrentTimer.MaxGreen;
                        //time when extension ends for subject phase
                        Timer.PhaseParms.Temp2 = Phases[phaseIndex - 1].Timer.PhaseParms.DurationUnbalanced + Phase.Timer.PhaseParms.DurationUnbalanced - Phase.Timer.IntergreenTimeSec;

                        //if (subject phase extends after concurrent phase reaches max then extension is reduced because only subject stream is extending
                        //both phases
                        if (Timer.PhaseParms.Temp2 > Timer.PhaseParms.Temp1)
                        {
                            Timer.PhaseParms.DurationUnbalanced -= -Timer.GreenExtension; // subtract out extension time
                            //See discussion for Step N, page 31-19 (Draft Version 6.1)
                            Timer.PhaseParms.VolumeRatio = (Timer.UnbunchedFlow[1] + Timer.UnbunchedFlow[2] + Timer.UnbunchedFlow[3]) / Timer.UnbunchedFlow[0];
                            Timer.PhaseParms.ExtensionByOnePhase = Timer.PhaseParms.Temp2 - Timer.PhaseParms.Temp1;

                            if (Timer.PhaseParms.ExtensionByOnePhase > Timer.GreenExtension)
                            {
                                Timer.GreenExtension *= Timer.PhaseParms.VolumeRatio;
                            }
                            else
                            {
                                Timer.GreenExtension -= Timer.PhaseParms.ExtensionByOnePhase + Timer.PhaseParms.ExtensionByOnePhase * Timer.PhaseParms.VolumeRatio;
                            }
                            Timer.PhaseParms.DurationUnbalanced += Timer.GreenExtension; // add back new extension time
                        }
                        if (Timer.PhaseParms.DurationUnbalanced < 0) { Timer.PhaseParms.DurationUnbalanced = 0; }
                    }
                }
            }

            //COMPUTE AVERAGE PHASE DURATION

            //Eq. 31-38?
            durationCrossStreet = Phases[2].Timer.PhaseParms.DurationUnbalanced + Phases[3].Timer.PhaseParms.DurationUnbalanced;
            if (durationCrossStreet < Phases[6].Timer.PhaseParms.DurationUnbalanced + Phases[7].Timer.PhaseParms.DurationUnbalanced)
                durationCrossStreet = Phases[6].Timer.PhaseParms.DurationUnbalanced + Phases[7].Timer.PhaseParms.DurationUnbalanced;

            foreach (SignalPhaseData Phase in Phases)
            {
                TimerData Timer = Phase.Timer;
                int phaseIndex = Phases.IndexOf(Phase);
                Timer.PhaseParms.ConflictingPhase = GetPhaseFromMovement(Phases, MovementData.GetOpposingMvmt(Phase.NemaMvmtId));   //timer serving conflicting/opposing mvmt on same street
                int conflictingPhaseIndex = Phases.IndexOf(Timer.PhaseParms.ConflictingPhase);
                if (Phase.NemaPhaseId == 1 || Phase.NemaPhaseId == 2 || Phase.NemaPhaseId == 5 || Phase.NemaPhaseId == 6)
                {
                    // Timer is associated with the coordinated street
                    if (Phase.NemaPhaseId == 2 | Phase.NemaPhaseId == 6) // Coord phase, check phase numbers here because coord phases get unused time
                    {
                        //Eq. 31-40?
                        Timer.PhaseParms.AdjustedTimerDuration = CycleLengthSec - Timer.PhaseParms.DurationCrossStreet - Phases[conflictingPhaseIndex].Timer.PhaseParms.DurationUnbalanced;
                    }
                    else // Left-turn
                    {
                        Timer.PhaseParms.AdjustedTimerDuration = Timer.PhaseParms.DurationUnbalanced;
                    }
                }
                else if (Phase.NemaPhaseId == 3 || Phase.NemaPhaseId == 4 || Phase.NemaPhaseId == 7 || Phase.NemaPhaseId == 8)
                {
                    // Timer is associated with the cross street
                    if (phaseIndex == 2 | phaseIndex == 6)  // check timer numbers here because timers 3 and 7 go first in each ring
                    {
                        Timer.PhaseParms.AdjustedTimerDuration = Timer.PhaseParms.DurationUnbalanced;
                    }
                    else // j == 4 or j == 8
                    {
                        Timer.PhaseParms.AdjustedTimerDuration = Timer.PhaseParms.DurationCrossStreet - Phases[conflictingPhaseIndex].Timer.PhaseParms.DurationUnbalanced;
                    }
                }
            }
            // ESTIMATE NEW PHASE DURATION AS AVERAGE OF PREVIOUS ESTIMATE AND NEW ESTIMATE
            // THIS TECHNIQUE SLOWS CONVERGENCE BUT PREVENTS OSCILLATIONS TO EXTREME VALUES WITH LOSS OF CONVERGENCE
            foreach (SignalPhaseData Phase in Phases)
            {
                Phase.Timer.Duration += Phase.Timer.PhaseParms.AdjustedTimerDuration / 2;
            }
            return Phases;
        }

        public static SignalPhaseData GetPhaseFromPhaseNumber(List<SignalPhaseData> Phases, int PhaseNumber)
        {
            foreach (SignalPhaseData Phase in Phases)
                if (Phase.NemaPhaseId == PhaseNumber)
                    return Phase;

            return null;
        }

        public static SignalPhaseData GetPhaseFromMovement(List<SignalPhaseData> Phases, NemaMovementNumbers NemaMoveNum)
        {
            foreach (SignalPhaseData Phase in Phases)
                if (Phase.NemaMvmtId == NemaMoveNum)
                    return Phase;

            return null;
        }
    }
}

