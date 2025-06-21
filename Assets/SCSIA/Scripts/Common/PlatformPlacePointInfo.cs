using UnityEngine;

namespace SCSIA
{
    public struct PlatformPlacePointInfo
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        public float minX;
        public float maxX;
        public float width;

        //############################################################################################
        // PUBLIC  METHODS
        //############################################################################################
        public PlatformPlacePointInfo(float minX, float maxX, float width)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.width = width;
        }

        public void Set(float minX, float maxX, float width)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.width = width;
        }

        public void Set(float minX, float maxX)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.width = maxX - minX;
        }
    }
}
