# SuperCube3

A hyper-casual endless runner built in Unity — a revised, cleaned-up version of an earlier game (SuperGlitchCube), with 3-lane dodge/shoot mechanics, procedural level generation, and mobile touch controls.

## What It Does

- **Lane-based runner mechanics** — player moves between left/middle/right positions, shoots projectiles, and reacts to a slam mechanic with fall detection.
- **Procedural level generation** — platforms, portals, and enemy spawns are generated rather than hand-placed, so levels extend indefinitely.
- **Mobile-first controls** — a dedicated mobile controller alongside standard input, built for touch play.
- **Full game loop** — health/damage, scoring (point system), UI management, and level-transition triggers tie the mechanics into a complete playable game rather than a mechanics demo.
- **Cinematic camera** — Cinemachine-driven camera work and post-processing for game feel beyond default Unity camera behavior.

## Tech Stack

Unity, C#, Cinemachine (camera systems), DOTween (animation/tweening).

## Why This Project

Game dev roots: state management (health, scoring, level progression), real-time systems (collision/physics-driven mechanics), and procedural content generation — the same underlying skills (state machines, event-driven logic, systems that must hold up under real-time interaction) that carry directly into building responsive software and agent systems today.
