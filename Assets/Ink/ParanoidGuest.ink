=== PARANOID_GUEST ===

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
Ummm... hi, hello. I... can I ask you a favour? I think I saw my doppleganger. I think they're following me. Do you think you could hide me here? Like if he comes here, send him another way?

+Of course[!], protecting our guests is one of core values here at Brownie Cove Hotel. 
~NICENESS+=2
~safe = true
	#ghuest
	Thank you so much.
->DONE

+I'm afraid not[.], we treat all guests fairly, and to help you would be to hinder them.
~NICENESS-=2
	#ghuest
	Oh... ummm... I see, could you, just check me in anyway?

->DONE

- (giveMoney)

So... that'll be 10000000 tokens.

#ghuest
Oh jeez! Ummm... ok, here you go.

->DONE

- (giveKey)

Here is your key.

#ghuest
Thank you so much!

->DONE

- (bye)

I hope you enjoy your stay with us.

{
- safe == true:

And good luck, I too know the pain of a malicious doppleganger.

- else:

->STOP_EVENT
}

->STOP_EVENT



= PHONECALL

{ - denounced == true :
	#ghuest
	Cashbags are you there? It's me from earlier. My doppleganger is here!
	{
	
		- safe == true:
			#ghuest
			I can't believe you betrayed me! I hope that whatever happens to me falls heavy on your conscience... you are an evil man.
			->HANG_UP
		- else :
			#ghuest
			I mean, I guess you said you wouldn't keep me safe, but I can't quite believe this! You are truly a dispicable man and I hope that one day your doppleganger finds you too.
			->HANG_UP
	}
	
 - else :
	#ghuest
	Cashbags are you there? It's me from earlier. I just wanted to say that I haven't seen my doppleganger anywhere, I'm having a lovely stay here and feel much more relaxed.
	{
		- safe == true:
		#ghuest
		I knew I was right to trust you.
		->HANG_UP
		- else :
		#ghuest
		I guess you were nice all along.
		->HANG_UP
	}
	
 
}

->HANG_UP