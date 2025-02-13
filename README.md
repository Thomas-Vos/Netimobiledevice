# Netimobiledevice

Netimobiledevice is a .Net Core implementation for working with all iOS devices (iPhone, iPad, iPod) as well the plists that they use.

- [Netimobiledevice](#Netimobiledevice)
    * [Features](#Features)
    * [Installation](#Installation)
    * [Usage](#Usage)
    * [Services](#Services)
    * [License](#License)
    * [Contributing](#Contributing)
    * [Acknowledgments](#Acknowledgments)

## Features

 - Device discovery and connection via Usbmux.
 - Interact with iOS services
 - Handle all Plists whether they are in XML or Binary format

## Installation

To install Netimobiledevice, you can use the following command in the Package Manager Console:

```powershell
Install-Package Netimobiledevice
```

Alternatively, you can use the .NET CLI:

```csharp
dotnet add package Netimobiledevice
```

# Usage

A few examples of how to use Netimobiledevice are below.

Get a list of all currently connected devices using:

```csharp
using Netimobiledevice.Usbmuxd;

List<UsbmuxdDevice> devices = Usbmux.GetDeviceList();
Console.WriteLine($"There's {devices.Count} devices connected");
foreach (UsbmuxdDevice device in devices) {
    Console.WriteLine($"Device found: {device.DeviceId} - {device.Serial}");
}
```

Listen to connection events:

```csharp
Usbmux.Subscribe(SubscriptionCallback);

private static void SubscriptionCallback(UsbmuxdDevice device, UsbmuxdConnectionEventType connectionEvent)
{
    Console.WriteLine("NewCallbackExecuted");
    Console.WriteLine($"Connection event: {connectionEvent}");
    Console.WriteLine($"Device: {device.DeviceId} - {device.Serial}");
}
```

Get the app icon displayed on the home screen as a PNG:

```csharp
LockdownClient lockdown = LockdownClient.CreateLockdownClient("60653a518d33eb53b3ca2212cd1f44e162a42069");
SpringBoardServicesService springBoard = new SpringBoardServicesService(lockdown);
PropertyNode png = springBoard.GetIconPNGData("net.whatsapp.WhatsApp");
```

## Services

The list of all the services from lockdownd which have been implemented and the functions available for each one. Clicking on the service name will take you to it's implementation, to learn more about it.

- [com.apple.mobile.diagnostics_relay](https://github.com/artehe/Netimobiledevice/blob/main/Netimobiledevice/Lockdown/Services/DiagnosticsService.cs)
  * Query MobileGestalt & IORegistry keys.
  * Reboot, shutdown or put the device in sleep mode.
- [com.apple.mobile.installation_proxy](https://github.com/artehe/Netimobiledevice/blob/main/Netimobiledevice/Lockdown/Services/OsTraceService.cs)
  * Browse installed applications
  * Manage applications (install/uninstall/update)
- [com.apple.mobile.notification_proxy](https://github.com/artehe/Netimobiledevice/blob/main/Netimobiledevice/Lockdown/Services/NotificationProxyService.cs) & [com.apple.mobile.insecure_notification_proxy](https://github.com/artehe/Netimobiledevice/blob/main/Netimobiledevice/Lockdown/Services/NotificationProxyService.cs)
  * API wrapper for NotifyPost() & NotifyRegisterCallback()
- [com.apple.mobilebackup2](https://github.com/artehe/Netimobiledevice/blob/main/Netimobiledevice/Lockdown/Services/Mobilebackup2Service.cs)
  * Backup management
- [com.apple.os_trace_relay](https://github.com/artehe/Netimobiledevice/blob/main/Netimobiledevice/Lockdown/Services/InstallationProxyService.cs)
  * Get pid list
- [com.apple.springboardservices](https://github.com/artehe/Netimobiledevice/blob/main/Netimobiledevice/Lockdown/Services/SpringBoardServicesService.cs)
  * Get icons from the installed apps on the device.
- [com.apple.syslog_relay](https://github.com/artehe/Netimobiledevice/blob/main/Netimobiledevice/Lockdown/Services/SyslogService.cs)
  * Streams the raw syslog lines from the device.

## License

This project is licensed under the [MIT LICENSE](https://github.com/artehe/Netimobiledevice/blob/main/LICENSE).

## Contributing

Contributions are welcome. Please submit a pull request or create an issue to discuss your proposed changes.

## Acknowledgments

This library was based on the following repositories with either some refactoring or in the case of libraries such as libusbmuxd translating from C to C#.

- **[BitConverter](https://github.com/davidrea-MS/BitConverter):** Provides a big-endian and little-endian BitConverter that convert base data types to an array of bytes, and an array of bytes to base data types, regardless of machine architecture.
- **[libusbmuxd](https://github.com/libimobiledevice/libusbmuxd):** A client library for applications to handle usbmux protocol connections with iOS devices.
- **[PList-Net](https://github.com/PList-Net/PList-Net):** .Net Library for working with Apple *.plist Files.
- **[pymobiledevice3](https://github.com/doronz88/pymobiledevice3):** A pure python3 implementation to work with iOS devices.
