# Smart __ASCII Art Generator__ in C#

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

After starting the application user needs to **click on the *'Browse'* button** and an *'Open file dialog'* will pop up. 

Then, after **choosing the desired input image**, miniature version of it will appear in the top right corner.

Right now user can either **leave settings on default** and simply **press the *'Generate'* button** or **set the settings** to better the result.

Availabe setting options are:

- *'Luminosity Range Maximum'* - Determins the maximum luminosity value above which every value will be translated to whitespace. Also shifts the translation values of the other ASCII characters to the appropriate range. (Set lower for images with lots of darker areas)

- *'Luminosity Range Minimum'* - Determins the minimum luminosity value below which every value will be translated to '@' and also shifts the translation values of the other ASCII characters to the appropriate range. (Set higher for images with not many darker areas)

- *'Edge Threshold'* - Determins the limit of edge intensity needed to be surpassed for the ASCII "edge characters" (such as '/', '\\', '_') to be used in the final ASCII Art.

- *'ASCII Art Width'* - Determins the width (in number of characters) of the final ASCII Art.

After every tuning of the settings the *'Generate'* button needs to be pressed for the effect to take place.

**Finally**, if the user is satisfied with the result, she or he can **click on the *'Copy to Clipboard'* button** to copy the ASCII Art to clipboard.








