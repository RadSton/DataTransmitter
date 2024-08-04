namespace io.radston12.datatransmitter.Events
{
    using System.Collections.Generic;

    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.Events.EventArgs.Player;
    using Exiled.Events.EventArgs.Server;
    using Exiled.Events.EventArgs.Map;

    using io.radston12.datatransmitter;

    internal sealed class PlayerHandler
    {

        public void OnVerified(VerifiedEventArgs ev)
        {
            PacketSender.send(new JoinedPacket
            {
                PlayerName = ev.Player.DisplayNickname,
                PlayerId = ev.Player.UserId,
            });

            GameStateSender.onElapsed(null, null); // Leave IdleMode faster at round restart 
        }

        public void OnLeft(LeftEventArgs ev)
        {
            PacketSender.send(new LeftPacket
            {
                PlayerName = ev.Player.DisplayNickname,
                PlayerId = ev.Player.UserId,
            });

            GameStateSender.onElapsed(null, null); // Activate IdleMode faster at round restart 
        }

        public void OnRoundEnded()
        {
            PacketSender.send(new RoundRestartPacket { });
        }

        public void OnRoundStarted()
        {
            PacketSender.send(new RoundStartPacket { });
        }

        public void OnMapGenerated()
        {
            PacketSender.send(new MapGeneratedPacket
            {
                Seed = Exiled.API.Features.Map.Seed,
            });
        }

    }
}
