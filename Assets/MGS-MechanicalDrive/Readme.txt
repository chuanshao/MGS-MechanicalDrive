==========================================================================
  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
  Name: MGS-MechanicalDrive
  Author: Mogoson   Version: 0.1.0   Date: 11/14/2017
==========================================================================
  [Summary]
    Unity plugin for binding mechanical drive in scene.
--------------------------------------------------------------------------
  [Demand]
    Binding Mesh Gear.
    Binding proportional velocity mechanism.
    Binding worm gear.
    Binding belt flywheel.
    Binding chain gear.
--------------------------------------------------------------------------
  [Environment]
    Unity 5.0 or above.
    .Net Framework 3.0 or above.
--------------------------------------------------------------------------
  [Achieve]
    Gear : Rotate around Z axis.

    Belt : Move texture UV on X axis.

    Chain : Move and rotate base on anchor curve.

    DynamicChain : Move and rotate base on dynamic anchor curve.

    RollerChain : Constitute of rollers and chain piece, Move and rotate
    base on anchor curve.

    DynamicRollerChain : Constitute of rollers and chain piece, Move and
    rotate base on dynamic anchor curve.

    LinearVibrator : Reciprocating motion on Z axis.

    CentrifugalVibrator : Eccentric motion around Z axis.

    Synchronizer : All mechanisms of the synchronizer driven by same
    velocity.

    Transmission : All mechanisms of the Transmission driven by
    proportional velocity.

    WormGear : Worm gear mechanism.

    Engine : Unified drive all mechanisms. 

    Damper : Simulate engine startup acceleration and stop deceleration.
--------------------------------------------------------------------------
  [Usage]
    Reference the prefabs and demos to binding mechanical drive in your
    project and use the components.
    
    Use the Anchor Editor to help you create the anchors of chain.
    Create a empty gameobject as chain root and attach the Chain
    component.
    Create a empty child gameobject of chain root as anchor root and
    set it to "Anchor Root" parameter of Chain component.
    Click the "Anchor Editor" button in Chain component Inspector to
    open the editor window and use it to create chain anchors.

    Use the Node Editor to help you create nodes of chain.
    Create a empty child gameobject of chain root as node root and
    set it to "Node Root" parameter of Chain component.
    Set your prefab of node to "Node Prefab" parameter of Chain.
    If you already create chain anchors, use the Node Editor in chain
    component Inspector to create chain nodes.
--------------------------------------------------------------------------
  [Suggest]
    The radius of gear should be set precisely.

    UV of belt model should be transverse arrangement, the texture of
    belt is preferably all sides continuous.

    Make sure the gear engages perfectly with the worm when building
    model.

    Create smooth track anchors as much as possible, the "count" and
    "space" of chain should be set reasonably.

    The amplitude radius of CentrifugalVibrator or LinearVibrator
    usually set a small value.
--------------------------------------------------------------------------
  [Demo]
    Prefabs in the path "MGS-MechanicalDrive/Prefabs" provide reference
    to you.

    Demos in the path "MGS-MechanicalDrive/Scenes" provide reference to
    you.
--------------------------------------------------------------------------
  [Resource]
    https://github.com/mogoson/MGS-MechanicalDrive.
--------------------------------------------------------------------------
  [Contact]
    If you have any questions, feel free to contact me at mogoson@qq.com.
--------------------------------------------------------------------------