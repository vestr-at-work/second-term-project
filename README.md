# Smart ASCII Art Generator in C#

Generating __ASCII Art__ with the help of **edge detection** as a second term university project.

**ASCII Art** are pictures made only of ASCII characters (basicaly text characters). See more on wiki: https://en.wikipedia.org/wiki/ASCII_art

For the edge detection and determining of the edge angle Sobel operator (https://en.wikipedia.org/wiki/Sobel_operator) is used.  
In order to mitigate false detections of edges the input image is (before detection of the edges) blured by a small Gaussian blur (https://en.wikipedia.org/wiki/Gaussian_blur).

The whole project is written in C# and uses Windows Forms for the UI.

----------------------------------------------------------------










