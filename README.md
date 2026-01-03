# FreeLookFixNO

Fixes the Nuclear Option “Free Look” virtual joystick bug when Free Look is rebound to anything other than Right Mouse Button.

## What’s the bug?

Nuclear Option uses a mouse-driven on-screen **virtual joystick** for pitch/roll (and sometimes yaw on tarmac).  
When you press **Free Look**, the camera rotates using mouse movement.

Default behavior (Free Look bound to RMB): while Free Look is held, the virtual joystick **stops updating and stops centering** (“freezes”), so looking around does not inject extra control input.

Bug behavior (Free Look bound to any other key/button): Free Look still rotates the camera, but the virtual joystick **does not freeze** correctly. This can cause unintended roll/pitch while you are looking around.

## Why it happens

Free Look is handled through the game’s rebindable input system (Rewired), but the virtual joystick “freeze” check is hardcoded to **Right Mouse Button** instead of the actual **Free Look** binding.  
So rebinding Free Look doesn’t update the joystick-freeze behavior.

## What this mod changes

This mod makes the virtual joystick freeze follow the **Free Look** bind (whatever you set it to), matching the default RMB behavior.

## Installation

1. Install **BepInEx 5 (x64)** for Nuclear Option (if you don’t already have it). Link: https://github.com/BepInEx/BepInEx/releases
2. Copy `FreeLookFixNO.dll` into:
   - `Nuclear Option/BepInEx/plugins/`
3. Launch the game.

## How to use

Rebind **Free Look** to any key/button you want. The virtual joystick will now freeze while Free Look is held.

## Troubleshooting

- If nothing changes, confirm the DLL is in `BepInEx/plugins/`.
- Check `BepInEx/LogOutput.log` for plugin load errors.
