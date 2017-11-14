# MGS-MechanicalDrive
- [中文手册](./README_ZH.md)

## Summary
- Unity plugin for binding mechanical drive in scene.

## Demand
- Binding Mesh Gear.
- Binding proportional velocity mechanism.
- Binding worm gear.
- Binding belt flywheel.
- Binding chain gear.

## Environment
- Unity 5.0 or above.
- .Net Framework 3.0 or above.

## Achieve
- Gear : Rotate around Z axis.
- Belt : Move texture UV on X axis.
- Chain : Move and rotate base on anchor curve.
- DynamicChain : Move and rotate base on dynamic anchor curve.
- RollerChain : Constitute of rollers and chain piece, Move and rotate base on anchor curve.
- DynamicRollerChain : Constitute of rollers and chain piece, Move and rotate base on dynamic anchor curve.
- LinearVibrator : Reciprocating motion on Z axis.
- CentrifugalVibrator : Eccentric motion around Z axis.
- Synchronizer : All mechanisms of the synchronizer driven by same velocity.
- Transmission : All mechanisms of the Transmission driven by proportional velocity.
- WormGear : Worm gear mechanism.
- Engine : Unified drive all mechanisms. 
- Damper : Simulate engine startup acceleration and stop deceleration.

## Demo
- Prefabs in the path "MGS-MechanicalDrive/Prefabs" provide reference to you.
- Demos in the path "MGS-MechanicalDrive/Scenes" provide reference to you.

## Preview
- MeshGears

![MeshGears](./Attachments/README_Image/MeshGears.gif)

- WormGear

![WormGear](./Attachments/README_Image/WormGear.gif)

- Belt

![Belt](./Attachments/README_Image/Belt.gif)

- Vibrosieve

![Vibrosieve](./Attachments/README_Image/Vibrosieve.gif)

- DynamicChainSys

![DynamicChainSys](./Attachments/README_Image/DynamicChainSys.gif)

- DynamicRollerChainSys

![DynamicRollerChainSys](./Attachments/README_Image/DynamicRollerChainSys.gif)

- NodeEditor

![NodeEditor](./Attachments/README_Image/NodeEditor.gif)

## Contact
- If you have any questions, feel free to contact me at mogoson@qq.com.