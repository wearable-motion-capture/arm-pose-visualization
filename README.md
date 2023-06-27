# arm-pose-visualization

The repository provides code to set up and run Unity simulations to view real-time arm movement. This is the last piece of the arm pose estimation and visualization pipeline. 

Using the raw data from smart devices, the `arm-pose-estimation` block processes the data and estimates the arm pose. The repository also sends the necessary data, required for the visualization. 

Associated repositories:
* [sensor-stream-apps](https://github.com/wearable-motion-capture/sensor-stream-apps) provides the apps to stream sensor readings from wearable devices to a remote machine.
* [arm-pose-estimation](https://github.com/wearable-motion-capture/arm-pose-estimation.git) provides the processing of streamed data into arm posture estimations.


This figure summarizes how the three repositories interact. The UDP streaming requires a local WiFi connection.
![Modules Image](https://github.com/wearable-motion-capture/.github-private/blob/main/profile/modules.png)

## Install and Use

Open the project in Unity and run the appropriate script from `arm-pose-estimation` repository. 

In Unity, click the play button at the top to run the scene. 

Please read
the [step-by-step instructions](https://docs.google.com/document/d/1ayMBF9kDCB9rlcrqR0sPumJhIVJgOF-SENTdoE4a6DI/edit?usp=sharing) for a detailed instruction guide.
