# Gif to PGN Converter

This program converts GIF files of chess games into PGN notation.

Simply click load and it will analyze your game! If the board is flipped (i.e. black is on bottom), all you have to do is click the button labeled "click for black on bottom".

Make sure the board is as square as possible and you crop out anything bordering the board. For now, only works on games that start on turn 1(i.e. the gif starts on whites first move, not an initialized board state or a title screen). This will be fixed soon

If your gif leaves a different color on the square that had a piece move from it, get the RGB or Hexcidecimal values of those colors, click the check box, and enter the values. You can click the button labeled "RGB" to switch to Hexidecimal input. This will possibly be automated in the future, but may not be logistically feasible with how the code currently works.

The size field is not the resolution of your image, but the resolution at which your image will be analyzed. You likely won't have to change this.

If your pgn is not complete, it means for some reason the progam had an issue reading the game. If you send me the gif and the pgn at m3aga1nthelper@gmail.com, I can try to update the code to account for whatever issues pop up.
