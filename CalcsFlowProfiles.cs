using System;
using System.Linq;
using System.Xml.Serialization;


namespace HCMCalc_UrbanStreets
{
    /// <summary>
    /// Contains the Discharge and Projected Flow Profiles, as well as several variables used in their calculations. These profiles are used in platoon dispersion calculations.
    /// </summary>
    public class FlowProfileParms
    {
        public FlowProfileParms()
        {

        }

        float[,] _dischargeFlowProfile;
        float[,] _projectedFlowProfile;
        float _weightedDischargeFlowRate;
        float _dischargeFlowRateVehPerSec;
        float _satFlowRateVehPerSec;
        int leftMovementLaneGroupLanes;
        int throughMovementLaneGroupLanes;
        int rightMovementLaneGroupLanes;

        [XmlIgnore]
        public float[,] DischargeFlowProfile { get => _dischargeFlowProfile; set => _dischargeFlowProfile = value; }
        [XmlIgnore]
        public float[,] ProjectedFlowProfile { get => _projectedFlowProfile; set => _projectedFlowProfile = value; }
        public float WeightedDischargeFlowRate { get => _weightedDischargeFlowRate; set => _weightedDischargeFlowRate = value; }
        public float DischargeFlowRateVehPerSec { get => _dischargeFlowRateVehPerSec; set => _dischargeFlowRateVehPerSec = value; }
        public float SatFlowRateVehPerSec { get => _satFlowRateVehPerSec; set => _satFlowRateVehPerSec = value; }
        public int LeftMovementLaneGroupLanes { get => leftMovementLaneGroupLanes; set => leftMovementLaneGroupLanes = value; }
        public int ThroughMovementLaneGroupLanes { get => throughMovementLaneGroupLanes; set => throughMovementLaneGroupLanes = value; }
        public int RightMovementLaneGroupLanes { get => rightMovementLaneGroupLanes; set => rightMovementLaneGroupLanes = value; }
    }
    public class CalcsFlowProfiles
    {
        /// <summary>
        /// Computes the Discharge Flow Profile for a segment based on the three left, through, and right discharge volumes.
        /// </summary>
        /// <param name="analysisSegment"></param>
        /// <param name="dischargeMovementsArray"></param>
        /// <returns></returns>
        public static float[,] ComputeDischargeFlowProfile(SegmentData analysisSegment, NemaMovementNumbers[] dischargeMovementsArray)
        {
            FlowProfileParms FlowProfileParms = new FlowProfileParms();
            int CycleLengthSec = analysisSegment.Intersection.Signal.CycleLengthSec;
            FlowProfileParms.DischargeFlowProfile = new float[4, CycleLengthSec];

            foreach (SignalPhaseData Phase in analysisSegment.Intersection.Signal.Phases)
            {
                TimerData Timer = Phase.Timer;
                LaneGroupData analysisLaneGroup = Phase.AssociatedLaneGroup;
                Timer.Results.Movement = dischargeMovementsArray.Select((NemaMvmtID, index) => new { NemaMvmtID, index }).FirstOrDefault(x => x.NemaMvmtID.Equals(Phase.NemaMvmtId))?.index ?? -1;
                int Movement = Timer.Results.Movement;

                if ((Phase.NemaMvmtId == dischargeMovementsArray[0] || Phase.NemaMvmtId == dischargeMovementsArray[1] || Phase.NemaMvmtId == dischargeMovementsArray[2]) && (GetDischargeVolume(analysisLaneGroup, Movement) != 0))
                {
                    Timer.Results.P_R = (analysisLaneGroup.PctRightTurns * analysisLaneGroup.NumLanes / 100);
                    Timer.Results.F_RT = 1f / (1 + Timer.Results.P_R * (analysisLaneGroup.AnalysisResults.SatFlowRate.AdjFact.RightTurnEquivalency - 1));
                    if (Movement == 0)
                        Timer.MvmtSatFlow[Movement] = analysisLaneGroup.AnalysisResults.SatFlowRate.AdjustedValueVehHrLane * analysisLaneGroup.NumLanes;
                    else if (Movement == 1)
                        Timer.MvmtSatFlow[Movement] = ((1 - Timer.Results.P_R) * Timer.Results.F_RT + (analysisLaneGroup.NumLanes - 1)) * analysisLaneGroup.AnalysisResults.SatFlowRate.BaseValuePCHrLane;
                    else if (Movement == 2)
                        Timer.MvmtSatFlow[Movement] = Timer.Results.P_R * Timer.Results.F_RT * analysisLaneGroup.AnalysisResults.SatFlowRate.BaseValuePCHrLane;

                    Timer.Results.WeightedDischargeFlowRate = GetDischargeVolume(analysisLaneGroup, Movement);
                    Timer.Results.DischargeFlowRateVehPerSec = GetDischargeVolume(analysisLaneGroup, Movement) / (analysisLaneGroup.NumLanes * 3600);
                    Timer.Results.SatFlowRateVehPerLanePerSec = Timer.MvmtSatFlow[Movement] / (analysisLaneGroup.NumLanes * 3600); // Protected sat-flow
                    if ((Movement == 2 && analysisLaneGroup.Type == LaneMovementsAllowed.ThruRightShared) || (Movement == 0 && analysisLaneGroup.Type == LaneMovementsAllowed.ThruLeftShared))
                    {
                        Timer.Results.DischargeFlowRateVehPerSec = GetDischargeVolume(analysisLaneGroup, Movement) / 3600;
                        Timer.Results.SatFlowRateVehPerLanePerSec = Timer.MvmtSatFlow[Movement] / 3600; // Protected sat-flow
                    }
                    Timer.Results.RedTimeFlow = (1 - analysisLaneGroup.PortionOnGreen) * Timer.Results.DischargeFlowRateVehPerSec * CycleLengthSec / (CycleLengthSec - analysisLaneGroup.SignalPhase.GreenEffectiveSec);
                    Timer.Results.GreenTimeFlow = analysisLaneGroup.PortionOnGreen * Timer.Results.DischargeFlowRateVehPerSec * CycleLengthSec / analysisLaneGroup.SignalPhase.GreenEffectiveSec;
                    Timer.Results.EffectiveRedSec = CycleLengthSec - (Timer.Duration - Timer.IntergreenTimeSec - analysisLaneGroup.SignalPhase.StartUpLostTimeSec + analysisLaneGroup.SignalPhase.EndUseSec);
                    Timer.Results.QueueRedOnly = Timer.Results.WeightedDischargeFlowRate * Timer.Results.EffectiveRedSec / (Timer.MvmtSatFlow[Movement] - Timer.Results.WeightedDischargeFlowRate);
                    Timer.Results.CycleStart = (int)(Timer.StartTime + 0.5);
                    Timer.Results.QueueServiceStart = (int)(Timer.StartTime + analysisLaneGroup.SignalPhase.StartUpLostTimeSec + 0.5);
                    Timer.Results.PhaseTotalRunningTime = (int)(Timer.StartTime + analysisLaneGroup.SignalPhase.StartUpLostTimeSec + Timer.Results.QueueRedOnly + 0.5);
                    Timer.Results.EffectiveRedStart = (int)(Timer.StartTime + Timer.Duration - Timer.IntergreenTimeSec + analysisLaneGroup.SignalPhase.EndUseSec + 0.5);
                    if (Timer.Results.PhaseTotalRunningTime < 0)
                        Timer.Results.PhaseTotalRunningTime = Timer.Results.EffectiveRedStart;
                    for (int TimeIndex = 0; TimeIndex < CycleLengthSec; TimeIndex++)
                    {
                        int StepsSinceGreenStart = TimeIndex + Timer.Results.CycleStart;
                        int ArrayStorageIndex = StepsSinceGreenStart % CycleLengthSec;
                        if (StepsSinceGreenStart >= Timer.Results.QueueServiceStart && StepsSinceGreenStart < Timer.Results.PhaseTotalRunningTime)  // Through, Right, & Prot. Left
                            FlowProfileParms.DischargeFlowProfile[Movement, ArrayStorageIndex] = Timer.Results.SatFlowRateVehPerLanePerSec;
                        else if (StepsSinceGreenStart >= Timer.Results.PhaseTotalRunningTime && StepsSinceGreenStart < Timer.Results.EffectiveRedStart)
                            if (Timer.Results.GreenTimeFlow > 0)
                                FlowProfileParms.DischargeFlowProfile[Movement, ArrayStorageIndex] = Timer.Results.GreenTimeFlow;
                            else
                                FlowProfileParms.DischargeFlowProfile[Movement, ArrayStorageIndex] = 0;
                    }
                }
            }
            return FlowProfileParms.DischargeFlowProfile;
        }

        /// <summary>
        /// Computes the Projected Flow Profile for a segment, and smooths it a set amount of times by recalculating (default loops = 5)
        /// </summary>
        /// <param name="analysisSegment"></param>
        /// <param name="RunTime"></param>
        /// <returns></returns>
        public static float[,] ComputeProjectedProfile(SegmentData analysisSegment, float RunTime)
        {
            float[,] ProjectedFlowProfile = analysisSegment.Link.Results.ProjectedFlowProfile; // may not want to use FlowProfileParms since it would overwrite itself
            analysisSegment.Link.Results.SmoothingFactor = 1 / (1 + 0.138f * RunTime + 0.315f); // Eq. 30-11
            analysisSegment.Link.Results.PlatoonArrivalTime = (int)(RunTime - 1 / analysisSegment.Link.Results.SmoothingFactor + 1 + 0.25f + 0.5);
            
            for (int SmoothProjectedProfile = 0; SmoothProjectedProfile < 5; SmoothProjectedProfile++) // CNB: This may want to be an input. Increasing the iterations makes the ProjectedFlowProfile "smoother"
            {
                foreach (int Movement in Enum.GetValues(typeof(MovementDirectionWithMidBlock)))
                {
                    for (int TimeIndex = 0; TimeIndex < analysisSegment.Intersection.Signal.CycleLengthSec; TimeIndex++)
                    {
                        int StepsSinceGreenStart = TimeIndex + analysisSegment.Link.Results.PlatoonArrivalTime;
                        int ArrayStorageIndex = StepsSinceGreenStart % analysisSegment.Intersection.Signal.CycleLengthSec;
                        int PreviousStorageIndex = ArrayStorageIndex - 1;
                        if (PreviousStorageIndex < 0) { PreviousStorageIndex = analysisSegment.Intersection.Signal.CycleLengthSec - 1; }
                        ProjectedFlowProfile[Movement, ArrayStorageIndex] = analysisSegment.Link.Results.DischargeFlowProfile[Movement, TimeIndex] * analysisSegment.Link.Results.SmoothingFactor + ProjectedFlowProfile[Movement, PreviousStorageIndex] * (1 - analysisSegment.Link.Results.SmoothingFactor);
                    }
                }
            }
            return ProjectedFlowProfile;
        }

        /// <summary>
        /// Returns the adjusted Discharge Volume for a lane group based on turning percents and the lane group's movement type.
        /// </summary>
        /// <param name="analysisLaneGroup"></param>
        /// <param name="moveDir"></param>
        /// <returns></returns>
        public static float GetDischargeVolume(LaneGroupData analysisLaneGroup, int moveDir)
        {
            if (analysisLaneGroup.Type == LaneMovementsAllowed.LeftOnly || analysisLaneGroup.Type == LaneMovementsAllowed.RightOnly || analysisLaneGroup.Type == LaneMovementsAllowed.ThruOnly)
                return analysisLaneGroup.DischargeVolume;
            else if (analysisLaneGroup.Type == LaneMovementsAllowed.ThruLeftShared)
            {
                if ((MovementDirection)moveDir == MovementDirection.Left)
                    return analysisLaneGroup.DischargeVolume * (analysisLaneGroup.PctLeftTurns / 100);
                else if ((MovementDirection)moveDir == MovementDirection.Through)
                    return analysisLaneGroup.DischargeVolume * (1 - analysisLaneGroup.PctLeftTurns / 100);
                else
                    return 0;
            }
            else if (analysisLaneGroup.Type == LaneMovementsAllowed.ThruRightShared)
            {
                if ((MovementDirection)moveDir == MovementDirection.Right)
                    return analysisLaneGroup.DischargeVolume * (analysisLaneGroup.PctRightTurns / 100);
                else if ((MovementDirection)moveDir == MovementDirection.Through)
                    return analysisLaneGroup.DischargeVolume * (1 - analysisLaneGroup.PctRightTurns / 100);
                else
                    return 0;
            }
            return 0;
        }

        /// <summary>
        /// Returns the portion of time blocked using block times for an access point.
        /// </summary>
        /// <param name="Segment"></param>
        /// <param name="AccessPoint"></param>
        /// <returns></returns>
        public static float[] ComputePortionTimeBlocked(SegmentData Segment, AccessPointData AccessPoint)
        {
            // BlockTime[0] = Both dir.
            // BlockTime[1] = EBNB
            // BlockTime[2] = WBSB
            float[] PortionTimeBlocked = new float[12];
            PortionTimeBlocked[0] = AccessPoint.BlockTime[2] / Segment.Link.AccessPoints.Count;
            PortionTimeBlocked[1] = 0;
            PortionTimeBlocked[2] = 0;
            PortionTimeBlocked[3] = AccessPoint.BlockTime[1] / Segment.Link.AccessPoints.Count;
            PortionTimeBlocked[4] = 0;
            PortionTimeBlocked[5] = 0;
            PortionTimeBlocked[6] = AccessPoint.BlockTime[0] / Segment.Link.AccessPoints.Count;
            PortionTimeBlocked[7] = AccessPoint.BlockTime[0] / Segment.Link.AccessPoints.Count;
            PortionTimeBlocked[8] = AccessPoint.BlockTime[1] / Segment.Link.AccessPoints.Count;
            PortionTimeBlocked[9] = AccessPoint.BlockTime[0] / Segment.Link.AccessPoints.Count;
            PortionTimeBlocked[10] = AccessPoint.BlockTime[0] / Segment.Link.AccessPoints.Count;
            PortionTimeBlocked[11] = AccessPoint.BlockTime[2] / Segment.Link.AccessPoints.Count;
            return PortionTimeBlocked;
        }


        /// <summary>
        /// Computes the block time for an access point.
        /// </summary>
        /// <param name="Segment"></param>
        /// <param name="AccessPoint"></param>
        /// <returns></returns>
        public static int[] ComputeBlockTime(SegmentData Segment, AccessPointData AccessPoint)
        {
            float MaxPlatoonHdwySec = 0;
            float MinBlockingFlowRate = 1 / MaxPlatoonHdwySec;
            int[] BlockTime = new int[3];
            for (int TimeIndex = 0; TimeIndex < Segment.Intersection.Signal.CycleLengthSec; TimeIndex++)
            {
                float EBNB_Flow = 0;
                float WBSB_Flow = 0;
                //VBA Code: AcPtLanesH uses indexes: [Segment, Access Point, 1-12 Movement] These movements are most likely from the HCM map, not NEMA map in the code
                if (AccessPoint.ArrivalFlowRate[0, TimeIndex] > 0)
                    EBNB_Flow = AccessPoint.ArrivalFlowRate[0, TimeIndex] / MovementData.GetLaneGroupFromNemaMovementNumber(Segment.Intersection, NemaMovementNumbers.EBThru).Lanes.Count; // VBA Comment: lanes for eb/nb thru, HCM numbering
                if (AccessPoint.ArrivalFlowRate[1, TimeIndex] > 0)
                    WBSB_Flow = AccessPoint.ArrivalFlowRate[1, TimeIndex] / MovementData.GetLaneGroupFromNemaMovementNumber(Segment.Intersection, NemaMovementNumbers.WBThru).Lanes.Count; // VBA Comment: lanes for wb/sb thru, HCM numbering
                if (EBNB_Flow > MinBlockingFlowRate)
                    BlockTime[1]++;
                if (WBSB_Flow > MinBlockingFlowRate)
                    BlockTime[2]++;
                if (EBNB_Flow > MinBlockingFlowRate | WBSB_Flow > MinBlockingFlowRate)
                    BlockTime[0]++;
            }
            return BlockTime;
        }

        /// <summary>
        /// Computes the Arrival Flow Profile for an approach using the projected flow profile and the upstream demands.
        /// </summary>
        /// <param name="analysisSegment"></param>
        /// <param name="upstreamSegmentIndex"></param>
        /// <param name="analysisApproach"></param>
        /// <param name="accessPointIndex"></param>
        /// <param name="segDirection"></param>
        /// <param name="ODdata"></param>
        /// <returns></returns>
        public static float[,] ComputeArrivalFlowProfile(SegmentData analysisSegment, int upstreamSegmentIndex, ApproachData analysisApproach, int accessPointIndex, SegmentDirection segDirection, OriginDestinationData ODdata)
        {
            float[,] arrivalFlowProfile = new float[3, analysisSegment.Intersection.Signal.CycleLengthSec];
            FlowProfileParms FlowProfileParms = new FlowProfileParms();
            FlowProfileParms.LeftMovementLaneGroupLanes = 0;
            FlowProfileParms.ThroughMovementLaneGroupLanes = 0;
            FlowProfileParms.RightMovementLaneGroupLanes = 0;
            foreach (LaneGroupData LaneGroup in analysisApproach.LaneGroups)
            {
                if (LaneGroup.Type == LaneMovementsAllowed.ThruLeftShared || LaneGroup.Type == LaneMovementsAllowed.ThruAndLeftTurnBay)
                {
                    FlowProfileParms.LeftMovementLaneGroupLanes = 1;
                    FlowProfileParms.ThroughMovementLaneGroupLanes = LaneGroup.NumLanes;
                }
                else if (LaneGroup.Type == LaneMovementsAllowed.LeftOnly)
                {
                    FlowProfileParms.LeftMovementLaneGroupLanes = LaneGroup.NumLanes;
                }
                else if (LaneGroup.Type == LaneMovementsAllowed.ThruRightShared || LaneGroup.Type == LaneMovementsAllowed.ThruAndRightTurnBay)
                {
                    FlowProfileParms.RightMovementLaneGroupLanes = 1;
                    FlowProfileParms.ThroughMovementLaneGroupLanes = LaneGroup.NumLanes;
                }
                else if (LaneGroup.Type == LaneMovementsAllowed.RightOnly)
                {
                    FlowProfileParms.RightMovementLaneGroupLanes = LaneGroup.NumLanes;
                }
                else
                {
                    FlowProfileParms.ThroughMovementLaneGroupLanes = LaneGroup.NumLanes;
                }
        }
            for (int downMvmt = 0; downMvmt < 3; downMvmt++)
            {
                for (int TimeIndex = 0; TimeIndex < analysisSegment.Intersection.Signal.CycleLengthSec; TimeIndex++)
                {
                    foreach (int Movement in Enum.GetValues(typeof(MovementDirectionWithMidBlock)))
                    {
                        if (Movement == 0)
                            arrivalFlowProfile[downMvmt, TimeIndex] += analysisSegment.Link.Results.ProjectedFlowProfile[Movement, TimeIndex] * ODdata.Matrix[upstreamSegmentIndex, (int)segDirection, accessPointIndex, Movement, downMvmt] * FlowProfileParms.LeftMovementLaneGroupLanes;
                        else if (Movement == 1)
                            arrivalFlowProfile[downMvmt, TimeIndex] += analysisSegment.Link.Results.ProjectedFlowProfile[Movement, TimeIndex] * ODdata.Matrix[upstreamSegmentIndex, (int)segDirection, accessPointIndex, Movement, downMvmt] * FlowProfileParms.ThroughMovementLaneGroupLanes;
                        else if (Movement == 2)
                            arrivalFlowProfile[downMvmt, TimeIndex] += analysisSegment.Link.Results.ProjectedFlowProfile[Movement, TimeIndex] * ODdata.Matrix[upstreamSegmentIndex, (int)segDirection, accessPointIndex, Movement, downMvmt] * FlowProfileParms.RightMovementLaneGroupLanes;
                        else if (Movement == 3)
                            arrivalFlowProfile[downMvmt, TimeIndex] += analysisSegment.Link.Results.ProjectedFlowProfile[Movement, TimeIndex] * ODdata.Matrix[upstreamSegmentIndex, (int)segDirection, accessPointIndex, Movement, downMvmt];
                    }
                }
            }
            return arrivalFlowProfile;
        }

        /// <summary>
        /// Computes the conflict rate for an access point.
        /// </summary>
        /// <param name="analysisSegment"></param>
        /// <param name="analysisAccessPoint"></param>
        /// <param name="analysisApproach"></param>
        /// <param name="conflictingMovements"></param>
        /// <param name="segDirection"></param>
        /// <returns></returns>
        public static float[,] ComputeConflictFlowRate(SegmentData analysisSegment, AccessPointData analysisAccessPoint, ApproachData analysisApproach, NemaMovementNumbers[] conflictingMovements, SegmentDirection segDirection)
        {
            float[,] arrivalFlowRate = analysisAccessPoint.ArrivalFlowRate;
            for (int TimeIndex = 0; TimeIndex < analysisSegment.Intersection.Signal.CycleLengthSec; TimeIndex++)
            {
                float ConflictingFlow = analysisApproach.Results.ArrivalFlowProfile[1, TimeIndex];
                if (MovementData.GetLaneGroupFromNemaMovementNumber(analysisSegment.Intersection, conflictingMovements[0]).Type == LaneMovementsAllowed.ThruLeftShared)
                {
                    ConflictingFlow += analysisApproach.Results.ArrivalFlowProfile[0, TimeIndex]; // Downstream left at access point
                }
                if (MovementData.GetLaneGroupFromNemaMovementNumber(analysisSegment.Intersection, conflictingMovements[2]).Type == LaneMovementsAllowed.ThruRightShared)
                {
                    ConflictingFlow += analysisApproach.Results.ArrivalFlowProfile[2, TimeIndex]; // Downstream right
                }
                arrivalFlowRate[(int)segDirection, TimeIndex] = ConflictingFlow;
            }
            return arrivalFlowRate;
        }

        /// <summary>
        /// Computes the portion on green, using the platoon dispersion method, for a lane group.
        /// </summary>
        /// <param name="analysisSegment"></param>
        /// <param name="analysisApproach"></param>
        /// <param name="analysisLaneGroup"></param>
        /// <param name="downMvmt"></param>
        /// <param name="segDirection"></param>
        /// <returns></returns>
        public static float ComputePortionOnGreen(SegmentData analysisSegment, ApproachData analysisApproach, LaneGroupData analysisLaneGroup, int downMvmt, SegmentDirection segDirection)
        {
            int CycleLengthSec = analysisSegment.Intersection.Signal.CycleLengthSec;
            TimerData Timer = analysisLaneGroup.SignalPhase.Timer;
            Timer.Results.CycleStart = (int)(Timer.StartTime + 0.5);
            Timer.Results.QueueServiceStart = (int)(Timer.StartTime + analysisLaneGroup.SignalPhase.StartUpLostTimeSec + 0.5);
            Timer.Results.EffectiveRedStart = (int)(Timer.StartTime + Timer.Duration - Timer.IntergreenTimeSec + analysisLaneGroup.SignalPhase.EndUseSec + 0.5);
            float arrivalFlow = 0;
            float arrivalOnGreen = 0;
            for (int TimeIndex = 0; TimeIndex < CycleLengthSec; TimeIndex++)
            {
                int stepsSinceGreenStart = TimeIndex + Timer.Results.CycleStart;
                int arrayStorageIndex = (stepsSinceGreenStart % CycleLengthSec);
                arrivalFlow += analysisApproach.Results.ArrivalFlowProfile[downMvmt, arrayStorageIndex];
                if (stepsSinceGreenStart >= Timer.Results.QueueServiceStart && stepsSinceGreenStart < Timer.Results.EffectiveRedStart)
                {
                    arrivalOnGreen += analysisApproach.Results.ArrivalFlowProfile[downMvmt, arrayStorageIndex];
                }
            }
            if (arrivalFlow > 0)
            {
                return arrivalOnGreen / arrivalFlow;
            }
            else
            {
                return 0;
            }
        }
    }
}