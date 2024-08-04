# DataTransmitter EXILED Plugin

A SCP: SL Exiled Plugin that sends basic gameplay info to a endpoint using a POST request

Currently the Plugin supports:

- Info: Sends current players in an defined interval
- Joined: When a player joins
- Left: When a player leaves
- RoundRestart: When the roundrestart is triggered
- RoundStart: When the round starts
- ServerAvailable: When the server starts loading 
- MapGenerated: When the map is finished generating 
- (IdleMode: Tells server if the client is in idle or active)

But it is designed with updates in mind so adding another is not that hard:

- Register an Exiled Event for example: Exiled.Events.Handlers.Player.Died
- Create a simple packet:
```cs
public class DeathPacket
{
    public string PacketName = "Death";
    public string PlayerName { get; set; }
}
``` 
- In the exiled event handler simply invoke:
```cs
PacketSender.send(new DeathPacket
{
    PlayerName = ev.Player.DisplayNickname,
});
```
- In the backend part of this project you now just need register a callback for it:
```js
DataTransmitServer.registerPacketListener("Death", (packet) => {
  console.log("DEATH: " + packet.PlayerName);
})
```