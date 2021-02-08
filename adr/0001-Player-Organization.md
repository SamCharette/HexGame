# Player Organization

## Status

Approved

## Context

The original project on which this one is based was intended to be two independant programs, the players, speaking with a third, the game/referee.  The decision is whether to go the same route, as this is an attempt to recreate the original to some capacity, or to choose a simpler in-process organization to focus more on the game and AI, than on the networking between agents.

## Decision

Players will be in-process and will not be expected to communicate with outside programs.

## Consequences

If/when a player is needed to be out of process, from another application for instance, a player wil need to be created whose sole purpose is to communicate in that way.