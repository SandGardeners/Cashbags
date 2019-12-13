=== RANDOM_GUEST ===


#CHECK_IN
#PHONECALL
#GET_MAIL
#CHECK_OUT
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
{~I'd like to check in.|Could I check in?|Would it be possible to check in?|Can I have a room?|I need to check in, please.}

+[No problem at all.] {~We've got you in one of our finest rooms.|Let me just see if I can find you in the book.|I'll see to it myself you have a wonderful stay here.}
~NICENESS+=2
+[Sure, one second.] {~I'll just jot your name down here.|I'll just mark you as checked in.|Let me see if I can find you.}
~NICENESS+=1
+[Ok, ok, hold on.] {~Must be rush hour or something.|We're so busy right now.|I could really do with a break.}
~NICENESS-=1
+[Can't you see I'm busy?] {~It'll have to be quick.|Next time, arrive at your check-in at the time we give you.|I swear I can't take this job much longer.|It's just guest after guest.}
~NICENESS-=2

-->DONE
- (giveMoney)
Ok so... {~2 nights|3 nights|4 nights|5 nights} in one of our {~basic rooms|luxury suites|extreme expensive resort rooms|basic rooms with a spa pass|basic rooms, no breakfast included|luxury suites, with golf course access} comes to a total of {~4000 Tokens|5050 Tokens|3 Tokens|600 Tokens|36 Tokens}.
->DONE
- (giveKey)
Thank you very much... {~And here is your key, I hope you enjoy your stay.|Here's the key to your room, you'll find all the information you need up there.|Here is your key, if you need anything else just let me know.|There you go, you'll find the elevators over to your left.}
->DONE
- (bye)
#ghuest
Thank you very much.
->STOP_EVENT

->DONE
= PHONECALL

This is the Brownie Cove Hotel concierge desk, you're speaking to Cashbags Michael, how may I help you?

#ghuest
{~Hello.|Hello.|Hello.|Good evening.|Good evening.|Good evening.|Ah yes, thank you.|Great, ummm.|Oh thank Gods you're still available.|I've been trying to get through all night.} I was wondering if you could help me with something.

That's what I'm here for.

{~->ROOM_SERVICE|->COMPLAINT|->TAXI}

=ROOM_SERVICE

#ghuest
Could I order some room service?

+That's no problem at all.[] What would you like to order?
~NICENESS+=2
    
    #ghuest
    Ummmmmm... I think I'd like the {~neon noodles and teriyaki jellyfish|squid ink toast and pickled squash|feet burger}.
    
    And to drink?
    
    #ghuest
    All the {~absinthe|vodka|rum|whiskey|beer|wine} you've got.
    
    Wonderful, it should be with you shortly.
    
    #ghuest
    Many thanks, I will recommend this hotel to all of my friends!

+Ah, yes, would you like the number of the restaurant?[] Then you can give them your order?
~NICENESS+=1

    #ghuest
    Ah, yes please that'd be very helpful.
    
    The number is 135, that should phone the restaurant for you. Bon apetit.
    
    #ghuest
    Thank you.

+No, you've called the wrong number[.], you need to call the restaurant to order room service.
~NICENESS-=1

    #ghuest
    {~Well there's no need to be rude about it, I'm shocked!|Goodness I'm sorry, you must be very busy, my apologies.|Sorry, I must've typed the wrong number.}
    
    Goodbye.

+I'm a little busy right now[.], call back later.
~NICENESS-=2

    #ghuest
    {~Well I never, how rude!|I can't believe this, I'm going to warn all my friends about this place.|How rude!}

-->HANG_UP

=COMPLAINT

#ghuest
I have a complaint.

+I'm so sorry to hear that, how can I help?
~NICENESS+=2

+That really saddens me to hear.[] What is the problem?
~NICENESS+=1

+I don't believe there's a problem with our wonderful hotel[.], but still, what is your complaint?
~NICENESS-=1

    #ghuest
    {~Of course you wouldn't think there was a problem, typical.|Just because you think the hotel is great, doesn't mean it is, Mr Michael!|}

+I won't hear any of it[.], this hotel has been perfect for generations. Get out of here with your complaints!
~NICENESS-=2
    
    #ghuest
    {~Perhaps your rudeness is part of the problem! Did you ever think of that?|Gosh, of course you would just ignore complaints! That's why this hotel is falling to pieces!|My fucking kid died due to a disease he caught from this hotel and you won't even listen to me??|This is all your fault, Cashbags!}
    ->HANG_UP

-->COMPLAINT_STAGE2

=COMPLAINT_STAGE2

#ghuest
Well,{~I seem to be having a problem connecting to the wifi.|I can't seem to flush my toilet.|My fish got food poisoning from the food at the restaurant.|My room service has not arrived yet.|I took the pill that was in the welcome package and now I can't see.|I found a fish in my percolator!|I found a note on the ground, and it said I was going to die.|A worm keeps crawling out from beneath my bed when I try to sleep.|There are dead rats in my pillow case!|I ordered a sandwich, but it was just a brick between two slices of bread.|Someone was smoking by the pool, do you know how disgusting that is? I'm pregnant!|I keep hearing scratching from inside my stomach.}

Gosh, I'm terribly sorry to hear that, we'll have someone come and help you right now.

#ghuest
Thank you very much.

->HANG_UP

=TAXI

#ghuest
Could I book a taxi?

+Why yes of course.[] Where would you like to go?
~NICENESS+=2

+Indeed, that is why I am here.[] Where are you going?
~NICENESS+=1

+But, why would you want to leave the Hotel?[] Everything you could want is right here!
~NICENESS-=1

    #ghuest
    I've been getting a little sick of the hotel, wanted a change of scenery.
    
    Where would you like to go?
    
+I'm too busy![] You could've just done this yourself!
~NICENESS-=2

    #ghuest
    {~How dare you talk to me that way, I'm a paying guest!|Your job is to help people like me, if you don't want to then maybe you should just leave!|Just because it was your father who decided to build this hell on Earth, doesn't mean we should be the ones paying for it!}
    ->HANG_UP
    
-->TAXI_STAGE2


=TAXI_STAGE2

#ghuest
I was hoping you could recommend somewhere to eat?

+Yes I know a few places.
    ~NICENESS+=1
    
    #ghuest
    {~Ah, wonderful|That's great.|Excellent!}
    
+There's plenty of places in the hotel.
    ~NICENESS-=1
    
    #ghuest
    {~I really don't want to stay here anymore, I'd like to get out.|I want to explore out and about in the Bleak District, see what the locals have to offer.}
    
+I'm not here to recommend places.
    ~NICENESS-=2
        #ghuest
        I thought that was exactly why you were here! I just won't bother calling next time, thanks.
        ->HANG_UP

-
I'm going to book you a taxi and a table for {~Batter Joe's Fish and Freedom Diner|Stinky Croc's Italian Restaurant|Neon Noodle's Sushi Bar|Waxy Ear's Burgers|The Hall of the Mountain King|FUCK YEAH SPAGHETTI|Eat It All Up Yummy Yummy|Lula Lime's Cafe}. Enjoy your meal!


->HANG_UP

