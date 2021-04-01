using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


namespace HCMCalc_UrbanStreets
{

    public class FileInputOutput2
    {

        public static void SerializeArterialData(string filename, ArterialData arterial)
        {
            // Writing the file requires a TextWriter.
            //TextWriter myStreamWriter = new StreamWriter(filename);
            FileStream myStream = new FileStream(filename, FileMode.Create);

            // Create the XmlSerializer instance.
            XmlSerializer mySerializer = new XmlSerializer(typeof(ArterialData));

            mySerializer.Serialize(myStream, arterial);
            myStream.Close();
        }

        public static ArterialData DeserializeArterialData(string filename)
        {
            FileStream myFileStream = new FileStream(filename, FileMode.Open);
            // Create the XmlSerializer instance.
            XmlSerializer mySerializer = new XmlSerializer(typeof(ArterialData));
            ArterialData arterial = (ArterialData)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();

            return arterial;
        }

        public static List<SerVolTablesByClass> DeserializeSerVolTables(string filename)
        {
            FileStream myFileStream = new FileStream(filename, FileMode.Open);
            // Create the XmlSerializer instance.
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<SerVolTablesByClass>));
            List<SerVolTablesByClass> serVolTables = (List<SerVolTablesByClass>)mySerializer.Deserialize(myFileStream);
            myFileStream.Close();

            return serVolTables;
        }
    }



    /*public class CreateArterial
    {

        public static ArterialData NewArterial()
        {
            LinkData newLink = CreateLink();
            IntersectionData newIntersection = CreateIntersection();

            List<SegmentData> Segments = new List<SegmentData>();
            SegmentData newSegment = new SegmentData(1, newLink, newIntersection);
            Segments.Add(newSegment);

            ArterialData newArterial = new ArterialData(AreaType.LargeUrbanized, 1, TravelDirection.Eastbound, Segments);

            return newArterial;
        }

        public static LinkData CreateLink()
        {
            LinkData newLink = new LinkData(1, 1320, 6000, 350, 2, 35, 40, 100, MedianType.Restrictive, 100, false, ParkingActivityLevel.NotApplicable, 0);

            return newLink;
        }

        public static IntersectionData CreateIntersection()
        {
            List<ApproachData> newApproaches;
            ApproachData newApproach;
            List<LaneGroupData> newLaneGroups;
            LaneGroupData newLaneGroup;
            List<LaneData> newLanes;
            LaneData newLane;

            newApproaches = new List<ApproachData>();
            newLaneGroups = new List<LaneGroupData>();

            newLanes = new List<LaneData>();
            newLane = new LaneData(1, LaneMovementsAllowed.ThruOnly);
            newLanes.Add(newLane);
            newLane = new LaneData(2, LaneMovementsAllowed.ThruOnly);
            newLanes.Add(newLane);
            
            newLaneGroup = new LaneGroupData(1, "EB Thru", LaneGroupType.ThruOnly, NemaMovementNumbers.EBThru, TravelDirection.Eastbound, 1440, newLanes, 3, new SignalPhaseData());
            newLaneGroups.Add(newLaneGroup);

            newLanes = new List<LaneData>();
            newLane = new LaneData(3, LaneMovementsAllowed.LeftOnly);
            newLanes.Add(newLane);

            newLaneGroup = new LaneGroupData(2, "EB Left", LaneGroupType.LeftOnly, NemaMovementNumbers.EBLeft, TravelDirection.Eastbound, 160, newLanes, 3, new SignalPhaseData());
            newLaneGroups.Add(newLaneGroup);

            newApproach = new ApproachData(1, TravelDirection.Eastbound, "EB", 1600, 10, 5, 0, newLaneGroups);
            newApproaches.Add(newApproach);

            newLanes = new List<LaneData>();
            newLane = new LaneData(1, LaneMovementsAllowed.ThruOnly);
            newLanes.Add(newLane);
            newLane = new LaneData(2, LaneMovementsAllowed.ThruOnly);
            newLanes.Add(newLane);

            newLaneGroups = new List<LaneGroupData>();
            newLaneGroup = new LaneGroupData(1, "WB Thru", LaneGroupType.ThruOnly, NemaMovementNumbers.WBThru, newApproach.Dir, 720, newLanes, 3, new SignalPhaseData());
            newLaneGroups.Add(newLaneGroup);

            newLanes = new List<LaneData>();
            newLane = new LaneData(3, LaneMovementsAllowed.LeftOnly);
            newLanes.Add(newLane);

            newLaneGroup = new LaneGroupData(2, "WB Left", LaneGroupType.LeftOnly, NemaMovementNumbers.WBLeft, newApproach.Dir, 80, newLanes, 3, new SignalPhaseData());
            newLaneGroups.Add(newLaneGroup);

            newApproach = new ApproachData(1, TravelDirection.Westbound, "WB", 800, 10, 5, 0, newLaneGroups);
            newApproaches.Add(newApproach);

            newLanes = new List<LaneData>();
            newLane = new LaneData(1, LaneMovementsAllowed.ThruOnly);
            newLanes.Add(newLane);

            newLaneGroups = new List<LaneGroupData>();
            newLaneGroup = new LaneGroupData(1, "NB Thru", LaneGroupType.ThruOnly, NemaMovementNumbers.NBThru, newApproach.Dir, 75, newLanes, 3, new SignalPhaseData());
            newLaneGroups.Add(newLaneGroup);

            newLanes = new List<LaneData>();
            newLane = new LaneData(2, LaneMovementsAllowed.LeftOnly);
            newLanes.Add(newLane);

            newLaneGroup = new LaneGroupData(2, "NB Left", LaneGroupType.LeftOnly, NemaMovementNumbers.NBLeft, newApproach.Dir, 25, newLanes, 3, new SignalPhaseData());
            newLaneGroups.Add(newLaneGroup);

            newApproach = new ApproachData(1, TravelDirection.Northbound, "NB", 100, 20, 15, 0, newLaneGroups);
            newApproaches.Add(newApproach);

            newLanes = new List<LaneData>();
            newLane = new LaneData(1, LaneMovementsAllowed.ThruOnly);
            newLanes.Add(newLane);

            newLaneGroups = new List<LaneGroupData>();
            newLaneGroup = new LaneGroupData(1, "SB Thru", LaneGroupType.ThruOnly, NemaMovementNumbers.SBThru, newApproach.Dir, 75, newLanes, 3, new SignalPhaseData());
            newLaneGroups.Add(newLaneGroup);

            newLanes = new List<LaneData>();
            newLane = new LaneData(2, LaneMovementsAllowed.LeftOnly);
            newLanes.Add(newLane);

            newLaneGroup = new LaneGroupData(2, "SB Left", LaneGroupType.LeftOnly, NemaMovementNumbers.SBLeft, newApproach.Dir, 25, newLanes, 3, new SignalPhaseData());
            newLaneGroups.Add(newLaneGroup);

            newApproach = new ApproachData(1, TravelDirection.Southbound, "SB", 100, 20, 15, 0, newLaneGroups);
            newApproaches.Add(newApproach);

            SignalCycleData newSignalData = new SignalCycleData(SigControlType.Pretimed, 94);

            IntersectionData newIntersection = new IntersectionData(1, AreaType.LargeUrbanized, 1, newApproaches, newSignalData);

            return newIntersection;
        }
    }

    public class CreateArterial2
    {

        public static ArterialData NewArterial()
        {
            List<SegmentData> Segments = new List<SegmentData>();
            SegmentData newSegment;
            LinkData newLink;
            IntersectionData newIntersection;

            newLink = CreateLink();
            newIntersection = CreateIntersection1();

            newSegment = new SegmentData(1, newLink, newIntersection);
            Segments.Add(newSegment);

            newLink = CreateLink();
            newIntersection = CreateIntersections2through4();

            newSegment = new SegmentData(2, newLink, newIntersection);
            Segments.Add(newSegment);

            newLink = CreateLink();
            newIntersection = CreateIntersections2through4();

            newSegment = new SegmentData(3, newLink, newIntersection);
            Segments.Add(newSegment);

            newLink = CreateLink();
            newIntersection = CreateIntersections2through4();

            newSegment = new SegmentData(4, newLink, newIntersection);
            Segments.Add(newSegment);

            ArterialData newArterial = new ArterialData(AreaType.LargeUrbanized, 1, TravelDirection.Eastbound, Segments);

            return newArterial;
        }

        public static LinkData CreateLink()
        {
            LinkData newLink = new LinkData(1, 1320, 6000, 1600, 2, 30, 40, 100, MedianType.Restrictive, 100, false, ParkingActivityLevel.NotApplicable, 0);

            return newLink;
        }

        public static IntersectionData CreateIntersection1()
        {
            List<ApproachData> newApproaches;
            ApproachData newApproach;
            List<LaneGroupData> newLaneGroups;
            LaneGroupData newLaneGroup;
            List<LaneData> newLanes;
            LaneData newLane;
            SignalPhaseData newSignalPhase;

            newApproaches = new List<ApproachData>();
            newLaneGroups = new List<LaneGroupData>();
            //newLaneGroup = new LaneGroupData(1, "EB Thru", LaneGroupType.ThruOnly, 2, 3, PhasingType.Protect, new SignalPhaseData());

            newLanes = new List<LaneData>();
            newLane = new LaneData(1, LaneMovementsAllowed.RightOnly);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(12, PhasingType.Protected, 42, 3, 1);
            newLaneGroup = new LaneGroupData(1, "EB Right", LaneGroupType.RightOnly, NemaMovementNumbers.EBRight, TravelDirection.Eastbound, 160, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            //newSignalPhase.NemaPhaseId = (byte)NemaMovementNumbers.EBRight;

            newLanes = new List<LaneData>();
            newLane = new LaneData(2, LaneMovementsAllowed.ThruOnly);
            newLanes.Add(newLane);
            newLane = new LaneData(3, LaneMovementsAllowed.ThruOnly);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(2, PhasingType.Protected, 42, 3, 1);
            newLaneGroup = new LaneGroupData(2, "EB Thru", LaneGroupType.ThruOnly, NemaMovementNumbers.EBThru, TravelDirection.Eastbound, 1280, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newLanes = new List<LaneData>();
            newLane = new LaneData(4, LaneMovementsAllowed.LeftOnly);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(5, PhasingType.Protected, 11, 3, 1);
            newLaneGroup = new LaneGroupData(3, "EB Left", LaneGroupType.LeftOnly, NemaMovementNumbers.EBLeft, TravelDirection.Eastbound, 160, newLanes, 1, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newApproach = new ApproachData(1, TravelDirection.Eastbound, "EB", 1600, 10, 10, 0, newLaneGroups);
            newApproaches.Add(newApproach);

            newLaneGroups = new List<LaneGroupData>();

            newLanes = new List<LaneData>();
            newLane = new LaneData(1, LaneMovementsAllowed.ThruRightShared);
            newLanes.Add(newLane);
            newLane = new LaneData(2, LaneMovementsAllowed.ThruOnly);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(6, PhasingType.Protected, 42, 3, 1);
            newLaneGroup = new LaneGroupData(1, "WB Thru+Right", LaneGroupType.ThruRightShared, NemaMovementNumbers.WBThru, TravelDirection.Westbound, 720, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newLanes = new List<LaneData>();
            newLane = new LaneData(3, LaneMovementsAllowed.LeftOnly);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(1, PhasingType.Protected, 11, 3, 1);
            newLaneGroup = new LaneGroupData(2, "WB Left", LaneGroupType.LeftOnly, NemaMovementNumbers.WBLeft, TravelDirection.Westbound, 80, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newApproach = new ApproachData(1, TravelDirection.Westbound, "WB", 800, 10, 10, 0, newLaneGroups);
            newApproaches.Add(newApproach);

            newLaneGroups = new List<LaneGroupData>();

            newLanes = new List<LaneData>();
            newLane = new LaneData(1, LaneMovementsAllowed.All);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(8, PhasingType.Protected, 10, 3, 1);
            newLaneGroup = new LaneGroupData(1, "NB All", LaneGroupType.All, NemaMovementNumbers.NBThru, TravelDirection.Northbound, 100, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newApproach = new ApproachData(1, TravelDirection.Northbound, "NB", 100, 25, 25, 0, newLaneGroups);
            newApproaches.Add(newApproach);

            newLaneGroups = new List<LaneGroupData>();

            newLanes = new List<LaneData>();
            newLane = new LaneData(1, LaneMovementsAllowed.All);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(4, PhasingType.Protected, 10, 3, 1);
            newLaneGroup = new LaneGroupData(1, "SB All", LaneGroupType.All, NemaMovementNumbers.SBThru, TravelDirection.Southbound, 100, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newApproach = new ApproachData(1, TravelDirection.Southbound, "SB", 100, 25, 25, 0, newLaneGroups);
            newApproaches.Add(newApproach);

            SignalCycleData newSignalData = new SignalCycleData(SigControlType.Pretimed, 94);

            IntersectionData newIntersection = new IntersectionData(1, AreaType.LargeUrbanized, 1, newApproaches, newSignalData);

            return newIntersection;
        }

        public static IntersectionData CreateIntersections2through4()
        {
            List<ApproachData> newApproaches;
            ApproachData newApproach;
            List<LaneGroupData> newLaneGroups;
            LaneGroupData newLaneGroup;
            List<LaneData> newLanes;
            LaneData newLane;
            SignalPhaseData newSignalPhase;

            newApproaches = new List<ApproachData>();

            newLaneGroups = new List<LaneGroupData>();
            //newLaneGroup = new LaneGroupData(1, "EB Thru", LaneGroupType.ThruOnly, 2, 3, PhasingType.Protect, new SignalPhaseData());

            newLanes = new List<LaneData>();
            newLane = new LaneData(1, LaneMovementsAllowed.RightOnly);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(12, PhasingType.Protected, 56, 3, 1);
            newLaneGroup = new LaneGroupData(1, "EB Right", LaneGroupType.RightOnly, NemaMovementNumbers.EBRight, TravelDirection.Eastbound, 160, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newLanes = new List<LaneData>();
            newLane = new LaneData(2, LaneMovementsAllowed.ThruOnly);
            newLanes.Add(newLane);
            newLane = new LaneData(3, LaneMovementsAllowed.ThruOnly);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(2, PhasingType.Protected, 56, 3, 1);
            newLaneGroup = new LaneGroupData(1, "EB Thru", LaneGroupType.ThruOnly, NemaMovementNumbers.EBThru, TravelDirection.Eastbound, 1280, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newLanes = new List<LaneData>();
            newLane = new LaneData(4, LaneMovementsAllowed.LeftOnly);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(5, PhasingType.Protected, 15, 3, 1);
            newLaneGroup = new LaneGroupData(2, "EB Left", LaneGroupType.LeftOnly, NemaMovementNumbers.EBLeft, TravelDirection.Eastbound, 160, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newApproach = new ApproachData(1, TravelDirection.Eastbound, "EB", 1600, 10, 10, 0, newLaneGroups);
            newApproaches.Add(newApproach);

            newLaneGroups = new List<LaneGroupData>();

            newLanes = new List<LaneData>();
            newLane = new LaneData(1, LaneMovementsAllowed.ThruRightShared);
            newLanes.Add(newLane);
            newLane = new LaneData(2, LaneMovementsAllowed.ThruOnly);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(6, PhasingType.Protected, 56, 3, 1);
            newLaneGroup = new LaneGroupData(1, "WB Thru+Right", LaneGroupType.ThruRightShared, NemaMovementNumbers.WBThru, TravelDirection.Westbound, 720, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newLanes = new List<LaneData>();
            newLane = new LaneData(3, LaneMovementsAllowed.LeftOnly);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(1, PhasingType.Protected, 15, 3, 1);
            newLaneGroup = new LaneGroupData(2, "WB Left", LaneGroupType.LeftOnly, NemaMovementNumbers.WBLeft, TravelDirection.Westbound, 80, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newApproach = new ApproachData(1, TravelDirection.Westbound, "WB", 800, 10, 10, 0, newLaneGroups);
            newApproaches.Add(newApproach);

            newLaneGroups = new List<LaneGroupData>();

            newLanes = new List<LaneData>();
            newLane = new LaneData(1, LaneMovementsAllowed.ThruRightShared);
            newLanes.Add(newLane);            

            newSignalPhase = new SignalPhaseData(8, PhasingType.Protected, 15, 3, 1);
            newLaneGroup = new LaneGroupData(1, "NB Thru+Right", LaneGroupType.ThruRightShared, NemaMovementNumbers.NBThru, TravelDirection.Northbound, 450, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newLanes = new List<LaneData>();
            newLane = new LaneData(2, LaneMovementsAllowed.LeftOnly);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(3, PhasingType.Protected, 10, 3, 1);
            newLaneGroup = new LaneGroupData(2, "NB Left", LaneGroupType.LeftOnly, NemaMovementNumbers.NBLeft, TravelDirection.Northbound, 150, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newApproach = new ApproachData(1, TravelDirection.Northbound, "NB", 600, 25, 25, 0, newLaneGroups);
            newApproaches.Add(newApproach);

            newLanes = new List<LaneData>();
            newLane = new LaneData(1, LaneMovementsAllowed.ThruRightShared);
            newLanes.Add(newLane);            

            newLaneGroups = new List<LaneGroupData>();
            newSignalPhase = new SignalPhaseData(4, PhasingType.Protected, 15, 3, 1);
            newLaneGroup = new LaneGroupData(1, "SB Thru+Right", LaneGroupType.ThruRightShared, NemaMovementNumbers.SBThru, TravelDirection.Southbound, 450, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newLanes = new List<LaneData>();
            newLane = new LaneData(2, LaneMovementsAllowed.LeftOnly);
            newLanes.Add(newLane);

            newSignalPhase = new SignalPhaseData(7, PhasingType.Protected, 10, 3, 1);
            newLaneGroup = new LaneGroupData(2, "SB Left", LaneGroupType.LeftOnly, NemaMovementNumbers.SBLeft, TravelDirection.Southbound, 150, newLanes, 3, newSignalPhase);
            newLaneGroups.Add(newLaneGroup);

            newApproach = new ApproachData(1, TravelDirection.Southbound, "SB", 600, 25, 25, 0, newLaneGroups);
            newApproaches.Add(newApproach);
            newLaneGroups.Add(newLaneGroup);

            SignalCycleData newSignalData = new SignalCycleData(SigControlType.Pretimed, 93);

            IntersectionData newIntersection = new IntersectionData(1, AreaType.LargeUrbanized, 1, newApproaches, newSignalData);

            return newIntersection;
        }

    }*/

}