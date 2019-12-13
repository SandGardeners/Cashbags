//name is of course Tutorial Timothy the Third

=== TUTORIAL_GUEST ===
#CHECK_IN
#PHONECALL
->DONE

= CHECK_IN
{ 
	-  checkInStep == 3:
		->bye
	-  checkInStep == 2 :
		->giveKey
	-  checkInStep == 1 :
		->giveMoney
	-  checkInStep == 0 :
		->writeName
	- else :
		->DONE
}

- (writeName) 
#ghuest
Hello Cashbags, my name is Tutorial Timothy the Third, Eduardo the Hacker sent me to make sure you're all up to date with the current system. Would you like me to walk you through how to check in a guest?

* I guess so.

#ghuest
Remember Cashbags, guests will register how polite you're being... so maybe be a little bit nicer than that in the future. The first step is to write my name in the guest book, so click the book boyyyyy!
->DONE

* Not right now, Cashbags needs no tutorials!!!

#ghuest
Oh, I see, alright, good luck!
->STOP_EVENT

- (giveMoney)
#ghuest
Now you will ask me for money and I will put it on this little exchange zone up here. Then, you take it, and drag it to the money drawer.

CASH!

#ghuest
You know it, Cashbags.

->DONE
- (giveKey)
#ghuest
Great, now open the key drawer, and drag a key up to the exchange zone.

Keys are like, little windows into other lives.

#ghuest
Now is not the time for pseudo-intellectual rubbish, Cashbags.
->DONE
- (bye)
#ghuest
And that's all it takes! Knew you could do it, champ.

Of course, I am Cashbags!

#ghuest
Yes, you certainly are. Good luck! I'll see you soon.

->STOP_EVENT

=PHONECALL

#ghuest
Hello, Cashbags? This is Tutorial Timothy the Third, can you hear me?

Yes, I can hear you Tutorial Timothy the Third.

#ghuest
Great, well, as you have picked up the phone you have demonstrated that you know how to use the phone, which is great news.

*Tutorial Timothy the Third, I am a highly trained concierge.

#ghuest
Eduardo the Hacker just wanted me to make sure. And of course, you know that guests you have checked in can call at any time if they have any questions or complaints. It would be best to make sure they have their calls answered... politely...

No problem.

#ghuest
Great! Laters boyo.

->HANG_UP

*I don't have time for this.

#ghuest
Sure, no problem, no need to shout.
->HANG_UP
