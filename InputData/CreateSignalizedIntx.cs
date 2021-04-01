using System.Collections.Generic;


namespace HCMCalc_UrbanStreets
{
    public class CreateSignalizedIntx
    {                

        public static IntersectionData NewIntersection(List<TimerData> Timers, int numThruLanes, AnalysisMode ProjectAnalMode, int crossStreetWidth)
        {
            List<ApproachData> newApproaches = new List<ApproachData>();
            List<LaneGroupData> newLaneGroups = new List<LaneGroupData>();
            List<LaneData> newLanes;
            List<SignalPhaseData> Phases = new List<SignalPhaseData>();            

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
            newLaneGroups.Add(new LaneGroupData(2, "WB Left", LaneMovementsAllowed.LeftOnly, NemaMovementNumbers.WBLeft, TravelDirection.Westbound, newLanes, WBLSignalPhase, arvType: 3));
            newApproaches.Add(new ApproachData(4, TravelDirection.Westbound, "WB", 0, newLaneGroups));

            SignalCycleData newSignalData = new SignalCycleData(SigControlType.Pretimed, 124, Phases);

            IntersectionData newIntersection = new IntersectionData(1, AreaType.LargeUrbanized, ArterialClass.ClassI, newApproaches, newSignalData, ProjectAnalMode, crossStreetWidth);

            return newIntersection;
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


    }
}
