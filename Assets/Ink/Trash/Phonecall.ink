/*=== PHONECALL_GHUEST

CASHBAGS MICHAEL: This is the Brownie Cove Hotel concierge desk, you're speaking to Cashbags Michael, how may I help you?

GHUEST: {~Hello.|Hello.|Hello.|Good evening.|Good evening.|Good evening.|Ah yes, thank you.|Great, ummm.|Oh thank Gods you're still available.|I've been trying to get through all night.} I was wondering if you could help me with something.

CASHBAGS MICHAEL: That's what I'm here for.

{~->ROOM_SERVICE|->COMPLAINT|->TAXI}

=ROOM_SERVICE

GHUEST: Could I order some room service?


CASHBAGS MICHAEL:
//the best kindest option
+That's no problem at all.[] What would you like to order?
~NICENESS+=2
//second best
+Ah, yes, would you like the number of the restaurant?[] Then you can give them your order?
~NICENESS+=1
//third best
+No, you've called the wrong number[.], you need to call the restaurant to order room service.
~NICENESS-=1
//worst, there will be four options in each variation each with like niceness factor
+I'm a little busy right now[.], call back later.
~NICENESS-=2

-->LOOP_OR_RESET//Whatch the hierarchy and scope, as an aditional "-" is needed for the loop to happen after each choice (not just the last)

=COMPLAINT

GHUEST: I have a complaint.

+I'm so sorry to hear that, how can I help?
~NICENESS+=2
+That really saddens me to hear.[] What is the problem?
~NICENESS+=1
+I don't believe there's a problem with our wonderful hotel[.], but still, what is your complaint?
~NICENESS-=1
+I won't hear any of it[.], this hotel has been perfect for generations. Get out of here with your complaints!
~NICENESS-=2

-->LOOP_OR_RESET

=TAXI

GHUEST: Could I book a taxi?

+Why yes of course.[] Where would you like to go?
~NICENESS+=2
+Indeed, that is why I am here.[] Where are you going?
~NICENESS+=1
+But, why would you want to leave the Hotel?[] Everything you could want is right here!
~NICENESS-=1
+I'm too busy![] You could've just done this yourself!
~NICENESS-=2

-->LOOP_OR_RESET
*/