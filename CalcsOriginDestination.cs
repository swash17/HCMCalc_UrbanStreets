using System;

namespace HCMCalc_UrbanStreets
{
    /// <summary>
    /// Handles origin-destination related parameters such as the HCM defined OD seeds, an OD matrix, and segment origin/destination volume.
    /// </summary>
    public class OriginDestinationData
    {
        float[,] _odSeeds;
        float[,,,,] _odMatrix;
        float[,,,] _segOriginVol;                //computed origin volumes at upstream signalized intersection
                                                                        //1st = segmentIndex
                                                                        //2nd = segmentDirection
                                                                        //3rd = junctionNo
                                                                        //4th = upstream movement, 1 = upleft, 2 = upthru, 3 = upright, 4 = midblock
        float[,,,] _segDestinationVol;           //computed destination volume at dwnstrm junctions
                                                                        //1st = segmentIndex
                                                                        //2nd = segmentDirection
                                                                        //3rd = junctionNo
                                                                        //4th = downstream movement, 1 = downleft, 2 = downthru, 3 = downright, 4 = midblock
        /// <summary>
        /// Contains parameters necessary for creating an OD matrix.
        /// </summary>
        /// <param name="numSegments"></param>
        /// <param name="numAccessPointsWithinSegment"></param>
        public OriginDestinationData(int numSegments, int numAccessPointsWithinSegment)
        {
            _odSeeds = new float[4, 4];
            _odMatrix = new float[numSegments-1, 2, numAccessPointsWithinSegment, 4, 4];             // Segment Index - Direction - AP Index (within Segment) - Upstream Mvmt - Downstream Mvmt
            _segOriginVol = new float[numSegments-1, 2, numAccessPointsWithinSegment, 4];            // Segment Index - Direction - AP Index (within Segment) - Upstream Mvmt
            _segDestinationVol = new float[numSegments-1, 2, numAccessPointsWithinSegment, 4];       // Segment Index - Direction - AP Index (within Segment) - Downstream Mvmt

            //default values (HCM 2016, Chap. 30, p. 5)
            _odSeeds[0, 0] = 0.02f;
            _odSeeds[0, 1] = 0.91f;
            _odSeeds[0, 2] = 0.05f;
            _odSeeds[0, 3] = 0.02f;
            _odSeeds[1, 0] = 0.10f;
            _odSeeds[1, 1] = 0.78f;
            _odSeeds[1, 2] = 0.10f;
            _odSeeds[1, 3] = 0.02f;
            _odSeeds[2, 0] = 0.05f;
            _odSeeds[2, 1] = 0.92f;
            _odSeeds[2, 2] = 0.02f;
            _odSeeds[2, 3] = 0.01f;
            _odSeeds[3, 0] = 0.02f;
            _odSeeds[3, 1] = 0.97f;
            _odSeeds[3, 2] = 0.01f;
            _odSeeds[3, 3] = 0;
        }
        /// <summary>
        /// Seeds defined by the HCM.
        /// </summary>
        public float[,] Seeds { get => _odSeeds; set => _odSeeds = value; }
        /// <summary>
        /// The O-D Matrix.
        /// \nFirst index: Segment
        /// \nSecond index: Direction
        /// \nThird index: Access point
        /// \nFourth index: Upstream movement
        /// \nFifth index: Downstream movement
        /// </summary>
        public float[,,,,] Matrix { get => _odMatrix; set => _odMatrix = value; }
        /// <summary>
        /// The segment origin volume.
        /// \nFirst index: Segment
        /// \nSecond index: Direction
        /// \nThird index: Access point
        /// \nFourth index: Upstream movement
        /// </summary>
        public float[,,,] SegOriginVol { get => _segOriginVol; set => _segOriginVol = value; }
        /// <summary>
        /// The segment destination volume.
        /// \nFirst index: Segment
        /// \nSecond index: Direction
        /// \nThird index: Access point
        /// \nFourth index: Downstream movement
        /// </summary>
        public float[,,,] SegDestinationVol { get => _segDestinationVol; set => _segDestinationVol = value; }
    }

    /// <summary>
    /// Handles calculations related to the creation of an OD matrix.
    /// </summary>
    public class CalcsOriginDestination
    {
        /// <summary>
        /// Creates the OD matrix using demands taken from an input <param name="ArterialData"></param>.
        /// </summary>
        /// <param name="analysisArterial"></param>
        /// <returns></returns>
        public static OriginDestinationData DefineODmatrix(ArterialData analysisArterial)
        {
            int maxNumAP=0;
            foreach (SegmentData Segment in analysisArterial.Segments)
                if (Segment.Link.AccessPoints.Count > maxNumAP)
                    maxNumAP = Segment.Link.AccessPoints.Count;

            float[] originVol = new float[4];
            float[] destinationVol = new float[4];

            int TotalSegments = analysisArterial.Segments.Count;
            OriginDestinationData ODdata = new OriginDestinationData(TotalSegments, maxNumAP+1);

            // Evaluate Analysis Direction (EB / NB)
            // All OD's are relative to the upstream signal as the origin
            
            for (int segmentIndex = 0; segmentIndex < TotalSegments-1; segmentIndex++)
            {
                SegmentData analysisSegment = analysisArterial.Segments[segmentIndex];
                SegmentData downstreamSegment = analysisArterial.Segments[segmentIndex + 1];

                LaneGroupData LaneGroupLeft = MovementData.GetLaneGroupFromNemaMovementNumber(analysisSegment.Intersection, NemaMovementNumbers.SBLeft);
                LaneGroupData LaneGroupThru = MovementData.GetLaneGroupFromNemaMovementNumber(analysisSegment.Intersection, NemaMovementNumbers.EBThru);
                LaneGroupData LaneGroupRight = MovementData.GetLaneGroupFromNemaMovementNumber(analysisSegment.Intersection, NemaMovementNumbers.NBThru); // NB Right
                originVol = ComputeDischargeVolume(originVol, LaneGroupLeft, LaneGroupThru, LaneGroupRight);

                // Compute OD's to each downstream access point
                foreach (AccessPointData AccessPoint in analysisSegment.Link.AccessPoints)
                {
                    destinationVol = new float[] { AccessPoint.Volume[0], AccessPoint.Volume[1], AccessPoint.Volume[2], 0 };
                    originVol[3] = 0;
                    for (int addUpAccessPts = 0; addUpAccessPts < analysisSegment.Link.AccessPoints.IndexOf(AccessPoint); addUpAccessPts++)
                    {
                        originVol[3] += analysisSegment.Link.AccessPoints[addUpAccessPts].Volume[9] + analysisSegment.Link.AccessPoints[addUpAccessPts].Volume[8];
                        destinationVol[3] += analysisSegment.Link.AccessPoints[addUpAccessPts].Volume[0] + analysisSegment.Link.AccessPoints[addUpAccessPts].Volume[2];
                    }
                    ODdata = ComputeODs(analysisArterial, analysisArterial.Segments.IndexOf(analysisSegment), SegmentDirection.EBNB, analysisSegment.Link.AccessPoints.IndexOf(AccessPoint)+1, originVol, destinationVol, ODdata);
                }

                // Compute OD's to downstream intersection
                LaneGroupLeft = MovementData.GetLaneGroupFromNemaMovementNumber(downstreamSegment.Intersection, NemaMovementNumbers.EBLeft);
                LaneGroupThru = MovementData.GetLaneGroupFromNemaMovementNumber(downstreamSegment.Intersection, NemaMovementNumbers.EBThru);
                LaneGroupRight = MovementData.GetLaneGroupFromNemaMovementNumber(downstreamSegment.Intersection, NemaMovementNumbers.EBThru); // EB Right
                destinationVol = ComputeDemandVolume(destinationVol, LaneGroupLeft, LaneGroupThru, LaneGroupRight);
                originVol[3] = 0;
                destinationVol[3] = 0;
                for (int addUpAccessPts = 0; addUpAccessPts < analysisSegment.Link.AccessPoints.Count; addUpAccessPts++)
                {
                    originVol[3] += analysisSegment.Link.AccessPoints[addUpAccessPts].Volume[9] + analysisSegment.Link.AccessPoints[addUpAccessPts].Volume[8];
                    destinationVol[3] += analysisSegment.Link.AccessPoints[addUpAccessPts].Volume[0] + analysisSegment.Link.AccessPoints[addUpAccessPts].Volume[2];
                }
                ODdata = ComputeODs(analysisArterial, analysisArterial.Segments.IndexOf(analysisSegment), SegmentDirection.EBNB, 0, originVol, destinationVol, ODdata);
            }

            // Evaluate Opposing Direction (WB / SB)
            for (int segmentIndex = TotalSegments-2; segmentIndex >= 0; segmentIndex--)
            {
                //Should segment index loop iterate from 0 to max, rather than max to zero? Analysis should work from upstream to downstream
                SegmentData analysisSegment = analysisArterial.Segments[segmentIndex + 1];
                SegmentData downstreamSegment = analysisArterial.Segments[segmentIndex];
                LaneGroupData LaneGroupLeft = MovementData.GetLaneGroupFromNemaMovementNumber(analysisSegment.Intersection, NemaMovementNumbers.NBLeft);
                LaneGroupData LaneGroupThru = MovementData.GetLaneGroupFromNemaMovementNumber(analysisSegment.Intersection, NemaMovementNumbers.WBThru);
                LaneGroupData LaneGroupRight = MovementData.GetLaneGroupFromNemaMovementNumber(analysisSegment.Intersection, NemaMovementNumbers.SBThru); // SB Right
                originVol = ComputeDischargeVolume(originVol, LaneGroupLeft, LaneGroupThru, LaneGroupRight);

                // Compute OD's to each downstream access point
                for (int AccessPointIndex = downstreamSegment.Link.AccessPoints.Count-1; AccessPointIndex >= 0; AccessPointIndex--)
                {
                    AccessPointData AccessPoint = downstreamSegment.Link.AccessPoints[AccessPointIndex];
                    destinationVol[0] = AccessPoint.Volume[3];
                    destinationVol[1] = AccessPoint.Volume[4];
                    destinationVol[2] = AccessPoint.Volume[5];
                    originVol[3] = 0;
                    destinationVol[3] = 0;
                    int addUpAccessPts;
                    for (addUpAccessPts = downstreamSegment.Link.AccessPoints.Count-1; addUpAccessPts > AccessPointIndex; addUpAccessPts--)
                    {
                        originVol[3] += downstreamSegment.Link.AccessPoints[addUpAccessPts].Volume[11] + downstreamSegment.Link.AccessPoints[addUpAccessPts].Volume[6];
                        destinationVol[3] += downstreamSegment.Link.AccessPoints[addUpAccessPts].Volume[3] + downstreamSegment.Link.AccessPoints[addUpAccessPts].Volume[5];
                    }
                    ODdata = ComputeODs(analysisArterial, analysisArterial.Segments.IndexOf(downstreamSegment), SegmentDirection.WBSB, AccessPointIndex+1, originVol, destinationVol, ODdata);
                }

                // Compute OD's to downstream intersection
                LaneGroupLeft = MovementData.GetLaneGroupFromNemaMovementNumber(downstreamSegment.Intersection, NemaMovementNumbers.WBLeft);
                LaneGroupThru = MovementData.GetLaneGroupFromNemaMovementNumber(downstreamSegment.Intersection, NemaMovementNumbers.WBThru);
                LaneGroupRight = MovementData.GetLaneGroupFromNemaMovementNumber(downstreamSegment.Intersection, NemaMovementNumbers.WBThru); //WB Right
                destinationVol = ComputeDemandVolume(destinationVol, LaneGroupLeft, LaneGroupThru, LaneGroupRight);
                destinationVol[3] = 0;
                originVol[3] = 0;
                for (int AccessPt = downstreamSegment.Link.AccessPoints.Count-1; AccessPt >= 0; AccessPt--)
                {
                    originVol[3] += downstreamSegment.Link.AccessPoints[AccessPt].Volume[11] + downstreamSegment.Link.AccessPoints[AccessPt].Volume[6];
                    destinationVol[3] += downstreamSegment.Link.AccessPoints[AccessPt].Volume[3] + downstreamSegment.Link.AccessPoints[AccessPt].Volume[5];
                }
                ODdata = ComputeODs(analysisArterial, analysisArterial.Segments.IndexOf(downstreamSegment), SegmentDirection.WBSB, 0, originVol, destinationVol, ODdata);
            }
            return ODdata;
        }

        /// <summary>
        /// Returns adjusted discharge volumes based off of the turning percentages.
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="LaneGroupLeft"></param>
        /// <param name="LaneGroupThru"></param>
        /// <param name="LaneGroupRight"></param>
        /// <returns></returns>
        public static float[] ComputeDischargeVolume(float[] volume, LaneGroupData LaneGroupLeft, LaneGroupData LaneGroupThru, LaneGroupData LaneGroupRight)
        {
            if (LaneGroupLeft.Type == LaneMovementsAllowed.LeftOnly)
                volume[0] = LaneGroupLeft.DischargeVolume;
            else if (LaneGroupLeft.Type == LaneMovementsAllowed.ThruLeftShared)
                volume[0] = (int)(LaneGroupLeft.DischargeVolume * (LaneGroupLeft.PctLeftTurns / 100) + 0.5);

            if (LaneGroupThru.Type == LaneMovementsAllowed.ThruOnly)
                volume[1] = LaneGroupThru.DischargeVolume;
            else if (LaneGroupThru.Type == LaneMovementsAllowed.ThruLeftShared)
                volume[1] = (int)(LaneGroupThru.DischargeVolume * (1 - LaneGroupThru.PctLeftTurns / 100) + 0.5);
            else if (LaneGroupThru.Type == LaneMovementsAllowed.ThruRightShared)
                volume[1] = (int)(LaneGroupThru.DischargeVolume * (1 - LaneGroupThru.PctRightTurns / 100) + 0.5);

            if (LaneGroupRight.Type == LaneMovementsAllowed.RightOnly)
                volume[2] = LaneGroupRight.DischargeVolume;
            else if (LaneGroupRight.Type == LaneMovementsAllowed.ThruRightShared)
                volume[2] = (int)(LaneGroupRight.DischargeVolume * (LaneGroupRight.PctRightTurns / 100) + 0.5);

            //originVol[0] = analysisSegment.Intersection.Approaches[(int)TravelDirection.Southbound].PctLeftTurns/100 * analysisSegment.Intersection.Approaches[(int)TravelDirection.Southbound].DemandVolume;
            return volume;
        }

        /// <summary>
        /// Returns adjusted demand volumes based off of the turning percentages.
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="LaneGroupLeft"></param>
        /// <param name="LaneGroupThru"></param>
        /// <param name="LaneGroupRight"></param>
        /// <returns></returns>
        public static float[] ComputeDemandVolume(float[] volume, LaneGroupData LaneGroupLeft, LaneGroupData LaneGroupThru, LaneGroupData LaneGroupRight)
        {
            // Volume array must be an input since the input array may already contain some values, and only a few indicies may be changed
            if (LaneGroupLeft.Type == LaneMovementsAllowed.LeftOnly)
                volume[0] = LaneGroupLeft.DemandVolumeVehPerHr;
            else if (LaneGroupLeft.Type == LaneMovementsAllowed.ThruLeftShared)
                volume[0] = (int)(LaneGroupLeft.DemandVolumeVehPerHr * (LaneGroupLeft.PctLeftTurns / 100) + 0.5);

            if (LaneGroupThru.Type == LaneMovementsAllowed.ThruOnly)
                volume[1] = LaneGroupThru.DemandVolumeVehPerHr;
            else if (LaneGroupThru.Type == LaneMovementsAllowed.ThruLeftShared)
                volume[1] = (int)(LaneGroupThru.DemandVolumeVehPerHr * (1 - LaneGroupThru.PctLeftTurns / 100) + 0.5);
            else if (LaneGroupThru.Type == LaneMovementsAllowed.ThruRightShared)
                volume[1] = (int)(LaneGroupThru.DemandVolumeVehPerHr * (1 - LaneGroupThru.PctRightTurns / 100) + 0.5);

            if (LaneGroupRight.Type == LaneMovementsAllowed.RightOnly)
                volume[2] = LaneGroupRight.DemandVolumeVehPerHr;
            else if (LaneGroupRight.Type == LaneMovementsAllowed.ThruRightShared)
                volume[2] = (int)(LaneGroupRight.DemandVolumeVehPerHr * (LaneGroupRight.PctRightTurns / 100) + 0.5);

            //originVol[0] = analysisSegment.Intersection.Approaches[(int)TravelDirection.Southbound].PctLeftTurns/100 * analysisSegment.Intersection.Approaches[(int)TravelDirection.Southbound].DemandVolume;
            return volume;
        }

        /// <summary>
        /// Computes the ODS for the matrix based off of the origin and destination volumes.
        /// </summary>
        /// <param name="analysisArterial"></param>
        /// <param name="segmentIndex"></param>
        /// <param name="segDirection"></param>
        /// <param name="junctionNo"></param>
        /// <param name="originVol"></param>
        /// <param name="destinationVol"></param>
        /// <param name="ODdata"></param>
        /// <returns></returns>
        public static OriginDestinationData ComputeODs(ArterialData analysisArterial, int segmentIndex, SegmentDirection segDirection, int junctionNo, float[] originVol, float[] destinationVol, OriginDestinationData ODdata)
        {
            float convergeErr = 99999;                  // convergeErr
            int loopsCompleted = 0;                     // n
            float[] AdjOriginVol = new float[4];        // ac
            float[] AdjDestVol = new float[4];          // bc
            float SumOriginVolAdj = 0;                  // sumac
            float SumDestVolAdj = 0;                    // sumbc

            float[] OriginAdjFactor = new float[4];     // denI
            float[] DestAdjFactor = new float[4];       // denJ
            float[] an = new float[4];                  // an (What is this?)
            float[] RevDestinationVol = new float[4];   // revDestinationVol

            //These calculations are described in HCM 2016, Chap. 30, pp. 5-7)

            //The following comment is outdated--eventually delete
            // THIS SUBROUTINE AUTOMATES THE TURNING MOVEMENT ESTIMATION TECHNIQUE DESCRIBED IN CHAPTER 10
            // (pages 19-20) OF THE 2000 HCM.  THIS TECHNIQUE IS USED TO ESTIMATE THE ORIGIN-DESTINATION PROPORTIONS
            // BETWEEN THE UPSTREAM SIGNALIZED INTERSECTION AND A DOWNSTREAM JUNCTION (ACCESS POINT OR INTERSECTION).

            for (int Movement = 0; Movement < 4; Movement++)
            {
                AdjOriginVol[Movement] = originVol[Movement];
                SumOriginVolAdj += AdjOriginVol[Movement];
                AdjDestVol[Movement] = destinationVol[Movement];
                SumDestVolAdj += AdjDestVol[Movement];
            }
            if ((SumDestVolAdj == 0) || (SumOriginVolAdj == 0)) { return ODdata; }
            for (int Movement = 0; Movement < 4; Movement++)
            {
                RevDestinationVol[Movement] = destinationVol[Movement] * SumOriginVolAdj / SumDestVolAdj;
            }
            

            // Adjust destination volume to ensure it equals the origin volume (as required by the technique)

            while (convergeErr >= 0.01 && loopsCompleted <= 20)
            {
                for (int downMvmt = 0; downMvmt < 4; downMvmt++)
                {
                    DestAdjFactor[downMvmt] = 0;
                    for (int upMvmt = 0; upMvmt < 4; upMvmt++)
                    {
                        DestAdjFactor[downMvmt] += (ODdata.Seeds[upMvmt, downMvmt] * AdjOriginVol[upMvmt]);  //Eq. 30-4
                    }
                    AdjDestVol[downMvmt] = RevDestinationVol[downMvmt] / (DestAdjFactor[downMvmt] + 0.00001f);  //Eq. 30-5
                }
                for (int upMvmt = 0; upMvmt < 4; upMvmt++)
                {
                    OriginAdjFactor[upMvmt] = 0;
                    for (int downMvmt = 0; downMvmt < 4; downMvmt++)
                    {
                        OriginAdjFactor[upMvmt] += (ODdata.Seeds[upMvmt, downMvmt] * AdjDestVol[downMvmt]);  //Eq. 30-6
                    }
                    an[upMvmt] = originVol[upMvmt] / (OriginAdjFactor[upMvmt] + 0.00001f);  //Eq. 30-7
                }
                convergeErr = 0;
                for (int Movement = 0; Movement < 4; Movement++)
                {
                    convergeErr += (Math.Abs(an[Movement] - AdjOriginVol[Movement]));
                }
                for (int Movement = 0; Movement < 4; Movement++)
                {
                    AdjOriginVol[Movement] = an[Movement];
                }
                loopsCompleted++;
            }
            for (int upMvmt = 0; upMvmt < 4; upMvmt++)
            {
                ODdata.SegOriginVol[segmentIndex, (int)segDirection, junctionNo, upMvmt] = originVol[upMvmt];
                ODdata.SegDestinationVol[segmentIndex, (int)segDirection, junctionNo, upMvmt] = destinationVol[upMvmt];

                for (int downMvmt = 0; downMvmt < 4; downMvmt++) 
                {
                    //the right side looks like it should be Eq. 30-8, but the last three terms are not equal to the destination adjustment factor 
                    ODdata.Matrix[segmentIndex, (int)segDirection, junctionNo, upMvmt, downMvmt] = ODdata.Seeds[upMvmt, downMvmt] * AdjOriginVol[upMvmt] * AdjDestVol[downMvmt] / (originVol[upMvmt] + 0.00001f); 
                }
            }
            return ODdata;
        }
    }
}
