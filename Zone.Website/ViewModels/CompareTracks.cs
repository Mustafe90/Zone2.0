using System.Collections.Generic;

namespace Zone.ViewModels
{
    public class CompareTracks : IEqualityComparer<RecentlyPlayedTrackViewModel>
    {
        public bool Equals(RecentlyPlayedTrackViewModel x, RecentlyPlayedTrackViewModel y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            return x.Track == y.Track;
        }

        public int GetHashCode(RecentlyPlayedTrackViewModel obj)
        {
            return obj.Track.GetHashCode();
        }
    }
}