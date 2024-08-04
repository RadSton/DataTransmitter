namespace io.radston12.datatransmitter
{
    public class InfoPacket
    {
        public string PacketName = "Info";
        public int PlayerCount { get; set; }
        public int MapSeed { get; set; }
        public string[] Players { get; set; }
    }

}