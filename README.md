# Cities: Skylines – Asset Importer Texture Loader Fix

## Overview
This mod provides a patch for the `AssetImporterTextureLoader` and related classes in Cities: Skylines.  
After the Race Day DLC update, a mathematical error in the way texture arrays were matched to texture slots/channels began causing an `ArrayOutOfRangeException`, which completely breaks the in‑game Asset Editor.

This mod corrects that issue by fixing the indexing logic so that texture channels are matched safely and consistently. At least until Tantalus pushes their own fix through. If ever.

## What This Fix Does
- Prevents `ArrayOutOfRangeException` during asset import  
- Restores functionality of the Asset Editor  
- Corrects the internal texture‑slot mapping logic  
- Does **not** modify or replace any game assets  
- Does **not** collect or transmit any data  

This is a lightweight patch intended solely to restore broken vanilla functionality.

## Why This Repository Exists  
This repository provides full transparency while protecting the author's rights through the GPL‑3.0 license.

## Known Bug & Limitations
Currently, this fix works flawlessly on some asset types, and not on others. And, strangely, only during night/day cycles. Building assets assign all maps and textures correctly. Prop assets, however, will either have no illumination during the day, or 100% illumination for the whole model at night. If this one bug can be sorted out, then I think we can call this project done.

## How It Works
The fix adjusts the array indexing logic used when the importer assigns textures to channels.  
The original code incorrectly assumed matching array lengths, causing out‑of‑range access when the Race Day patch changed texture handling.

This patch:
- Validates array bounds  
- Ensures safe indexing  
- Applies corrected mapping logic  

## Building
1. Open the project in Visual Studio or Rider  
2. Reference the Cities: Skylines assemblies from your installation directory. Install the CitiesHarmonyAPI nuget to include the necessary Harmony dependencies. Make sure to turn off 'Local Copy' to 'False' so you aren't trying to compile and push copies of these files with the rebuilt .dll. You may break things.  
3. Build the project in Release mode  
4. Place the resulting DLL in your `Cities_Skylines/Addons/Mods/AssetImporterFIX` folder along with the CitiesHarmonyApi.dll file, as per the Harmony 2 repo. If you have any questions about Harmony 2, refer to bofomer's github repo which this mod is built using the Harmony 2 mod example he has there. 

## License
This project is licensed under the **GNU GPL‑3.0**.  
You are free to inspect, modify, and redistribute the code **as long as derivative works remain open‑source and properly attributed**.

See the `LICENSE` file for details.

## Contact
If you encounter issues or want to contribute improvements, feel free to open an issue or pull request.
