namespace io.radston12.datatransmitter
{
    using System.ComponentModel;
    using System.IO;

    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    public sealed class Config : IConfig
    {

        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; }

        [Description("URL to post to")]
        public string Endpoint { get; set; } = "http://localhost/";

        [Description("Active Interval for Info packet")]
        public int IntervalActive { get; set; } = 30 * 1000;

        [Description("Idle Interval for Info packet")]
        public int IntervalIdle { get; set; } = 1 * 60 * 1000;
    }
}