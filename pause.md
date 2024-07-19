# writing a pause button script

the first thing that comes to mind when setting up a pause button is to have the game track whether or not the game is paused with a boolean value, some sort of toggle. then my pseudocode for `Update()` would look like:

```
update:
    if paused:
        pause game
        wait for user input to unpause
    else
        read input as normal
```

i probably want to attach the pause behavior to the `esc` button, so i'm going to look up the way Unity references that button in the [Scripting API](https://docs.unity3d.com/ScriptReference/). i'm fairly certain Unity can check if the key that has been pressed is a specific key by using a `KeyCode`, which brings me to [this page](https://docs.unity3d.com/ScriptReference/KeyCode.html). from there, i can find the `esc` key's KeyCode ([link](https://docs.unity3d.com/ScriptReference/KeyCode.Escape.html)). 
