=== POLITE_GUEST ===
#CHECK_IN
#PHONECALL

->DONE

=CHECK_IN
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
Hello there, one of my friends recommended this hotel to me, they said the service here was excellent. 

{
- NICENESS < 2:
#ghuest
But honestly, it doesn't seem to be that way. Everyone I have passed seems to be unhappy, as if they have not been treated well. And well frankly, I don't think I want to stay here anymore... Good day!
->STOP_EVENT

-else:
#ghuest
I have no reason to doubt their recommendation, it seems like a lovely place!
}

I'd like a room please.

+Ah wonderful, I'm glad your friend enjoyed their stay.[] Let me just sign you in quickly.
~NICENESS+=2

+Ah good good, no problem.[] One second.
~NICENESS+=1

+Well of course you were recommended![] Our hotel is fantastic!
~NICENESS-=1
    #ghuest
    Ah... uh... yes!

+Can't you see I'm busy?[] It'll have to be quick.
~NICENESS-=5
    #ghuest
    Well I never, I certainly wasn't expecting that kind of welcome. My friend must have been mistaken. I won't be staying here after all. Good day.
->STOP_EVENT

-->DONE

- (giveMoney)

How long would you like to stay?

#ghuest
Maybe just three nights?

Ok, sure. So... 3 nights in one of our {~basic rooms|luxury suites|extreme expensive resort rooms|basic rooms with a spa pass|basic rooms, no breakfast included|luxury suites, with golf course access} comes to a total of {~4000 Tokens|5050 Tokens|3 Tokens|600 Tokens|36 Tokens}.

->DONE
- (giveKey)

Thank you. And let me just grab your key.

->DONE
- (bye)
There we go, you're all set.

#ghuest
Great, I'm really looking forward to staying here, thank you so much.
->STOP_EVENT


= PHONECALL

#ghuest
Hello, it's me again! You checked me in earlier. I just wanted to say I'm having a lovely time!

*That's wonderful to hear!
~NICENESS+=1
    #ghuest
    Your service really is excellent here. Just like my friend recommended me, I will you recommend to all my friends! Expect lots more customers, Mr Michael!
    
    Aha oh great!

*I don't really have time for this.
~NICENESS-=5
    #ghuest
    But my friend told me how great the service was here! I think I'm going to leave, and tell all of my friends how rude you are. Damn you, Cashbags!

-->HANG_UP
->DONE