using System;
using System.Collections.Generic;

/* Analysis Facility Details:

*/

namespace HCMCalc_UrbanStreets
{


    public class CreateArterial_FDOTSerVols
    {
        public static SerVolTablesByMultiLane CreateServiceVolTableFDOT(AreaType SerVolAreaType, ArterialClass ArtClass)
        {
            ServiceVolumeTableFDOT SerVolTableSingleLane = new ServiceVolumeTableFDOT(SerVolAreaType, ArtClass, false);
            ServiceVolumeTableFDOT SerVolTableMultiLane = new ServiceVolumeTableFDOT(SerVolAreaType, ArtClass, true);
            // Sample Structure:
            // public RoadwayData(bool multiLane, byte postedSpeedMPH, byte freeFlowSpeedMPH, MedianType median, TerrainType terrain, bool exclusiveLeftTurns, bool exclusiveRightTurns, byte facilityLengthMiles, byte numIntersections, byte accessPointsPerMile)

            switch (SerVolAreaType)
            {
                case AreaType.LargeUrbanized: // Urbanized: Tables 1, 4, 7
                    switch (ArtClass)
                    {
                        case ArterialClass.Class_I:
                            SerVolTableSingleLane.Roadway = new RoadwayData(45, 50, MedianType.None, TerrainType.Level, true, false, 2, 5, 2);
                            SerVolTableSingleLane.Traffic = new TrafficData(0.09f, 0.55f, 1.0f, 1950, 1.0f, 12, 12);
                            SerVolTableSingleLane.Signal = new SignalData(4, 3, SigControlType.CoordinatedActuated, 120, 0.44f);

                            SerVolTableMultiLane.Roadway = new RoadwayData(50, 55, MedianType.Restrictive, TerrainType.Level, true, false, 2, 5, 2);
                            SerVolTableMultiLane.Traffic = new TrafficData(0.09f, 0.56f, 1.0f, 1950, 1.0f, 12, 12);
                            SerVolTableMultiLane.Signal = new SignalData(4, 3, SigControlType.CoordinatedActuated, 150, 0.45f);
                            break;
                        case ArterialClass.Class_II:
                            SerVolTableSingleLane.Roadway = new RoadwayData(30, 35, MedianType.None, TerrainType.Level, true, false, 1.9f, 11, 2);
                            SerVolTableSingleLane.Traffic = new TrafficData(0.09f, 0.565f, 1.0f, 1950, 1.0f, 12, 12);
                            SerVolTableSingleLane.Signal = new SignalData(10, 4, SigControlType.CoordinatedActuated, 120, 0.44f);

                            SerVolTableMultiLane.Roadway = new RoadwayData(30, 35, MedianType.Restrictive, TerrainType.Level, true, false, 1.8f, 11, 2);
                            SerVolTableMultiLane.Traffic = new TrafficData(0.09f, 0.56f, 1.0f, 1950, 1.0f, 12, 12);
                            SerVolTableMultiLane.Signal = new SignalData(10, 4, SigControlType.CoordinatedActuated, 120, 0.44f);
                            break;
                    }
                    break;
                case AreaType.Transitioning: // Transitioning / Urban: Tables 2, 5, 8
                    switch (ArtClass)
                    {
                        case ArterialClass.Class_I:
                            SerVolTableSingleLane.Roadway = new RoadwayData(45, 50, MedianType.None, TerrainType.Level, true, false, 1.8f, 6, 2);
                            SerVolTableSingleLane.Traffic = new TrafficData(0.09f, 0.55f, 1.0f, 1950, 2.0f, 12, 12);
                            SerVolTableSingleLane.Signal = new SignalData(5, 4, SigControlType.CoordinatedActuated, 120, 0.44f);

                            SerVolTableMultiLane.Roadway = new RoadwayData(50, 55, MedianType.Restrictive, TerrainType.Level, true, false, 2, 5, 2);
                            SerVolTableMultiLane.Traffic = new TrafficData(0.09f, 0.57f, 1.0f, 1950, 3.0f, 12, 12);
                            SerVolTableMultiLane.Signal = new SignalData(4, 3, SigControlType.CoordinatedActuated, 150, 0.45f);
                            break;
                        case ArterialClass.Class_II:
                            SerVolTableSingleLane.Roadway = new RoadwayData(30, 35, MedianType.None, TerrainType.Level, true, false, 2, 11, 2);
                            SerVolTableSingleLane.Traffic = new TrafficData(0.09f, 0.57f, 1.0f, 1950, 2.0f, 12, 12);
                            SerVolTableSingleLane.Signal = new SignalData(10, 4, SigControlType.CoordinatedActuated, 120, 0.44f);

                            SerVolTableMultiLane.Roadway = new RoadwayData(30, 35, MedianType.Restrictive, TerrainType.Level, true, false, 2, 11, 2);
                            SerVolTableMultiLane.Traffic = new TrafficData(0.09f, 0.565f, 1.0f, 1950, 3.0f, 12, 12);
                            SerVolTableMultiLane.Signal = new SignalData(10, 4, SigControlType.CoordinatedActuated, 150, 0.45f);
                            break;
                    }
                    break;
                case AreaType.RuralDeveloped: // Rural Developed / Undeveloped: Tables 3, 6, 9. No arterial class differentiation
                    SerVolTableSingleLane.Roadway = new RoadwayData(45, 50, MedianType.None, TerrainType.Level, true, false, 1.9f, 6, 2);
                    SerVolTableSingleLane.Traffic = new TrafficData(0.095f, 0.55f, 1.0f, 1950, 3.0f, 12, 12);
                    SerVolTableSingleLane.Signal = new SignalData(5, 3, SigControlType.CoordinatedActuated, 90, 0.44f);

                    SerVolTableMultiLane.Roadway = new RoadwayData(45, 50, MedianType.Restrictive, TerrainType.Level, true, false, 2.2f, 7, 2);
                    SerVolTableMultiLane.Traffic = new TrafficData(0.095f, 0.55f, 1.0f, 1950, 3.0f, 12, 12);
                    SerVolTableMultiLane.Signal = new SignalData(6, 3, SigControlType.CoordinatedActuated, 90, 0.44f);
                    break;
            }
            return new SerVolTablesByMultiLane(ArtClass, SerVolTableSingleLane, SerVolTableMultiLane);
        }

        public static ArterialData NewArterial(ServiceVolumeTableFDOT arterialInputs, AnalysisMode ProjectAnalMode, float analysisDirectionDemandVol = 800, int numLanes = 2)
        {
            arterialInputs.Signal.EffGreen = arterialInputs.Signal.CalcEffectiveGreen(arterialInputs.Signal.EffGreenToCycleLengthRatio, arterialInputs.Signal.CycleLengthSec);
            arterialInputs.Signal.EffGreenLeft = arterialInputs.Signal.CalcEffectiveGreen(0.1f, arterialInputs.Signal.CycleLengthSec);
            List<SegmentData> Segments = new List<SegmentData>();
            IntersectionData newIntersection = CreateIntersection(numLanes, arterialInputs.Signal.CycleLengthSec, arterialInputs.SerVolAreaType, ProjectAnalMode);
            newIntersection.Signal.ControlType = arterialInputs.Signal.SigType;
            LinkData newLink;
            float segmentLength;

            for (int intIndex = 0; intIndex < arterialInputs.Roadway.NumIntersections; intIndex++)
            {
                if (intIndex == 0)
                    segmentLength = 0;
                else
                    segmentLength = arterialInputs.Roadway.ArterialLenghtFt / (arterialInputs.Roadway.NumIntersections - 1);

                newLink = new LinkData(segmentLength-newIntersection.CrossStreetWidth, numLanes, arterialInputs.Roadway.PostedSpeedMPH, arterialInputs.Roadway.PropCurbRightSide, arterialInputs.Roadway.Median);
                Segments.Add(new SegmentData(intIndex, newLink, newIntersection, arterialInputs.SerVolAreaType, arterialInputs.Roadway.PostedSpeedMPH, newIntersection.CrossStreetWidth));
            }
            
            ArterialData newArterial = new ArterialData(arterialInputs, Segments);
            ChangeArterialVolume(ref newArterial, analysisDirectionDemandVol);

            // Calcs Arterial code
            foreach (SegmentData segment in newArterial.Segments)
            {
                foreach (ApproachData approach in segment.Intersection.Approaches)
                {
                    approach.PctLeftTurns = arterialInputs.Traffic.PctLeftTurns;
                    approach.PctRightTurns = arterialInputs.Traffic.PctRightTurns;
                    foreach (LaneGroupData laneGroup in approach.LaneGroups)
                    {
                        if (laneGroup.Type == LaneMovementsAllowed.LeftOnly)
                        {
                            laneGroup.SignalPhase.GreenEffectiveSec = arterialInputs.Signal.EffGreenLeft;
                            laneGroup.TurnBayLeftLengthFeet = arterialInputs.Roadway.TurnBayLeftLengthFeet;
                        }
                        else
                            laneGroup.SignalPhase.GreenEffectiveSec = arterialInputs.Signal.EffGreen;
                        laneGroup.BaseSatFlow = arterialInputs.Traffic.BaseSatFlow;
                        laneGroup.PctHeavyVehicles = arterialInputs.Traffic.PctHeavyVeh;
                        laneGroup.ArvType = arterialInputs.Signal.ArvType;
                        float[] PlatoonRatioValues = new float[] { 0.333f, 0.667f, 1.0f, 1.333f, 1.667f, 2.0f };
                        laneGroup.PlatoonRatio = PlatoonRatioValues[laneGroup.ArvType - 1];
                        laneGroup.PeakHourFactor = arterialInputs.Traffic.PHF;
                    }
                }
            }
            newArterial.Thresholds.Delay = newArterial.Thresholds.GetLOSBoundaries_Delay(newArterial.Area);
            newArterial.Thresholds.Speed = newArterial.Thresholds.GetLOSBoundaries_Speed(arterialInputs.Roadway.PostedSpeedMPH);
            newArterial.AnalysisTravelDir = arterialInputs.Roadway.AnalysisTravelDir;

            return newArterial;
        }

        public static void ChangeArterialVolume(ref ArterialData Arterial, float analysisDirectionDemandVol = 800)
        {
            float minorStreetVolumeRatio = 0.75f;
            float leftTurningPctInt = 0.12f;
            float rightTurningPctInt = 0.12f;

            float[] demandVolumesLeft = { analysisDirectionDemandVol * leftTurningPctInt, analysisDirectionDemandVol * leftTurningPctInt, minorStreetVolumeRatio * analysisDirectionDemandVol * leftTurningPctInt, minorStreetVolumeRatio * analysisDirectionDemandVol * leftTurningPctInt };
            float[] demandVolumesThru = { analysisDirectionDemandVol * (1 - (leftTurningPctInt + rightTurningPctInt)), analysisDirectionDemandVol * (1 - (leftTurningPctInt + rightTurningPctInt)), minorStreetVolumeRatio * analysisDirectionDemandVol * (1 - (leftTurningPctInt + rightTurningPctInt)), minorStreetVolumeRatio * analysisDirectionDemandVol * (1 - (leftTurningPctInt + rightTurningPctInt)) };
            float[] demandVolumesRight = { analysisDirectionDemandVol * rightTurningPctInt, analysisDirectionDemandVol * rightTurningPctInt, minorStreetVolumeRatio * analysisDirectionDemandVol * rightTurningPctInt, minorStreetVolumeRatio * analysisDirectionDemandVol * rightTurningPctInt };

            foreach (SegmentData Segment in Arterial.Segments)
            {
                foreach (ApproachData Approach in Segment.Intersection.Approaches)
                {
                    int travelDirectionIndex = (int)Approach.Dir;
                    float totalDemand = demandVolumesLeft[travelDirectionIndex] + demandVolumesThru[travelDirectionIndex] + demandVolumesRight[travelDirectionIndex];
                    Approach.DemandVolume = totalDemand;
                    Approach.PctLeftTurns = (demandVolumesLeft[travelDirectionIndex] / totalDemand) * 100;
                    Approach.PctRightTurns = (demandVolumesRight[travelDirectionIndex] / totalDemand) * 100;

                    foreach (LaneGroupData LaneGroup in Approach.LaneGroups)
                    {
                        
                        if (LaneGroup.Type == LaneMovementsAllowed.LeftOnly)
                        {
                            LaneGroup.DischargeVolume = demandVolumesLeft[travelDirectionIndex];
                            LaneGroup.DemandVolumeVehPerHr = demandVolumesLeft[travelDirectionIndex];
                            LaneGroup.DemandVolumeVehPerHrSplit[0] = LaneGroup.DemandVolumeVehPerHr;
                        }
                        if (LaneGroup.Type == LaneMovementsAllowed.ThruRightShared)
                        {
                            LaneGroup.DischargeVolume = demandVolumesThru[travelDirectionIndex] + demandVolumesRight[travelDirectionIndex];
                            LaneGroup.DemandVolumeVehPerHr = demandVolumesThru[travelDirectionIndex] + demandVolumesRight[travelDirectionIndex];
                            LaneGroup.PctRightTurns = demandVolumesRight[travelDirectionIndex] / (demandVolumesThru[travelDirectionIndex] + demandVolumesRight[travelDirectionIndex]) * 100;
                            LaneGroup.DemandVolumeVehPerHrSplit[1] = demandVolumesThru[travelDirectionIndex];
                            LaneGroup.DemandVolumeVehPerHrSplit[2] = demandVolumesRight[travelDirectionIndex];
                        }
                        LaneGroup.AnalysisFlowRate = LaneGroup.CalcAnalysisFlowRate(LaneGroup.DemandVolumeVehPerHr, LaneGroup.PeakHourFactor);
                    }
                }
            }
        }

        public static IntersectionData CreateIntersection(int numThruLanes, int cycleLengthSec, AreaType ArtAreaType, AnalysisMode ProjAnalMode)
        {
            List<TimerData> Timers = TimerData.GetStandardTimers();
            List<ApproachData> newApproaches = new List<ApproachData>();
            List<LaneGroupData> newLaneGroups = new List<LaneGroupData>();
            List<LaneData> newLanes;
            List<SignalPhaseData> Phases = new List<SignalPhaseData>();

            // Intersection Signal Data
            SignalPhaseData WBLSignalPhase = new SignalPhaseData(nemaPhaseId: 1, nemaMvmtId: NemaMovementNumbers.WBLeft, phaseType: PhasingType.Protected, greenTime: 20, yellowTime: 3, allRedTime: 1, startUpLostTime: 2, timer: Timers[0]);
            SignalPhaseData EBLSignalPhase = new SignalPhaseData(5, NemaMovementNumbers.EBLeft, PhasingType.Protected, 20, 3, 1, 2, Timers[1]);
            SignalPhaseData EBTSignalPhase = new SignalPhaseData(2, NemaMovementNumbers.EBThru, PhasingType.Protected, 45, 3, 1, 2, Timers[2]);
            SignalPhaseData WBTSignalPhase = new SignalPhaseData(6, NemaMovementNumbers.WBThru, PhasingType.Protected, 45, 3, 1, 2, Timers[3]);
            SignalPhaseData NBLSignalPhase = new SignalPhaseData(3, NemaMovementNumbers.NBLeft, PhasingType.Protected, 8, 3, 1, 2, Timers[4]);
            SignalPhaseData SBLSignalPhase = new SignalPhaseData(7, NemaMovementNumbers.SBLeft, PhasingType.Protected, 8, 3, 1, 2, Timers[5]);
            SignalPhaseData SBTSignalPhase = new SignalPhaseData(4, NemaMovementNumbers.SBThru, PhasingType.Protected, 35, 3, 1, 2, Timers[6]);
            SignalPhaseData NBTSignalPhase = new SignalPhaseData(8, NemaMovementNumbers.NBThru, PhasingType.Protected, 35, 3, 1, 2, Timers[7]);

            Phases.Add(WBLSignalPhase);
            Phases.Add(EBTSignalPhase);
            Phases.Add(NBLSignalPhase);
            Phases.Add(SBTSignalPhase);
            Phases.Add(EBLSignalPhase);
            Phases.Add(WBTSignalPhase);
            Phases.Add(SBLSignalPhase);
            Phases.Add(NBTSignalPhase);

            newLanes = CreateArterial_HCMExample1.CreateLanes(numThruLanes, 0);
            newLaneGroups.Add(new LaneGroupData(1, "NB Thru+Right", LaneMovementsAllowed.ThruRightShared, NemaMovementNumbers.NBThru, TravelDirection.Northbound, newLanes, NBTSignalPhase, arvType: 3));
            newLanes = CreateArterial_HCMExample1.CreateLanes(numThruLanes, numThruLanes);
            newLaneGroups.Add(new LaneGroupData(2, "NB Left", LaneMovementsAllowed.LeftOnly, NemaMovementNumbers.NBLeft, TravelDirection.Northbound, newLanes, NBLSignalPhase, arvType: 3));
            newApproaches.Add(new ApproachData(1, TravelDirection.Northbound, "NB", 0, newLaneGroups));

            newLaneGroups = new List<LaneGroupData>();
            newLanes = CreateArterial_HCMExample1.CreateLanes(numThruLanes, 0);
            newLaneGroups.Add(new LaneGroupData(1, "EB Thru+Right", LaneMovementsAllowed.ThruRightShared, NemaMovementNumbers.EBThru, TravelDirection.Eastbound, newLanes, EBTSignalPhase, arvType: 3));
            newLanes = CreateArterial_HCMExample1.CreateLanes(numThruLanes, numThruLanes);
            newLaneGroups.Add(new LaneGroupData(2, "EB Left", LaneMovementsAllowed.LeftOnly, NemaMovementNumbers.EBLeft, TravelDirection.Eastbound, newLanes, EBLSignalPhase, arvType: 3));
            newApproaches.Add(new ApproachData(2, TravelDirection.Eastbound, "EB", 0, newLaneGroups));
            
            newLaneGroups = new List<LaneGroupData>();
            newLanes = CreateArterial_HCMExample1.CreateLanes(numThruLanes, 0);
            newLaneGroups.Add(new LaneGroupData(1, "SB Thru+Right", LaneMovementsAllowed.ThruRightShared, NemaMovementNumbers.SBThru, TravelDirection.Southbound, newLanes, SBTSignalPhase, arvType: 3));
            newLanes = CreateArterial_HCMExample1.CreateLanes(numThruLanes, numThruLanes);
            newLaneGroups.Add(new LaneGroupData(2, "SB Left", LaneMovementsAllowed.LeftOnly, NemaMovementNumbers.SBLeft, TravelDirection.Southbound, newLanes, SBLSignalPhase, arvType: 3));
            newApproaches.Add(new ApproachData(3, TravelDirection.Southbound, "SB", 0, newLaneGroups));
            
            newLaneGroups = new List<LaneGroupData>();
            newLanes = CreateArterial_HCMExample1.CreateLanes(numThruLanes, 0);
            newLaneGroups.Add(new LaneGroupData(1, "WB Thru+Right", LaneMovementsAllowed.ThruRightShared, NemaMovementNumbers.WBThru, TravelDirection.Westbound, newLanes, WBTSignalPhase, arvType: 3));
            newLanes = CreateArterial_HCMExample1.CreateLanes(numThruLanes, numThruLanes);
            newLaneGroups.Add(new LaneGroupData(2, "WB Left", LaneMovementsAllowed.LeftOnly, NemaMovementNumbers.WBLeft, TravelDirection.Westbound,  newLanes, WBLSignalPhase, arvType: 3));
            newApproaches.Add(new ApproachData(4, TravelDirection.Westbound, "WB", 0, newLaneGroups));

            SignalCycleData newSignalData = new SignalCycleData(SigControlType.CoordinatedActuated, cycleLengthSec, Phases);

            IntersectionData newIntersection = new IntersectionData(1, ArtAreaType, ArterialClass.Class_I, newApproaches, newSignalData, ProjAnalMode);

            return newIntersection;
        }
    }
}