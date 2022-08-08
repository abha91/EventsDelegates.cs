This piece of code shows implementation of events & delegates in c#. 
Before going through code, please go through this doc to understand the basic terminologies used in this implemntation.

Delegates - Simply a container for a function that can be used as a variable. 

Events - Allows you to specify a delegate that gets called when some event in your code is triggered.

Overview
--------

Objects that want to listen for a specific event can do so by registering as a listener, and when that event is trigged, all the listeners will have their listening function directly called. As many listeners can be registered to each delegate as you want and each listener can call its own different function from the same event call. 

This means if say you have a GUI, another unit and a Controller that are all listening for the same event, each one can react differently. The GUI may need to update itself, the other unit maybe check distance and the Controller may do something else. 

Given below are the steps performed in the implemntation:-

1. Define delegates and events
2. Trigger events 
3. Subscribe objects to listen to Events
