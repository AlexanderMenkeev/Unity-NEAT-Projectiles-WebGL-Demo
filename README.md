![Alt text](demo.gif?raw=true "Title")

# Unity-Particles
Particles controlled by a compositional pattern-producing networks (CPPN).

## How to start
1. Clone this repository and open it using Unity Hub. Unity will download all necessary packages;
2. Open SampleScene and press play.
   
Preferable version of Unity is 2022.3

## How does it work? 
+ Each weapon contains a single evolved CPPN;
+ Every frame, each particle issued from the weapon inputs its current position relative to position of its spawn point $(p_x, p_y)$ and distance from the spawn point $(d_c)$ into the CPPN;
+ The CPPN is activated and outputs the particle’s velocity $(v_x, v_y)$ and color $(r, g, b)$ for that frame.

## Evolution of CPPNs
You can choose **two** parent CPPNs and create new generation:
+ 2 offspring networks which were created via asexual reproduction
+ 6 offspring networks which were created via sexual reproduction

To choose network as parent click on weapon gameobject (white triangle) and set property **IsEvaluated = true**

## Pictures of patterns

![Alt text](screenshots/191633.png?raw=true "Title")

![Alt text](screenshots/191546.png?raw=true "Title")

![Alt text](screenshots/191501.png?raw=true "Title")

![Alt text](screenshots/191309.png?raw=true "Title")
