﻿using Netimobiledevice.Plist;
using System;
using System.Net.Sockets;

namespace Netimobiledevice.Usbmuxd
{
    /// <summary>
    /// Usbmuxd Device information.
    /// </summary>
    public class UsbmuxdDevice
    {
        public UsbmuxdConnectionType ConnectionType { get; private set; }
        public long DeviceId { get; private set; }
        public string Serial { get; private set; }

        public UsbmuxdDevice(IntegerNode deviceId, DictionaryNode propertiesDict)
        {
            DeviceId = deviceId.Value;
            Serial = propertiesDict["SerialNumber"].AsStringNode().Value;

            string connectionTypeString = propertiesDict["ConnectionType"].AsStringNode().Value;
            if (connectionTypeString == "USB") {
                ConnectionType = UsbmuxdConnectionType.Usb;
            }
            else if (connectionTypeString == "Network") {
                ConnectionType = UsbmuxdConnectionType.Network;
            }
            else {
                throw new NotImplementedException($"Unknown connection type: {connectionTypeString}");
            }
        }

        public UsbmuxdDevice(int deviceId, string serialNumber, UsbmuxdConnectionType connectionType)
        {
            DeviceId = deviceId;
            Serial = serialNumber;
            ConnectionType = connectionType;
        }

        public Socket Connect(ushort port)
        {
            var muxConnection = UsbmuxConnection.Create();
            try {
                return muxConnection.Connect(this, port);
            }
            catch {
                muxConnection.Close();
                throw;
            }
        }
    }
}
