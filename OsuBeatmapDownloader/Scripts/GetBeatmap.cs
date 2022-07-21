using System;
using System.Collections.Generic;

namespace OsuBeatmapDownloader.Scripts
{
    /// <summary>
    /// Get the Beatmap Informations
    /// </summary>
    public class GetBeatmap
    {
        public int SetID { get; set; }
        public int RankedStatus { get; set; }
        public string ApprovedDate { get; set; }
        public string LastUpdate { get; set; }
        public string LastChecked { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public string Source { get; set; }
        public string Tags { get; set; }
        public bool HasVideo { get; set; }
        public int Genre { get; set; }
        public int Language { get; set; }
        public int Favourites { get; set; }
        public int Disabled { get; set; }



        public int BeatmapId { get; set; }
        public int ParentSetId { get; set; }
        public string DiffName { get; set; }
        public string FileMD5 { get; set; }
        public int Mode { get; set; }
        public int BPM { get; set; }
        public float AR { get; set; }
        public float OD { get; set; }
        public float CS { get; set; }
        public float HP { get; set; }
        public int TotalLength { get; set; }
        public int HitLength { get; set; }
        public int Playcount { get; set; }
        public int Passcount { get; set; }
        public int MaxCombo { get; set; }
        public double DifficultyRating { get; set; }
        public string OsuFile { get; set; }
        public string DownloadPath { get; set; }
    }
}
