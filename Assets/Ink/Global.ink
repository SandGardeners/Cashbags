//INCLUDES 
INCLUDE Objects.ink
INCLUDE RandomGuest.ink
INCLUDE QuestionGuest.ink
INCLUDE ReligiousGuest.ink
INCLUDE StoryGuest.ink
INCLUDE StoryGuest2.ink
INCLUDE TutorialGuest.ink
INCLUDE EmployeeMessages.ink
INCLUDE PoliteGuest.ink
INCLUDE ParanoidGuest.ink
INCLUDE DopplegangerGuest.ink
INCLUDE THE_END.ink

//VARIABLES
VAR NICENESS = 0 
VAR checkInStep = 0
VAR CHAOS = 0
VAR reorder = false
VAR update = false
VAR dentist = false
VAR safe = false
VAR denounced = false
EXTERNAL ResetStory()
EXTERNAL EndEvent()
EXTERNAL HangUp()
EXTERNAL EndEmployeeMessage()
EXTERNAL TriggerGameFinale()
EXTERNAL GiveItem(name)
EXTERNAL ActualFinish()

//DIVERT TO WANTED STARTING KNOT
->STARTING_POINT //DO NOT TOUCH THIS LINE
=== STARTING_POINT ===
->DONE //EDIT THIS LINE


=== LOOP_OR_RESET ===
What do you wanna do?
+ Loop[]
->STARTING_POINT
+ [Reset]
{ResetStory()}
->DONE


=== STOP_EVENT ===
{EndEvent()}
->DONE


=== HANG_UP ===
{HangUp()}
->DONE

=== TRIGGER_GAME_FINALE ===
{TriggerGameFinale()}
{EndEvent()}
->DONE

=== ACTUAL_FINISH ===
{ActualFinish()}
->DONE

=== STOP_EMPLOYEE_MESSAGE ===
{EndEmployeeMessage()}
->DONE

 == function ResetStory() ==
 ~return
 
 == function EndEvent() ==
 ~return
 
 == function HangUp() ==
 ~return
 
 == function EndEmployeeMessage() ==
 ~return 
 
 == function GiveItem(name) ==
 ~return
 
 == function TriggerGameFinale() ==
 ~return
 
 == function ActualFinish() ==
 ~return