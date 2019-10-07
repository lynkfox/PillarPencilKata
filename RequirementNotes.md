## Write
 * Append to end of current document
 
## Tip degrade
 * Whitespace = 0
 * lowercase letter = 1
 * uppercase letter = 2
 
 * if tipDurability=0 only white letters after
 
## Errser Degrade
 * upper and lower case = 1

 
## Sharpen
 * return to tipDurabiliyt to whatever it started with.
 * Reduce pencil length by 1
 
## Erase
 * Remove instane of string that matches at the END of the line
 * replace with same amount of whitespace
 * remember white space before and after!!!!
 * Remember the # of space it was bfore
 
## Edit
 * add to FIRST white space left by erasing (from the begining, left to right like reading)
 * If too long, write into the next word!
 * replace overwritten characters of next word with @
 * don't forget the white space between words!