# README #

NTP Project

v1.05 - added self-made sprites of the player (it's still temporary I think) and animations
v1.0 - edited QuestionMaster & QuestionPoint scripts, randomizing answers in questions, list of wrong answered questions (eg. for next level), all the enemies & all the questionpoints placed on the map
v0.985 - fixed canvas position in question points (now attached to camera) and other minor fixes
v0.98 - added PointsIndicator & Mark UI, fixed problem with invoking Start() multiple times (QuestionPoints don't inherit from QuestionMaster any more), now questions are truly random, I hope...
v0.97 - edited Player class (fields for won and lost points), PlayerInfoUI with Points text and new script PlayerCounter, fixed nasty problem with double invoking Check() func when button clicked
v0.95 - new object holding all question points, added QuestionMaster script (reading json & loading it to the struct), QuestionPoint script edited (inherit from QM; displaying data & checking answers)
v0.91 - fixed answering questions (text color changes if it was true but not checked)
v0.9 - added temporary .json file, edited QuestionPoint scr - reading data from json, load questions & checking answers with it (TODO: color is changing accordingly to true/false from file, not using player input)
v0.8 - edited QuestionPoint script - pausing game, checking answers (ilustrative changing color, uninteractable toggles & changed button text), destroing point
v0.7 - slightly edited player&enemy stats subclass, status indicator (health bar) above player&enemy, edited method Flip() from PlatformCharacter2D (health bar is not getting reversed)
v0.6 - added example of QuestionUI and script for turning it on when collision
v0.5 - added enemy srcipt, killenemy method (not used yet), enemy hurts player on collision enter
v0.4 - player respawning after delay at the respawn point, camera searches for player at intervals
v0.31 - non-fixed camera clamping y position done
v0.3 - clamping camera on y (TODO: non-fixed camera y position, x restricition), added Player script & GameMaster script, killing Player when he falls down
v0.25 - added surfaces, player can jump only on them (earlier he could jump on the edges of grounds); added temporary "enemy" walking from left to right, added temporary "question point"
v0.21 - added all temporal environment for Level1
v0.2 - added temporary environment, point of camera chenged
v0.1 - import 2D Sample Assets, added temporary sample player to the scene, added sample Camera2DFollow
v0.0 - clean Unity3D project