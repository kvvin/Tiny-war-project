- The scripts folder has multiple scripts.

- "BlueUnit" and "RedUnit" scripts take the properties from the parent class "Unit" and deals with the movement and other necessary functions.
	- The movement is done using the AiPath script taken from the A* pathfinder project.

- There is also a script called "UnitMovementTest" which is a script that I tried making from scratch to use instead of the AiPath but as 
  it was not optimised and had multiple bugs I decided not to use it.

- The project has a few bugs such as when multiple targets are spawned the Units that already have a target set do not reset when the target 
  are destroyed hence they stay in place and keep attacking.