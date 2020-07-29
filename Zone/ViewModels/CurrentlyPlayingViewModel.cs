namespace Zone.ViewModels
{
    public class CurrentlyPlayingViewModel
    {
        public long Timestamp { get; set; }

        public bool IsPlaying { get; set; }

        public TrackViewModel Track { get; set; }

        public ContextViewModel ContextDataModel { get; set; }

    }
}
