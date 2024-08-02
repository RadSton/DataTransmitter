namespace io.radston12.datatransmitter
{
    using System;
    using System.Linq;
    using System.Timers;
    using System.Collections.Generic;

    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.Events.Handlers;

    using Events;

    using MEC;

    public class DataTransmitter : Plugin<Config>
    {
        private static DataTransmitter Singleton;
        public static DataTransmitter Instance => Singleton;
        private PlayerHandler playerHandler;
        public override PluginPriority Priority { get; } = PluginPriority.Last;


        public override void OnEnabled()
        {
            Singleton = this;
            PacketSender.setEndpoint(Config.Endpoint);
            PacketSender.send(new ServerAvailablePacket {});

            GameStateSender.start();

            playerHandler = new PlayerHandler();
            Exiled.Events.Handlers.Player.Verified += playerHandler.OnVerified;
            Exiled.Events.Handlers.Player.Left += playerHandler.OnLeft;
            Exiled.Events.Handlers.Server.RestartingRound += playerHandler.OnRoundEnded;
            Exiled.Events.Handlers.Server.RoundStarted += playerHandler.OnRoundStarted;

            base.OnEnabled();

        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Verified -= playerHandler.OnVerified;
            Exiled.Events.Handlers.Player.Left -= playerHandler.OnLeft;
            Exiled.Events.Handlers.Server.RestartingRound -= playerHandler.OnRoundEnded;
            Exiled.Events.Handlers.Server.RoundStarted -= playerHandler.OnRoundStarted;

            GameStateSender.stop();

            base.OnDisabled();
        }
    }
}