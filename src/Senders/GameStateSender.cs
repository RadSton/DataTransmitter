namespace io.radston12.datatransmitter
{

    using System;
    using System.Linq;
    using System.Timers;
    using System.Collections.Generic;

    using Exiled.API.Enums;
    using Exiled.API.Features;

    public class GameStateSender
    {

        private static bool IdleMode = false;
        private static Timer MainTimer = null;

        public static void start()
        {
            if (MainTimer == null)
                MainTimer = new System.Timers.Timer();

            MainTimer.Interval = DataTransmitter.Instance.Config.IntervalActive;
            MainTimer.Elapsed += onElapsed;

            MainTimer.Enabled = true;
        }

        public static void stop()
        {
            MainTimer.Enabled = false;
            MainTimer.Elapsed -= onElapsed;
        }

        public static void onElapsed(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (IdleMode != (Player.List.Count <= 0))
            {
                PacketSender.send(new IdleModePacket
                {
                    Active = !IdleMode,
                });
            }

            IdleMode = Player.List.Count <= 0;

            MainTimer.Interval = IdleMode ? DataTransmitter.Instance.Config.IntervalIdle : DataTransmitter.Instance.Config.IntervalActive;

            try
            {
                string[] playerList = new string[Player.List.Count];

                for (int i = 0; i < Player.List.Count; i++)
                    playerList[i] = Player.List.ElementAt(i).DisplayNickname;


                PacketSender.send(new InfoPacket
                {
                    PlayerCount = Player.List.Count,
                    Players = playerList,
                });
            }
            catch (System.Exception excep)
            {
                Log.Error(excep.Message);
            }
        }
    }
}