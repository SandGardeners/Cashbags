=== QUESTION_GUEST ===
#CHECK_IN
#PHONECALL
->DONE

=QUESTION

Then ask away, child!

#ghuest
Is it true that everyone that stays at Brownie Cove Hotel is entitled to unlimited free antidepressants?

That is absolutely true, my child. Here at Brownie Cove Hotel we are committed to making sure everyone is relaxed and feeling their best.

#ghuest
Great! May I have a room then please?

Of course! Let me just check you in to an available room.
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
How may I help you?
#ghuest
Are you Cashbags Michael?


+Why yes, that is me!
~NICENESS+=2
#ghuest
Excellent, I have a question.
->QUESTION
+That's what it says on the nameplate HAHA!
~NICENESS+=1
#ghuest
Ah, good, I'm glad I found you, I have a question.
->QUESTION
+That's me, why do you ask?
~NICENESS-=1
#ghuest
Oh, nothing suspicious, I just had a question.
->QUESTION
+Of course that is me, open your eyes!
~NICENESS-=2
#ghuest
Gosh, I'm sorry I didn't mean to cause offence.

Then next time, think before you speak! I'm busy, come back later.
->STOP_EVENT

- (giveMoney)
How long would you like to stay?

#ghuest
Would it be possible to stay forever?

Absolutely, in fact, many of our guests go for this option. I'll require a deposit of 1000 tokens if that is ok, and then every month we will ask for more money until you die. Is that ok?

#ghuest
No problem.
->DONE
- (giveKey)
And... here is your key. I hope you will find your room to be everything you hope.

#ghuest
Thank you.
->DONE
- (bye)
If you need anything else just let me know.
->STOP_EVENT



=PHONECALL

#ghuest
Hello, Mr Michael?

Yes, hello?

#ghuest
It's me, from earlier. You helped me check in.

Ah yes, I remember, our latest Forever Ghuest. Is there anything I can help you with?

#ghuest
Oh no, no, nothing I need help with. I just wanted to say, thank you for everything, Mr Michael. I really needed this in my life. This hotel came into my life at just the right time.

*That's great![] Do you think you will want to check out soon?

    #ghuest
    I... I don't think that will be necessary, thank you. I... ummmm, just yeah, wanted to say thank you.
    
    No problem at all, my child. Is that all?
    
*What do you mean?

    #ghuest
    You know when your life feels like it is missing something, there's a big hole that can't be filled... but then suddenly it is? It feels almost like fate... But yes, thank you.
    
    No problem at all, my child, I understand. Is that all?
    
 -
  #ghuest
    Yes, I think so. Goodbye, Mr Michael.

->HANG_UP