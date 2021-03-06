﻿namespace AkkaOverview.Streaming.ActorSystem.Models
{
    public class Candle
    {
        public string Time { get; set; }
        public string OpenMid { get; set; }
        public string HighMid { get; set; }
        public string LowMid { get; set; }
        public string CloseMid { get; set; }
        public string Volume { get; set; }
        public string Complete { get; set; }

        public static explicit operator Candle(Providers.Models.Candle candle)
        {
            return new Candle() { CloseMid = candle.CloseMid, Complete = candle.Complete, HighMid = candle.HighMid, LowMid = candle.LowMid, OpenMid = candle.OpenMid, Time = candle.Time, Volume = candle.Volume };

        }

        public override string ToString()
        {
            return this.ToObjectString();
        }
    }
}