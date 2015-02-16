# Kinect v2 for Windows using Model-View-ViewModel and Reactive Extensions

![Screenshot](https://pbs.twimg.com/media/B9WwHtmIUAID5cU.jpg:medium)

View a demo video on YouTube [here](https://www.youtube.com/watch?v=1aqxnynKuqQ). Skip to 0:28 for said demo.

A C# Windows Store app using the Model-View-ViewModel design pattern that consumes data from the Kinect v2 sensor using Reactive Extensions (Rx).

The head vector scales depending upon the user's Z position, but this behaviour needs further tweaking.

I've separated my concerns to the best of my ability here. All of the Kinect data comes from a single Sensor class, and bitmap encoding only occurs in the view layer.
