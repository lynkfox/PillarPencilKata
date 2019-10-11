## Write
 * Append to end of current document
 
## Tip degrade
 * Whitespace = 0
 * lowercase letter = 1
 * uppercase letter = 2
 
 * if tipDurability=0 only white letters after
 
 * from email correspondance
  You are doing the right thing to ask for questions to clarify the behavior of the product. Now I will be a typical client and say, "Do what makes the most sense to you, just make sure there are tests that cover the cases and make it clear to the other artisans how the library behaves."
 
## Eraser Degrade
 * upper and lower case = 1

 
## Sharpen
 * return to tipDurabiliy to whatever it started with.
 * Reduce pencil length by 1
 
## Erase
 * Remove instance of string that matches at the END of the line
 * replace with same amount of whitespace
 * ~remember white space before and after!!!!~ - ended up doing this differently than I thought I would
 * Remember the # of space it was bfore
 
## Edit
 * ~add to FIRST white space left by erasing (from the begining, left to right like reading)~
 * - Change - Edit Last In First Out white spaces
 * - If no edits, add to end.
 * If too long, write into the next word!
 * replace overwritten characters of next word with @
 * don't forget the white space between words!
