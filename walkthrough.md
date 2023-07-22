# unity 2d tutorial

## creating a new project

in Unity Hub, click the blue button that says `New Project`

select `2D` and give the project a name. i called mine `cosmos2023-2D`.

make sure the Location is the folder that you want it to show up in

> if you move the project folder, you’ll have to re-open the project from its new folder.

click the blue button that says `Create Project`

## adding a player

when your project opens, you should have a scene called `SampleScene` that has a single GameObject in the Hierarchy window: `Main Camera`

> for now, we want a basic shape we can move around to represent our player. let’s grab a square

GameObject > 2D Object > Sprite > Square
or
in the Hierarchy window [right click] > 2D Object > Sprite > Square

call this square whatever you want! i called my square `Player`

> excellent! we’ve got a player sprite in the game world. let’s add a Character Controller to it and use a script to make it move!

## Character Controller

while selecting Player in the Hierarchy window, in the Inspector window find the `Add Component` button. click it.

look for a component called `Character Controller` (you can search by beginning to type the name of the component). click it. 

while selecting Player in the Hierarchy window, in the Inspector window find the `Add Component` button. click it.

> we want to add a script to this Player object! it doesn’t exist yet, so we are going to make one!

type the name of a script to attach to your Player object. i chose `PlayerScript`. you should see an option to `Create New Script`. click it. click `Create and Add`.

### cleaning up your Assets folder

Assets > Create > Folder
or
in the Project window @ bottom of screen, navigate to the Assets folder and [right click] > Create > Folder

call this new folder `Scripts`

move your `PlayerScript.cs` file into the `Scripts` folder

### Character Controller (continued)


by default, `PlayerScript.cs` will look like this: 
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
```

> we are going to want to do a couple things here. first, we want to grab the Character Controller component from the Player GameObject. we can do this two ways. the first way is in the editor and the second way is through our script. either way, we need to add the following line to our code.

`public CharacterController controller;`

after the class declaration, but before the `Start()` method is defined, add a public variable of type CharacterController and call it something descriptive. i called mine `controller`

> this is going to be the box that stores our CharacterController component! it’s public so that we can see it in the editor.

### grabbing the Character Controller in the editor

save your script and let Unity reload. 

you should be able to see a field for the CharacterController variable when you select the `Player` object in the Hierarchy and look at the PlayerScript component in the Inspector. 

you should also be able to see the Character Controller component in the Inspector. 

drag the Character Controller component into the `controller` variable field. 

> this tells Unity that the variable `controller` will look for the Character Controller component that you just dragged into the field!

### grabbing the Character Controller in a script

in the `Start()` method, add this line of code:

`controller = this.GetComponent<CharacterController>();`

> this sets the controller variable equal to the Character Controller component that is on the `Player` object! there will be an error if you don’t have a Character Controller component. 

### moving with the Character Controller

> we are going to write two lines of code in the `Update()` function that will move our player left and right. 

`Vector3 move = new Vector3(10.0f * Input.GetAxis("Horizontal"), 0, 0);`

> this makes a new Vector3 and sets it equal to a vector that is 0 y, 0 z, and how much we want to move in the x axis (horizontal movement on the screen, left and right). the `10 * Input.GetAxis("Horizontal”)` part just reads player input (which will be between -1 and 1) and multiplies it by 10, which is our speed for right now. 

`controller.Move(move * Time.deltaTime);` 

> this uses the `Move()` function built into the Character Controller in Unity and passes it the direction we want to move in (the Vector3 `move`) times the amount of time since the last frame. this makes our game run smoothly regardless of frame rate.

save your script and test it out!

> for reference, your `PlayerController.cs` script should look like this:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(10.0f * Input.GetAxis("Horizontal"), 0, 0);

        controller.Move(move * Time.deltaTime);
    }
}
```

## cleaning up the code and adding comments

> let’s pull that hard coded `10.0f` from the declaration and assignment of `Vector3 move` and put it in a variable!

below the `CharacterController controller` declaration, add a new line that reads:

`public float speed = 10.0f;`

> this just makes it easier to change how fast our player is moving (and by making it public we can edit it from the Inspector!)

> i’ll also comment my code so that it explains what i’ve done 

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // variables go here

    // controller is going to be the box where we put the Character Controller
    //      component on our Player object 
    public CharacterController controller;
    // how fast our player will move! default to 10.0f, will change as needed
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        // grab the Character Controller component on the GameObject this script
        //      is attached to! set controller equal to it!
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // move in the x direction (left/right) based on player input from keyboard
        //      (or controller!)
        Vector3 move = new Vector3(speed * Input.GetAxis("Horizontal"), 0, 0);
        // use the Character Controller Move() function and move in the direction we
        //      want to move times how long it's been since the last frame was called
        //      (so that speed isn't tied to framerate, it's tied to time)
        controller.Move(move * Time.deltaTime);
    }
}

```

## adding some gravity

> we can move left and right, but what about up and down? the first thing we’ll attempt is adding gravity to the player object.

> gravity is just a downward force applied to an object (physicists don’t @ me) so let’s do exactly that!

below the speed variable declaration, let’s type: 

`public float gravity = -9.0f;`

> the gravity is going to be negative because it is pulling everything down in the y direction

> now let’s add a gravity implementation to the `Update()`

after moving the controller, let’s `Move()` the Player’s Character Controller down by `gravity * Time.deltaTime`:

`controller.Move(new Vector3(0, gravity, 0) * Time.deltaTime);`

> all right let’s see how this looks in action! let’s save our script and head back to the Unity Editor. it should compile with no errors and we should be able to hit the play button and see gravity in effect!

> uh oh! the player just falls forever! what a nightmare! (ᵒʰ ᵍᵒᵈˢ, ⁿᵒᵗ ᵗʰᵉ ᴺᶦᵍʰᵗ ᴹᵃʳᵉ…) 

> let’s add a ground for the player to bump up against!

## adding some ~~gravity~~ ground

> first things first, we need a Thing to be the ground. remember how we made the Player? let’s do that and make a GameObject called Ground

GameObject > 2D Object > Sprite > Square
or
in the Hierarchy window [right click] > 2D Object > Sprite > Square

> now we are going to *mess up* this Square

> in the Inspector, let’s edit the y Position to be `-2` and the x Scale to be `10`

> we should have a long block underneath the player square

> if we save the scene and press the play button now, we still fall. in fact, we fall *through* the Ground. we haven’t told Unity that this Ground object is solid ground.

> … oh gods

> so there’s a slight problem,,,

> CharacterController just Doesn’t Work for 2D games,,, 

> i’m gonna scream

> okay `*`deep breath`*`

> let’s fix this :triumph:

> instead of a CharacterController, we are going to add a RigidBody2D to the Player GameObject

remove the CharacterController component from the Player and delete the code in `PlayerScript.cs`

> right now our script looks like this:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // variables go here

    
    // how fast our player will move! default to 10.0f, will change as needed
    public float speed = 10.0f;
    // gravity pulling the player down
    public float gravity = -9.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
```

## adding a Rigidbody2D to Player

while selecting Player in the Hierarchy window, in the Inspector window find the `Add Component` button. click it.

look for a component called `Rigid Body 2D` (you can search by beginning to type the name of the component). click it.

> we are going to want to do a couple things here. first, we want to grab the Rigid Body 2D component from the Player GameObject. we need to add the following line to our code.

`public Rigidbody2D rigidbody;`

place this after the class declaration, but before the `Start()` method is defined

> this is going to be the box that stores our Rigidbody2D component! it’s public so that we can see it in the editor.

in the `Start()` method, add this line of code:

`rigidbody = this.GetComponent<Rigidbody2D>();`

> this sets the controller variable equal to the Rigid Body 2D component that is on the `Player` object! there will be an error if you don’t have a Rigid Body 2D component. 

> now that we have a Rigidbody2D, we don't need gravity! Unity already has that programmed in! let's delete the `gravity` varaible declaration. our script now looks like this:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // variables go here

    // friendship ended with charactercontroller
    // rigidbody2d is my new best friend
    public Rigidbody2D rigidbody;
    // how fast our player will move! default to 10.0f, will change as needed
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
```

> let's make the character move left and right again

> actually, first let's walk through how i figured out CharacterController wouldn't work for this 2D game

## finding answers to problems

> first, let's look at the [Unity Scripting Reference](https://docs.unity3d.com/ScriptReference/CharacterController.html) for the Character Controller and the [Unity User Manual](https://docs.unity3d.com/Manual/class-CharacterController.html) page for the Character Controller

> the user manual says `The Character Controller is mainly used for third-person or first-person player control that does not make use of Rigidbody physics.`

> the scripting reference shows that the CharacterController class has a property called `isGrounded`

> if we check to see if the player is grounded with `Debug.Log(controller.isGrounded);` we would not see a message in the console

> between that and the manual saying Character Controller is used for first/third person 3D games, we're pretty sure that we are going to have to use a Rigidbody2D for movement

## Rigidbody2D movement

> we have attached a Rigidbody2D to the player, now let's make it move!

> let's pull up the [Unity User Manual](https://docs.unity3d.com/Manual/class-Rigidbody2D.html) page for Rigidbody2D and the [Unity Scripting Reference](https://docs.unity3d.com/ScriptReference/Rigidbody2D.html) page for Rigidbody2D

> this is going to work a little differently from the Character Controller; there is no `Move()`` function and instead it looks like we have the option to `AddForce()`, which takes as parameters two arguments: one is a `Vector2` and one is a `ForceMode2D`

> let's look at what `ForceMode2D` means by reading the [Unity Scripting Reference](https://docs.unity3d.com/ScriptReference/ForceMode2D.html) page for it!

> this page says that we `Use this to apply a certain type of force to a 2D RigidBody. There are two types of forces to apply: Force mode and Impulse Mode.`

> we know that we want to make the `Vector2` equal to `(Input.GetAxis("Horizontal") * speed, 0) * Time.deltaTime`, just like when we were messing around with the Character Controller. let's write a line in `Update()` that declares a new `Vector2` and sets it equal to that!

`Vector2 forceToAdd = new Vector2(Input.GetAxis("Horizontal"), 0.0f) * Time.deltaTime;`

> and then let's `Debug.Log()` that `Vector2`

`Debug.Log(forceToAdd);`

> if we run this, it *should* print out `(-1.0, 0.0)` when we press `a` or `left arrow`

> does it do this?

> for me, it's hard to tell because i'm getting a new `Debug.Log()` statement every frame and it's cluttering up my console

> let's put that `Debug.Log()` in an `if()` check

```
if(forceToAdd.x != 0.0f)
{
    Debug.Log(forceToAdd);
}
```

> i'm getting `(0.0, 0.0)` in the console when i press the `a` and `d` keys. that's weird. 

> let's change the line of code where we multiple by `Time.deltaTime` and just leave it as:

`Vector2 forceToAdd = new Vector2(Input.GetAxis("Horizontal"), 0.0f);`

> now i am getting non-zero numbers. weird, but i'll take that as a win (~~beause i really need a win right now~~)

> we can remove the `if()` and `Debug.Log()` code for now!

> let's try to add that `Vector2` as a force to the `Rigidbody2D`

`rigidbody.AddForce(forceToAdd, ForceMode2D.Impulse);`

> i don't remember the difference between an impulse and a force in physics right now, so we are gonna mess around and see what feels best in engine! first up, i chose `ForceMode2D.Impulse`

> when i play this, that box goes fast! we haven't even put our speed in the `forceToAdd` and it's rocketing around!

> let's try `ForceMode2D.Force` real quick

change the `AddForce()` line to `rigidbody.AddForce(forceToAdd, ForceMode2D.Force);`

> that moves much slower! which is what we want right now!

> real quick, let's add the speed in

make the `forceToAdd` declaration `Vector2 forceToAdd = new Vector2(Input.GetAxis("Horizontal") * speed, 0.0f);`

right now our code looks like this

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // variables go here

    // friendship ended with charactercontroller
    // rigidbody2d is my new best friend
    public Rigidbody2D rigidbody;
    // how fast our player will move! default to 10.0f, will change as needed
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 forceToAdd = new Vector2(Input.GetAxis("Horizontal") * speed, 0.0f);
        
        rigidbody.AddForce(forceToAdd, ForceMode2D.Force);
    }
}
```

> it is rocketing around, but that just means that `speed` is too high! let's set `speed` to `2.0f` and check that

> it works!

## adding ground with Rigidbody2D

> now we want to make sure that Ground and Player interact like a ground and a player are supposed to! 

> we are going to add two components to the Ground GameObject: a Rigidbody 2D and a Box Collider 2D

while selecting Ground in the Hierarchy window, in the Inspector window find the `Add Component` button. click it.

look for a component called `Rigidbody 2D` (you can search by beginning to type the name of the component). click it. 

while selecting Ground in the Hierarchy window, in the Inspector window find the `Add Component` button. click it.

look for a component called `Box Collider 2D` (you can search by beginning to type the name of the component). click it. 

> now let's see if we can mess around with some collision physics!

while selecting Ground in the Hierarchy window, in the Inspector window find the `Rigidbody 2D` component. locate the `Body Type` property and select `Static` from the dropdown

> what does `Static` mean? let's go to the [Unity User Manual](https://docs.unity3d.com/Manual/rigidbody2D-body-types.html) page for Rigidbody 2D Body Types

> we can read that we want to `Use the Static Body Type to design your Rigidbody 2D to not move under simulation.` this sounds like the perfect Body Type for our Ground!

> but if we test our game right now, the Player still falls through the Ground. let's write some code to detect the ground!

> the Rigidbody 2D has a method called `IsTouching()` that takes a `Collider2D` as an argument

> let's give the player a public `Collider2D` field and grab the Ground's Collider 2D! this will go right under our `speed` declaration

`public Collider2D ground;`

> now let's grab that in the code.

> one small problem: we are in a script attached to the Player. we don't know anything about Ground because we have no way to reference it! 

> let's look up ways to find specific GameObjeects

> it looks like one way we can do this is with tags

> here's the [Unity Scripting Reference](https://docs.unity3d.com/ScriptReference/GameObject.FindWithTag.html) page for the `GameObject.FindWithTag()` method

> in our `Start()` we could write the following line:

`ground = GameObject.FindWithTag("ground");`

> one small problem: this will get us a GameObject, not the Collider2D component of that GameObject

> let's throw in a `.GetComponent<Collider2D>()` to that line of code. we should end up with this:

`ground = GameObject.FindWithTag("ground").GetComponent<Collider2D>();`

> now let's give Ground the tag "ground"

## tagging an object

while selecting Ground in the Hierarchy window, in the Inspector window find the `Tag` field at the top of the Inspector window. click it.

select `Add tag..` from the dorpdown.

click the `+` button and add a tag named `ground`

while selecting Ground in the Hierarchy window, in the Inspector window find the `Tag` field at the top of the Inspector window. click it.

select `ground` from the list of tags

> this has added the `ground` tag to our Ground GameObject, so when we `FindWithTag("ground")` this object will be identifiable!

> right now, our code looks like this:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // variables go here

    // friendship ended with charactercontroller
    // rigidbody2d is my new best friend
    public Rigidbody2D rigidbody;
    // how fast our player will move! default to 10.0f, will change as needed
    public float speed = 2.0f;
    // for finding the Ground
    public Collider2D ground;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        ground = GameObject.FindWithTag("ground").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 forceToAdd = new Vector2(Input.GetAxis("Horizontal") * speed, 0.0f);
        
        rigidbody.AddForce(forceToAdd, ForceMode2D.Force);
    }
}
```

## `IsTouching()` time babey

> now we can check to see if Player `IsTouching()` `ground` with the following line of code:

`if(rigidbody.IsTouching(ground)) {}`

in that `if()` check, let's `Debug.Log("ground!");`

> it doesn't print to the console? what the heck?

> let's put a Rigidbody2D on the Player object

while selecting Player in the Hierarchy window, in the Inspector window find the `Add Component` button. click it.

look for a component called `Box Collider 2D` (you can search by beginning to type the name of the component). click it.

> let's test this!

> it should work! they collide!

## adding a jump

> let's read if the player has pressed the space bar!

> we're going to use `Input.GetButton()` for this, so let's look at the [Unity Scripting Reference](https://docs.unity3d.com/ScriptReference/Input.GetButton.html) page.

> we're also going to look at our input settings

navigate to Edit > Project Settings... and look for Input Manager in the Project Settings window click on `Jump` to expand it. 

> the positive button for jump should be `space`

> now let's see if we can detect input from the spacebar

> in our `Update()` function, let's put a `Debug.Log()` that looks to see if the `Jump` button (the spacebar) has been pressed

`Debug.Log(Input.GetButton("Jump"));`

> now let's test this

> it prints `False` to the console until we press the spacebar, when it printws `True`!

> so let's add a jump with an `if()` statement!

```
if(Input.GetButton("Jump"))
{
    rigidbody.AddForce(new Vector2(0.0f, 1.0f), ForceMode2D.Force);
}
```

> this adds a force of `1.0f` in the y direction (up and down) to the rigidbody!!

> let's test this!

> ... nothing too big seems to be happening

> when i look at the Inspector, the Transform component's X Position property seems to be changing ever so slightly. weird. 

> real quick, let's try to make that Force an Impulse

`rigidbody.AddForce(new Vector2(0.0f, 1.0f), ForceMode2D.Impulse);`

> WOW!

> it just shoots straight up in the air! like, really high!

> that seems to be working a little bit, so let's make the Impulse force *reallllllllly* small, like `0.1f`

`rigidbody.AddForce(new Vector2(0.0f, 0.1f), ForceMode2D.Impulse);`

> that was really small, but then i held down the spacebar and it just shot in the air

> let's go back to `ForceMode2D.Force` and see if bumping the number up from `1.0f` to `10.0f` does anything

`rigidbody.AddForce(new Vector2(0.0f, 10.0f), ForceMode2D.Force);`

> okay, that works too. 

> the problem seems to be that i can hold down the spaecbar and just fly away lol

> let's change the `if()` condition to include `&& rigidbody.IsTouching(ground)`

> we jump a teeny tiny amount and then we can't jump much higher! this is closer to what we want!

> let's make that jump REAL BIG, like `50.0f`,,, no `100.0f`!!

> jesus that was really high up. bring it down to like,,, `30.0f`

> there's also another weird thing that's happening if you mess around in this scene a lot,,, the square rotates???

> let's stop that. 

## locking sprite rotation

> in the [Unity Scripting Manual](https://docs.unity3d.com/ScriptReference/Rigidbody2D-rotation.html) page for Rigidbody2D, we can look at (and set) the rotation! 

> let's stop it from rotating by just setting the rotation to `0.0f` every frame

`rigidbody.rotation = 0.0f;`

> that should(?) fix that!

> here's another small problem, the player can fall off screen and the camera doesn't follow them! let's fix that

## parenting the Player to the Main Camera

in the Hierarchy, drag the `Main Camera` GameObject on top of the `Player` GameObject

> uh oh, our rotation issue isn't totally fixed

> at this point, you might want to cry. i know i do.

## take a cry break

> sob.

## compose ourselves

> try your best to look like you didn't cry. 

## variable clean up

> after some tweaking, our code looks like this:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // variables go here

    // friendship ended with charactercontroller
    // rigidbody2d is my new best friend
    public Rigidbody2D rigidbody;
    // for finding the Ground
    public Collider2D ground;
    // how fast our player will move!
    public float speed = 2.0f;
    // how high our player will jump!
    public float jump = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        ground = GameObject.FindWithTag("ground").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 forceToAdd = new Vector2(Input.GetAxis("Horizontal") * speed, 0.0f);

        rigidbody.AddForce(forceToAdd, ForceMode2D.Force);

        if(Input.GetButton("Jump") && rigidbody.IsTouching(ground))
        {
            rigidbody.AddForce(new Vector2(0.0f, jump), ForceMode2D.Force);
        }

        rigidbody.rotation = 0.0f;
    }
}
```

## actually freezing the z rotation

while the Player is selected in the Hierarchy, go in the Inspector and look at the Rigidbody 2D. click on `Constraints`. check the `Freeze Rotation: Z` checkbox.

> incredible. it was that easy

## take another cry break

> sob harder.

## compose ourselves (again)

> try your best to look like you didn't cry, do one thing, and then cry again.

## go crazy with level design

> add as many platforms in as many weird configurations as you want! just make sure they are all tagged with ground

> wait, we only have one ground variable. hmmm

GameObject > Create Empty
or
in the Hierarchy window [right click] > Create Empty

call it something memorable. i called mine `Also Ground`. give it the `ground` tag.

> i did some digging in the [Unity Scripting Reference](https://docs.unity3d.com/ScriptReference/Collider2D.OnCollisionEnter2D.html) and found this `Collider2D` function called `OnCollisionEnter2D()`

> we're going to make a `bool` variable called `isGrounded`

`public bool isGrounded = false;`

> then, let's add the following functions:

```
void OnCollisionEnter2D(Collision2D collision)
{
    if(collision.gameObject.tag == "ground")
    {
        isGrounded = true;
    }
}

void OnCollisionExit2D(Collision2D collision)
{
    if(collision.gameObject.tag == "ground")
    {
        isGrounded = false;
    }
}
```

> this just sets the `isGrounded` variable to true when we are touching an object tagged ground and false when we are not!

> now let's add a second platform and just throw it into the `Also Ground` object!

GameObject > 2D Object > Sprite > Square
or
in the Hierarchy window [right click] > 2D Object > Sprite > Square

name it something memorable. i named mine `Also Also Ground`

drag it on top of the `Also Ground` GameObject

give it the `ground` tag

while selecting Ground in the Hierarchy window, in the Inspector window find the `Add Component` button. click it.

look for a component called `Box Collider 2D` (you can search by beginning to type the name of the component). click it. 

set `Also Also Ground`'s X Position and Y Position to be somehwere you can theoretically jump to

> let's test!

[to be continued...]
