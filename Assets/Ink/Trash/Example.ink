/*//Hello my friend
//I've been thinking about the way we would handle characters and events and stuff in a way you could produce lot of them
//And I figured we'd do a separate Ink file for each character
//With different possible kinds of events

//Suggestion : Characters could be sorted by like "tiers", could be tiers of complexity, lots of dialogues to handle etc...
//Suggestion : Different characters could come up depending on the state of some global variables that would be influenced by all guests

//List of tags to indicate which knots are present in the file
//if there's one of those tags, it's mandatory to have a stitch named exactly the same.
//Suggestion : the tags order could have an importance, like they'd play one after one, as you could do some "levels", like in "check in" step or the others you'd be mean and it would have an influence on "check out" step for example



=== CHARACTER_SAM_MACHELL ===
#CHECK_IN
#PHONECALL
#GET_MAIL
#CHECK_OUT
->DONE

//I'm sorry I couldn't really do shorter then that.
//You'd have to copy/paste it for each character CHECK_IN I guess...
//The check in steps are examples we'd have to define those
//The template for other events like "check out" could be kinda similar
//You can check the result and run some tests in the game by clicking the book. You have to imagine there's a ghuest and each time you click the book you played a step (gave keys, wrote name, etc.)
= CHECK_IN
{ 
	- bye :
		->DONE
	- giveKey :
		->bye
	- giveMoney :
		->giveKey
	- writeName :
		->giveMoney
	- else:
		->writeName
}

- (writeName) 
Hello my name is Sam and I'd like a room
* [Ok dude] 
->DONE
- (giveMoney)
Step 2
->DONE
- (giveKey)
Step 3
->DONE
- (bye)
Step 4
->DONE

= PHONECALL

->DONE

= CHECK_OUT
->DONE

= GET_MAIL
->DONE
*/