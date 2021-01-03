using System;
using System.Collections.Generic;
using System.Windows.Forms;

/* Analysis Facility Details:

Oriented: East - West travel direction (Segment 1 start from the west)
Active access point intersections: 2 per segment
Two lanes in each direction
1.5 ft curb/gutter section
200 ft left turn bays on each approach to each signalized intersection
RT vehicles share the outside lane with through vehicles at each intersection
6 ft sidewalk on both sides of the street

100s Cycle Length
Signal conditions are the same for every intersection, except offset

Total length:           1 mi
# of segments:          5
Lane width:             12 ft
Corner raidus:          6 ft
Pavement Cond. Rating:  3.5
Crosswalk width:        12 ft
Total Walkway Width:    6 ft
Buffer:                 0 ft

Segments 1:
Office/Strip Commercial mix
Length:         1320 ft
Speed limit:    35 mi/h
Signal Offset:  0s

Segment 2:
Office/Strip Commercial mix
Length:         1320 ft
Speed limit:    35 mi/h
Signal Offset:  50s

Segment 3:
Office/Strip Commercial mix
Length:         1320 ft
Speed limit:    35 mi/h
Signal Offset:  50s

Segment 4:
Shopping Area
Length:         660 ft
Speed limit:    30 mi/h
Signal Offset:  0s

Segment 5:
Shopping Area
Length:         660 ft
Speed limit:    30 mi/h
Signal Offset:  0s

Endpoint (Point 6)
Signal Offset:  0s

*/


/*Segment 1 Demand Volume:

NBL: 60
NBT: 540
EBL: 80
EBT: 720
SBL: 60
SBT: 540
WBL: 80
WBT: 720

NBL: 49
NBT: 48
EBL: 39
EBT: 741
SBL: 48
SBT: 49
WBL: 39
WBT: 741

NBL: 48
NBT: 49
EBL: 39
EBT: 741
SBL: 49
SBT: 48
WBL: 38
WBT: 722

NBL: 60
NBT: 540
EBL: 80
EBT: 720
SBL: 60
SBT: 540
WBL: 80
WBT: 720

*/

namespace HCMCalc_UrbanStreets
{


    public class CreateArterial_HCMExample1
    {

        public static ArterialData NewArterial(AnalysisMode ProjectAnalMode, float analysisDirectionDemandVol = 800, int numLanes = 2)
        {
            // Timers
            /*TimerData Timer1 = new TimerData(1, NemaMovementNumbers.WBLeft, 15.9f, 3, 35.27f, 51.16f, 3.13f, 30.73f, 12.646f, 0.311f, 0.995f, 0);
            TimerData Timer2 = new TimerData(5, NemaMovementNumbers.EBLeft, 16.1f, 3, 35.27f, 51.37f, 3.13f, 30.73f, 12.829f, 0.322f, 0.996f, 0);
            TimerData Timer3 = new TimerData(2, NemaMovementNumbers.EBThru, 52.84f, 4, 51.16f, 4f, 0, 0, 0, 0, 0, 0);
            TimerData Timer4 = new TimerData(6, NemaMovementNumbers.WBThru, 52.63f, 4, 51.37f, 4f, 0, 0, 0, 0, 0, 0);
            TimerData Timer5 = new TimerData(3, NemaMovementNumbers.NBLeft, 9.13f, 3, 4, 13.13f, 3.13f, 17, 6.442f, 0.099f, 0.938f, 0);
            TimerData Timer6 = new TimerData(7, NemaMovementNumbers.SBLeft, 9.13f, 3, 4, 13.13f, 3.13f, 17, 6.442f, 0.099f, 0.938f, 0);
            TimerData Timer7 = new TimerData(4, NemaMovementNumbers.SBThru, 22.13f, 4, 13.14f, 35.27f, 3.06f, 31.87f, 16.165f, 1.968f, 1, 0.016f);
            TimerData Timer8 = new TimerData(8, NemaMovementNumbers.NBThru, 22.13f, 4, 13.14f, 35.27f, 3.06f, 31.87f, 16.165f, 1.968f, 1, 0.016f);*/

            List<SegmentData> Segments = new List<SegmentData>();

            // Intersection 1 / First Ave. //// "Segment-less" Intersection
            TimerData TimerWBL = new TimerData(11.50f, 4.00f, 39.75f, 51.25f, 3.13f, 35.25f, 7.886f, 0.120f, 0.936f, 0.000f);
            TimerData TimerEBT = new TimerData(76.75f, 4.00f, 51.25f, 4.00f, 0.00f, 0.00f, 0.000f, 0.000f, 0.000f, 0.000f);
            TimerData TimerNBL = new TimerData(9.59f, 4.00f, 4.00f, 13.59f, 3.13f, 8.00f, 6.389f, 0.009f, 0.873f, 1.000f);
            TimerData TimerSBT = new TimerData(26.16f, 4.00f, 13.59f, 39.75f, 3.06f, 37.41f, 20.199f, 1.963f, 1.000f, 0.009f);
            TimerData TimerEBL = new TimerData(11.45f, 4.00f, 39.75f, 51.20f, 3.13f, 35.25f, 7.832f, 0.120f, 0.936f, 0.000f);
            TimerData TimerWBT = new TimerData(76.80f, 4.00f, 51.20f, 4.00f, 0.00f, 0.00f, 0.000f, 0.000f, 0.000f, 0.000f);
            TimerData TimerSBL = new TimerData(9.59f, 4.00f, 4.00f, 13.59f, 3.13f, 8.00f, 6.389f, 0.009f, 0.873f, 1.000f);
            TimerData TimerNBT = new TimerData(26.16f, 4.00f, 13.59f, 39.75f, 3.06f, 37.41f, 20.199f, 1.963f, 1.000f, 0.009f);
            List<TimerData> Timers = new List<TimerData> { TimerWBL, TimerEBL, TimerEBT, TimerWBT, TimerNBL, TimerSBL, TimerSBT, TimerNBT };

            IntersectionData newIntersection;
            LinkData newLink;
            int upstreamIntersectionWidth = 0;

            newIntersection = CreateIntersection(Timers, numLanes, ProjectAnalMode, 60);
            newLink = new LinkData(0, numLanes, 35, 0.94f, MedianType.None);
            Segments.Add(new SegmentData(0, newLink, newIntersection, upstreamIntersectionWidth));
            upstreamIntersectionWidth = newIntersection.CrossStreetWidth;
            Segments[0].Link.AccessPoints.Add(CreateAPIntersection(1f / 3f, Segments[0].Intersection.Signal.CycleLengthSec));
            Segments[0].Link.AccessPoints.Add(CreateAPIntersection(2f / 3f, Segments[0].Intersection.Signal.CycleLengthSec));

            // Intersection 2 / Second Ave.
            TimerWBL = new TimerData(11.50f, 4.00f, 39.75f, 51.25f, 3.13f, 35.25f, 7.886f, 0.120f, 0.936f, 0.000f);
            TimerEBT = new TimerData(76.75f, 4.00f, 51.25f, 4.00f, 0.00f, 0.00f, 0.000f, 0.000f, 0.000f, 0.000f);
            TimerNBL = new TimerData(9.59f, 4.00f, 4.00f, 13.59f, 3.13f, 8.00f, 6.389f, 0.009f, 0.873f, 1.000f);
            TimerSBT = new TimerData(26.16f, 4.00f, 13.59f, 39.75f, 3.06f, 37.41f, 20.199f, 1.963f, 1.000f, 0.009f);
            TimerEBL = new TimerData(11.49f, 4.00f, 39.75f, 51.24f, 3.13f, 35.25f, 7.877f, 0.120f, 0.936f, 0.000f);
            TimerWBT = new TimerData(76.76f, 4.00f, 51.24f, 4.00f, 0.00f, 0.00f, 0.000f, 0.000f, 0.000f, 0.000f);
            TimerSBL = new TimerData(9.59f, 4.00f, 4.00f, 13.59f, 3.13f, 8.00f, 6.389f, 0.009f, 0.873f, 1.000f);
            TimerNBT = new TimerData(26.16f, 4.00f, 13.59f, 39.75f, 3.06f, 37.41f, 20.199f, 1.963f, 1.000f, 0.009f);
            Timers = new List<TimerData> { TimerWBL, TimerEBL, TimerEBT, TimerWBT, TimerNBL, TimerSBL, TimerSBT, TimerNBT };

            newIntersection = CreateIntersection(Timers, numLanes, ProjectAnalMode, 60);
            newLink = new LinkData(1320 - upstreamIntersectionWidth, numLanes, 35, 0.94f, MedianType.None);
            Segments.Add(new SegmentData(1, newLink, newIntersection, upstreamIntersectionWidth));
            upstreamIntersectionWidth = newIntersection.CrossStreetWidth;
            Segments[1].Link.AccessPoints.Add(CreateAPIntersection(1f / 3f, Segments[0].Intersection.Signal.CycleLengthSec));
            Segments[1].Link.AccessPoints.Add(CreateAPIntersection(2f / 3f, Segments[0].Intersection.Signal.CycleLengthSec));

            // Intersection 3 / Third Ave.
            TimerWBL = new TimerData(11.50f, 4.00f, 39.75f, 51.25f, 3.13f, 35.25f, 7.885f, 0.120f, 0.936f, 0.000f);
            TimerEBT = new TimerData(76.75f, 4.00f, 51.25f, 4.00f, 0.00f, 0.00f, 0.000f, 0.000f, 0.000f, 0.000f);
            TimerNBL = new TimerData(9.59f, 4.00f, 4.00f, 13.59f, 3.13f, 8.00f, 6.389f, 0.009f, 0.873f, 1.000f);
            TimerSBT = new TimerData(26.16f, 4.00f, 13.59f, 39.75f, 3.06f, 37.41f, 20.199f, 1.963f, 1.000f, 0.009f);
            TimerEBL = new TimerData(11.48f, 4.00f, 39.75f, 51.23f, 3.13f, 35.25f, 7.872f, 0.119f, 0.936f, 0.000f);
            TimerWBT = new TimerData(76.77f, 4.00f, 51.23f, 4.00f, 0.00f, 0.00f, 0.000f, 0.000f, 0.000f, 0.000f);
            TimerSBL = new TimerData(9.59f, 4.00f, 4.00f, 13.59f, 3.13f, 8.00f, 6.389f, 0.009f, 0.873f, 1.000f);
            TimerNBT = new TimerData(26.16f, 4.00f, 13.59f, 39.75f, 3.06f, 37.41f, 20.199f, 1.963f, 1.000f, 0.009f);
            Timers = new List<TimerData> { TimerWBL, TimerEBL, TimerEBT, TimerWBT, TimerNBL, TimerSBL, TimerSBT, TimerNBT };

            newIntersection = CreateIntersection(Timers, numLanes, ProjectAnalMode, 60);
            newLink = new LinkData(1320 - upstreamIntersectionWidth, numLanes, 35, 0.94f, MedianType.None);
            Segments.Add(new SegmentData(2, newLink, newIntersection, upstreamIntersectionWidth));
            upstreamIntersectionWidth = newIntersection.CrossStreetWidth;
            Segments[2].Link.AccessPoints.Add(CreateAPIntersection(1f / 3f, Segments[0].Intersection.Signal.CycleLengthSec));
            Segments[2].Link.AccessPoints.Add(CreateAPIntersection(2f / 3f, Segments[0].Intersection.Signal.CycleLengthSec));

            // Intersection 4 / Fourth Ave.
            TimerWBL = new TimerData(11.50f, 4.00f, 39.75f, 51.25f, 3.19f, 35.25f, 7.884f, 0.125f, 0.936f, 0.000f);
            TimerEBT = new TimerData(76.75f, 4.00f, 51.25f, 4.00f, 0.00f, 0.00f, 0.000f, 0.000f, 0.000f, 0.000f);
            TimerNBL = new TimerData(9.59f, 4.00f, 4.00f, 13.59f, 3.13f, 8.00f, 6.389f, 0.009f, 0.873f, 1.000f);
            TimerSBT = new TimerData(26.16f, 4.00f, 13.59f, 39.75f, 3.06f, 37.41f, 20.199f, 1.963f, 1.000f, 0.009f);
            TimerEBL = new TimerData(11.48f, 4.00f, 39.75f, 51.23f, 3.13f, 35.25f, 7.868f, 0.119f, 0.936f, 0.000f);
            TimerWBT = new TimerData(76.77f, 4.00f, 51.23f, 4.00f, 0.00f, 0.00f, 0.000f, 0.000f, 0.000f, 0.000f);
            TimerSBL = new TimerData(9.59f, 4.00f, 4.00f, 13.59f, 3.13f, 8.00f, 6.389f, 0.009f, 0.873f, 1.000f);
            TimerNBT = new TimerData(26.16f, 4.00f, 13.59f, 39.75f, 3.06f, 37.41f, 20.199f, 1.963f, 1.000f, 0.009f);
            Timers = new List<TimerData> { TimerWBL, TimerEBL, TimerEBT, TimerWBT, TimerNBL, TimerSBL, TimerSBT, TimerNBT };

            newIntersection = CreateIntersection(Timers, numLanes, ProjectAnalMode, 60);
            newLink = new LinkData(1320 - upstreamIntersectionWidth, numLanes, 35, 0.94f, MedianType.None);
            Segments.Add(new SegmentData(3, newLink, newIntersection, upstreamIntersectionWidth));
            upstreamIntersectionWidth = newIntersection.CrossStreetWidth;
            Segments[3].Link.AccessPoints.Add(CreateAPIntersection(1f / 3f, Segments[0].Intersection.Signal.CycleLengthSec));
            Segments[3].Link.AccessPoints.Add(CreateAPIntersection(2f / 3f, Segments[0].Intersection.Signal.CycleLengthSec));

            // Intersection 5 / Fifth Ave.
            TimerWBL = new TimerData(11.50f, 4.00f, 39.75f, 51.25f, 3.19f, 35.25f, 7.883f, 0.125f, 0.936f, 0.000f);
            TimerEBT = new TimerData(76.75f, 4.00f, 51.25f, 4.00f, 0.00f, 0.00f, 0.000f, 0.000f, 0.000f, 0.000f);
            TimerNBL = new TimerData(9.59f, 4.00f, 4.00f, 13.59f, 3.13f, 8.00f, 6.389f, 0.009f, 0.873f, 1.000f);
            TimerSBT = new TimerData(26.16f, 4.00f, 13.59f, 39.75f, 3.06f, 37.41f, 20.199f, 1.963f, 1.000f, 0.009f);
            TimerEBL = new TimerData(11.48f, 4.00f, 39.75f, 51.23f, 3.19f, 35.25f, 7.865f, 0.125f, 0.936f, 0.000f);
            TimerWBT = new TimerData(76.77f, 4.00f, 51.23f, 4.00f, 0.00f, 0.00f, 0.000f, 0.000f, 0.000f, 0.000f);
            TimerSBL = new TimerData(9.59f, 4.00f, 4.00f, 13.59f, 3.13f, 8.00f, 6.389f, 0.009f, 0.873f, 1.000f);
            TimerNBT = new TimerData(26.16f, 4.00f, 13.59f, 39.75f, 3.06f, 37.41f, 20.199f, 1.963f, 1.000f, 0.009f);
            Timers = new List<TimerData> { TimerWBL, TimerEBL, TimerEBT, TimerWBT, TimerNBL, TimerSBL, TimerSBT, TimerNBT };

            newIntersection = CreateIntersection(Timers, numLanes, ProjectAnalMode, 60);
            newLink = new LinkData(660 - upstreamIntersectionWidth, numLanes, 30, 0.88f, MedianType.None);
            Segments.Add(new SegmentData(4, newLink, newIntersection, upstreamIntersectionWidth));
            upstreamIntersectionWidth = newIntersection.CrossStreetWidth;
            Segments[4].Link.AccessPoints.Add(CreateAPIntersection(1f / 3f, Segments[0].Intersection.Signal.CycleLengthSec));
            Segments[4].Link.AccessPoints.Add(CreateAPIntersection(2f / 3f, Segments[0].Intersection.Signal.CycleLengthSec));

            // Intersection 6 / Sixth Ave.
            TimerWBL = new TimerData(11.45f, 4.00f, 39.75f, 51.20f, 3.19f, 35.25f, 7.832f, 0.125f, 0.936f, 0.000f);
            TimerEBT = new TimerData(76.80f, 4.00f, 51.20f, 4.00f, 0.00f, 0.00f, 0.000f, 0.000f, 0.000f, 0.000f);
            TimerNBL = new TimerData(9.59f, 4.00f, 4.00f, 13.59f, 3.13f, 8.00f, 6.389f, 0.009f, 0.873f, 1.000f);
            TimerSBT = new TimerData(26.16f, 4.00f, 13.59f, 39.75f, 3.06f, 37.41f, 20.199f, 1.963f, 1.000f, 0.009f);
            TimerEBL = new TimerData(11.48f, 4.00f, 39.75f, 51.23f, 3.19f, 35.25f, 7.864f, 0.125f, 0.936f, 0.000f);
            TimerWBT = new TimerData(76.77f, 4.00f, 51.23f, 4.00f, 0.00f, 0.00f, 0.000f, 0.000f, 0.000f, 0.000f);
            TimerSBL = new TimerData(9.59f, 4.00f, 4.00f, 13.59f, 3.13f, 8.00f, 6.389f, 0.009f, 0.873f, 1.000f);
            TimerNBT = new TimerData(26.16f, 4.00f, 13.59f, 39.75f, 3.06f, 37.41f, 20.199f, 1.963f, 1.000f, 0.009f);
            Timers = new List<TimerData> { TimerWBL, TimerEBL, TimerEBT, TimerWBT, TimerNBL, TimerSBL, TimerSBT, TimerNBT };

            newIntersection = CreateIntersection(Timers, numLanes, ProjectAnalMode, 60);
            newLink = new LinkData(660 - upstreamIntersectionWidth, numLanes, 30, 0.88f, MedianType.None);
            Segments.Add(new SegmentData(5, newLink, newIntersection, AreaType.LargeUrbanized, newLink.PostSpeedMPH + 5, upstreamIntersectionWidth));
            Segments[5].Link.AccessPoints.Add(CreateAPIntersection(1f / 3f, Segments[0].Intersection.Signal.CycleLengthSec));
            Segments[5].Link.AccessPoints.Add(CreateAPIntersection(2f / 3f, Segments[0].Intersection.Signal.CycleLengthSec));
            
            ArterialData newArterial = new ArterialData(AreaType.LargeUrbanized, ArterialClass.Class_I, TravelDirection.Eastbound, Segments);
            ChangeArterialVolume(ref newArterial, analysisDirectionDemandVol);

            // Calcs Arterial code
            foreach (SegmentData segment in newArterial.Segments)
            {
                newArterial.LengthMiles += segment.Link.LengthFt / 5280;
                foreach (ApproachData approach in segment.Intersection.Approaches)
                {
                    foreach (LaneGroupData laneGroup in approach.LaneGroups)
                    {
                        laneGroup.SignalPhase.GreenEffectiveSec = Math.Max(laneGroup.SignalPhase.Timer.Duration - laneGroup.SignalPhase.Timer.IntergreenTimeSec - laneGroup.SignalPhase.StartUpLostTimeSec + laneGroup.SignalPhase.EndUseSec, 0);
                        laneGroup.PctHeavyVehicles = 0; // 3%
                    }
                }
            }
            //newArterial.ThreshDelay = newArterial.GetLOSBoundaries_Delay(newArterial.Area);
            //newArterial.ThreshSpeed = newArterial.GetLOSBoundaries_Speed(artClass: 2);
            newArterial.AnalysisTravelDir = TravelDirection.Eastbound;

            return newArterial;
        }

        public static void ChangeArterialVolume(ref ArterialData Arterial, float analysisDirectionDemandVol = 800)
        {
            float minorStreetVolumeRatio = 0.75f;
            float midSegVolumeRatio = 0.5f;
            float midSegAnalysisDirVolumeRatio = 0.1276f;
            float leftTurningPctInt = 0.1f; // 0.125f
            float rightTurningPctInt = 0.1f; // 0.125f
            float leftTurningPctAP = 1f / 20f; // 0.125f
            float rightTurningPctAP = 1f / 20f; // 0.125f

            float[] demandVolumesLeft = { analysisDirectionDemandVol * leftTurningPctInt, analysisDirectionDemandVol * leftTurningPctInt, minorStreetVolumeRatio * analysisDirectionDemandVol * leftTurningPctInt, minorStreetVolumeRatio * analysisDirectionDemandVol * leftTurningPctInt };
            float[] demandVolumesThru = { analysisDirectionDemandVol * (1 - (leftTurningPctInt + rightTurningPctInt)), analysisDirectionDemandVol * (1 - (leftTurningPctInt + rightTurningPctInt)), minorStreetVolumeRatio * analysisDirectionDemandVol * (1 - (leftTurningPctInt + rightTurningPctInt)), minorStreetVolumeRatio * analysisDirectionDemandVol * (1 - (leftTurningPctInt + rightTurningPctInt)) };
            float[] demandVolumesRight = { analysisDirectionDemandVol * rightTurningPctInt, analysisDirectionDemandVol * rightTurningPctInt, minorStreetVolumeRatio * analysisDirectionDemandVol * rightTurningPctInt, minorStreetVolumeRatio * analysisDirectionDemandVol * rightTurningPctInt };
            float EBApproachAccessPoint = demandVolumesThru[0] + demandVolumesRight[2] + demandVolumesLeft[3];
            float WBApproachAccessPoint = demandVolumesThru[1] + demandVolumesRight[3] + demandVolumesLeft[2]; // The + 20 needs to be investigated
            float[] demandVolumesAP1EB = { leftTurningPctAP * EBApproachAccessPoint, (1 - (leftTurningPctAP + rightTurningPctAP)) * EBApproachAccessPoint, rightTurningPctAP * EBApproachAccessPoint }; // EBT + NBR + SBL EWNS
            float[] demandVolumesAP1WB = { leftTurningPctAP * WBApproachAccessPoint, (1 - (leftTurningPctAP + rightTurningPctAP)) * WBApproachAccessPoint, rightTurningPctAP * WBApproachAccessPoint }; // WBT + NBL + SBR
            float[] demandVolumesAP1NB = { midSegVolumeRatio * midSegAnalysisDirVolumeRatio * WBApproachAccessPoint, 0, (1 - midSegVolumeRatio) * midSegAnalysisDirVolumeRatio * EBApproachAccessPoint };
            float[] demandVolumesAP1SB = { (1 - midSegVolumeRatio) * midSegAnalysisDirVolumeRatio * EBApproachAccessPoint, 0, midSegVolumeRatio * midSegAnalysisDirVolumeRatio * WBApproachAccessPoint };

            EBApproachAccessPoint = demandVolumesAP1EB[1] + demandVolumesAP1NB[2] + demandVolumesAP1SB[0]; // The + 20 needs to be investigated
            WBApproachAccessPoint = demandVolumesThru[1] + demandVolumesRight[3] + demandVolumesLeft[2];
            float[] demandVolumesAP2EB = { leftTurningPctAP * EBApproachAccessPoint, (1 - (leftTurningPctAP + rightTurningPctAP)) * EBApproachAccessPoint, rightTurningPctAP * EBApproachAccessPoint }; // EBT + NBR + SBL EWNS
            float[] demandVolumesAP2WB = { leftTurningPctAP * WBApproachAccessPoint, (1 - (leftTurningPctAP + rightTurningPctAP)) * WBApproachAccessPoint, rightTurningPctAP * WBApproachAccessPoint }; // WBT + NBL + SBR
            float[] demandVolumesAP2NB = { midSegVolumeRatio * midSegAnalysisDirVolumeRatio * WBApproachAccessPoint, 0, (1 - midSegVolumeRatio) * midSegAnalysisDirVolumeRatio * EBApproachAccessPoint };
            float[] demandVolumesAP2SB = { (1 - midSegVolumeRatio) * midSegAnalysisDirVolumeRatio * EBApproachAccessPoint, 0, midSegVolumeRatio * midSegAnalysisDirVolumeRatio * WBApproachAccessPoint };


            float[] demandVolumesAP1 = { demandVolumesAP1EB[0], demandVolumesAP1EB[1],demandVolumesAP1EB[2],
                                         demandVolumesAP1WB[0], demandVolumesAP1WB[1], demandVolumesAP1WB[2],
                                         demandVolumesAP1NB[0], demandVolumesAP1NB[1], demandVolumesAP1NB[2],
                                         demandVolumesAP1SB[0], demandVolumesAP1SB[1], demandVolumesAP1SB[2], };

            float[] demandVolumesAP2 = { demandVolumesAP2EB[0], demandVolumesAP2EB[1],demandVolumesAP2EB[2],
                                         demandVolumesAP2WB[0], demandVolumesAP2WB[1], demandVolumesAP2WB[2],
                                         demandVolumesAP2NB[0], demandVolumesAP2NB[1], demandVolumesAP2NB[2],
                                         demandVolumesAP2SB[0], demandVolumesAP2SB[1], demandVolumesAP2SB[2], };

            foreach (SegmentData Segment in Arterial.Segments)
            {
                Segment.Link.AccessPoints[0].Volume = demandVolumesAP1;
                Segment.Link.AccessPoints[1].Volume = demandVolumesAP2;
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
                        }
                        if (LaneGroup.Type == LaneMovementsAllowed.ThruRightShared)
                        {
                            LaneGroup.DischargeVolume = demandVolumesThru[travelDirectionIndex] + demandVolumesRight[travelDirectionIndex];
                            LaneGroup.DemandVolumeVehPerHr = demandVolumesThru[travelDirectionIndex] + demandVolumesRight[travelDirectionIndex];
                            LaneGroup.PctRightTurns = demandVolumesRight[travelDirectionIndex] / (demandVolumesThru[travelDirectionIndex] + demandVolumesRight[travelDirectionIndex]) * 100;
                        }
                        LaneGroup.AnalysisFlowRate = LaneGroup.CalcAnalysisFlowRate(LaneGroup.DemandVolumeVehPerHr, LaneGroup.PeakHourFactor);
                    }
                }
            }
        }

        public static AccessPointData CreateAPIntersection(float inputLocation, int cycleLengthSec) // Creates an Access Point within a Link
        {
            AccessPointData newAP = new AccessPointData( // Placeholder Values
            rightTurnEquivalency: 0,
            decelRate: 0,
            arrivalFlowRate: new float[2,cycleLengthSec],
            blockTime: new int[3],
            portionTimeBlocked: new float[12],
            probInsideLaneBlocked: 0,
            thruDelay: 0,
            location:inputLocation);

            return newAP;
        }
        public static List<LaneData> CreateLanes(int numLanes, int MoveIdStartingIndex)
        {
            List<LaneData> newLanes = new List<LaneData>();
            for (int lanes = 1; lanes <= numLanes; lanes++)
            {
                if (MoveIdStartingIndex != 0)
                {
                    newLanes.Add(new LaneData(MoveIdStartingIndex + lanes, LaneMovementsAllowed.LeftOnly));
                    break;
                }
                else
                {
                    if (lanes == 1)
                        newLanes.Add(new LaneData(MoveIdStartingIndex + lanes, LaneMovementsAllowed.ThruRightShared));
                    else
                        newLanes.Add(new LaneData(MoveIdStartingIndex + lanes, LaneMovementsAllowed.ThruOnly));
                }
            }
            return newLanes;
        }
        public static IntersectionData CreateIntersection(List<TimerData> Timers, int numThruLanes, AnalysisMode ProjectAnalMode, int crossStreetWidth)
        {
            
            List<ApproachData> newApproaches = new List<ApproachData>();
            List<LaneGroupData> newLaneGroups = new List<LaneGroupData>();
            List<LaneData> newLanes;
            List<SignalPhaseData> Phases = new List<SignalPhaseData>();

            /*TimerData TimerNBL = new TimerData(3, NemaMovementNumbers.NBLeft, 9.13f, 3, 68.74f, 77.87f, 3.13f, 17, 6.442f, 0.099f, 0.938f, 0);
            TimerData TimerNBT = new TimerData(8, NemaMovementNumbers.NBThru, 22.13f, 4, 77.86f, 99.99f, 3.06f, 31.87f, 16.165f, 1.968f, 1, 0.016f);
            TimerData TimerEBL = new TimerData(5, NemaMovementNumbers.EBLeft, 16.1f, 3, 0, 16.1f, 3.13f, 30.73f, 12.829f, 0.322f, 0.996f, 0);
            TimerData TimerEBT = new TimerData(2, NemaMovementNumbers.EBThru, 52.84f, 4, 15.9f, 68.74f, 0, 0, 0, 0, 0, 0);
            TimerData TimerSBL = new TimerData(7, NemaMovementNumbers.SBLeft, 9.13f, 3, 68.73f, 77.86f, 3.13f, 17, 6.442f, 0.099f, 0.938f, 0);
            TimerData TimerSBT = new TimerData(4, NemaMovementNumbers.SBThru, 22.13f, 4, 77.87f, 100, 3.06f, 31.87f, 16.165f, 1.968f, 1, 0.016f);
            TimerData TimerWBL = new TimerData(1, NemaMovementNumbers.WBLeft, 15.9f, 3, 0, 15.9f, 3.13f, 30.73f, 12.646f, 0.311f, 0.995f, 0);
            TimerData TimerWBT = new TimerData(6, NemaMovementNumbers.WBThru, 52.63f, 4, 16.1f, 68.73f, 0, 0, 0, 0, 0, 0);*/

            // Intersection Signal Data
            //Example problem specifies permitted left turn for NB LT. Use protected until permitted delay calculations are implemented.
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

            LaneData[] numLeftLanes = new LaneData[1];

            newLanes = CreateLanes(numThruLanes, 0);
            newLaneGroups.Add(new LaneGroupData(1, "NB Thru+Right", LaneMovementsAllowed.ThruRightShared, NemaMovementNumbers.NBThru, TravelDirection.Northbound, newLanes, NBTSignalPhase, arvType: 3));
            newLanes = CreateLanes(numThruLanes, numThruLanes);
            newLaneGroups.Add(new LaneGroupData(2, "NB Left", LaneMovementsAllowed.LeftOnly, NemaMovementNumbers.NBLeft, TravelDirection.Northbound, newLanes, NBLSignalPhase, arvType: 3));
            newApproaches.Add(new ApproachData(1, TravelDirection.Northbound, "NB", 0, newLaneGroups));
            

            newLaneGroups = new List<LaneGroupData>();
            newLanes = CreateLanes(numThruLanes, 0);
            newLaneGroups.Add(new LaneGroupData(1, "EB Thru+Right", LaneMovementsAllowed.ThruRightShared, NemaMovementNumbers.EBThru, TravelDirection.Eastbound, newLanes, EBTSignalPhase, arvType: 4));
            newLanes = CreateLanes(numThruLanes, numThruLanes);
            newLaneGroups.Add(new LaneGroupData(2, "EB Left", LaneMovementsAllowed.LeftOnly, NemaMovementNumbers.EBLeft, TravelDirection.Eastbound, newLanes, EBLSignalPhase, arvType: 3));
            newApproaches.Add(new ApproachData(2, TravelDirection.Eastbound, "EB", 0, newLaneGroups));
            

            newLaneGroups = new List<LaneGroupData>();
            newLanes = CreateLanes(numThruLanes, 0);
            newLaneGroups.Add(new LaneGroupData(1, "SB Thru+Right", LaneMovementsAllowed.ThruRightShared, NemaMovementNumbers.SBThru, TravelDirection.Southbound, newLanes, SBTSignalPhase, arvType: 3));
            newLanes = CreateLanes(numThruLanes, numThruLanes);
            newLaneGroups.Add(new LaneGroupData(2, "SB Left", LaneMovementsAllowed.LeftOnly, NemaMovementNumbers.SBLeft, TravelDirection.Southbound, newLanes, SBLSignalPhase, arvType: 3));
            newApproaches.Add(new ApproachData(3, TravelDirection.Southbound, "SB", 0, newLaneGroups));
            

            newLaneGroups = new List<LaneGroupData>();
            newLanes = CreateLanes(numThruLanes, 0);
            newLaneGroups.Add(new LaneGroupData(1, "WB Thru+Right", LaneMovementsAllowed.ThruRightShared, NemaMovementNumbers.WBThru, TravelDirection.Westbound, newLanes, WBTSignalPhase, arvType: 4));
            newLanes = CreateLanes(numThruLanes, numThruLanes);
            newLaneGroups.Add(new LaneGroupData(2, "WB Left", LaneMovementsAllowed.LeftOnly, NemaMovementNumbers.WBLeft, TravelDirection.Westbound,  newLanes, WBLSignalPhase, arvType: 3));
            newApproaches.Add(new ApproachData(4, TravelDirection.Westbound, "WB", 0, newLaneGroups));

            SignalCycleData newSignalData = new SignalCycleData(SigControlType.Pretimed, 124, Phases);

            IntersectionData newIntersection = new IntersectionData(1, AreaType.LargeUrbanized, ArterialClass.Class_I, newApproaches, newSignalData, ProjectAnalMode, crossStreetWidth);

            return newIntersection;
        }

        /*public static IntersectionData CreateIntersection(float[] demandVolumesLeft, float[] demandVolumesThru, float[] demandVolumesRight, TimerData[] Timers)
        {
            List<ApproachData> newApproaches = new List<ApproachData>();
            List<LaneGroupData> newLaneGroups = new List<LaneGroupData>();
            List<LaneData> newLanes;

            /*TimerData TimerNBL = new TimerData(3, NemaMovementNumbers.NBLeft, 9.13f, 3, 68.74f, 77.87f, 3.13f, 17, 6.442f, 0.099f, 0.938f, 0);
            TimerData TimerNBT = new TimerData(8, NemaMovementNumbers.NBThru, 22.13f, 4, 77.86f, 99.99f, 3.06f, 31.87f, 16.165f, 1.968f, 1, 0.016f);
            TimerData TimerEBL = new TimerData(5, NemaMovementNumbers.EBLeft, 16.1f, 3, 0, 16.1f, 3.13f, 30.73f, 12.829f, 0.322f, 0.996f, 0);
            TimerData TimerEBT = new TimerData(2, NemaMovementNumbers.EBThru, 52.84f, 4, 15.9f, 68.74f, 0, 0, 0, 0, 0, 0);
            TimerData TimerSBL = new TimerData(7, NemaMovementNumbers.SBLeft, 9.13f, 3, 68.73f, 77.86f, 3.13f, 17, 6.442f, 0.099f, 0.938f, 0);
            TimerData TimerSBT = new TimerData(4, NemaMovementNumbers.SBThru, 22.13f, 4, 77.87f, 100, 3.06f, 31.87f, 16.165f, 1.968f, 1, 0.016f);
            TimerData TimerWBL = new TimerData(1, NemaMovementNumbers.WBLeft, 15.9f, 3, 0, 15.9f, 3.13f, 30.73f, 12.646f, 0.311f, 0.995f, 0);
            TimerData TimerWBT = new TimerData(6, NemaMovementNumbers.WBThru, 52.63f, 4, 16.1f, 68.73f, 0, 0, 0, 0, 0, 0);

            // Intersection Signal Data
            //Example problem specifies permitted left turn for NB LT. Use protected until permitted delay calculations are implemented.
            SignalPhaseData WBLSignalPhase = new SignalPhaseData(nemaPhaseId: 1, phaseType: PhasingType.Protected, greenTime: 20, yellowTime: 3, allRedTime: 1, startUpLostTime: 2, timer: Timers[0]);
            SignalPhaseData EBLSignalPhase = new SignalPhaseData(5, PhasingType.Protected, 20, 3, 1, 2, Timers[1]);
            SignalPhaseData EBTSignalPhase = new SignalPhaseData(2, PhasingType.Protected, 45, 3, 1, 2, Timers[2]);
            SignalPhaseData WBTSignalPhase = new SignalPhaseData(6, PhasingType.Protected, 45, 3, 1, 2, Timers[3]);
            SignalPhaseData NBLSignalPhase = new SignalPhaseData(3, PhasingType.Protected, 8, 3, 1, 2, Timers[4]);
            SignalPhaseData SBLSignalPhase = new SignalPhaseData(7, PhasingType.Protected, 8, 3, 1, 2, Timers[5]);
            SignalPhaseData SBTSignalPhase = new SignalPhaseData(4, PhasingType.Protected, 35, 3, 1, 2, Timers[6]);
            SignalPhaseData NBTSignalPhase = new SignalPhaseData(8, PhasingType.Protected, 35, 3, 1, 2, Timers[7]);


            float totalDemand = demandVolumesLeft[0] + demandVolumesThru[0] + demandVolumesRight[0];
            newLanes = CreateLanes(new LaneMovementsAllowed[] { LaneMovementsAllowed.ThruOnly, LaneMovementsAllowed.ThruRightShared }, new int[] { 1, 2 });
            newLaneGroups.Add(new LaneGroupData(1, "NB Thru+Right", LaneMovementsAllowed.ThruRightShared, NemaMovementNumbers.NBThru, TravelDirection.Northbound, demandVolumesThru[0] + demandVolumesRight[0], newLanes, NBTSignalPhase, pctRightTurns: demandVolumesRight[0] / (demandVolumesThru[0] + demandVolumesRight[0]) * 100, arvType: 3));
            newLanes = CreateLanes(new LaneMovementsAllowed[] { LaneMovementsAllowed.LeftOnly }, new int[] { 3 });
            newLaneGroups.Add(new LaneGroupData(2, "NB Left", LaneMovementsAllowed.LeftOnly, NemaMovementNumbers.NBLeft, TravelDirection.Northbound, demandVolumesLeft[0], newLanes, NBLSignalPhase, arvType: 3));
            newApproaches.Add(new ApproachData(1, TravelDirection.Northbound, "NB", totalDemand, demandVolumesLeft[0] / totalDemand, demandVolumesRight[0] / totalDemand, 0, newLaneGroups));


            newLaneGroups = new List<LaneGroupData>();
            totalDemand = demandVolumesLeft[1] + demandVolumesThru[1] + demandVolumesRight[1];
            newLanes = CreateLanes(new LaneMovementsAllowed[] { LaneMovementsAllowed.ThruOnly, LaneMovementsAllowed.ThruRightShared }, new int[] { 1, 2 });
            newLaneGroups.Add(new LaneGroupData(1, "EB Thru+Right", LaneMovementsAllowed.ThruRightShared, NemaMovementNumbers.EBThru, TravelDirection.Eastbound, demandVolumesThru[1] + demandVolumesRight[1], newLanes, EBTSignalPhase, pctRightTurns: demandVolumesRight[1] / (demandVolumesThru[1] + demandVolumesRight[1]) * 100, arvType: 4));
            newLanes = CreateLanes(new LaneMovementsAllowed[] { LaneMovementsAllowed.LeftOnly }, new int[] { 3 });
            newLaneGroups.Add(new LaneGroupData(2, "EB Left", LaneMovementsAllowed.LeftOnly, NemaMovementNumbers.EBLeft, TravelDirection.Eastbound, demandVolumesLeft[1], newLanes, EBLSignalPhase, arvType: 3));
            newApproaches.Add(new ApproachData(2, TravelDirection.Eastbound, "EB", totalDemand, demandVolumesLeft[1] / totalDemand, demandVolumesRight[1] / totalDemand, 0, newLaneGroups));


            newLaneGroups = new List<LaneGroupData>();
            totalDemand = demandVolumesLeft[2] + demandVolumesThru[2] + demandVolumesRight[2];
            newLanes = CreateLanes(new LaneMovementsAllowed[] { LaneMovementsAllowed.ThruOnly, LaneMovementsAllowed.ThruRightShared }, new int[] { 1, 2 });
            newLaneGroups.Add(new LaneGroupData(1, "SB Thru+Right", LaneMovementsAllowed.ThruRightShared, NemaMovementNumbers.SBThru, TravelDirection.Southbound, demandVolumesThru[2] + demandVolumesRight[2], newLanes, SBTSignalPhase, pctRightTurns: demandVolumesRight[2] / (demandVolumesThru[2] + demandVolumesRight[2]) * 100, arvType: 3));
            newLanes = CreateLanes(new LaneMovementsAllowed[] { LaneMovementsAllowed.LeftOnly }, new int[] { 3 });
            newLaneGroups.Add(new LaneGroupData(2, "SB Left", LaneMovementsAllowed.LeftOnly, NemaMovementNumbers.SBLeft, TravelDirection.Southbound, demandVolumesLeft[2], newLanes, SBLSignalPhase, arvType: 3));
            newApproaches.Add(new ApproachData(3, TravelDirection.Southbound, "SB", totalDemand, demandVolumesLeft[2] / totalDemand, demandVolumesRight[2] / totalDemand, 0, newLaneGroups));


            newLaneGroups = new List<LaneGroupData>();
            totalDemand = demandVolumesLeft[3] + demandVolumesThru[3] + demandVolumesRight[3];
            newLanes = CreateLanes(new LaneMovementsAllowed[] { LaneMovementsAllowed.ThruOnly, LaneMovementsAllowed.ThruRightShared }, new int[] { 1, 2 });
            newLaneGroups.Add(new LaneGroupData(1, "WB Thru+Right", LaneMovementsAllowed.ThruRightShared, NemaMovementNumbers.WBThru, TravelDirection.Westbound, demandVolumesThru[3] + demandVolumesRight[3], newLanes, WBTSignalPhase, pctRightTurns: demandVolumesRight[3] / (demandVolumesThru[3] + demandVolumesRight[3]) * 100, arvType: 4));
            newLanes = CreateLanes(new LaneMovementsAllowed[] { LaneMovementsAllowed.LeftOnly }, new int[] { 3 });
            newLaneGroups.Add(new LaneGroupData(2, "WB Left", LaneMovementsAllowed.LeftOnly, NemaMovementNumbers.WBLeft, TravelDirection.Westbound, demandVolumesLeft[3], newLanes, WBLSignalPhase, arvType: 3));
            newApproaches.Add(new ApproachData(4, TravelDirection.Westbound, "WB", totalDemand, demandVolumesLeft[3] / totalDemand, demandVolumesRight[3] / totalDemand, 0, newLaneGroups));

            SignalCycleData newSignalData = new SignalCycleData(SigControlType.Pretimed, 124, Timers);

            IntersectionData newIntersection = new IntersectionData(1, AreaType.LargeUrbanized, 1, newApproaches, newSignalData);

            return newIntersection;
        }*/
    }
}