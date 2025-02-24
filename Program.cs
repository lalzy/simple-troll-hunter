// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;

/*
step 1: ny level (as in dungeon level), med miniboss (goblin Champion).
step 2: rest-mechanic (30% hp regen, +1 shield hp hvis du fortsat har shield).
step 3: litt mer items for litt mer strategisk variasjon.
        > Fakkel - kan ikke bli surprised. Kan bruke (1 gang, mister etter) for å sette fyr på fiende.
            > Damage over time? Stun?
step 4: ny level (as in dungeon level) med miniboss (Goblin Shaman?)
step 5: character creation (Choose starting items?)


Dungeon:
    rooms 10 - array?

enemies (diffuclty) populated every X?

*/



void Start(){
    bool programRunning = true;
    while(programRunning){
        programRunning = Game.MainLoop();
    }
}

Start();