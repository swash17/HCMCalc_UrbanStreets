using System;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace HCMCalc_UrbanStreets
{
    public class MovementData
    {
        public static NemaMovementNumbers GetIntersectionMovement(MovementDirection movementDirection, ApproachData Approach)
        {
            if (Approach.Dir == TravelDirection.Eastbound || Approach.Dir == TravelDirection.Northbound)
            {
                if (movementDirection == MovementDirection.Left)
                {
                    return NemaMovementNumbers.SBLeft;
                }
                else if (movementDirection == MovementDirection.Through)
                {
                    return NemaMovementNumbers.EBThru;
                }
                else
                {
                    return NemaMovementNumbers.NBRight;
                }
            }
            else
            {
                if (movementDirection == MovementDirection.Left)
                {
                    return NemaMovementNumbers.NBLeft;
                }
                else if (movementDirection == MovementDirection.Through)
                {
                    return NemaMovementNumbers.WBThru;
                }
                else
                {
                    return NemaMovementNumbers.SBRight;
                }
            }
        }

        public static NemaMovementNumbers GetOpposingMvmt(NemaMovementNumbers Movement)
        {
            switch (Movement)
            {
                case NemaMovementNumbers.WBLeft:
                    return NemaMovementNumbers.EBThru;
                case NemaMovementNumbers.EBThru:
                    return NemaMovementNumbers.WBLeft;
                case NemaMovementNumbers.NBLeft:
                    return NemaMovementNumbers.SBThru;
                case NemaMovementNumbers.SBThru:
                    return NemaMovementNumbers.NBLeft;
                case NemaMovementNumbers.EBLeft:
                    return NemaMovementNumbers.WBThru;
                case NemaMovementNumbers.WBThru:
                    return NemaMovementNumbers.EBLeft;
                case NemaMovementNumbers.SBLeft:
                    return NemaMovementNumbers.NBThru;
                case NemaMovementNumbers.NBThru:
                    return NemaMovementNumbers.SBLeft;
                default:
                    return NemaMovementNumbers.WBThru;
            }
        }

        public static NemaMovementNumbers GetAdjLeftMvmt(NemaMovementNumbers Movement)
        {
            if ((int)Movement > 8) { Movement = (NemaMovementNumbers)((int)Movement - 10); }
            switch (Movement)
            {
                case NemaMovementNumbers.WBLeft:
                    return NemaMovementNumbers.WBThru;
                case NemaMovementNumbers.WBThru:
                    return NemaMovementNumbers.WBLeft;
                case NemaMovementNumbers.NBLeft:
                    return NemaMovementNumbers.NBThru;
                case NemaMovementNumbers.NBThru:
                    return NemaMovementNumbers.NBLeft;
                case NemaMovementNumbers.EBLeft:
                    return NemaMovementNumbers.EBThru;
                case NemaMovementNumbers.EBThru:
                    return NemaMovementNumbers.EBLeft;
                case NemaMovementNumbers.SBLeft:
                    return NemaMovementNumbers.SBThru;
                case NemaMovementNumbers.SBThru:
                    return NemaMovementNumbers.SBLeft;
                default:
                    return NemaMovementNumbers.WBThru;
            }
        }

        public static LaneGroupData GetLaneGroupFromNemaMovementNumber(IntersectionData analysisIntersection, NemaMovementNumbers Movement)
        {
            foreach (ApproachData Approach in analysisIntersection.Approaches)
            {
                foreach (LaneGroupData LaneGroup in Approach.LaneGroups)
                {
                    if ((int)LaneGroup.NemaMvmtID == (int)Movement)
                    {
                        return LaneGroup;
                    }
                }
            }
            return null;
        }
    }
}
