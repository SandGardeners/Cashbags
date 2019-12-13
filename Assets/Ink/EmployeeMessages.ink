=== EMPLOYEE_MESSAGE_0 ===
#Tutorial Timothy the Third

#ghuest
Mr Michael, this is Tutorial Timothy the Third again. This is how you will recieve messages from your employees asking you to make decisions. Comprende?

*[You got it.]
->STOP_EMPLOYEE_MESSAGE

*[There's no other option.]

->STOP_EMPLOYEE_MESSAGE

=== EMPLOYEE_MESSAGE_1 ===
#Chef Jean Baptiste

#ghuest
Mr Michael, we're all out of food. What should I do?

*[Order more!!!]
~reorder=true


*[Think of something!!!]
~CHAOS+=1

-
->STOP_EMPLOYEE_MESSAGE

=== EMPLOYEE_MESSAGE_2 ===
#Chef Jean Baptiste

{ reorder==true:
    #ghuest
    Mr Michael, I tried to order more food but all our suppliers are out. May I have permission to improvise?

    *[Yes!]
    ~CHAOS+=2
    ->STOP_EMPLOYEE_MESSAGE

    *[No!]
    ->STOP_EMPLOYEE_MESSAGE

- else:

    #ghuest
    Mr Michael, I thought of something, but the guests seem unhappy. May I have permission to improvise?

    *[Yes!]
    ~CHAOS+=2
    ->STOP_EMPLOYEE_MESSAGE

    *[No!]
    ->STOP_EMPLOYEE_MESSAGE
}

=== EMPLOYEE_MESSAGE_3 ===
#Eduardo the Hacker

#ghuest

Ey Cashbags what up boy, can I get permission to upgrade the BCHotel System of Augmented Relaxation?

*[Sure!]
~ CHAOS+=2
~ update = true

*[No!]
-
->STOP_EMPLOYEE_MESSAGE

=== EMPLOYEE_MESSAGE_4 ===
#Eduardo the Hacker

{ update==true:
    #ghuest
    Umm... Cashbags, I updated the system but it seems to have crashed. Crashed the whole hotel... Can I improvise and fix it?
    
    *[Yes!]
    ~CHAOS+=2
    ->STOP_EMPLOYEE_MESSAGE
    
    *[No!]
    ->STOP_EMPLOYEE_MESSAGE

- else:
    #ghuest
    Cashbags, the system has crashed with the influx of new guests. If we had the new upgrade everything would be ok. Can I have permission to fix it?
    
    *[Yes!]
    ~CHAOS+=2
    ->STOP_EMPLOYEE_MESSAGE
    
    *[No!]
    ->STOP_EMPLOYEE_MESSAGE
}

=== EMPLOYEE_MESSAGE_5
#Doctor Harmonica

#ghuest
Mr Michael, I don't know what to do, I have too many patients and not enough time. May I use my new proposed method?

*[Ok!]
~CHAOS+=2
~dentist=true
*[No!]
-
->STOP_EMPLOYEE_MESSAGE

=== EMPLOYEE_MESSAGE_6
#Doctor Harmonica

{ dentist==true:
    #ghuest
    Mr Michael, I tried my new method and the patients didn't take too kindly to it. May I have permission to improvise?
    *[Yes!]
    ~CHAOS+=2
    ->STOP_EMPLOYEE_MESSAGE
    
    *[No!]
    ->STOP_EMPLOYEE_MESSAGE

- else:
    #ghuest
    Mr Michael, the patients keep coming and coming and coming. May I have permission to improvise?
    *[Yes!]
    ~CHAOS+=2
    ->STOP_EMPLOYEE_MESSAGE
		
    *[No!]
    ->STOP_EMPLOYEE_MESSAGE
}