// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;


/// <summary>
/// The program loop.
/// </summary>
void Start(){
    bool programRunning = true;
    while(programRunning){
        programRunning = Game.MainLoop();
    }
}

/// calls to start.
Start();