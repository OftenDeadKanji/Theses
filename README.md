# Physically based lighting models

<p align="justify">
The purpose of this thesis is to <b>compare</b> four lighting models – three physically based and one empirical. I used the Unreal Engine 4 for this purpose.
The implementation consisted of modifications to the engine’s source code.
The main difference between the individual models was the use of different functions that are part of the bidirectional reflectance distribution function – BRDF.
The lighting models were compared in terms of achieved quality and performance.
A comparison of the generated images was made using the structure similarity index measure – SSIM.
The final analysis was based on the results of the metric and the average times between frames from the performance test.
</p>

## Scenes

Scene for quality test - Cornell box:

<p align="center">
<img src="https://user-images.githubusercontent.com/32665400/190833361-bf34021c-3966-43c5-b9ee-cec290bfbc18.png">
</p>

Scene for performance test - over 11k spheres:

<p align="center">
<img src="https://user-images.githubusercontent.com/32665400/190833497-d03d1113-7fd5-4d98-b230-d81127d51b77.png">
</p>

## Results

Results of quality test:
- the first image shows results when I changed <i>Metallic</i> parameter from 0.0 to 1.0,
- the second image shows results when I changed <i>Roughness</i> parameter from 0.0 to 1.0,
- it's all compared to Unreal's Default Lit
- used lighting models:
  - blue - my first proporsal of physically based lighting model (I used already existing functions for BRDF)
  - orange - my second proporsal of physically based lighting model (I used already existing functions for BRDF)
  - gray - Blinn-Phong

<p align="center">
<img width=75% src="https://user-images.githubusercontent.com/32665400/190833544-4ee03aa6-3f3a-4f3c-b067-1329396870a5.png">
<img width=75% src="https://user-images.githubusercontent.com/32665400/190833870-ebee8436-3d80-48d7-94be-f40ffa17cd9d.png">
</p>

Results of performance test shows the average time between frames and its standard deviation.  
Lighting models:
- first - DefaultLit
- second - my first proporsal of physically based lighting model
- third - my second proporsal of physically based lighting model
- fourth - Blinn-Phong



<p align="center">
<img width=60% src="https://user-images.githubusercontent.com/32665400/190833896-d487b276-30e4-4901-bbe5-e5f05f79d6c4.png">
</p>
