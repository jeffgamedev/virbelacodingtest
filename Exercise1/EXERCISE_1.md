# Exercise 1 #

In this exercise you'll configure a Unity scene and write scripts to create an interactive experience. As you progress through the steps, feel free to add comments to the code about *why* you choose to do things a certain way. Add comments if you felt like there's a better, but more time intensive way to implement specific functionality. It's OK to be more verbose in your comments than typical, to give us a better idea of your thoughts when writing the code.

## What you need ##

* Unity 2020 (latest, or whatever you have already)
* IDE of your choice
* Git

## Instructions ##

This test is broken into multiple phases. You can implement one phase at a time or all phases at once, whatever you find to be best for you.

### Phase 1 ###

**Project setup**:

 1. Create a new Unity project inside this directory, put "Virbela" and your name in the project name.
 1. Configure the scene:
     1. Add a central object named "Player"
     1. Add 5 objects named "Item", randomly distributed around the central object
 1. Add two C# scripts named "Player" and "Item" to you project
     1. Attach the scripts to the objects in the scene according to their name, Item script goes on Item objects, Player script goes on Player object.
     1. You may use these scripts or ignore them when pursuing the Functional Goals, the choice is yours. You're free to add any additional scripts you require to meet the functional goals.

**Functional Goal 1**:

When the game is running, make the Item closest to Player turn red. One and only one Item is red at a time. Ensure that when Player is moved around in the scene manually (by dragging the object in the scene view), the closest Item is always red.

### Phase 2 ###

**Project modification**:

 1. Add 5 objects randomly distributed around the central object with the name "Bot"
 1. Add a C# script named "Bot" to your project.
 1. Attach the "Bot" script to the 5 new objects.
     1. Again, you may use this script or ignore it when pursing the Functional Goals.

**Functional Goal 2**:

When the game is running, make the Bot closest to the Player turn blue. One and only one object (Item or Bot) has its color changed at a time. Ensure that when Player is moved around in the scene manually (by dragging the object in the scene view), the closest Item is red or the closest Bot is blue.

### Phase 3 ###

**Functional Goal 3**:

Ensure the scripts can handle any number of Items and Bots.

**Functional Goal 4**:

Allow the designer to choose the base color and highlight color for Items/Bots at edit time.

## Questions ##

 1. How can your implementation be optimized?
 2. How much time did you spend on your implementation?
 3. What was most challenging for you?
 4. What else would you add to this exercise?

## Optional ##

* Add Unit Tests
* Add XML docs
* Optimize finding nearest
* Add new Items/Bots automatically on key press
* Read/write Item/Bot/Player state to a file and restore on launch

## Next Steps ##

* Confirm you've addressed the functional goals
* Answer the questions above by adding them to this file
* Commit and push the entire repository, with your completed project, back into a repository host of your choice (bitbucket, github, gitlab, etc.)
* Share your project URL with your Virbela contact (Recruiter or Hiring Manager)

## If you have questions ##

* Reach out to your Virbela contact (Recruiter or Hiring Manager)


## Answered Questions ##

1. In the current implementation, all objects are being checked for distance. This may be overwhelming if too many objects exist.
    A physics check (I was thinking Physics.OverlaySphere should do fine) may be utilized to take advantage of detecting only the closest objects and checking only those. I did setup LayerMasks for this case but did not end up implementing it.

2. For the core functionality described in the readme, it took me just over an hour. Then I decided to have a lot of fun, and make this
    a playground. I spent about 2.5 hours adding all the fun stuff such as running around, music, sound effects, models, animations, and spawning objects.

3. Honestly, getting the IDE to intellisense (using VS-Code) on my workstation. However after just getting the latest Unty version it worked fine.

4. Getting the object highlighting to work while in edit-mode (using [ExecuteInEditMode] on top of the class). I actually thought this was a requirement for some time until re-reading the requirements, and this added a fair amount of difficulty! The "Ensure that when Player is moved around in the scene manually..." is what made me think this was a requirement at first. I did get it working, but it was a little messy and I chose to remove it.
