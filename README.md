# "Smart" __ASCII Art Generator__ in C#

Generating __ASCII Art__ with the help of **edge detection** as a second term university project.

**ASCII Art** are pictures made only of ASCII characters (basicaly text characters). See more on wiki: https://en.wikipedia.org/wiki/ASCII_art

For the edge detection and determining of the edge angle **Sobel operator** (https://en.wikipedia.org/wiki/Sobel_operator) is used.  
In order to mitigate false detections of edges the input image is (before detection of the edges) blured by a small **Gaussian blur** (https://en.wikipedia.org/wiki/Gaussian_blur).

The whole project is written in C# and uses Windows Forms for the UI.

----------------------------------------------------------------

## **User manual:**

### *UI Rundown*
__User interface__ of the application is __simple__. It consists of **three buttons** (*'Browse'*, *'Generate'* and *'Copy to Clipboard'*), **four trackbars** (*'Luminosity Range Maximum'*, *'Luminosity Range Minimum'*, *'Edge Threshold'* and *'ASCII Art Width'*) and a small **preview of the input image** on the right side. Rest of the right half and the whole left half is filled with a scrollable text box for the ASCII Art itself.

![screenshot of the UI](https://user-images.githubusercontent.com/32305565/187505065-8dd0922c-22c5-4679-8d0f-0daac7f97d0d.png)

### *Using the App*

After starting the application user needs to click on the *'Browse'* button and an *'Open file dialog'* will pop up. 









