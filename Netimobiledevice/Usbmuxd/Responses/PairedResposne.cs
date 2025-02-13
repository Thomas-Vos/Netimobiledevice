﻿using System;

namespace Netimobiledevice.Usbmuxd.Responses
{
    internal readonly struct PairedResposne
    {
        public UsbmuxdHeader Header { get; }
        public int DeviceId { get; }

        public PairedResposne(UsbmuxdHeader header, byte[] data)
        {
            Header = header;
            DeviceId = BitConverter.ToInt32(data);
        }
    }
}
