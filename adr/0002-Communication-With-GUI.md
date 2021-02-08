# Communication with GUIs 

## Status

Approved

## Context

The engine can either be tied directly into the interfaces, or it can use messaging to relay information to connected parties.

## Decision

We will use MediatR for in-process communication between the game and outside agents.  This will allow for the potential of multiple clients receiving information at the same time.  A client can be a GUI, or other process, such as persistence.

## Consequences

It will still be in-process, so exception handling will still need to be addressed.  Any modules running concurrently, such as GUI or persistence, will need to be compiled into the application.  A future ADR may address this by changing to a self-running engine process, but currently we will stay with in-process.