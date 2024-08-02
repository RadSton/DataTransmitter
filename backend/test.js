const DataTransmitServer = require("./DataTransmitServer");

DataTransmitServer.openServer(80);

DataTransmitServer.registerPacketListener("Info", (packet) => {
    console.log("INFO: " + packet.PlayerCount + " online ( " + packet.Players.join(" ") + " )");
});

DataTransmitServer.registerPacketListener("IdleMode", (packet) => {
    console.log("IDLE: " + packet.Active);
});

DataTransmitServer.registerPacketListener("Joined", (packet) => {
    console.log("JOIN: " + packet.PlayerName);
});

DataTransmitServer.registerPacketListener("Left", (packet) => {
    console.log("LEFT: " + packet.PlayerName);
});

DataTransmitServer.registerPacketListener("RoundRestart", (packet) => {
    console.log("----: RoundRestart");
});

DataTransmitServer.registerPacketListener("RoundStart", (packet) => {
    console.log("----: RoundStart");
});

DataTransmitServer.registerPacketListener("ServerAvailable", (packet) => {
    console.log("----: Server starting");
});
