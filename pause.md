# writing a pause button script

## pseudocode

the first thing that comes to mind when setting up a pause button is to have the game track whether or not the game is paused with a boolean value, some sort of toggle. then my pseudocode for `Update()` would look like:

```
update:
    if paused:
        pause game
        wait for user input to unpause
    else
        read input as normal
```
## the `esc` key

i probably want to attach the pause behavior to the `esc` button, so i'm going to look up the way Unity references that button in the [Scripting API](https://docs.unity3d.com/ScriptReference/). i'm fairly certain Unity can check if the key that has been pressed is a specific key by using a `KeyCode`, which brings me to [this page](https://docs.unity3d.com/ScriptReference/KeyCode.html). from there, i can find the `esc` key's KeyCode ([link](https://docs.unity3d.com/ScriptReference/KeyCode.Escape.html)). 

## the pause toggle

for now, i'm going to put the pause f*nctionality in the player controller script. first things first, i'll need to add a boolean variable to track the pause state. i'm a genius, so i will call this variable `paused`. i can declare this variable and initialize it to `false` at the start of the script (after the `public class PlayerScript : MonoBehavior {` line).

`public bool paused = false;`

then, i'll flip the toggle in `Update()` whenever the `esc` key is pressed.

```
void Update() {
    if(Input.GetKeyDown(KeyCode.Escape)) {
        paused = !paused;
    }
}
```

now to do something with the pause screen,,,
for now, i want to throw a piece of text on the canvas that just says "paused" because that seems simple and fine. 

## the pause screen

i need to make a new UI element in the hierarchy on the `Canvas`. i'll right click and select `UI > Text - TextMashPro`. then in the inspector, i'll make the text element say "paused." 
now i want to grab the `Canvas` element with the text, but i don't want to write a bunch of code to do that. so instead, i'll just make a GameObject variable to hold the pause screen text and drag & drop it from the Inspector. underneath my declaration of the `paused` variable, i'll write this:

`public GameObject pauseScreen;`

then, i'll save the script and open the Unity Editor. after selecting the `Player` in the Hierarchy, i'll scroll through the Inspector window until i see the `PlayerScript` component. this should now have a field called `pauseScreen` waiting for me to drag & drop the `Pause Text` element from the hierarchy into its field.

in `Start()` i want to turn off the pause screen. i can do that with this line:

`pauseScreen.SetActive(false);`

now i just need to toggle the `paused` variable and set the `pauseScreen` to active if `paused` is true and inactive if `paused` is false. this happens if ***and only if*** the `esc` key has been pressed. 

```
void Update() {
    if(Input.GetKeyDown(KeyCode.Escape)) {
        paused = !paused;
        pauseScreen.SetActive(paused);
    }
}
```

## the full(ish) code

```
public class PlayerScript : MonoBehaviour {
    public bool paused = false;
    public GameObject pauseScreen;

    void Start() {
        pauseScreen.SetActive(false);
    }

    void Update() {
        if(!paused) {
            // player movement and stuff
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
            pauseScreen.SetActive(paused);
        }
    }
}
```
