using System;
using System.Collections.Generic;


namespace HCMCalc_UrbanStreets
{
    /// <summary>
    /// Handles methods related to assigning calculation-related properties to arterials.
    /// </summary>
    public static class CalcsArterial
    {
        //public static bool LTMessage = false;
        /// <summary>
        /// Handles the assignment of arterial properties that require calculations and cannot be straightly assigned.
        /// </summary>
        /// <param name="Project"></param>
        /// <param name="Arterial"></param>
        public static void CalcResults(ProjectData Project, ref ArterialData Arterial/*, ref List<SegmentDataMM> SegMM*/)
        {
            float TotalLaneFeet = 0;
            float PrevVC = 0;
            float AnalysisLaneGroupControlDelay = 0;
            Arterial.Results = new ResultsArterialData();
            Arterial.OverCapacity = false;
            //CalcsBicycle.NumSidePathSegs = 0;

            OriginDestinationData ODdata = CalcsOriginDestination.DefineODmatrix(Arterial);

            foreach (SegmentDirection segDirection in Enum.GetValues(typeof(SegmentDirection)))
            {
                NemaMovementNumbers[] inputMovementsArray = { NemaMovementNumbers.SBLeft, NemaMovementNumbers.EBThru, NemaMovementNumbers.NBThru };
                NemaMovementNumbers[] outputMovementsArray = { NemaMovementNumbers.EBLeft, NemaMovementNumbers.EBThru, NemaMovementNumbers.EBThru };
                if (segDirection == SegmentDirection.WBSB)
                {
                    ReverseArterial(ref Arterial);
                    inputMovementsArray = new NemaMovementNumbers[] { NemaMovementNumbers.NBLeft, NemaMovementNumbers.WBThru, NemaMovementNumbers.SBThru }; // 3rd = SBR
                    outputMovementsArray = new NemaMovementNumbers[] { NemaMovementNumbers.WBLeft, NemaMovementNumbers.WBThru, NemaMovementNumbers.WBThru }; // 3rd = WBR
                }

                foreach (SegmentData segment in Arterial.Segments)
                {
                    segment.Results = new ResultsSegmentData();
                    segment.Link.Results = new ResultsLinkData(segment.Intersection.Signal.CycleLengthSec);
                    segment.Intersection.Results = new ResultsIntersectionData();

                    segment.Intersection.DemandVolumeVehPerHr = 0;
                    segment.Intersection.Results.ControlDelay = 0;
                    segment.Link.OutsideLaneWidth = OutLaneWidth.Typical;
                    //segment.Link.Results.SpeedRatio = segment.Link.Results.AverageSpeed / segment.Link.Results.BaseFreeFlowSpeed;

                    if (Arterial.Segments.IndexOf(segment) > 0)
                    {
                        if (Project.AnalMode == AnalysisMode.Operations)
                        {
                            segment.Link.Results.BaseFreeFlowSpeed = SegmentCalcs.BaseFreeFlowSpeed(Project.AnalMode, Arterial.Area, segment.Link.PostSpeedMPH, segment.Link.NumLanes, segment.Link.LengthFt, segment.Link.MedType, segment.Link.PropRestrictMedian, segment.Link.PropCurbRightSide, segment.Link.PctOnStreetParking, segment.Link.AccessPoints.Count, segment.Link.AccessPoints.Count);
                            segment.Link.Results.FreeFlowSpeedMPH = SegmentCalcs.FreeFlowSpeed(segment.Link.Results.BaseFreeFlowSpeed, segment.LengthFt);
                        }
                        else
                        {
                            segment.Link.Results.BaseFreeFlowSpeed = segment.Link.PostSpeedMPH;
                            segment.Link.Results.FreeFlowSpeedMPH = segment.Link.Results.BaseFreeFlowSpeed + 5;
                        }

                        segment.Link.AdjDDHV = 0;
                        for (int Movement = 0; Movement < 3; Movement++)
                        {
                            segment.Link.AdjDDHV += ODdata.SegOriginVol[Arterial.Segments.IndexOf(segment) - 1, (int)segDirection, 0, Movement]; // CNB: May not necessarily be AdjDDHV. Could be Midsegment Volume.
                        }

                        segment.Link.Results.RunningTimeCalcParms = SegmentCalcs.GetRunningTimeParms(Project.AnalMode, Arterial.Area, segment.Link.Results.FreeFlowSpeedMPH, segment.Link.AdjDDHV, segment.Link.NumLanes, segment.LengthFt, segment.Link.OnStreetParkingExists, segment.Link.ParkingActivity, segment.Link.AccessPoints.Count, segment.Link.AccessPoints.Count);
                        //segment.Link.Results.RunningTimeCalcParms = SegmentCalcs.GetRunningTimeParms(Project.AnalMode, Arterial.Area, segment.Link.Results.FreeFlowSpeedMPH, Arterial.TestSerVol, segment.Link.NumLanes, segment.Link.LengthFt, segment.Link.OnStreetParkingExists, segment.Link.ParkingActivity, segment.Link.AccessPoints.Count, segment.Link.AccessPoints.Count);
                        SegmentData upstreamSegment = Arterial.Segments[Arterial.Segments.IndexOf(segment) - 1];
                        if (Project.AnalMode == AnalysisMode.Operations)
                        {
                            segment.Link.Results.DischargeFlowProfile = CalcsFlowProfiles.ComputeDischargeFlowProfile(upstreamSegment, inputMovementsArray);
                            for (int accessPtIndex = 0; accessPtIndex < upstreamSegment.Link.AccessPoints.Count; accessPtIndex++)
                            {
                                for (int TimeIndex = 0; TimeIndex < upstreamSegment.Intersection.Signal.CycleLengthSec; TimeIndex++)
                                    segment.Link.Results.DischargeFlowProfile[3, TimeIndex] = ODdata.SegOriginVol[Arterial.Segments.IndexOf(upstreamSegment), (int)segDirection, accessPtIndex + 1, 3] / 3600;
                                segment.Link.Results.RunTimeSec = SegmentCalcs.RunningTime(segment.Link.Results.RunningTimeCalcParms, segment.Link.Results.FreeFlowSpeedMPH, segment.LengthFt * upstreamSegment.Link.AccessPoints[accessPtIndex].Location, Project.AnalMode);
                                segment.Link.Results.ProjectedFlowProfile = CalcsFlowProfiles.ComputeProjectedProfile(segment, segment.Link.Results.RunTimeSec);
                            }
                            for (int TimeIndex = 0; TimeIndex < upstreamSegment.Intersection.Signal.CycleLengthSec; TimeIndex++)
                                segment.Link.Results.DischargeFlowProfile[3, TimeIndex] = ODdata.SegOriginVol[Arterial.Segments.IndexOf(upstreamSegment), (int)segDirection, 0, 3] / 3600;
                        }
                        //Eq. 18-7
                        segment.Link.Results.RunTimeSec = SegmentCalcs.RunningTime(segment.Link.Results.RunningTimeCalcParms, segment.Link.Results.FreeFlowSpeedMPH, segment.LengthFt, Project.AnalMode);
                        segment.Link.Results.RunSpeedMPH = (segment.LengthFt / segment.Link.Results.RunTimeSec) * (3600 / (float)5280); //segment.Intersection.Width
                        segment.Link.Results.BaseFreeFlowTravelTime = SegmentCalcs.BaseFFTravTime(segment.LengthFt, segment.Link.Results.BaseFreeFlowSpeed);  //segment.Intersection.Width
                        segment.Link.Results.FreeFlowTravelTime = SegmentCalcs.FFTravTime(segment.LengthFt, segment.Link.Results.FreeFlowSpeedMPH);
                        segment.Link.Results.ProjectedFlowProfile = CalcsFlowProfiles.ComputeProjectedProfile(segment, segment.Link.Results.RunTimeSec);
                    }

                    /* Necessary Inputs to run Signalized Intersection Module:
                    *ComputeQAPolygon*
                    All Phases
                    Cycle Length

                    *VolumeComputations*
                    All Phases

                    *MaximumAllowableHeadway*
                    All Phases
                    Posted Speed

                    *ComputeEquivalentMaxGreen*
                    All Phases
                    Reference Phase ID
                    Cycle Length
                    Offset
                    Cycle Force Mode

                    *ComputeAveragePhaseDuration*
                    All Phases
                    Detector Length (specifically, the one for a movement, so possibly lane group detector length)
                    Cycle Length
                    */

                    foreach (ApproachData approach in segment.Intersection.Approaches)
                    {
                        foreach (LaneGroupData laneGroup in approach.LaneGroups)
                        {
                            if (laneGroup.Type == LaneMovementsAllowed.LeftOnly || laneGroup.Type == LaneMovementsAllowed.ThruAndLeftTurnBay)
                            {
                                approach.LeftTurnBayExists = true;
                            }
                            else if (laneGroup.Type == LaneMovementsAllowed.RightOnly || laneGroup.Type == LaneMovementsAllowed.ThruAndRightTurnBay)
                            {
                                approach.RightTurnBayExists = true;
                            }
                        }

                        approach.Results = new ResultsIntersectionApproachData(segment.Intersection.Signal.CycleLengthSec);
                        approach.Results.ControlDelay = 0;
                        approach.DemandVolume = 0;
                        if (Arterial.Segments.IndexOf(segment) > 0)
                            approach.Results.ArrivalFlowProfile = CalcsFlowProfiles.ComputeArrivalFlowProfile(segment, Arterial.Segments.IndexOf(segment) - 1, approach, 0, segDirection, ODdata);

                        foreach (LaneGroupData laneGroup in approach.LaneGroups)
                        {
                            laneGroup.AnalysisResults = new ResultsIntersectionLaneGroupData();

                            if (segDirection == SegmentDirection.EBNB || Project.AnalMode == AnalysisMode.Planning)
                            {
                                if (laneGroup.DischargeVolume > 0)
                                    laneGroup.PortionOnGreen = Math.Min(Math.Min(laneGroup.PlatoonRatio, 2) * laneGroup.SignalPhase.GreenEffectiveSec / segment.Intersection.Signal.CycleLengthSec, 1); // includes green
                                else
                                    laneGroup.PortionOnGreen = 0;
                            }
                            
                            if (Project.AnalMode == AnalysisMode.Operations && (laneGroup.NemaMvmtID == outputMovementsArray[0] || laneGroup.NemaMvmtID == outputMovementsArray[1] || laneGroup.NemaMvmtID == outputMovementsArray[2]))
                            {
                                if (Arterial.Segments.IndexOf(segment) > 0)
                                {
                                    if (laneGroup.Type == LaneMovementsAllowed.LeftOnly)
                                    {
                                        laneGroup.PortionOnGreen = CalcsFlowProfiles.ComputePortionOnGreen(segment, approach, laneGroup, 0, segDirection);
                                    }
                                    else if (laneGroup.Type == LaneMovementsAllowed.RightOnly)
                                    {
                                        laneGroup.PortionOnGreen = CalcsFlowProfiles.ComputePortionOnGreen(segment, approach, laneGroup, 2, segDirection);
                                    }
                                    else if (laneGroup.Type == LaneMovementsAllowed.ThruRightShared)
                                    {
                                        laneGroup.PortionOnGreen = CalcsFlowProfiles.ComputePortionOnGreen(segment, approach, laneGroup, 1, segDirection);
                                        laneGroup.PortionOnGreenOfSharedLane = CalcsFlowProfiles.ComputePortionOnGreen(segment, approach, laneGroup, 2, segDirection);
                                    }
                                    else if (laneGroup.Type == LaneMovementsAllowed.ThruLeftShared)
                                    {
                                        laneGroup.PortionOnGreen = CalcsFlowProfiles.ComputePortionOnGreen(segment, approach, laneGroup, 1, segDirection);
                                        laneGroup.PortionOnGreenOfSharedLane = CalcsFlowProfiles.ComputePortionOnGreen(segment, approach, laneGroup, 0, segDirection);
                                    }
                                    else
                                    {
                                        laneGroup.PortionOnGreen = CalcsFlowProfiles.ComputePortionOnGreen(segment, approach, laneGroup, 1, segDirection);
                                    }
                                }
                            }

                            //TimerData TimerForLaneGroup = MovementData.GetTimerForMovement(segment, laneGroup.NemaMvmtID);
                            //laneGroup.Results.AdjThruVol = SignalCalcs.ThruVolume(Art.PHF, Segs[i].DDHV, Intersections[i + 1].PTXL);

                            if (Project.AnalMode == AnalysisMode.Operations)
                                laneGroup.AnalysisResults.SatFlowRate = SatFlowRateCalculations.HCM2016(Arterial.Area, laneGroup.NumLanes, 12, laneGroup.PctHeavyVehicles, laneGroup.Type, laneGroup.PctLeftTurns, laneGroup.PctRightTurns, approach.PctGrade, laneGroup.BaseSatFlow);
                            else if (Project.AnalMode == AnalysisMode.Planning)
                                laneGroup.AnalysisResults.SatFlowRate = SatFlowRateCalculations.Planning(Project.Mode, Arterial.Area, segment.Link.OutsideLaneWidth, 12, laneGroup.DemandVolumeVehPerHr, laneGroup.PctHeavyVehicles, laneGroup.NumLanes, segment.Link.MedType, segment.Link.PostSpeedMPH, approach.LeftTurnBayExists, approach.RightTurnBayExists, approach.PctLeftTurns, approach.PctRightTurns, segment.Intersection.Signal.CycleLengthSec, segment.Link.PctGrade, laneGroup.BaseSatFlow);

                            laneGroup.SignalPhase.gC = laneGroup.SignalPhase.GreenEffectiveSec / segment.Intersection.Signal.CycleLengthSec; //Equation 19-16

                            laneGroup.AnalysisResults.CapacityPerLane = SignalCalcs.Capacity(laneGroup.AnalysisResults.SatFlowRate.AdjustedValueVehHrLane, laneGroup.SignalPhase.gC, laneGroup.NumLanes);
                            laneGroup.AnalysisResults.vcRatio = SignalCalcs.vcRatio(laneGroup.AnalysisFlowRate, laneGroup.AnalysisResults.CapacityPerLane);

                            if (Arterial.Segments.IndexOf(segment) > 0 && (laneGroup.NemaMvmtID == NemaMovementNumbers.EBThru || laneGroup.NemaMvmtID == NemaMovementNumbers.WBThru))
                                laneGroup.AnalysisResults.OverCap = SignalCalcs.OverCapacityCheck(laneGroup.AnalysisResults.vcRatio, laneGroup.PeakHourFactor);

                            if (laneGroup.AnalysisResults.OverCap)
                                Arterial.OverCapacity = true;

                            if (Arterial.Segments.IndexOf(segment) == 0)
                                PrevVC = laneGroup.AnalysisResults.vcRatio;
                            else
                                PrevVC = Arterial.Segments[Arterial.Segments.IndexOf(segment) - 1].Intersection.Approaches[segment.Intersection.Approaches.IndexOf(approach)].LaneGroups[approach.LaneGroups.IndexOf(laneGroup)].AnalysisResults.vcRatio;

                            if (laneGroup.NemaMvmtID == NemaMovementNumbers.EBThru && Arterial.TestSerVol == 730)
                            { }
                            laneGroup.AnalysisResults.SignalControlParms = SignalCalcs.SigDelay(segment.Intersection.Signal.ControlType, segment.Intersection.Signal.CycleLengthSec, laneGroup.SignalPhase.gC, laneGroup.ArvType, laneGroup.AnalysisFlowRate, laneGroup.AnalysisResults.SatFlowRate.AdjustedValueVehHrLane, laneGroup.NumLanes, laneGroup.AnalysisResults.CapacityPerLane, laneGroup.AnalysisResults.vcRatio, PrevVC, laneGroup.PortionOnGreen, laneGroup.NemaMvmtID);
                            laneGroup.AnalysisResults.LOS = SignalCalcs.LOSintersection(laneGroup.AnalysisResults.SignalControlParms.AvgOverallDelay, segment.Thresholds.Delay);     //determine intersection LOS, as a function of signal delay

                            if (approach.Dir == Arterial.AnalysisTravelDir && (laneGroup.Type == LaneMovementsAllowed.ThruOnly || laneGroup.Type == LaneMovementsAllowed.ThruRightShared))
                                AnalysisLaneGroupControlDelay = laneGroup.AnalysisResults.SignalControlParms.AvgOverallDelay;

                            approach.Results.ControlDelay += (laneGroup.AnalysisFlowRate * laneGroup.AnalysisResults.SignalControlParms.AvgOverallDelay) / approach.DemandVolume;
                            approach.DemandVolume += laneGroup.AnalysisFlowRate;
                            //PrevVC = laneGroup.AnalysisResults.vcRatio;
                        }

                        segment.Intersection.DemandVolumeVehPerHr += approach.DemandVolume;
                        segment.Intersection.Results.ControlDelay += ((approach.Results.ControlDelay * approach.DemandVolume) / segment.Intersection.DemandVolumeVehPerHr);
                    }

                    //if (segment.Intersection.Signal.ControlType != SigControlType.Pretimed)
                    //{
                    //segment.Intersection.Signal.Phases = SignalCalcs.ComputeQAPolygon(segment.Intersection.Signal.Phases, segment.Intersection.Signal.CycleLengthSec);
                    //segment.Intersection.Signal.Phases = SignalCalcs.VolumeComputations(segment.Intersection.Signal.Phases);
                    //segment.Intersection.Signal.Phases = SignalCalcs.MaximumAllowableHeadway(segment.Intersection.Signal.Phases, segment.Link.PostSpeedMPH);
                    //segment.Intersection.Signal.Phases = SignalCalcs.ComputeEquivalentMaxGreen(segment.Intersection);
                    //segment.Intersection.Signal.Phases = SignalCalcs.ComputeAveragePhaseDuration(segment.Intersection.Signal.Phases, segment.Intersection.Signal.CycleLengthSec);
                    //}

                    foreach (AccessPointData accessPoint in segment.Link.AccessPoints)
                    {
                        if (segment.Id > 0) //SSW: added 4/1/21, review
                        {
                            accessPoint.ArrivalFlowRate = CalcsFlowProfiles.ComputeConflictFlowRate(segment, accessPoint, segment.Intersection.Approaches[(int)Arterial.AnalysisTravelDir], outputMovementsArray, segDirection);
                            accessPoint.BlockTime = CalcsFlowProfiles.ComputeBlockTime(segment, accessPoint);
                            accessPoint.PortionTimeBlocked = CalcsFlowProfiles.ComputePortionTimeBlocked(segment, accessPoint);
                        }
                    }

                    segment.Results.TravelTime = segment.Link.Results.RunTimeSec + AnalysisLaneGroupControlDelay;
                    segment.Results.AverageSpeed = SegmentCalcs.SegAvgSpeed(segment.LengthFt, segment.Results.TravelTime);

                    if (segment.Results.AverageSpeed < Arterial.Results.CritSpeed)
                        Arterial.Results.CritSpeed = segment.Results.AverageSpeed;    //save lowest segment speed to use for arterial speed if an intersection v/c is greater than 1/PHF
                    segment.Thresholds = new ThresholdData(Arterial.Area, segment.Link.PostSpeedMPH);
                    segment.Link.Results.LOS = SegmentCalcs.LOSsegmentAuto(segment.Results.AverageSpeed, segment.Thresholds.Speed);
                    //Art.Results.BaseFreeFlowTravelTime += segment.Link.Results.BaseFreeFlowTravelTime;
                    //Art.Results.FreeFlowTravelTime += segment.Link.Results.FreeFlowTravelTime;
                    if (Arterial.Segments.IndexOf(segment) > 0 && segDirection == SegmentDirection.EBNB)
                    {
                        Arterial.Results.TravelTime += segment.Results.TravelTime;
                        //Art.Results.TotalgC += Intersections[i + 1].gCthru;
                        TotalLaneFeet += segment.Link.NumLanes * segment.LengthFt;    // + Intersections[IntIndex].Width);
                    }
                }

                if (segDirection == SegmentDirection.WBSB)
                    ReverseArterial(ref Arterial);

                if (segDirection == SegmentDirection.EBNB)
                {
                    //Art.AvgSegLength = Art.LengthMiles * 5280 / Art.TotalSegs;
                    //Art.WtdgC = WeightedgC(Art.TotalSegs, ArtTotgC, CritgC);     //Calculate weighted g/C for arterial
                    //Art.FFSdelay = FFSDelay(ArtTravTime, ArtFFTravTime);            //Calculate Free-Flow speed delay
                    //Art.ThreshDelay = ThresholdDelay(Art.Area, ArtTravTime, ArtLengthMiles);    //Calculate Threshold delay
                    Arterial.Results.AverageSpeed = 3600 * Arterial.LengthMiles / Arterial.Results.TravelTime;
                    //Art.Results.BaseFreeFlowSpeed = 3600 * Art.LengthMiles / Art.Results.BaseFreeFlowTravelTime;
                    //Art.Results.SpeedRatio = Art.Results.AverageSpeed / Art.Results.BaseFreeFlowSpeed;
                    Arterial.Results.LOS = SegmentCalcs.LOSsegmentAuto(Arterial.Results.AverageSpeed, Arterial.Thresholds.Speed);
                    //Art.Results.AvgLanes = TotalLaneFeet / (Art.Results.LengthMiles * 5280);  //length weighted average number of lanes for the arterial; used in the bike & ped service vol calcs
                    if (Arterial.OverCapacity == true)
                    {
                        Arterial.Results.AverageSpeed = Arterial.Results.CritSpeed;
                        Arterial.Results.LOS = "F";
                    }
                }
            }
        }

        /// <summary>
        /// Reverses an arterial's direction by reversing the segments and access points lists, then manually changing their speeds, length, and access points locations.
        /// </summary>
        /// <param name="Arterial"></param>
        public static void ReverseArterial(ref ArterialData Arterial)
        {
            Arterial.Segments.Reverse();
            for (int segmentIndex = Arterial.Segments.Count - 1; segmentIndex >= 0; segmentIndex--)
            {
                if (segmentIndex == 0)
                {
                    Arterial.Segments[segmentIndex].Link.LengthFt = 0;
                    Arterial.Segments[segmentIndex].LengthFt = 0;
                }
                else
                {
                    Arterial.Segments[segmentIndex].Link.LengthFt = Arterial.Segments[segmentIndex - 1].Link.LengthFt;
                    Arterial.Segments[segmentIndex].LengthFt = Arterial.Segments[segmentIndex - 1].LengthFt;
                    Arterial.Segments[segmentIndex].Link.PostSpeedMPH = Arterial.Segments[segmentIndex - 1].Link.PostSpeedMPH;
                }

                Arterial.Segments[segmentIndex].Link.AccessPoints.Reverse();
                foreach (AccessPointData AccessPoint in Arterial.Segments[segmentIndex].Link.AccessPoints)
                    AccessPoint.Location = 1 - AccessPoint.Location;
            }
        }

        public static void IntersectionCalcs(IntersectionData intersection, AreaType area)
        {
            foreach (ApproachData approach in intersection.Approaches)
            {
                foreach (LaneGroupData laneGroup in approach.LaneGroups)
                {
                    if (laneGroup.Type == LaneMovementsAllowed.LeftOnly || laneGroup.Type == LaneMovementsAllowed.ThruAndLeftTurnBay)
                    {
                        approach.LeftTurnBayExists = true;
                    }
                    else if (laneGroup.Type == LaneMovementsAllowed.RightOnly || laneGroup.Type == LaneMovementsAllowed.ThruAndRightTurnBay)
                    {
                        approach.RightTurnBayExists = true;
                    }
                }

                approach.Results = new ResultsIntersectionApproachData(intersection.Signal.CycleLengthSec);
                approach.Results.ControlDelay = 0;
                approach.DemandVolume = 0;

                foreach (LaneGroupData laneGroup in approach.LaneGroups)
                {
                    laneGroup.AnalysisResults = new ResultsIntersectionLaneGroupData();

                    if (laneGroup.DischargeVolume > 0)
                        laneGroup.PortionOnGreen = Math.Min(Math.Min(laneGroup.PlatoonRatio, 2) * laneGroup.SignalPhase.GreenEffectiveSec / intersection.Signal.CycleLengthSec, 1); // includes green
                    else
                        laneGroup.PortionOnGreen = 0;

                    laneGroup.AnalysisResults.SatFlowRate = SatFlowRateCalculations.HCM2016(area, laneGroup.NumLanes, 12, laneGroup.PctHeavyVehicles, laneGroup.Type, laneGroup.PctLeftTurns, laneGroup.PctRightTurns, approach.PctGrade, laneGroup.BaseSatFlow);

                    laneGroup.SignalPhase.gC = laneGroup.SignalPhase.GreenEffectiveSec / intersection.Signal.CycleLengthSec; //Equation 19-16

                    laneGroup.AnalysisResults.CapacityPerLane = SignalCalcs.Capacity(laneGroup.AnalysisResults.SatFlowRate.AdjustedValueVehHrLane, laneGroup.SignalPhase.gC, laneGroup.NumLanes);
                    laneGroup.AnalysisResults.vcRatio = SignalCalcs.vcRatio(laneGroup.AnalysisFlowRate, laneGroup.AnalysisResults.CapacityPerLane);

                    laneGroup.AnalysisResults.OverCap = SignalCalcs.OverCapacityCheck(laneGroup.AnalysisResults.vcRatio, laneGroup.PeakHourFactor);

                    //if (laneGroup.AnalysisResults.OverCap)
                    //    Arterial.OverCapacity = true;                    

                    ThresholdData LOSthresholds = new ThresholdData(area);

                    float PrevVC = 0;
                    laneGroup.AnalysisResults.SignalControlParms = SignalCalcs.SigDelay(intersection.Signal.ControlType, intersection.Signal.CycleLengthSec, laneGroup.SignalPhase.gC, laneGroup.ArvType, laneGroup.AnalysisFlowRate, laneGroup.AnalysisResults.SatFlowRate.AdjustedValueVehHrLane, laneGroup.NumLanes, laneGroup.AnalysisResults.CapacityPerLane, laneGroup.AnalysisResults.vcRatio, PrevVC, laneGroup.PortionOnGreen, laneGroup.NemaMvmtID);
                    laneGroup.AnalysisResults.LOS = SignalCalcs.LOSintersection(laneGroup.AnalysisResults.SignalControlParms.AvgOverallDelay, LOSthresholds.Delay);     //determine intersection LOS, as a function of signal delay                    

                    approach.Results.ControlDelay += (laneGroup.AnalysisFlowRate * laneGroup.AnalysisResults.SignalControlParms.AvgOverallDelay) / approach.DemandVolume;
                    approach.DemandVolume += laneGroup.AnalysisFlowRate;

                }

                intersection.DemandVolumeVehPerHr += approach.DemandVolume;
                intersection.Results.ControlDelay += ((approach.Results.ControlDelay * approach.DemandVolume) / intersection.DemandVolumeVehPerHr);
            }
        }


    }
}
